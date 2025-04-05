﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Option1Text { get; set; }
        public string Option2Text { get; set; }
        public string Option3Text { get; set; }

        public int Option1VotesCount { get; set; } = 0;
        public int Option2VotesCount { get; set; } = 0;
        public int Option3VotesCount { get; set; } = 0;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
