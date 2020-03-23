namespace GSMAutoBackup
{
    partial class ProgramForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.filePathListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.manualRunButton = new System.Windows.Forms.Button();
            this.destinationButton = new System.Windows.Forms.Button();
            this.backupTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // filePathListBox
            // 
            this.filePathListBox.FormattingEnabled = true;
            this.filePathListBox.ItemHeight = 15;
            this.filePathListBox.Location = new System.Drawing.Point(11, 12);
            this.filePathListBox.Name = "filePathListBox";
            this.filePathListBox.Size = new System.Drawing.Size(347, 289);
            this.filePathListBox.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(36, 306);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(123, 38);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add Backup Directory";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(201, 306);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(128, 38);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remove Backup Directory";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // manualRunButton
            // 
            this.manualRunButton.Location = new System.Drawing.Point(36, 397);
            this.manualRunButton.Name = "manualRunButton";
            this.manualRunButton.Size = new System.Drawing.Size(293, 41);
            this.manualRunButton.TabIndex = 3;
            this.manualRunButton.Text = "Save && Close";
            this.manualRunButton.UseVisualStyleBackColor = true;
            this.manualRunButton.Click += new System.EventHandler(this.manualRunButton_Click);
            // 
            // destinationButton
            // 
            this.destinationButton.Location = new System.Drawing.Point(36, 350);
            this.destinationButton.Name = "destinationButton";
            this.destinationButton.Size = new System.Drawing.Size(293, 41);
            this.destinationButton.TabIndex = 4;
            this.destinationButton.Text = "Choose Backup Directory";
            this.destinationButton.UseVisualStyleBackColor = true;
            this.destinationButton.Click += new System.EventHandler(this.destinationButton_Click);
            // 
            // backupTimer
            // 
            this.backupTimer.Enabled = true;
            this.backupTimer.Interval = 900000;
            this.backupTimer.Tick += new System.EventHandler(this.backupTimer_Tick);
            // 
            // ProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 450);
            this.Controls.Add(this.manualRunButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.destinationButton);
            this.Controls.Add(this.filePathListBox);
            this.Name = "ProgramForm";
            this.Text = "GSM Auto Backup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox filePathListBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button manualRunButton;
        private System.Windows.Forms.Button destinationButton;
        private System.Windows.Forms.Timer backupTimer;
    }
}