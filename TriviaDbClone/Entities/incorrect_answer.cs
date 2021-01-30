using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaDbClone.Entities
{
    public class incorrect_answer
    {
        public Guid id { get; set; }

        public Guid question_id { get; set; }

        public string answer { get; set; }
    }
}
