using Microsoft.AspNet.Identity;
using System;

namespace mvc.Models
{
    public class AU : IUser
    {
        public AU(string name)
        {
            Id = Guid.NewGuid().ToString();
            UserName = name;
        }

        public string Id { get; private set; }
        public string UserName { get; set; }
    }
}