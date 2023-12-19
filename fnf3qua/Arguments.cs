#nullable enable
using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace fnf3qua
{
    public class Arguments
    {
        [Value(0, MetaName = "input-paths", Required = true,
            HelpText = "Path(s) to directories containing .qua files or direct .json file path(s)")]
        public IEnumerable<string> Paths { get; set; }

        [Option('o', "output", HelpText = "Specifies the output directory, uses original directory of .json by default")]
        public string Output { get; set; }

        [Option('c', "creator", HelpText = "Changes the creator username for all maps")]
        public string Creator { get; set; }

        [Option('r', "recursive", Default = false,
            HelpText = "Looks for .qua in all subdirectories of given directories")]
        public bool RecursiveSearch { get; set; }

        [Option("verbosity", SetName = "verbosity", Hidden = true, Default = 1,
            HelpText = "Show more of what's happening")]
        public int Verbosity { get; set; }

        public string SampleSet { get; set; }

        public void Validate()
        {
        }

        private double ValidateValue(double value, string name, double min, double max)
        {
            if ((value > max || value < min) && Verbosity >= 1)
                Console.WriteLine($"{name} was clamped between {min} and {max}");
            return Math.Clamp(value, min, max);
        }

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine("Paths: " + Paths);
            lines.AppendLine("Output: " + Output);
            lines.AppendLine("Creator: " + Creator);
            lines.AppendLine("RecursiveSearch: " + RecursiveSearch);
            lines.AppendLine("Verbosity: " + Verbosity);
            lines.AppendLine("SampleSet: " + SampleSet);
            return lines.ToString();
        }

        public void Print(string message, int minimumVerbosity = 1)
        {
            if (Verbosity >= minimumVerbosity)
                Console.WriteLine(message);
        }
    }
}