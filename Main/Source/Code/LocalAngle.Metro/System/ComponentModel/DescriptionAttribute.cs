using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute() { }

        public DescriptionAttribute(string description) { }
    }
}
