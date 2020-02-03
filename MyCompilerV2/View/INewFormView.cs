using System;

namespace MyCompilerV2.View
{
    public interface INewFormView : IView
    {
        event EventHandler<NameAndPathEventArgs> GetPath;
        event EventHandler<NamePathCheckboxEventArgs> CreateProject;
        void SetPathInBox(string path);
        void CloseWindow();

    }

    public class NameAndPathEventArgs : EventArgs
    {
        public string Name;
        public string Path;
    }

    public class NamePathCheckboxEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool Subdirectory { get; set; }
    }
}
