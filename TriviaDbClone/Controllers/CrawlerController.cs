using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TokenBasedAuth_NetCore.Utils;
using TriviaDbClone.Entities;
using TriviaDbClone.Models;
using TriviaDbClone.Providers;
using TriviaDbClone.Providers.ProxyManager;
using TriviaDbClone.Repository;
using TriviaDbClone.Services;
using TriviaDbClone.UnitofWork;

namespace TriviaDbClone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrawlerController : ControllerBase
    {


        private readonly ILogger<CrawlerController> _logger;
        private readonly IGenericRepository<Questions> _genericRepository;
        private readonly ICacheProvider _cacheProvider;
        private readonly IProxyManager _proxyManager;
        private readonly IQuestionService _questionService;
        private readonly ICorrectAnswerService _correctAnswerService;
        private readonly IInCorrectAnswerService _inCorrectAnswerService;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IUnitOfWork _unitOfWork;
        public CrawlerController(ILogger<CrawlerController> logger,
             IUnitOfWork unitOfWork,
             IProxyManager proxyManager,
        ICacheProvider cacheProvider,
        IQuestionService questionService,
        ICorrectAnswerService correctAnswerService,
        IInCorrectAnswerService inCorrectAnswerService,
        IRecurringJobManager recurringJobManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _genericRepository = _unitOfWork.GetRepository<Questions>();
            _cacheProvider = cacheProvider;
            _proxyManager = proxyManager;
            _questionService = questionService;
            _correctAnswerService = correctAnswerService;
            _inCorrectAnswerService = inCorrectAnswerService;
            _recurringJobManager = recurringJobManager;
        }



        [HttpGet]
        public IActionResult Get()
        {
            _recurringJobManager.AddOrUpdate(
             "insert-questions",
            () => recurringJob(),
              "* * * * *");

            return Ok();
        }

        public async Task recurringJob()
        {

            List<QuestionResult> SavedQuestions = _cacheProvider.GetFromCache<List<QuestionResult>>(CacheKeys.questionList);

            List<Question_Types> Question_Typess = _cacheProvider.GetFromCache<List<Question_Types>>(CacheKeys.questionTypeList);
            List<Categories> Categories = _cacheProvider.GetFromCache<List<Categories>>(CacheKeys.categoryList);
            List<Difficulty> Difficulties = _cacheProvider.GetFromCache<List<Difficulty>>(CacheKeys.difficultyList);


            if (Question_Typess == null || Question_Typess.Count < 1)
            {
                Question_Typess = _unitOfWork.GetRepository<Question_Types>().GetAll().ToList();
                _cacheProvider.SetCache<List<Question_Types>>(CacheKeys.questionTypeList, Question_Typess, DateTimeOffset.Now.AddDays(1));

            }
            if (Categories == null || Categories.Count  < 1)
            {
                Categories = _unitOfWork.GetRepository<Categories>().GetAll().ToList();
                _cacheProvider.SetCache<List<Categories>>(CacheKeys.categoryList, Categories, DateTimeOffset.Now.AddDays(1));

            }
            if (Difficulties == null || Difficulties.Count < 1)
            {
                Difficulties = _unitOfWork.GetRepository<Difficulty>().GetAll().ToList();
                _cacheProvider.SetCache<List<Difficulty>>(CacheKeys.difficultyList, Difficulties, DateTimeOffset.Now.AddDays(1));

            }

            var body = await _proxyManager.ReadAsStringAsync();


            List<QuestionResult> Questions;
            var QuestionsRaw = JsonSerializer.Deserialize<GetQuestions>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (SavedQuestions != null)
            {
                Questions = _filterDuplicates(QuestionsRaw, SavedQuestions);
            }
            else
            {
                Questions = QuestionsRaw.Results.ToList();
                SavedQuestions = new List<QuestionResult>();
            }
            SavedQuestions.AddRange(Questions.ToList());

            _cacheProvider.SetCache<List<QuestionResult>>(CacheKeys.questionList, SavedQuestions, DateTimeOffset.Now.AddDays(1));


            var watch = System.Diagnostics.Stopwatch.StartNew();

         

            List<Questions> questions = new List<Questions>();
            List<Correct_Answer> correct_Answers = new List<Correct_Answer>();
            List<incorrect_answer> incorrect_answers_List = new List<incorrect_answer>();

            foreach (var item in Questions)
            {
           
                Questions question = new Questions();
                Correct_Answer correct_Answer = new Correct_Answer();
                List<incorrect_answer> incorrect_answers_List1 = new List<incorrect_answer>();

                question.id = Guid.NewGuid();
                question.type_id = Question_Typess.FirstOrDefault(x => x.question_type == item.Type).id;
                question.difficulty_id = Difficulties.FirstOrDefault(x => x.difficulty_level == item.Difficulty).id;
                question.category_id = Categories.FirstOrDefault(x => x.name == item.Category).id;
                question.question = item.Question;

                correct_Answer.id = Guid.NewGuid();
                correct_Answer.question_id = question.id;
                correct_Answer.answer = item.Correct_answer;

                for (int y = 0; y < item.incorrect_answers.Count; y++)
                {
                    incorrect_answer incorrect_answers = new incorrect_answer();

                    incorrect_answers.id = Guid.NewGuid();
                    incorrect_answers.question_id = question.id;
                    incorrect_answers.answer = item.incorrect_answers[y];

                    incorrect_answers_List1.Add(incorrect_answers);
                }
                correct_Answers.Add(correct_Answer);
                incorrect_answers_List.AddRange(incorrect_answers_List1);

                questions.Add(question);
            }



            var a = _questionService.AddRange(questions);
            var b = _correctAnswerService.AddRange(correct_Answers);
            var c = _inCorrectAnswerService.AddRange(incorrect_answers_List);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;


        }

        private List<QuestionResult> _filterDuplicates(GetQuestions getQuestions, List<QuestionResult> questionResults)
        {
            List<QuestionResult> filteredResult = new List<QuestionResult>();


            foreach (var item in getQuestions.Results)
            {
                QuestionResult questionResult = new QuestionResult();

                if(!questionResults.Any(x=> x.Question == item.Question)){
                    questionResult.Category = item.Category;
                    questionResult.Difficulty = item.Difficulty;
                    questionResult.Correct_answer = item.Correct_answer;
                    questionResult.incorrect_answers = item.incorrect_answers;
                    questionResult.Question = item.Question;
                    questionResult.Type = item.Type;

                    filteredResult.Add(questionResult);

                }

            }


            return filteredResult;
        }
    }
}
