namespace MyCompilerV2.Model
{
    public class FileInformation
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public FileInformation()
        {
            Name = "";
            Path = "";
        }
    }
}
