using System;
using System.Security.Cryptography;
using System.Text;

namespace GetPet.Common
{
    public static class HashHelper
    {
        public static string Sha256(string text)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
