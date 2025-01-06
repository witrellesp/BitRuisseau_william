using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using Backend;
using Backend.Protocol;
using Microsoft.Extensions.Logging;
using BitRuisseau_william.Protocole;


namespace BitRuisseau_william
{
    public partial class Form1 : Form
    {
        private MqttCommunicator _mqttCommunicator;
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        private bool isPlaying = false; // Indique si le lecteur est en train de lire

        private Dictionary<Media, string> mediaPaths = new Dictionary<Media, string>();

        private Dictionary<string, Mediatheque> receivedMediatheques = new Dictionary<string, Mediatheque>();


        private Agent _agent;

        private readonly ILogger _logger;


        private Mediatheque mediatheque;


        public Form1()
        {
            InitializeComponent();

            // Initialisation du logger
            var loggerFactory = LoggerFactory.Create(
           builder => builder
          .AddProvider(new RichTextBoxLoggerProvider(txtConsole))
          .SetMinimumLevel(LogLevel.Debug)
           );


            //var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<Form1>();

            // Initialisation de l'agent MQTT
            string broker = "mqtt.blue.section-inf.ch"; // Adresse du broker MQTT

            _agent = new Agent(loggerFactory, broker, OnMessageReceived);
            _agent.Start();
            this.Text = $@"House {_agent.NodeId}";

            Console.WriteLine("Agent MQTT démarré.");

            this.mediatheque = new Mediatheque()
            {
                IsAvailable = false,
                Medias = new List<Media>(),
                DisplayName = "William Mediatheque"

            };

            // Envoi de la requête MEDIA_STATUS_REQUEST après initialisation
            SendMediaStatusRequest();

        }
        /// <summary>
        /// Envoie un message MEDIA_STATUS_REQUEST aux autres nœuds du réseau.
        /// </summary>
        private void SendMediaStatusRequest()
        {
            Console.WriteLine("Envoi de MEDIA_STATUS_REQUEST à tous les nœuds...");

            // Création de l'enveloppe pour le message MEDIA_STATUS_REQUEST
            var envelope = new Envelope(
                senderId: _agent.NodeId,    // Identifiant unique de l'expéditeur
                type: MessageType.MEDIA_STATUS_REQUEST, // Type de message
                message: string.Empty        // Pas de données à envoyer
            );

            // Envoi du message via l'agent MQTT
            _agent.Send(envelope);

            Console.WriteLine("MEDIA_STATUS_REQUEST envoyé.");
        }

        private void OnMessageReceived(Envelope envelope)
        {
            if (envelope.SenderId == _agent.NodeId)
            {
                _logger.LogInformation("Ignorer mon prope message");
                return;
            }
            Console.WriteLine($"Message reçu : {envelope.ToString()}");

            switch (envelope.Type)
            {
                case MessageType.MEDIA_STATUS_REQUEST:
                    HandleMediaStatusRequest();
                    break;

                case MessageType.MEDIA_STATUS:
                    HandleMediaStatus(envelope.Message);
                    break;

                default:
                    Console.WriteLine($"Type de message inconnu : {envelope.Type}");
                    break;
            }
        }
        private void HandleMediaStatusRequest()
        {
            Console.WriteLine("MEDIA_STATUS_REQUEST reçu. Envoi de la médiathèque...");


            string payload = JsonSerializer.Serialize(this.mediatheque);

            var envelope = new Envelope(
                senderId: _agent.NodeId,
                type: MessageType.MEDIA_STATUS,
                message: payload
            );

            _agent.Send(envelope);
            Console.WriteLine("MEDIA_STATUS envoyé.");
        }
        private void HandleMediaStatus(string message)
        {
            Console.WriteLine("MEDIA_STATUS reçu.");

            try
            {
                var mediathequeReceived = JsonSerializer.Deserialize<Mediatheque>(message);
                if (mediathequeReceived != null)
                {
                    Console.WriteLine($"Médiathèque de {mediathequeReceived.DisplayName} reçue avec {mediathequeReceived.Medias.Count} médias.");


                    receivedMediatheques[mediathequeReceived.DisplayName] = mediathequeReceived;

                    // Mettre à jour l'interface utilisateur
                    listViewRemoteMediatheques.Invoke(new MethodInvoker(() =>
                    {
                        var listViewItem = new ListViewItem(new[]
                        {
                           mediathequeReceived.DisplayName,
                           mediathequeReceived.Medias.Count.ToString()
                        });
                        listViewRemoteMediatheques.Items.Add(listViewItem);
                    }));
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la désérialisation de la médiathèque : {ex.Message}");
            }
        }
        private void listViewRemoteMediatheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRemoteMediatheques.SelectedItems.Count > 0)
            {

                string selectedMediathequeName = listViewRemoteMediatheques.SelectedItems[0].SubItems[0].Text;


                if (receivedMediatheques.TryGetValue(selectedMediathequeName, out Mediatheque selectedMediatheque))
                {

                    listView_myFiles.Items.Clear();

                    foreach (var media in selectedMediatheque.Medias)
                    {
                        listView_myFiles.Items.Add(media.FileName);
                    }
                }
            }
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

                    FileInfo fileInfo = new FileInfo(file);
                    long fileSize = fileInfo.Length;



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

        private void Online_CheckedChanged(object sender, EventArgs e)
        {
            if (Online.Checked)
            {
                mediatheque.IsAvailable = true;


                string payload = JsonSerializer.Serialize(this.mediatheque);

                Envelope envelope = new Envelope(
                    senderId: _agent.NodeId,          // Identifiant unique de l'expéditeur
                    type: MessageType.MEDIA_STATUS,  // Type de message
                    message: payload                 // Données sérialisés
                );


                _agent.Send(envelope);

                Console.WriteLine("MEDIA_STATUS envoyé.");
            }
            else
            {
                mediatheque.IsAvailable = false;

                Console.WriteLine("La médiathèque est maintenant hors ligne.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView_myFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_Online_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void Network_Click(object sender, EventArgs e)
        {


        }
    }
}
