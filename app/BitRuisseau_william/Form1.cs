using System.Diagnostics;
using System.Numerics;

namespace BitRuisseau_william
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        private bool isPlaying = false; // Indique si le lecteur est en train de lire
        //public List<string> Files { get; set; } = new List<string>(); 
        private Dictionary<Media, string> mediaPaths = new Dictionary<Media, string>();




        private Mediatheque mediatheque = new Mediatheque()
        {
            Medias = new List<Media>()
        };



        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bouton pour ajouter des medias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
                    long fileSize = file.Length;

                    string fileType = Path.GetExtension(file).TrimStart('.');

                    MediaTypes mediaType;

                    if (Enum.TryParse(fileType, true, out mediaType))
                    {
                        Media newMedia = new Media(fileName, fileSize, mediaType);

                        mediatheque.Medias.Add(newMedia);

                        mediaPaths[newMedia] = file;

                        listView_myFiles.Items.Add(fileName);
                    }
                    else
                    {
                        // La conversion a échoué
                        Console.WriteLine("Erreur : Le type de média n'est pas valide.");
                    }


                }



                MessageBox.Show("Fichier ajouté.", "Succès");
            }
            else
            {
                MessageBox.Show("Aucun fichier a été sélectionné", "Avertissement");
            }



        }
        /// <summary>
        /// Bouton pour supprimer un media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Bouton pour jouer le media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playFile_Click(object sender, EventArgs e)
        {

            if (listView_myFiles.SelectedItems.Count == 0) // Vérifie qu'un fichier est sélectionné
            {
                MessageBox.Show("Veuillez sélectionner un fichier à lire.", "Erreur");
                return;
            }

            int selectedIndex = listView_myFiles.SelectedItems[0].Index;
            Media selectedMedia = mediatheque.Medias[selectedIndex];

            if (!mediaPaths.TryGetValue(selectedMedia, out string fileSelectedPath))
            {
                MessageBox.Show("Erreur : impossible de trouver le fichier sélectionné.", "Erreur");
                return;
            }


            string type = Path.GetExtension(fileSelectedPath).ToLower();


            if (type == ".mp3")
            {
                if (player.URL != fileSelectedPath)
                {
                    player.URL = fileSelectedPath;
                }

                player.controls.play();


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

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Aucun fichier sélectionné.");
                }

            }
            // Inverse l'état de lecture
            isPlaying = !isPlaying;

        }
        /// <summary>
        /// Bouton pour arreter le media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void radioButton_Online_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Online.Checked)
            {
                mediatheque.IsAvailable = true;
            }
            else if (!radioButton_Online.Checked)
            {
                mediatheque.IsAvailable = false;
            }

        }

        private void Network_Click(object sender, EventArgs e)
        {


        }
    }
}
