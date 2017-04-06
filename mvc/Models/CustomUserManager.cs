using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace mvc.Models
{
    public class CustomUserManager : UserManager<AU>
    {
        public CustomUserManager(CustomUserStore store)
            : base(store)
        {
            PasswordHasher = new CustomPasswordHasher();
        }

        public override Task<AU> FindAsync(string userName, string password)
        {
            Task<AU> taskInvoke = Task<AU>.Factory.StartNew(() =>
            {
                //PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(userName, password);
                //if (result == PasswordVerificationResult.SuccessRehashNeeded)
                //{
                //    return Store.FindByNameAsync(userName).Result;
                //}
                //return null;
                var ret = Store.FindByNameAsync(userName).Result;
                return ret;
            });
            return taskInvoke;
        }

        public static CustomUserManager Create()
        {
            return new CustomUserManager(new CustomUserStore());
        }
    }

    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (true)
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}