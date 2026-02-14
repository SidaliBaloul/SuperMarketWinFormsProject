using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMBusinessLayer
{
    public class ClsSecurity
    {
        public static string ComputeHash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashbytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashbytes).Replace("-", "").ToLower();
            }
        }

    }
}
