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
            radioButton_Online = new RadioButton();
            listView1 = new ListView();
            SuspendLayout();
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("Segoe UI Symbol", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title.Location = new Point(12, 25);
            title.Name = "title";
            title.Size = new Size(177, 25);
            title.TabIndex = 1;
            title.Text = "BitRuisseau-Local";
            // 
            // AddTitle
            // 
            AddTitle.Location = new Point(531, 81);
            AddTitle.Name = "AddTitle";
            AddTitle.Size = new Size(101, 23);
            AddTitle.TabIndex = 2;
            AddTitle.Text = "Ajouter titre";
            AddTitle.UseVisualStyleBackColor = true;
            AddTitle.Click += AddTitle_Click;
            // 
            // Network
            // 
            Network.Location = new Point(531, 374);
            Network.Name = "Network";
            Network.Size = new Size(75, 23);
            Network.TabIndex = 3;
            Network.Text = "Réseau";
            Network.UseVisualStyleBackColor = true;
            Network.Click += Network_Click;
            // 
            // DropTitle
            // 
            DropTitle.Location = new Point(531, 114);
            DropTitle.Name = "DropTitle";
            DropTitle.Size = new Size(75, 23);
            DropTitle.TabIndex = 4;
            DropTitle.Text = "Drop titre";
            DropTitle.UseVisualStyleBackColor = true;
            DropTitle.Click += DropTitle_Click;
            // 
            // listView_myFiles
            // 
            listView_myFiles.Location = new Point(17, 81);
            listView_myFiles.Name = "listView_myFiles";
            listView_myFiles.Size = new Size(488, 328);
            listView_myFiles.TabIndex = 6;
            listView_myFiles.UseCompatibleStateImageBehavior = false;
            listView_myFiles.SelectedIndexChanged += listView_myFiles_SelectedIndexChanged;
            // 
            // playFile
            // 
            playFile.Location = new Point(531, 211);
            playFile.Name = "playFile";
            playFile.Size = new Size(75, 23);
            playFile.TabIndex = 7;
            playFile.Text = "▶";
            playFile.UseVisualStyleBackColor = true;
            playFile.Click += playFile_Click;
            // 
            // stopFile
            // 
            stopFile.Location = new Point(531, 240);
            stopFile.Name = "stopFile";
            stopFile.Size = new Size(75, 23);
            stopFile.TabIndex = 8;
            stopFile.Text = "❚❚";
            stopFile.UseVisualStyleBackColor = true;
            // 
            // radioButton_Online
            // 
            radioButton_Online.AutoSize = true;
            radioButton_Online.Location = new Point(531, 349);
            radioButton_Online.Name = "radioButton_Online";
            radioButton_Online.Size = new Size(67, 19);
            radioButton_Online.TabIndex = 9;
            radioButton_Online.TabStop = true;
            radioButton_Online.Text = "En ligne";
            radioButton_Online.UseVisualStyleBackColor = true;
            radioButton_Online.CheckedChanged += radioButton_Online_CheckedChanged;
            // 
            // listView1
            // 
            listView1.Location = new Point(679, 81);
            listView1.Name = "listView1";
            listView1.Size = new Size(121, 328);
            listView1.TabIndex = 10;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 450);
            Controls.Add(listView1);
            Controls.Add(radioButton_Online);
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
        private RadioButton radioButton_Online;
        private ListView listView1;
    }
}
