using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dictionaries
{
    public class DocumentTypesDictionary
    {
        private static Dictionary<byte, string> dictionary = new Dictionary<byte, string>()
        {
                  {(byte)DocumentTypesEnum.Photo, "Photo"},
                  {(byte)DocumentTypesEnum.CV, "CV"},
                  {(byte)DocumentTypesEnum.ML, "Motivation Letter"},
                  {(byte)DocumentTypesEnum.Other, "Other"}
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
