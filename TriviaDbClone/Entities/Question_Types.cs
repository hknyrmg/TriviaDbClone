using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaDbClone.Entities
{
    public class Question_Types
    {
        public Guid id { get; set; }

        public string question_type { get; set; }
    }
}
