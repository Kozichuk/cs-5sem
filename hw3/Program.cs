using System;
using System.IO;
using System.Linq;


namespace hw3
{

    /*
        Ключевые слова для поиска в тестах и их частоность
        memasiki-1 kira-1 irak-1 
        usa-1 britain-1 isis-1 
        iloopp-1 game-1 jero-1 
        lemon-3 biology-1 igra-1 ololo-1
        
        первый арг: путь к папке
        второй аргумент: маска. Если ищем без маски, то пишем "null"
        последующие аргументы: ключи

        Реализовано: 
        + поиск файлов во всех подпапках(не рекурсивно), 
        + поиск в файлах по маске 
        + поиск нескольких фраз одновременно
        Не реализовано:
        - Несколько вариантов поиска вхождения (e.g. StringComparison.OrdinalIgnoreCase)
    
     */
    public class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];
            var mask = args[1];
            var keys = new string[args.Length - 2];
            for (var i = 2; i < args.Length; i++)
            {
                keys[i - 2] = args[i];
            }

            var freq = ContainmentCounting(path, mask == "null" ? "*" : mask, keys);
            for (var i = 0; i < freq.Length; i++)
            {
                Console.WriteLine(keys[i] + " - " + freq[i]);
            }
        }

        public static int[] ContainmentCounting(string orPath, string mask, string[] keys)
        {
            var names = Directory.GetFiles(orPath, mask, SearchOption.AllDirectories);
            var ans = new int[keys.Length];
            foreach (var path in names)
            {
                Console.WriteLine(path);
                if (File.Exists(path))
                {
                    var text = System.IO.File.ReadAllText(path);
                    for (var i = 0; i < keys.Length; i++)
                    {
                        ans[i] += GetCountKeyInFile(text, keys[i]);
                    }
                }
                else if (Directory.Exists(path))
                {
                    var temp = ContainmentCounting(path, mask, keys);
                    for (var i = 0; i < ans.Length; i++)
                    {
                        ans[i] += temp[i];
                    }
                }
            }
            return ans;
        }

        public static int GetCountKeyInFile(string text, string key)
        {
            return text.Split(new string[] {key}, StringSplitOptions.None).Count() - 1;
        }
    }
}
