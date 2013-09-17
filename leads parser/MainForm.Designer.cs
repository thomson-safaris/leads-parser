namespace leads_parser
{
    partial class MainForm
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
            this.parser_completion_status_box = new System.Windows.Forms.Label();
            this.safarisLeadsButton = new System.Windows.Forms.Button();
            this.treksLeadsButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // parser_completion_status_box
            // 
            this.parser_completion_status_box.AutoSize = true;
            this.parser_completion_status_box.Location = new System.Drawing.Point(13, 120);
            this.parser_completion_status_box.Name = "parser_completion_status_box";
            this.parser_completion_status_box.Size = new System.Drawing.Size(0, 13);
            this.parser_completion_status_box.TabIndex = 0;
            // 
            // safarisLeadsButton
            // 
            this.safarisLeadsButton.Location = new System.Drawing.Point(16, 13);
            this.safarisLeadsButton.Name = "safarisLeadsButton";
            this.safarisLeadsButton.Size = new System.Drawing.Size(256, 27);
            this.safarisLeadsButton.TabIndex = 1;
            this.safarisLeadsButton.Text = "Parse thomsonsafaris.com leads";
            this.safarisLeadsButton.UseVisualStyleBackColor = true;
            // 
            // treksLeadsButton
            // 
            this.treksLeadsButton.Location = new System.Drawing.Point(16, 46);
            this.treksLeadsButton.Name = "treksLeadsButton";
            this.treksLeadsButton.Size = new System.Drawing.Size(256, 27);
            this.treksLeadsButton.TabIndex = 2;
            this.treksLeadsButton.Text = "Parse thomsontreks.com leads";
            this.treksLeadsButton.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(13, 85);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(195, 13);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.Text = "Click a button for glorious parsing action";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.treksLeadsButton);
            this.Controls.Add(this.safarisLeadsButton);
            this.Controls.Add(this.parser_completion_status_box);
            this.Name = "MainForm";
            this.Text = "Leads Parser";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label parser_completion_status_box;
        private System.Windows.Forms.Button safarisLeadsButton;
        private System.Windows.Forms.Button treksLeadsButton;
        private System.Windows.Forms.Label statusLabel;

    }
}

