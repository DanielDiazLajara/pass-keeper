using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Pass
{
    public static class Hash
    {
        public static string GetHash(string item)
        {
            if (item == "")
            {
                throw new ArgumentNullException(nameof(item));
            }
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] hashValue = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(item));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashValue.Length; i++)
                {
                    builder.Append(hashValue[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Devuelve n hashes de una cadena
        public static string GetNHash(string item, int n)
        {
            if (item == "")
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (n < 0)
            {
                n = 0;
            }
            string hash = GetHash(item);
            for (int i = 0; i < n; i++)
            {
                hash = GetHash(hash);
            }
            return hash;
        }
    }
}
