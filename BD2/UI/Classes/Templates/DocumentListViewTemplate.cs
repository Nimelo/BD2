using Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.Templates
{
    public class DocumentListViewTemplate
    {

        public int Id;
        public string Name { get; set; }
        public string Extension { get; set; }

        public byte type;
        public string Type
        {
            get
            {
                return DocumentTypesDictionary.Dictionary[this.type];
            }
            set
            {
                this.type = Byte.Parse(value);
            }
        }

        public DocumentListViewTemplate(int id)
        {
            this.Id = id;
        }
        public DocumentListViewTemplate()
        {

        }
    }
}
