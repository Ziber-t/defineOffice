using System.Collections.Generic;
using Define.Common.Extensions;
using Microsoft.Office.Interop.Word;

namespace Define.Models
{
    public class WordDefinition
    {
        public WordDefinition()
        {
            Locations = new List<Range>();
        }

        public string Name { get; set; }

        public string Singular
        {
            get
            {
                if (Name.IsPlural()) return Name.ToSingularForm();

                return Name;
            }
        }

        public string Plural
        {
            get
            {
                if (Name.IsPlural()) return Name;

                return Name.ToPluralForm();
            }
        }

        public List<Range> Locations { get; set; }
    }
}
