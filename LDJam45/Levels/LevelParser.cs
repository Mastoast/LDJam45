using System;
using System.IO;
using System.Collections;

namespace LDJam45
{
    class LevelParser
    {
        public static IEnumerable lines;
        public static IEnumerator enumerator;

        public LevelParser()
        {
        }

        public static void ReadFile(string path)
        {
            lines = File.ReadLines(path);
            enumerator = lines.GetEnumerator();

            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }
        }

        public static  string GetCurrent()
        {
            return enumerator.Current.ToString();
        }

        public static string GetNext()
        {
            enumerator.MoveNext();
            return GetCurrent();
        }
    }
}