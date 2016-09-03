using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Web.Models
{
    public class HttpFile
    {
        public HttpFile(string fileName, long size, string key, string path, string extension)
        {
            Name = fileName;
            Size = size;
            Key = key;
            Path = path;
            Extension = extension;
        }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public string Key { get; set; }
        public string Extension { get; set; }
    }
}
