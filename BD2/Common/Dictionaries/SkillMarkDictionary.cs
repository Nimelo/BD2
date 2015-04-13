using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dictionaries
{
    public class SkillMarkDictionary
    {
        private static Dictionary<byte, string> dictionary = new Dictionary<byte, string>()
        {
                  {(byte)SkillsMarkEnum.Zero, "Zero"},
                  {(byte)SkillsMarkEnum.One, "One"},
                  {(byte)SkillsMarkEnum.Two, "Two"},
                  {(byte)SkillsMarkEnum.Three, "Three"},
                  {(byte)SkillsMarkEnum.Four, "Four"},
                  {(byte)SkillsMarkEnum.Five, "Five"},
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
