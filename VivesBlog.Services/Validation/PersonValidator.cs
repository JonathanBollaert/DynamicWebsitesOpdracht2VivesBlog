using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;
using Vives.Services.Model.Extensions;

namespace VivesBlog.Services.Validation
{
    public static class PersonValidator
    {
        public static void Validate(this ServiceResult<PersonResult> serviceResult, PersonRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                serviceResult.NotEmpty(nameof(request.FirstName));
            }

            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                serviceResult.NotEmpty(nameof(request.LastName));
            }
        }

    }
}
