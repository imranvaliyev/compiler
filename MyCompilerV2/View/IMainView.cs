using MyCompilerV2.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyCompilerV2.View
{
    public interface IMainView : IView
    {
        event EventHandler<CreateSubDirectoryEventArgs> CreateProjectEvent;
        event EventHandler<EventArgs> CloseProgramEvent;
        event EventHandler<CodeEventArgs> SaveFocusedFileEvent;
        event EventHandler<PathEventArgs> OpenProjectEvent;
        event EventHandler<PathEventArgs> CreateNewFileEvent;
        event EventHandler<PathEventArgs> OpenSelectebFileEvent;
        event EventHandler<RemoveProjectEventArgs> RemoveProjectEvent;
        event EventHandler<TextEventArgs> BuildAndRunEvent;
        event EventHandler<TextEventArgs> Build;
        void GetErrors(List<Error> errors);
        void GetWarnings(List<Warning> warnings);
        void CloseProgram();
        string OpenProjectPath();
        void SetSettings(FileInformation newProject);
        void OpenSelectebFileNode(FileInformation file, string code);
    }
    public class TextEventArgs : EventArgs
    {
        public string Text { get; set; }
    }
    public class RemoveProjectEventArgs : EventArgs
    {
        public string SelectedItem { get; set; }
        public FileInformation fileInformation { get; set; }
        public TabControl tabControl { get; set; }
    }
    public class CreateSubDirectoryEventArgs : EventArgs
    {
        public bool CreateSubDirectory { get; set; }
    }
    public class PathEventArgs : EventArgs
    {
        public string Path { get; set; }
    }

    public class CodeEventArgs : EventArgs
    {
        public string Code { get; set; }
        public string PathToFile { get; set; }
    }
}