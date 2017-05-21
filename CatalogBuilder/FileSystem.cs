using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace CatalogBuilder
{
    public class FileSystem
    {
        public static string ZipAndSave(XDocument xDoc)
        {
            var tempDirectoryPath = Path.Combine(Environment.CurrentDirectory, "catalogbuildertemp");
            var zipFilePath = Path.Combine(Environment.CurrentDirectory, "Catalog " + DateTime.Now.ToString().Replace('/', '-').Replace(':', '.') + ".zip");

            if (Directory.Exists(tempDirectoryPath))
            {
                Directory.Delete(tempDirectoryPath, true);
            }

            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            Directory.CreateDirectory(tempDirectoryPath);
            xDoc.Save(Path.Combine(tempDirectoryPath, "Catalog.xml"));

            ZipFile.CreateFromDirectory(tempDirectoryPath, zipFilePath);

            return zipFilePath;
        }

        public static string ExtractAndSave(string zipFilePath)
        {
            var tempDirectoryPath = Path.Combine(Environment.CurrentDirectory, "catalogbuildertemp");
           
            if (Directory.Exists(tempDirectoryPath))
            {
                Directory.Delete(tempDirectoryPath, true);
            }

            Directory.CreateDirectory(tempDirectoryPath);

            var extractedFilePath = "";

            using (var archive = ZipFile.OpenRead(zipFilePath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        extractedFilePath = Path.Combine(tempDirectoryPath, entry.FullName);
                        entry.ExtractToFile(extractedFilePath);
                    }
                }
            }

            return extractedFilePath;
        }

        public static void Save(XDocument xDoc)
        {
            var tempDirectoryPath = Path.Combine(Environment.CurrentDirectory, "catalogbuildertemp");

            if (Directory.Exists(tempDirectoryPath))
            {
                Directory.Delete(tempDirectoryPath, true);
            }

            Directory.CreateDirectory(tempDirectoryPath);
            xDoc.Save(Path.Combine(tempDirectoryPath, "Catalog.xml"));

            
        }

        public static string GetDocumentPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "catalogbuildertemp", "Catalog.xml");

        }
    }
}