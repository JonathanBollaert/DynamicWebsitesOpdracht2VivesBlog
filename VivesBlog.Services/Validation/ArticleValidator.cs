using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;

namespace VivesBlog.Services.Validation
{
    public static class ArticleValidator
    {
        public static void Validate(this ServiceResult<ArticleResult> serviceResult, ArticleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                serviceResult.NotEmpty(nameof(request.Title));
            }
        }
    }
}
