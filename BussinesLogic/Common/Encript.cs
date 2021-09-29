using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BussinesLogic.Common
{
    public static class Encript
    {
        public static string GetSHA1(String text)
        {
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(text);
            Byte[] hash = sha1.ComputeHash(textOriginal);
            StringBuilder cadena = new StringBuilder();
            foreach (byte i in hash)
            {
                cadena.AppendFormat("{0:x2}", i);
            }
            return cadena.ToString();
        }
    }
}
