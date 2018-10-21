using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.Reflection.Model;
using Project.ViewModel;

namespace ReflectionMVM.ViewModel
{
    public abstract class TypeMetadataBaseViewModel: MetadataViewModel
    {
        public string TypeName { get; internal set; }
        public string Modifier { get; internal set; }

        internal string GetModifierName(AccessLevel? accessLevel)
        {
            string modifier = null;
            switch (accessLevel)
            {
                case AccessLevel.Public:
                    modifier = "public";
                    break;
                case AccessLevel.Protected:
                    modifier = "protected";
                    break;
                case AccessLevel.Internal:
                    modifier = "internal";
                    break;
                case AccessLevel.Private:
                    modifier = "private";
                    break;
            }

            return modifier;
        }

    }
}
