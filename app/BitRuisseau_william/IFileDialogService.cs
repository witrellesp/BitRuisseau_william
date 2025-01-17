using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRuisseau_william
{
    public interface IFileDialogService
    {
        bool ShowDialog();
        string[] FileNames { get; }
    }
}
