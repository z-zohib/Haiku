using System;
using System.Collections.Generic;

namespace Itpros.Haiku.Detection
{
    public class HaikuDetector
    {
        private static readonly List<char> Vowels = new List<char>
        {
            'a',
            'e',
            'i',
            'o',
            'u'
        }; 
        
        /// <summary>
        /// 4 rules of Haiku
        /// 1 - a vowel is one syllable
        /// 2 - consecutive vowels counts as 1
        /// 3 - 'y' at the end is a syllable
        /// 4 - 'e' at the end is NOT a syllable
        /// </summary>
        /// <param name="poem"></param>
        /// <returns></returns>
        public static bool IsHaiku(List<string> poem)
        {
            if (poem.Count != 3)
            {
                return false;
            }
            
            var syllablesPerLine = new List<int>();
            
            foreach (var lineUnfiltered in poem)
            {
                var line = lineUnfiltered.ToLower();
                var count = 0;
                var totalCharactersInLine = line.Length;
                var lastIndex = totalCharactersInLine - 1;
                
                for (var i = 0; i < totalCharactersInLine; i++)
                {
                    var currentCharacter = line[i];
                    if (Vowels.Contains(currentCharacter))
                    {
                        if (i > 0 && Vowels.Contains(line[i - 1]))
                        {
                            Console.WriteLine("skipped: " + line[i - 1] + currentCharacter);
                            continue;
                        }
                        
                        if (i != lastIndex && line[i + 1] == ' ' && currentCharacter == 'e')
                        {
                            Console.WriteLine("skipped: " + line[i - 1] + currentCharacter);
                            continue; 
                        }
                        
                        if (i > 0)
                        {
                            Console.WriteLine("counted: " + line[i - 1] + currentCharacter);
                        }
                        
                        count++;
                    }
                    else if (i == lastIndex && currentCharacter == 'y')
                    {
                        count++;
                    }
                }

                syllablesPerLine.Add(count);
            }
            
            Console.WriteLine("Line 1: " + syllablesPerLine[0]);
            Console.WriteLine("Line 2: " + syllablesPerLine[1]);
            Console.WriteLine("Line 3: " + syllablesPerLine[2]);

            return syllablesPerLine[0] == 5 &&
                   syllablesPerLine[1] == 7 &&
                   syllablesPerLine[2] == 5;

        }
    }
}