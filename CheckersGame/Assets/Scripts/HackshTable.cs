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
        public string[] getKeys()
        {
            string[] keys = new string[5];
            int i = 0;
            foreach(string key in this.table.Keys)
            {
                keys[i] = key;
                i++;
            }
            return keys;
        }
        public string[] get(string key)
        {
            return this.table[key];
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
        public string[] getAllInEntry(string key)
        {
            if(!table.ContainsKey(key))
            {
                return new string[0];
            }
            return table[key];
        }
        public string[][] toArray()
        {
            string[][] formatted;
            if (table.ContainsKey("PieceID"))
            {
                formatted = new string[5][];
                formatted[0] = table["PieceID"];
                formatted[1] = table["GameID"];
                formatted[2] = table["Location"];
                formatted[3] = table["Color"];
                formatted[4] = table["IsKing"];
            }
            else
            {
                formatted = new string[4][];
                formatted[0] = table["GameID"];
                formatted[1] = table["GameStatus"];
                formatted[2] = table["RequestStatus"];
                formatted[3] = table["Turn"];
            }
            return formatted;
        }
        public string toString()
        {
            string[][] data = new string[table.Count][];
            int iterator = 0;

            foreach (KeyValuePair<string, string[]> entry in table)
            {
                data[iterator] = entryToStringArray(entry.Key, entry.Value);
                iterator += 1;
            }

            string finalValue = "";
            for (int i = 0; i < data[0].Length; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    if (j == data.Length - 1)
                    {
                        finalValue += data[j][i] + "\n";
                    }
                    else
                    {
                        finalValue += data[j][i] + "|";
                    }
                }
                if (i == 0)
                {
                    int len = finalValue.Length;
                    for (int j = 0; j < len - 1; j++)
                    {
                        finalValue += "-";
                    }
                    finalValue += "\n";
                }
            }

            return finalValue;
        }
        private string[] entryToStringArray(string key, string[] values)
        {
            int width = key.Length;
            int rowCount = 1;
            foreach (string word in values)
            {
                if (width < word.Length)
                {
                    width = word.Length;
                }
                rowCount += 1;
            }
            string[] formatted = new string[rowCount];
            formatted[0] = key + makeSpace(width - key.Length);

            for (int i = 1; i < rowCount; i++)
            {
                formatted[i] = values[i-1] + makeSpace(width - values[i-1].Length);
            }

            return formatted;
        }
        private string makeSpace(int width)
        {
            string formatted = "";
            for(int i = 0; i < width; i++)
            {
                formatted += " ";
            }
            return formatted;
        }
        public void generateFromRaw(string raw)
        {
            string[] segmented = raw.Split('-');
            foreach (string segment in segmented)
            {
                string[] entry = segment.Split(':');
                if (entry.Length == 1)
                {
                    this.add(entry[0], null);
                }
                else
                {
                    this.add(entry[0], entry[1]);
                }
            }
        }
    }
}
