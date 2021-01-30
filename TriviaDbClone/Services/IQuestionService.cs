using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Entities;

namespace TriviaDbClone.Services
{
   public interface IQuestionService
    {
        IActionResult Add(Questions questions);

        IActionResult AddRange(List<Questions> questionList);

    }
}
