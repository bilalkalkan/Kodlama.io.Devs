using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Github : Entity
    {
        public string GithubUrl { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Github()
        {
        }

        public Github(int id, string githubUrl)
        {
            Id = id;
            GithubUrl = githubUrl;
        }
    }
}