using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace jobfinder_back.clases
{
    public class Cifrar
    {
        public string cifrarPassword(string password) 
        {
            SHA256 sha256 = new SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();
            byte[] bt = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < bt.Length; i++)
            {
                sb.AppendFormat("{0:x2}", bt[i]);
            }
            return sb.ToString();
        }
        
    }
}