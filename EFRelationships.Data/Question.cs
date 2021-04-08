using System;
using System.Collections.Generic;
using System.Text;

namespace EFRelationships.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }


        public List<QuestionsTags> QuestionsTags { get; set; }
    }
}
