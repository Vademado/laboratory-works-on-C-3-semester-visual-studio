using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using class_library_laboratory_work_7;
#nullable enable

namespace laboratory_work_8
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            Animal animal = new Cow("Russia", "Burenka");
            Serialize(animal, "C:\\Users\\admin\\Downloads", "Cow");

            Animal? deserializeAnimal = Deserialize("C:\\Users\\admin\\Downloads", "Cow");
            if (deserializeAnimal is not null) Console.WriteLine(deserializeAnimal);
            else { Console.WriteLine("Animal is null"); }

            await SearchFile("E:\\ИНТЕРНЕТ", "*.xml", true, eCompressionMode.None);

            await SearchFile("E:\\ИНТЕРНЕТ", "test.xml", true, eCompressionMode.Compress);
            await SearchFile("E:\\ИНТЕРНЕТ", "test.xml.gz", true, eCompressionMode.Decompress);
        }
        public static void Serialize(Animal animal, string path = "", string name = "Animal")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Animal));
            string filePath = Path.Combine(path, $"{name}.xml");
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fileStream, animal);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static Animal? Deserialize(string path = "", string name = "Animal")
        {
            Animal? deserializeAnimal = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Animal));
            string filePath = Path.Combine(path, $"{name}.xml");
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    deserializeAnimal = serializer.Deserialize(fileStream) as Animal;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return deserializeAnimal;
        }

        public async static Task SearchFile(string parentDirectory = "", string searchPattern = "", bool print = false, eCompressionMode compressionMode = eCompressionMode.None)
        {
            string[] files = Directory.GetFiles(parentDirectory, searchPattern, SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                Console.WriteLine($"Found file(s) {searchPattern}: \n" + new string('-', 50));
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                    if (print)
                    {
                        Console.WriteLine("The contents of the file:");
                        try
                        {
                            using (FileStream fileStream = new FileStream(file, FileMode.Open))
                            {
                                using (StreamReader streamReader = new StreamReader(fileStream))
                                {
                                    Console.WriteLine(streamReader.ReadToEnd());
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    Console.WriteLine("\n\n");
                }
            }
            else
            {
                Console.WriteLine($"File(s) {searchPattern} not found");
            }

            switch (compressionMode)
            {
                case eCompressionMode.Compress:
                    foreach (string file in files)
                    {
                        await CompressAsync(file, $"{file}.gz");
                    }
                    break;
                case eCompressionMode.Decompress:
                    foreach (string file in files)
                    {
                        await DecompressAsync(file, file.Replace(".gz", ""));
                    }
                    break;
                case eCompressionMode.None:
                    break;
            }
        }

        private async static Task CompressAsync(string sourceFile, string compressedFile)
        {
            try
            {
                using FileStream sourceStream = new FileStream(sourceFile, FileMode.Open);
                using FileStream targetStream = File.Create(compressedFile);

                using GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
                await sourceStream.CopyToAsync(compressionStream);

                Console.WriteLine($"File {sourceFile} compressed to {compressedFile}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async static Task DecompressAsync(string compressedFile, string targetFile)
        {
            try
            {
                using FileStream fileStream = new FileStream(compressedFile, FileMode.Open);
                using FileStream targetStream = File.Create(targetFile);

                using GZipStream decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
                await decompressionStream.CopyToAsync(targetStream);

                Console.WriteLine($"File {compressedFile} successfully decompressed to {targetFile}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    enum eCompressionMode
    {
        Compress,
        Decompress,
        None
    }
}
