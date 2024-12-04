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
            groupBoxMesTitres = new GroupBox();
            title = new Label();
            AddTitle = new Button();
            Network = new Button();
            DropTitle = new Button();
            SuspendLayout();
            // 
            // groupBoxMesTitres
            // 
            groupBoxMesTitres.Location = new Point(12, 80);
            groupBoxMesTitres.Name = "groupBoxMesTitres";
            groupBoxMesTitres.Size = new Size(580, 318);
            groupBoxMesTitres.TabIndex = 0;
            groupBoxMesTitres.TabStop = false;
            groupBoxMesTitres.Text = "Mes titres";
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
            AddTitle.Location = new Point(638, 47);
            AddTitle.Name = "AddTitle";
            AddTitle.Size = new Size(101, 23);
            AddTitle.TabIndex = 2;
            AddTitle.Text = "Ajouter titre";
            AddTitle.UseVisualStyleBackColor = true;
            // 
            // Network
            // 
            Network.Location = new Point(663, 306);
            Network.Name = "Network";
            Network.Size = new Size(75, 23);
            Network.TabIndex = 3;
            Network.Text = "Réseau";
            Network.UseVisualStyleBackColor = true;
            // 
            // DropTitle
            // 
            DropTitle.Location = new Point(638, 80);
            DropTitle.Name = "DropTitle";
            DropTitle.Size = new Size(75, 23);
            DropTitle.TabIndex = 4;
            DropTitle.Text = "Drop titre";
            DropTitle.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DropTitle);
            Controls.Add(Network);
            Controls.Add(AddTitle);
            Controls.Add(title);
            Controls.Add(groupBoxMesTitres);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxMesTitres;
        private Label title;
        private Button AddTitle;
        private Button Network;
        private Button DropTitle;
    }
}
