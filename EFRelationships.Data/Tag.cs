using System.Collections.Generic;

namespace EFRelationships.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<QuestionsTags> QuestionsTags { get; set; }
    }
}