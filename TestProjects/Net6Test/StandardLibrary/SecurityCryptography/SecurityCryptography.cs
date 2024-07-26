using System;
using System.Collections.Generic;
using System.Text;

namespace StandardLibrary.SecurityCryptography
{
    public static class SecurityCryptography
    {
        /// <summary>
        /// Rewirte in .net6, hope it works as before!
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToHashSha256(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] array = hash.ComputeHash(bytes);
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("X2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
