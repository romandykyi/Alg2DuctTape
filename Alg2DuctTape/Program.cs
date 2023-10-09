namespace Alg2DuctTape
{
    public static class Program
    {
        // Numer albumu:
        const string StudentId = "53xxx";
        // Numer grupy laboratoryjnej:
        const string GroupName = "200X";
        // Imię i nazwisko autora:
        const string AuthorFullName = "Imię Nazwisko";
        // Adres email:
        const string Email = "in53xxx@zut.edu.pl";

        private static bool TryParseInclude(string line, out string? result)
        {
            result = null;
            if (!line.TrimStart().StartsWith("#include"))
                return false;

            int startQuoteIndex = line.IndexOf('"') + 1;
            int endQuoteIndex = line.IndexOf('"', startQuoteIndex);
            if (startQuoteIndex != -1 && endQuoteIndex != -1)
            {
                result = line[startQuoteIndex..endQuoteIndex];
            }

            return true;
        }

        private static void ParseFile(string filePath, TextWriter writer,
            HashSet<string> includedFiles, HashSet<string> includes)
        {
            filePath = filePath.Trim();
            if (includedFiles.Contains(filePath)) return;

            Console.WriteLine($"Parsing {filePath}. . .");

            includedFiles.Add(filePath);

            string baseFolder = Path.GetDirectoryName(filePath)!;
            using StreamReader reader = new(filePath);

            string? currentLine;
            while (true)
            {
                currentLine = reader.ReadLine();
                if (currentLine == null) break;

                if (currentLine.Contains("#pragma once"))
                    continue;

                if (TryParseInclude(currentLine, out var relativePath))
                {
                    if (relativePath != null)
                    {
                        string includeFile = Path.GetFullPath(relativePath, baseFolder);
                        ParseFile(includeFile, writer, includedFiles, includes);
                    }
                    else
                    {
                        includes.Add(currentLine.Trim());
                    }
                }
                else
                {
                    writer.WriteLine(currentLine);
                }
            }
        }

        private static void DuctTape(string mainFilePath, string outputFile, string header)
        {
            string tempFileName = $"{Path.GetTempPath()}{Guid.NewGuid()}.cpp";
            HashSet<string> includes = new();
            using (StreamWriter writer = new(tempFileName))
            {
                ParseFile(mainFilePath, writer, new(), includes);
            }

            Console.WriteLine("Duct taping. . .");

            using (StreamWriter writer = new(outputFile))
            using (StreamReader reader = new(tempFileName))
            {
                writer.WriteLine($"// {header}");
                writer.WriteLine($"// {AuthorFullName}");
                writer.WriteLine($"// {Email}");
                writer.WriteLine();
                foreach (var include in includes)
                {
                    writer.WriteLine(include);
                }
                while (true)
                {
                    string? line = reader.ReadLine();
                    if (line == null) break;
                    writer.WriteLine(line);
                }
            }
            Console.WriteLine("Done!");
        }

        public static void Main()
        {
            while (true)
            {
                Console.Write("Main .cpp/.c file path: ");
                string? mainFilePath = (Console.ReadLine() ?? string.Empty).Trim('"');
                Console.Write("Lab name: ");
                string? labName = Console.ReadLine() ?? string.Empty;

                string emailSubject = $"ALGO2 IS1 {GroupName.ToUpper()} {labName.ToUpper()}";
                string outputFileName = $"{StudentId}.algo2.{labName.ToLower()}.main.cpp";
                string outputPath = Path.Combine(Path.GetDirectoryName(mainFilePath)!, outputFileName);
                try
                {
                    DuctTape(mainFilePath, outputPath.ToLower(), emailSubject);

                    Console.WriteLine($"Saved file: {outputPath}");
                    Console.WriteLine($"Email subject: {emailSubject}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error has occured: {ex.Message}");
                }
                Console.WriteLine();
            }
        }
    }
}
