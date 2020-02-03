using System.IO;
using System.Xml.Serialization;
using MyCompilerV2.Model;

namespace MyCompilerV2.Services
{
    class FileService : IFileService
    {
        public void CreateNewFile(FileInformation fileInformation)
        {
            var file = File.Create(fileInformation.Path + $@"\{fileInformation.Name}" + ".cs");
            file.Close();
        }

        public void CreateNewProject(FileInformation fileInformation, bool subdirectory = false)
        {
            string fullPath = "";
            if (subdirectory == false)
            {
                fullPath = fileInformation.Path;
            }
            else if (subdirectory)
            {
                fullPath = fileInformation.Path + @"\" + fileInformation.Name;
                Directory.CreateDirectory(fullPath);
            }

            var file = File.Create(fullPath + $@"\{fileInformation.Name}" + ".cs");
            file.Close();
            using (FileStream fs = new FileStream(fullPath + $@"\{fileInformation.Name}" + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(FileInformation));
                fs.Seek(0, SeekOrigin.Begin);
                serializer.Serialize(fs, fileInformation);
            }
        }

        public FileInformation OpenProject(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xml = new XmlSerializer(typeof(FileInformation));
                return (FileInformation)xml.Deserialize(fs);
            }
        }

        public string ReadCode(string pathToFile)
        {
            return File.ReadAllText(pathToFile);
        }

        public void RemoveFile(FileInformation fileInformation)
        {
            throw new System.NotImplementedException();
        }

        public void SaveFocusedFile(string path, string code)
        {
            using (StreamWriter file = new StreamWriter(path, false))
            {
                file.WriteLine(code);
            }
        }
    }
}