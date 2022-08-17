using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication.ExtendedProtection;
using System.Transactions;

namespace Itpros.Haiku.Detection
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var poem = new List<string>();
            var path = args[0];
            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("File does not exist.");
                    return;
                }
                
                using (var sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        poem.Add(sr.ReadLine());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            if (HaikuDetector.IsHaiku(poem))
            {
                Console.WriteLine("Valid haiku");
            }
            else
            {
                Console.WriteLine("Not a haiku poem because __");
            }
            
        }
    }
}