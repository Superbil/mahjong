namespace Mahjong.Forms
{
    partial class ChatServerForm
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
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.displayTextBox = new System.Windows.Forms.TextBox();
            this.createbutton = new System.Windows.Forms.Button();
            this.IPtextBox = new System.Windows.Forms.TextBox();
            this.connectbutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.IPlabel = new System.Windows.Forms.Label();
            this.nametextBox = new System.Windows.Forms.TextBox();
            this.namelabel = new System.Windows.Forms.Label();
            this.lanButton = new System.Windows.Forms.RadioButton();
            this.IPButton = new System.Windows.Forms.RadioButton();
            this.startbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(89, 36);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.ReadOnly = true;
            this.inputTextBox.Size = new System.Drawing.Size(267, 22);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
            // 
            // displayTextBox
            // 
            this.displayTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.displayTextBox.Location = new System.Drawing.Point(89, 64);
            this.displayTextBox.Multiline = true;
            this.displayTextBox.Name = "displayTextBox";
            this.displayTextBox.ReadOnly = true;
            this.displayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.displayTextBox.Size = new System.Drawing.Size(267, 144);
            this.displayTextBox.TabIndex = 1;
            this.displayTextBox.TextChanged += new System.EventHandler(this.displayTextBox_TextChanged);
            // 
            // createbutton
            // 
            this.createbutton.Location = new System.Drawing.Point(16, 90);
            this.createbutton.Name = "createbutton";
            this.createbutton.Size = new System.Drawing.Size(49, 24);
            this.createbutton.TabIndex = 2;
            this.createbutton.Text = "建立";
            this.createbutton.UseVisualStyleBackColor = true;
            this.createbutton.Click += new System.EventHandler(this.createbutton_Click);
            // 
            // IPtextBox
            // 
            this.IPtextBox.Enabled = false;
            this.IPtextBox.Location = new System.Drawing.Point(229, 8);
            this.IPtextBox.Name = "IPtextBox";
            this.IPtextBox.Size = new System.Drawing.Size(127, 22);
            this.IPtextBox.TabIndex = 3;
            // 
            // connectbutton
            // 
            this.connectbutton.Location = new System.Drawing.Point(16, 120);
            this.connectbutton.Name = "connectbutton";
            this.connectbutton.Size = new System.Drawing.Size(49, 24);
            this.connectbutton.TabIndex = 4;
            this.connectbutton.Text = "連線";
            this.connectbutton.UseVisualStyleBackColor = true;
            this.connectbutton.Click += new System.EventHandler(this.connectbutton_Click);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(16, 180);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(49, 24);
            this.cancelbutton.TabIndex = 5;
            this.cancelbutton.Text = "隱藏";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // IPlabel
            // 
            this.IPlabel.AutoSize = true;
            this.IPlabel.Location = new System.Drawing.Point(176, 11);
            this.IPlabel.Name = "IPlabel";
            this.IPlabel.Size = new System.Drawing.Size(51, 12);
            this.IPlabel.TabIndex = 6;
            this.IPlabel.Text = "IP位置：";
            // 
            // nametextBox
            // 
            this.nametextBox.Location = new System.Drawing.Point(71, 8);
            this.nametextBox.Name = "nametextBox";
            this.nametextBox.Size = new System.Drawing.Size(100, 22);
            this.nametextBox.TabIndex = 7;
            // 
            // namelabel
            // 
            this.namelabel.AutoSize = true;
            this.namelabel.Location = new System.Drawing.Point(13, 15);
            this.namelabel.Name = "namelabel";
            this.namelabel.Size = new System.Drawing.Size(52, 12);
            this.namelabel.TabIndex = 8;
            this.namelabel.Text = "ID(名稱):";
            // 
            // lanButton
            // 
            this.lanButton.AutoSize = true;
            this.lanButton.Checked = true;
            this.lanButton.Location = new System.Drawing.Point(12, 37);
            this.lanButton.Name = "lanButton";
            this.lanButton.Size = new System.Drawing.Size(71, 16);
            this.lanButton.TabIndex = 9;
            this.lanButton.TabStop = true;
            this.lanButton.Text = "區域網路";
            this.lanButton.UseVisualStyleBackColor = true;
            this.lanButton.CheckedChanged += new System.EventHandler(this.lanButton_CheckedChanged);
            // 
            // IPButton
            // 
            this.IPButton.AutoSize = true;
            this.IPButton.Location = new System.Drawing.Point(12, 64);
            this.IPButton.Name = "IPButton";
            this.IPButton.Size = new System.Drawing.Size(71, 16);
            this.IPButton.TabIndex = 10;
            this.IPButton.Text = "網際網路";
            this.IPButton.UseVisualStyleBackColor = true;
            this.IPButton.CheckedChanged += new System.EventHandler(this.IPButton_CheckedChanged);
            // 
            // startbutton
            // 
            this.startbutton.Location = new System.Drawing.Point(16, 150);
            this.startbutton.Name = "startbutton";
            this.startbutton.Size = new System.Drawing.Size(49, 24);
            this.startbutton.TabIndex = 11;
            this.startbutton.Text = "開始";
            this.startbutton.UseVisualStyleBackColor = true;
            this.startbutton.Click += new System.EventHandler(this.playgame_Click);
            // 
            // ChatServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 215);
            this.Controls.Add(this.startbutton);
            this.Controls.Add(this.IPButton);
            this.Controls.Add(this.lanButton);
            this.Controls.Add(this.namelabel);
            this.Controls.Add(this.nametextBox);
            this.Controls.Add(this.IPlabel);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.connectbutton);
            this.Controls.Add(this.IPtextBox);
            this.Controls.Add(this.createbutton);
            this.Controls.Add(this.displayTextBox);
            this.Controls.Add(this.inputTextBox);
            this.Name = "ChatServerForm";
            this.Text = "Chat Server";
            this.Load += new System.EventHandler(this.ChatServerForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatServerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox displayTextBox;
        private System.Windows.Forms.Button createbutton;
        private System.Windows.Forms.TextBox IPtextBox;
        private System.Windows.Forms.Button connectbutton;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.Label IPlabel;
        private System.Windows.Forms.TextBox nametextBox;
        private System.Windows.Forms.Label namelabel;
        private System.Windows.Forms.RadioButton lanButton;
        private System.Windows.Forms.RadioButton IPButton;
        public System.Windows.Forms.Button startbutton;
    }

}