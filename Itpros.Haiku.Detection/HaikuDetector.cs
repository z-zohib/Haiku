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
        /// 4 - 'e' at the end is NOT a syllable unless it's the only vowel
        /// </summary>
        /// <param name="poem"></param>
        /// <returns></returns>
        public static bool IsHaiku(List<List<string>> poem)
        {
            if (poem.Count != 3)
            {
                return false;
            }
            
            var syllablesPerLine = new List<int>();
            
            foreach (var line in poem)
            {
                var count = CountSyllablesInLine(line);

                syllablesPerLine.Add(count);
            }

            return syllablesPerLine[0] == 5 &&
                   syllablesPerLine[1] == 7 &&
                   syllablesPerLine[2] == 5;
        }

        private static int CountSyllablesInLine(ICollection<string> line)
        {
            var count = 0;

            foreach (var word in line)
            {
                count += CountSyllablesInWord(word);
            }

            return count;
        }
        private static int CountSyllablesInWord(string word)
        {
            var totalCharactersInWord = word.Length;
            var lastIndex = word.Length - 1;
            var count = 0;

            for (var i = 0; i < totalCharactersInWord; i++)
            {
                var currentCharacter = word[i];
                    
                if (Vowels.Contains(currentCharacter)) // rule 1 - a vowel is one syllable
                {
                    if (i > 0 && Vowels.Contains(word[i - 1])) // rule 2 - consecutive vowels counts as 1
                    {
                        continue;
                    }
                        
                    if (i == lastIndex && currentCharacter == 'e' && count != 0) // rule 4 - 'e' at the end is NOT a syllable unless it's the only vowel
                    {
                        continue; 
                    }
                        
                    count++;
                }
                else if (i == lastIndex && currentCharacter == 'y') // rule 3 - 'y' at the end is a syllable
                {
                    if (i > 0 && Vowels.Contains(word[i - 1])) // rule 2 - consecutive vowels counts as 1
                    {
                        continue;
                    }
                    
                    count++;
                }
            }

            return count;
        }
    }
}