using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompilerV2.Services
{
    public interface ICompiler
    {
        void Compile(FastColoredTextBox fastColoredTextBox);
    }
}
