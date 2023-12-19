using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using YamlDotNet.Core;

// Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "qua3osuGui", "qua3osuGui\qua3osuGui.csproj", "{A6856D25-2883-4C39-B18C-A10461955F2C}"
// EndProject

namespace fnf3qua
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = CommandLine.Parser.Default.ParseArguments<Arguments>(args).WithParsed(argument => ConvertListOfMapsets(argument));
        }

        static void ConvertListOfMapsets(Arguments args)
        {
            args.Validate();

            args.Print(args.ToString(), 3);

            var listOfQpFiles = new List<string>();
            foreach (var inputPath in args.Paths)
            {
                if (Directory.Exists(inputPath))
                {
                    listOfQpFiles.AddRange(
                        Directory.GetFiles(inputPath)
                            .Where(file => Path.GetExtension(file) == ".json")
                    );
                }
                else if (File.Exists(inputPath) && Path.GetExtension(inputPath) == ".json")
                {
                    listOfQpFiles.Add(inputPath);
                }
            }

            if (listOfQpFiles.Count == 0)
                args.Print("No files found");
            else
                args.Print($"Found {listOfQpFiles.Count} mapsets to convert", 2);

            foreach (var mapsetPath in listOfQpFiles)
            {
                args.Print($"Converting {mapsetPath}", 2);
                // try
                // {
                //     Conversion.ConvertMapset(mapsetPath, args);
                //     Console.WriteLine($"Finished converting mapset {mapsetPath}", 2);
                // }
                // catch (Exception e)
                // {
                //     args.Print($"Could not convert mapset {mapsetPath}, {e.Message}");
                // }
                Conversion.ConvertMapset(mapsetPath, args);
                Console.WriteLine($"Finished converting mapset {Path.GetFileNameWithoutExtension(mapsetPath) + ".qua"}", 2);
            }
        }
    }
}