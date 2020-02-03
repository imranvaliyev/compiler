using MyCompilerV2.Presenter;
using MyCompilerV2.Services;
using MyCompilerV2.View;
using System;
using System.Windows.Forms;

namespace MyCompilerV2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IOC.Register<MainViewPresenter>();
            IOC.Register<NewFormPresenter>();
            IOC.Register<FileService, IFileService>();
            IOC.Register<MainView, IMainView>();
            IOC.Register<NewFormView, INewFormView>();
            IOC.Build();

            var newFormPresenter = IOC.Resolve<MainViewPresenter>();

            Application.Run((Form)newFormPresenter.View);
        }
    }
}
