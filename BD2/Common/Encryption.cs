using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordHash;
namespace Common
{
    public static class Encryption
    {
        public static string Encrypt(string toEncrypt)
        {
            return PasswordHash.Encrypt.MD5(toEncrypt);
        }

        public static bool Match(string toDecrypt, string match)
        {
            return match == PasswordHash.Encrypt.MD5(toDecrypt);
        }
    }
}
