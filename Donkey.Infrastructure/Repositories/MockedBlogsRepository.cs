using Donkey.Core.Entities;
using Donkey.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure.Repositories
{
    public class MockedBlogsRepository : IBlogsRepository
    {
        public Task Create(Blog blog)
        {
            return Task.CompletedTask;
        }

        public Task Delete(string name)
        {
            return Task.CompletedTask;
        }

        public Task<Blog> Get(string name)
        {
            return Task.FromResult(new Blog()
            {
                Name = "gorace_karlice_z_twojej_okolicy",
                OwnerEmail = "karol@wp.pl",
            });
        }

        public Task<List<Blog>> GetAll(string email)
        {
            return Task.FromResult(new List<Blog>
            {
                new Blog
                {
                    Name = "testowy_blog_1",
                    OwnerEmail = "milosz@fajnygosc.pl"
                },

                new Blog
                {
                    Name = "gorace_karlice_z_twojej_okolicy",
                    OwnerEmail = "karol@wp.pl",
                }
            });
        }

        public Task Update(Blog blog)
        {
            throw new NotImplementedException();
        }
    }
}
