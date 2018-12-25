using System.IO;

namespace Define.Common.Helpers
{
    public class FileHelper
    {
        public static void SaveDiagnosticFileToTempFolder(string nameOfFile, string content)
        {
            var path = Path.Combine(Path.GetTempPath(), nameOfFile);

            File.WriteAllText(path, content);
        }
    }
}
