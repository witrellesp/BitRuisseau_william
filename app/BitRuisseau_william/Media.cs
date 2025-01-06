using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRuisseau_william
{
    public class Media
    {
        public Media(string filename, long size, MediaTypes mediaType)
        {
            this.FileName = filename;

            this.Size = size / 1024;

            this.MediaType = mediaType;
        }

        public string FileName { get; set; }
        public long Size { get; set; } // Taille du fichier en Octets

        public MediaTypes MediaType { get; set; }

    }

}
