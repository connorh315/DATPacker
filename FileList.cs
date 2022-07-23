using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATPacker
{
    public class FileList
    {
        // Someone teach me how hash buckets work please and then I can probably lower memory usage whilst maintaining performance
        private List<string> files = new();
        private Dictionary<long, bool> hashes = new();

        public void Add(string path)
        {
            files.Add(path.ToLower());
            hashes.Add(Hash.Calculate(path.ToUpper()), true);
        }

        public bool Contains(string path)
        {
            return hashes.ContainsKey(Hash.Calculate(path.ToUpper()));
        }

        public void Clear()
        {
            files.Clear();
            hashes.Clear();
        }

        public void Remove(string path)
        {
            files.Remove(path.ToLower());
            hashes.Remove(Hash.Calculate(path.ToUpper()));
        }

        public string Get(int index)
        {
            return files[index];
        }

        public string this[int index]
        {
            get
            {
                return files[index];
            }
        }

        public string[] ToArray() => files.ToArray();
        public int Length => files.Count;
    }
}
