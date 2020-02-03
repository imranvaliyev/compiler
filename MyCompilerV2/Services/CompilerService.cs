using FastColoredTextBoxNS;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompilerV2.Services
{
    public class CompilerService : ICompiler
    {
        public void Build()
        {
            throw new NotImplementedException();
        }

        public void Compile(FastColoredTextBox fastColoredTextBox)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = true;

           // string name = Path.GetFileNameWithoutExtension(tabControl_code.SelectedTab.Tag as string) as string;
            cp.OutputAssembly = @"..\..\compiler.exe";
            cp.GenerateInMemory = false;
            var result = provider.CompileAssemblyFromSource(cp, fastColoredTextBox.Text);
            //if (result.Errors.Count > 0)
            //{
            //    foreach (CompilerError compilerError in result.Errors)
            //    {
            //        if (compilerError.IsWarning)
            //        {
            //            Warnings.Controls[0].Text = "Line number" + compilerError + ", Warning Number: " + compilerError.ErrorNumber
            //                + ", '" + compilerError.ErrorText + ";" + Environment.NewLine + Environment.NewLine;
            //        }
            //        else
            //        {

            //            Errors.Controls[0].Text = "Line number" + compilerError + ", Error Number: " + compilerError.ErrorNumber
            //                + ", '" + compilerError.ErrorText + ";" + Environment.NewLine + Environment.NewLine;
            //        }
            //    }
            //}
            //else
            //{
            //    Output.Controls[0].Text = "Project has built successfull. \n" + cp + result.PathToAssembly;
                Process.Start(@"..\..\compiler.exe");
           // }
        }
    }
}
