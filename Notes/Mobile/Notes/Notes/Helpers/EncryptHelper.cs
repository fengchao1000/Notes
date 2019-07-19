using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Notes.Helpers
{
    public class EncryptHelper
    {
        public static string GetMD5(string source)
        {
            string result;
            using (MD5 mD = MD5.Create())
            {
                string md5Hash = EncryptHelper.GetMd5Hash(mD, source);
                result = md5Hash;
            }
            return result;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] array = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            int num;
            for (int i = 0; i < array.Length; i = num + 1)
            {
                stringBuilder.Append(array[i].ToString("x2"));
                num = i;
            }
            return stringBuilder.ToString();
        }
    }
}
