using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dictionaries
{
    public class SoftSkillMarkDictionary
    {
        private static Dictionary<byte, string> dictionary = new Dictionary<byte, string>()
        {
                  {(byte)SoftSkillsMarkEnum.Bad, "Bad"},
                  {(byte)SoftSkillsMarkEnum.Good, "Good"},
                  {(byte)SoftSkillsMarkEnum.Cool, "Cool"},
                  {(byte)SoftSkillsMarkEnum.Awesome, "Awesome"},

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
