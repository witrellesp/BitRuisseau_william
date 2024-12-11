using System.Diagnostics;
using System.Numerics;

namespace BitRuisseau_william
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        private bool isPlaying = false; // Indique si le lecteur est en train de lire
        public List<string> Files { get; set; } = new List<string>();


        private Mediatheque mediatheque = new Mediatheque()
        {
            Medias = new List<Media>()
        };



        public Form1()
        {
            InitializeComponent();
        }



        private void AddTitle_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Fichiers MP3|*.mp3|Fichiers MP4|*.mp4|Fichiers MOV|*.mov|Fichiers GIF|*.gif|Fichiers PNG|*.png|Fichiers JPEG|*.jpeg|Fichiers JPG|*.jpg|Fichiers WAV|*.wav"
            };



            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string[] selectedFiles = openFileDialog.FileNames;

                foreach (var file in selectedFiles)
                {
                    string fileName = Path.GetFileName(file);
                    //string fileType = Path.GetExtension(file);
                    long fileSize = file.Length;

                    string fileType = Path.GetExtension(file); // Exemple de string que vous souhaitez convertir
                    MediaTypes mediaType;

                    if (Enum.TryParse(fileType, out mediaType))
                    {
                        // La conversion a réussi, vous pouvez utiliser 'mediaType' qui est maintenant de type 'MediaTypes'
                        Console.WriteLine($"Le type de média est : {mediaType}");
                    }
                    else
                    {
                        // La conversion a échoué (par exemple si la chaîne ne correspond pas à une valeur de l'énumération)
                        Console.WriteLine("Erreur : Le type de média n'est pas valide.");
                    }

                    Media newMedia = new Media(fileName, fileSize, mediaType);

                    mediatheque.Medias.Add(newMedia);

                    listView_myFiles.Items.Add(fileName);
                }



                MessageBox.Show("Fichier ajouté.", "Succès");
            }
            else
            {
                MessageBox.Show("Aucun fichier a été sélectionné", "Avertissement");
            }



        }

        private void DropTitle_Click(object sender, EventArgs e)
        {
            if (listView_myFiles.SelectedItems.Count > 0) // Vérifie qu'il y a un seul élément sélectionné
            {
                string fileSelected = listView_myFiles.SelectedItems[0].Text; // Récupère le texte de l'élément

                // Supprime l'élément sélectionné
                listView_myFiles.Items.Remove(listView_myFiles.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Aucun fichier sélectionné.");
            }
        }


        private void playFile_Click(object sender, EventArgs e)
        {

            int selectedIndex = listView_myFiles.SelectedItems[0].Index;
            string fileSelectedPath = Files[selectedIndex];

            string type = Path.GetExtension(fileSelectedPath);


            if (type == ".mp3")
            {

                if (isPlaying)
                {
                    player.controls.pause();
                    playFile.Text = "▶";
                }
                if (player.URL != fileSelectedPath)
                {
                    player.URL = fileSelectedPath;
                    player.controls.play();
                    playFile.Text = "❚❚";
                }

            }
            else
            {
                try
                {

                    // Exécuter le fichier avec Process.Start
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = fileSelectedPath,
                        UseShellExecute = true
                    });

                    //else
                    //{
                    //    MessageBox.Show("Veuillez sélectionner un fichier à lire.", "Erreur");
                    //}

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Aucun fichier sélectionné.");
                }

            }
            // Inverse l'état de lecture
            isPlaying = !isPlaying;

        }

        private void stopFile_Click(object sender, EventArgs e)
        {
            player.controls.stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView_myFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
