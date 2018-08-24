/*
 * 
    This class is used in development. It is a handy tool for collecting and displaying
all information stored in the database. I made it because C#'s dictionary and hash table
structures were not intuitive for the algorithms I wanted to use. This acts as an interface
for that aid.

- Nick Jackson

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class HackshTable
    {
        private Dictionary<string, string[]> table;
        public HackshTable()
        {
            table = new Dictionary<string, string[]>();
        }
        public void add(string key, string word)
        {
            if(!table.ContainsKey(key))
            {
                string[] addition = new string[1];
                addition[0] = word;
                table.Add(key, addition);
            }
            else
            {
                int len = table[key].Length + 1;
                string[] addition = new string[len];
                for(int i = 0; i < len; i++)
                {
                    addition[i] = table[key][i];
                }
                addition[len - 1] = word;
                table.Remove(key);
                table.Add(key, addition);
            }
        }
        public void remove(string key)
        {
            table.Remove(key);
        }
        public void remove(string key, string word)
        {
            if(!table.ContainsKey(key) || !table[key].Contains(word))
            {
                return;
            }
            int len = table[key].Length - 1;
            string[] addition = new string[len];
            int j = 0;
            for(int i = 0; i <= len; i++)
            {
                if(table[key][i] != word)
                {
                    addition[j] = table[key][i];
                    j++;
                }
            }
        }
    }
}
