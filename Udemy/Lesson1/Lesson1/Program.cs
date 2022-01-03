using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Lesson1
{
    class Program
    {
        public class Journal 
        {
            private readonly List<string> entries = new List<string>();
            private static int count = 0;
            public int AddEntry(string text) 
            {
                entries.Add($"{++count}: {text}");
                return count; // memento
            }
            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public override string ToString()
            {
                string result = string.Empty;
                for (int i = 0; i < entries.Count; i++)
                {
                    result += entries[i] + "\n";
                }
                return result;
                //return string.Join(Environment.NewLine, entries);
            }
        }

        public class Persistence
        {
            public void SaveToFile(Journal j, string filename, bool overwrite = false)
            {
                if (overwrite || !File.Exists(filename))
                {
                    File.WriteAllText(filename, j.ToString());
                }
            }
        }
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");
            Console.WriteLine(j);
            var p = new Persistence();
            var filename = @"C:\Users\huber\OneDrive\Desktop\journal.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
        }
    }
}
