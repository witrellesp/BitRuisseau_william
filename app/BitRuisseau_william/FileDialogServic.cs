using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRuisseau_william
{
    public class FileDialogService : IFileDialogService
    {
        private readonly OpenFileDialog _openFileDialog;

        public FileDialogService()
        {
          
            _openFileDialog = new OpenFileDialog
            {
                Multiselect = true, 
                Filter = "Fichiers MP3|*.mp3|Fichiers MP4|*.mp4|Fichiers MOV|*.mov|Fichiers GIF|*.gif|Fichiers PNG|*.png|Fichiers JPEG|*.jpeg|Fichiers JPG|*.jpg|Fichiers WAV|*.wav" 
            };
        }

        public bool ShowDialog()
        {
            return _openFileDialog.ShowDialog() == DialogResult.OK;
        } 

       
        public string[] FileNames
        {
            get { return _openFileDialog.FileNames; } 
        }
    }
}
