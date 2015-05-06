using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dictionaries
{
    public class DecisionTypesDictionary
    {
        private static Dictionary<byte, string> dictionary = new Dictionary<byte, string>()
        {
                  {(byte)DecisionTypesEnum.Approved, "Approved"},
                  {(byte)DecisionTypesEnum.Rejected, "Rejected"},
                  {(byte)DecisionTypesEnum.DuringEvaluation, "During Evaluation"},
                  {(byte)DecisionTypesEnum.Confirmed, "Confirmed"}
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
