using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dictionaries
{
    public class RecruitmentStageMarkDictionary
    {
        private static Dictionary<byte, string> dictionary = new Dictionary<byte, string>()
        {
                  {(byte)RecruitmentStageMarkEnum.Zero, "Zero"},
                  {(byte)RecruitmentStageMarkEnum.One, "One"},
                  {(byte)RecruitmentStageMarkEnum.Two, "Two"},
                  {(byte)RecruitmentStageMarkEnum.Three, "Three"},
                  {(byte)RecruitmentStageMarkEnum.Four, "Four"},
                  {(byte)RecruitmentStageMarkEnum.Five, "Five"},
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
