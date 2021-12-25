using Donkey.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.API.DTOs.Responses
{
    public class PostDto
    {
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuthorMail { get; set; }

        public PostDto()
        {

        }

        public PostDto(Post post)
        {
            PostTitle = post.Title;
            PostContent = post.Content;
            CreateDate = post.CreatedDate;
            AuthorMail = post.AuthorEmail;
        }
    }
}
