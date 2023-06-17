using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UniqueCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string codesPath = Path.Combine(basePath, "codes.txt");

            int amountOfCode = 1000;
            List<string> codes = GenerateUniqueCodes(amountOfCode);

            WriteCodesToFile(codesPath, codes);
            
            foreach (string code in codes)
            {
                Console.WriteLine(code);
            }

            Console.Write("Codes are generated and saved to file.\nCampaign is started!\nEnter a code:");

            string userCode = Console.ReadLine();

            bool isValid = CheckCodeValidity(userCode, codes);

            if (isValid)
            {
                Console.WriteLine("Code is valid. You have successfully joined the campaign!");
            }
            else
            {
                Console.WriteLine("Code is not valid. Please enter a valid code.");
            }
        }

        static List<string> GenerateUniqueCodes(int count)
        {
            List<string> codes = new List<string>();
            Random random = new Random();

            while (codes.Count < count)
            {
                StringBuilder sb = new StringBuilder();

                while (sb.Length < 8)
                {
                    char character;

                    if (random.Next(2) == 0)
                    {
                        character = (char)random.Next('A', 'Z' + 1);
                    }
                    else
                    {
                        character = (char)random.Next('2', '9' + 1);
                    }

                    if ("ACDEFGHKLMNPRTXYZ234579".Contains(character))
                    {
                        sb.Append(character);
                    }
                }

                string code = sb.ToString();

                if (!codes.Contains(code))
                {
                    codes.Add(code);
                }
            }

            return codes;
        }

        static bool CheckCodeValidity(string code, List<string> codes)
        {
            if (code.Length != 8)
            {
                return false;
            }

            string allowedCharacters = "ACDEFGHKLMNPRTXYZ234579";
            foreach (char c in code)
            {
                if (!allowedCharacters.Contains(c))
                {
                    return false;
                }
            }

            return codes.Contains(code);
        }

        static void WriteCodesToFile(string codesPath, List<string> codes)
        {
            File.WriteAllLines(codesPath, codes);
        }
    }
}
