using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Core;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;
using VivesBlog.Model;
using VivesBlog.Services.Projection;
using VivesBlog.Services.Validation;

namespace VivesBlog.Services
{
    public class ArticleService
    {
        private readonly VivesBlogDbContext _dbContext;

        public ArticleService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<ArticleResult>> Find()
        {
            return await _dbContext.Articles
                .Project()
                .ToListAsync();
        }

        //Get (by id)
        public async Task<ServiceResult<ArticleResult>> Get(int id)
        {
            var serviceResult = new ServiceResult<ArticleResult>();

            var organization = await _dbContext.Articles
                .Project()
                .FirstOrDefaultAsync(p => p.Id == id);

            serviceResult.Data = organization;
            if (organization is null)
            {
                serviceResult.NotFound(nameof(Article), id);
            }

            return serviceResult;
        }

        //Create
        public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
        {
            var serviceResult = new ServiceResult<ArticleResult>();

            //Validate request
            serviceResult.Validate(request);

            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            var article = new Article
            {
                Description = request.Description,
                Title = request.Title,
                Content = request.Content,
                PublishedDate = DateTime.UtcNow,
                Author = request.AuthorId != 0 ? await _dbContext.People.FirstOrDefaultAsync(p => p.Id == request.AuthorId): null
            };

            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();

            return await Get(article.Id);
        }

        //Update
        public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
        {
            var serviceResult = new ServiceResult<ArticleResult>();

            //Validate request
            serviceResult.Validate(request);

            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            var article = _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);

            if (article is null)
            {
                serviceResult.NotFound(nameof(Article), id);
                return serviceResult;
            }

            article.Title = request.Title;
            article.Description = request.Description;
            article.Content = request.Content;
            article.AuthorId = request.AuthorId;

            await _dbContext.SaveChangesAsync();

            return await Get(article.Id);
        }

        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var serviceResult = new ServiceResult();

            var organization = await _dbContext.Articles
                .FirstOrDefaultAsync(p => p.Id == id);

            if (organization is null)
            {
                serviceResult.NotFound(nameof(Article), id);
                return serviceResult;
            }

            _dbContext.Articles.Remove(organization);
            await _dbContext.SaveChangesAsync();

            serviceResult.Deleted(nameof(Article));
            return serviceResult;
        }
    }
}
