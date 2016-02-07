namespace Google_Scholar_Crawler
{
    partial class SettingsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.applySettingButton = new System.Windows.Forms.Button();
            this.numRecordsTextBox = new System.Windows.Forms.TextBox();
            this.numPagesTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.applySettingButton);
            this.panel1.Controls.Add(this.numRecordsTextBox);
            this.panel1.Controls.Add(this.numPagesTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 139);
            this.panel1.TabIndex = 0;
            // 
            // applySettingButton
            // 
            this.applySettingButton.Location = new System.Drawing.Point(248, 104);
            this.applySettingButton.Name = "applySettingButton";
            this.applySettingButton.Size = new System.Drawing.Size(75, 23);
            this.applySettingButton.TabIndex = 4;
            this.applySettingButton.Text = "Apply";
            this.applySettingButton.UseVisualStyleBackColor = true;
            this.applySettingButton.Click += new System.EventHandler(this.applySettingButton_Click);
            // 
            // numRecordsTextBox
            // 
            this.numRecordsTextBox.Location = new System.Drawing.Point(223, 61);
            this.numRecordsTextBox.Name = "numRecordsTextBox";
            this.numRecordsTextBox.Size = new System.Drawing.Size(100, 20);
            this.numRecordsTextBox.TabIndex = 3;
            // 
            // numPagesTextBox
            // 
            this.numPagesTextBox.Location = new System.Drawing.Point(223, 24);
            this.numPagesTextBox.Name = "numPagesTextBox";
            this.numPagesTextBox.Size = new System.Drawing.Size(100, 20);
            this.numPagesTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of records per file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of following search result pages";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(22, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Set 50000 for default value";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 139);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button applySettingButton;
        private System.Windows.Forms.TextBox numRecordsTextBox;
        private System.Windows.Forms.TextBox numPagesTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}