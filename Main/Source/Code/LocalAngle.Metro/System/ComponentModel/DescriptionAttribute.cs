using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute() { }

        public DescriptionAttribute(string description) { Description = description; }

        public string Description { get; private set; }
    }
}
