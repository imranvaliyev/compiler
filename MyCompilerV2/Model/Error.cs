using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompilerV2.Model
{
    public class Error
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public int Line { get; set; }
        public string File { get; set; }
    }
}
