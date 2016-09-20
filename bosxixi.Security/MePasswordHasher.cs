using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Security
{
    public class MePasswordHasher : PasswordHasher
    {
        private readonly string salt;
        public MePasswordHasher(string salt)
        {
            this.salt = salt;
        }
        public override string HashPassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException();
            }
            DESEncrypt des = new DESEncrypt();
            return des.EncryptString(password, salt);
        }

        public string DehashPassword(string hashedPassword)
        {
            DESEncrypt des = new DESEncrypt();
            return des.DecryptString(hashedPassword, salt);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            DESEncrypt des = new DESEncrypt();
            var password = des.EncryptString(providedPassword, salt);

            if (hashedPassword == password)
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}
