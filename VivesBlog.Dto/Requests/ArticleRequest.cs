using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesBlog.Dto.Results;

namespace VivesBlog.Dto.Requests
{
    public class ArticleRequest
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Content { get; set; }
        [Required]
        public int AuthorId { get; set; } = 0;
    }
}
