namespace BitRuisseau_william
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            title = new Label();
            AddTitle = new Button();
            Network = new Button();
            DropTitle = new Button();
            listView_myFiles = new ListView();
            playFile = new Button();
            stopFile = new Button();
            listViewRemoteMediatheques = new ListView();
            txtConsole = new RichTextBox();
            Online = new CheckBox();
            SuspendLayout();
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("Segoe UI Symbol", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title.Location = new Point(343, 9);
            title.Name = "title";
            title.Size = new Size(162, 37);
            title.TabIndex = 1;
            title.Text = "BitRuisseau";
            // 
            // AddTitle
            // 
            AddTitle.AutoSize = true;
            AddTitle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddTitle.Location = new Point(531, 81);
            AddTitle.Name = "AddTitle";
            AddTitle.Size = new Size(101, 25);
            AddTitle.TabIndex = 2;
            AddTitle.Text = "Ajouter titre";
            AddTitle.TextImageRelation = TextImageRelation.ImageAboveText;
            AddTitle.UseVisualStyleBackColor = true;
            AddTitle.Click += AddTitle_Click;
            // 
            // Network
            // 
            Network.Location = new Point(537, 374);
            Network.Name = "Network";
            Network.Size = new Size(75, 23);
            Network.TabIndex = 3;
            Network.Text = "Réseau";
            Network.UseVisualStyleBackColor = true;
            Network.Click += Network_Click;
            // 
            // DropTitle
            // 
            DropTitle.AutoSize = true;
            DropTitle.Location = new Point(543, 112);
            DropTitle.Name = "DropTitle";
            DropTitle.Size = new Size(75, 25);
            DropTitle.TabIndex = 4;
            DropTitle.Text = "Drop titre";
            DropTitle.UseVisualStyleBackColor = true;
            DropTitle.Click += DropTitle_Click;
            // 
            // listView_myFiles
            // 
            listView_myFiles.BackColor = Color.RosyBrown;
            listView_myFiles.Location = new Point(17, 81);
            listView_myFiles.Name = "listView_myFiles";
            listView_myFiles.Size = new Size(258, 328);
            listView_myFiles.TabIndex = 6;
            listView_myFiles.UseCompatibleStateImageBehavior = false;
            listView_myFiles.SelectedIndexChanged += listView_myFiles_SelectedIndexChanged;
            // 
            // playFile
            // 
            playFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            playFile.Location = new Point(540, 211);
            playFile.Name = "playFile";
            playFile.Size = new Size(75, 23);
            playFile.TabIndex = 7;
            playFile.Text = "▶";
            playFile.UseVisualStyleBackColor = true;
            playFile.Click += playFile_Click;
            // 
            // stopFile
            // 
            stopFile.Location = new Point(540, 240);
            stopFile.Name = "stopFile";
            stopFile.Size = new Size(75, 23);
            stopFile.TabIndex = 8;
            stopFile.Text = "❚❚";
            stopFile.UseVisualStyleBackColor = true;
            // 
            // listViewRemoteMediatheques
            // 
            listViewRemoteMediatheques.Location = new Point(679, 81);
            listViewRemoteMediatheques.Name = "listViewRemoteMediatheques";
            listViewRemoteMediatheques.Size = new Size(121, 328);
            listViewRemoteMediatheques.TabIndex = 10;
            listViewRemoteMediatheques.UseCompatibleStateImageBehavior = false;
            // 
            // txtConsole
            // 
            txtConsole.Location = new Point(304, 81);
            txtConsole.Name = "txtConsole";
            txtConsole.Size = new Size(192, 328);
            txtConsole.TabIndex = 11;
            txtConsole.Text = "";
            // 
            // Online
            // 
            Online.AutoSize = true;
            Online.Location = new Point(541, 324);
            Online.Name = "Online";
            Online.Size = new Size(68, 19);
            Online.TabIndex = 12;
            Online.Text = "En ligne";
            Online.UseVisualStyleBackColor = true;
            Online.CheckedChanged += Online_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(820, 450);
            Controls.Add(Online);
            Controls.Add(txtConsole);
            Controls.Add(listViewRemoteMediatheques);
            Controls.Add(stopFile);
            Controls.Add(playFile);
            Controls.Add(listView_myFiles);
            Controls.Add(DropTitle);
            Controls.Add(Network);
            Controls.Add(AddTitle);
            Controls.Add(title);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label title;
        private Button AddTitle;
        private Button Network;
        private Button DropTitle;
        private ListView listView_myFiles;
        private Button playFile;
        private Button stopFile;
        private ListView listViewRemoteMediatheques;
        private RichTextBox txtConsole;
        private CheckBox Online;
    }
}
