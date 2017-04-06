using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class CustomUserStore : IUserStore<AU>
    {
        static readonly List<AU> Users = new List<AU>();

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task CreateAsync(AU user)
        {
            return Task.Factory.StartNew(() => Users.Add(user));
        }

        public Task UpdateAsync(AU user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AU user)
        {
            throw new NotImplementedException();
        }

        public Task<AU> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<AU> FindByNameAsync(string userName)
        {
            //return Task<AU>.Factory.StartNew(() => Users.FirstOrDefault(u => u.UserName == userName));
            return Task<AU>.Factory.StartNew(() => new AU(userName));
        }
    }
}