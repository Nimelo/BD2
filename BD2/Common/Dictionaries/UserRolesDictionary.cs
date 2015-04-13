using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dictionaries
{
    public class UserRolesDictionary
    {
        private static Dictionary<byte, string> dictionary = new Dictionary<byte, string>()
        {
                  {(byte)UserRolesEnum.Administrator, "Administrator"},
                  {(byte)UserRolesEnum.Evaluator, "Evaluator"},
                  {(byte)UserRolesEnum.Intern, "Intern"},
                  {(byte)UserRolesEnum.Candidate, "Candidate"}
        };
        public static Dictionary<byte, string> Dictionary
        {
            get
            {
                return dictionary;
            }
        }
    }
}
