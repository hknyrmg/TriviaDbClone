using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Controllers;
using TriviaDbClone.Entities;
using TriviaDbClone.Providers;
using TriviaDbClone.Providers.ProxyManager;
using TriviaDbClone.Repository;
using TriviaDbClone.UnitofWork;

namespace TriviaDbClone.Services
{
    public class QuestionService : ControllerBase, IQuestionService
    {
        private readonly ILogger<QuestionService> _logger;
        private readonly IGenericRepository<Questions> _genericRepository;
        private readonly ICacheProvider _cacheProvider;
        private readonly IProxyManager _proxyManager;
        private readonly IUnitOfWork _unitOfWork;
        public QuestionService(ILogger<QuestionService> logger,
             IUnitOfWork unitOfWork,
             IProxyManager proxyManager,
        ICacheProvider cacheProvider)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _genericRepository = _unitOfWork.GetRepository<Questions>();
            _cacheProvider = cacheProvider;
            _proxyManager = proxyManager;
        }
        public IActionResult Add(Questions questions)
        {
            _genericRepository.Add(questions);
            var result = _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return Ok(questions);
            }
            else
            {
                return BadRequest("Bad Request...");

            }
        }

        public IActionResult AddRange(List<Questions> questionList)
        {
            _genericRepository.AddRange(questionList);
            var result = _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return Ok(questionList);
            }
            else
            {
                return BadRequest("Bad Request...");

            }
        }
    }
}
