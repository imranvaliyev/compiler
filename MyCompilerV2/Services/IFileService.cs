using MyCompilerV2.Model;

namespace MyCompilerV2.Services
{
    public interface IFileService
    {
        void CreateNewProject(FileInformation fileInformation, bool subdirectory = false);
        void CreateNewFile(FileInformation fileInformation);
        void RemoveFile(FileInformation fileInformation);
        void SaveFocusedFile(string path, string code);
        FileInformation OpenProject(string path);
        string ReadCode(string pathToFile);
    }
}