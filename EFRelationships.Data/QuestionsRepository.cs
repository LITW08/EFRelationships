using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFRelationships.Data
{
    public class QuestionsRepository
    {
        private readonly string _connectionString;

        public QuestionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private Tag GetTag(string name)
        {
            using var ctx = new PeopleCarsContext(_connectionString);
            return ctx.Tags.FirstOrDefault(t => t.Name == name);
        }

        private int AddTag(string name)
        {
            using var ctx = new PeopleCarsContext(_connectionString);
            var tag = new Tag { Name = name };
            ctx.Tags.Add(tag);
            ctx.SaveChanges();
            return tag.Id;
        }

        public List<Question> GetQuestionsForTag(string name)
        {
            using var ctx = new PeopleCarsContext(_connectionString);
            return ctx.Questions.Include(q => q.QuestionsTags)
                .ThenInclude(qt => qt.Tag)
                .Where(c => c.QuestionsTags.Any(t => t.Tag.Name == name))
                .ToList();
        }

        public void AddQuestion(Question question, IEnumerable<string> tags)
        {
            using var ctx = new PeopleCarsContext(_connectionString);
            ctx.Questions.Add(question);
            ctx.SaveChanges();
            foreach (string tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                ctx.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }

            ctx.SaveChanges();
        }
    }
}
