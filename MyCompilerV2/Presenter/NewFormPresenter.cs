using MyCompilerV2.Model;
using MyCompilerV2.Services;
using MyCompilerV2.View;

namespace MyCompilerV2.Presenter
{
    public class NewFormPresenter
    {
        INewFormView newFormView;
        IFileService fileService;
        public IView View { get => newFormView; }

        public FileInformation NewFile { get; set; }

        public NewFormPresenter(INewFormView newFormView,IFileService fileService)
        {
           
            this.newFormView = newFormView;
            this.fileService = fileService;
            EventSubscription();
        }

        private void EventSubscription()
        {
            newFormView.GetPath += NewFormView_GetPath;
            newFormView.CreateProject += NewFormView_CreateProject;
        }

        private void NewFormView_CreateProject(object sender, NamePathCheckboxEventArgs e)
        {
            fileService.CreateNewProject(NewFile=new FileInformation { Name = e.Name, Path = e.Path },e.Subdirectory);
            newFormView.CloseWindow();
        }
         
        private void NewFormView_GetPath(object sender, NameAndPathEventArgs e)
        {
            newFormView.SetPathInBox(e.Path);
        }


    }
}