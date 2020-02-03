using Microsoft.CSharp;
using MyCompilerV2.Model;
using MyCompilerV2.Services;
using MyCompilerV2.View;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MyCompilerV2.Presenter
{
    public class MainViewPresenter
    {
        private IMainView mainView;
        private IFileService fileService;
        public IView View { get => mainView; }
        private static int tabCount = 0;


        public MainViewPresenter(IMainView mainView, IFileService fileService)
        {
            this.mainView = mainView;
            this.fileService = fileService;
            EventSubscription();
        }

        private void EventSubscription()
        {
            mainView.Build += MainView_Build;
            mainView.BuildAndRunEvent += MainView_BuildAndRunEvent;
            mainView.RemoveProjectEvent += MainView_RemoveProjectEvent;
            mainView.CreateProjectEvent += MainView_CreateProjectEvent;
            mainView.SaveFocusedFileEvent += MainView_SaveFocusedFileEvent;
            mainView.OpenProjectEvent += MainView_OpenProjectEvent;
            mainView.CreateNewFileEvent += MainView_CreateNewFileEvent;
            mainView.OpenSelectebFileEvent += MainView_OpenSelectebFileEvent;
            mainView.CloseProgramEvent += MainView_CloseProgramEvent;
        }

        private void MainView_BuildAndRunEvent(object sender, TextEventArgs e)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = true;
            cp.OutputAssembly = @"..\..\compiler.exe";
            cp.GenerateInMemory = false;
            var result = provider.CompileAssemblyFromSource(cp, e.Text);
            if (result.Errors.Count > 0)
            {
                List<Error> errors = new List<Error>();
                List<Warning> warnings = new List<Warning>();
                foreach (CompilerError error in result.Errors)
                {
                    if (error.IsWarning)
                    {
                        warnings.Add(new Warning() { Code = error.ErrorNumber, File = error.FileName, Text = error.ErrorText, Line = error.Line });
                    }
                    else
                    {
                        errors.Add(new Error() { Code = error.ErrorNumber, File = error.FileName, Text = error.ErrorText, Line = error.Line });
                    }
                    mainView.GetWarnings(warnings);
                    mainView.GetErrors(errors);
                    
                }
            }
            else
            {
                Process.Start(@"..\..\compiler.exe");
            }
        }

        private void MainView_Build(object sender, TextEventArgs e)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = true;
            cp.GenerateInMemory = false;
            var result = provider.CompileAssemblyFromSource(cp, e.Text);
                List<Error> errors = new List<Error>();
                List<Warning> warnings = new List<Warning>();
                foreach (CompilerError error in result.Errors)
                {
                    if (error.IsWarning)
                    {
                        warnings.Add(new Warning() { Code = error.ErrorNumber, File = error.FileName, Text = error.ErrorText, Line = error.Line });
                    }
                    else
                    {
                        errors.Add(new Error() { Code = error.ErrorNumber, File = error.FileName, Text = error.ErrorText, Line = error.Line });
                    }
                    mainView.GetWarnings(warnings);
                    mainView.GetErrors(errors);
            }
        }

        private void MainView_RemoveProjectEvent(object sender, RemoveProjectEventArgs e)
        {
            File.Delete(e.SelectedItem);
        }
        private void MainView_CloseProgramEvent(object sender, EventArgs e)
        {
            mainView.CloseProgram();
        }

        private void MainView_OpenSelectebFileEvent(object sender, PathEventArgs e)
        {
            if (File.Exists(e.Path) && Path.GetExtension(e.Path) == ".cs")
            {
                string name = Path.GetFileName(e.Path);
                string path = e.Path.Remove(e.Path.Length - name.Length - 1);
                string code = fileService.ReadCode(e.Path);
                mainView.OpenSelectebFileNode(new FileInformation { Path = path, Name = name }, code);
            }
        }

        private void MainView_CreateNewFileEvent(object sender, PathEventArgs e)
        {
            FileInformation newf = new FileInformation { Name = "new tab" + $"({++tabCount })", Path = e.Path };
            fileService.CreateNewFile(newf);
        }

        private void MainView_OpenProjectEvent(object sender, PathEventArgs e)
        {
            if (File.Exists(e.Path))
            {
                mainView.SetSettings(fileService.OpenProject(e.Path));
            }
        }

        private void MainView_SaveFocusedFileEvent(object sender, CodeEventArgs e)
        {
            fileService.SaveFocusedFile(e.PathToFile, e.Code);
        }

        private void MainView_CreateProjectEvent(object sender, EventArgs e)
        {
            NewFormPresenter newForm = IOC.Resolve<NewFormPresenter>();

            if (newForm.View.ShowDialog())
            {
                mainView.SetSettings(newForm.NewFile);
            }
        }
    }
}