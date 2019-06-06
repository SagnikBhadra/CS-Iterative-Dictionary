using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var integers = new List<int>();
            //for (int i = 2; i > 0; i--)
            //{
            //    integers.Add(i);
            //}
            //Console.WriteLine(integers.FirstOrDefault(x => x > 10));

            //foreach (var i in integers)
            //{
            //    Console.WriteLine(i.ToString());
            //}
            //var sum = integers.Sum();
            //Console.WriteLine($"The sum of the list is: {sum}");
            //var peakStrings = integers.FirstOrDefault()
            //    .Select(x => x.ToString())
            //    .ToArray();
            //integers.Add(100);
            //Console.WriteLine(String.Join(", ", peakStrings));

            var dictionaryString = await ReadFilesAsync();

            var dictionary = new Dictionary<string, List<string>>();

            //var dictionaryString = File.ReadAllText("../../../Documents/GitHub/Interactive-Dictionary/data.json");
            dictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(dictionaryString);
            //foreach (var kv in dictionary)
            //{
            //    Console.WriteLine($"{kv.Key} : {String.Join(", ", kv.Value)}");
            //}
            Console.WriteLine("Enter word: ");
            var defin = DefinitionOfWord(Console.ReadLine(), dictionary);
            Console.WriteLine(String.Join("\n", defin));
        }
        static async Task<String> ReadFilesAsync()
        {
            string file = "../../../Documents/GitHub/Interactive-Dictionary/data.json";
            char[] buffer;
            //File.ReadAllTextAsync()
            using (var sr = new StreamReader(file))
            {
                buffer = new char[(int)sr.BaseStream.Length];
                await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
            }
            return new String(buffer);
        }
        static string[] DefinitionOfWord(string word, IDictionary<string, List<string>> dictionary)
        {
            string[] wordDoesNotExist = { "The word doesn't exist. Please double check it." };
            word = word.ToLower();

            if (dictionary[word].Any())
            {
                return dictionary[word].ToArray();
            }
            else if (dictionary.Keys.Contains(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word)))
            {
                return dictionary[CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word)].ToArray();
            }
            else
            {
                return wordDoesNotExist;
            }
        }
    }
}
