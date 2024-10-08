using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesBlog.Dto.Results;
using VivesBlog.Model;

namespace VivesBlog.Services.Projection
{
    public static class ProjectExtensions
    {
        public static IQueryable<ArticleResult> Project(this IQueryable<Article> query)
        {
            return query.Select(o => new ArticleResult
            {
                Id = o.Id,
                AuthorId = o.AuthorId,
                Content = o.Content,
                Title = o.Title,
                PublishedDate = o.PublishedDate,
                Description  = o.Description,
                Author = o.AuthorId != null ? new PersonResult
                {
                    Id = o.Author.Id,
                    FirstName = o.Author.FirstName,
                    LastName = o.Author.LastName,
                    Email = o.Author.Email
                } : null
            });
        }

        public static IQueryable<PersonResult> Project(this IQueryable<Person> query)
        {
            return query.Select(p => new PersonResult
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email
            });
        }
    }
}
