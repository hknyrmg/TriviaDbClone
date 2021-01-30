using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Entities;

namespace TriviaDbClone.Services
{
   public interface ICorrectAnswerService
    {
        IActionResult Add(Correct_Answer correct_answer);
        IActionResult AddRange(List<Correct_Answer> correct_answers);

    }
}
