namespace Mahjong.Forms
{
    partial class InputName
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.label_E = new System.Windows.Forms.Label();
            this.label_N = new System.Windows.Forms.Label();
            this.label_W = new System.Windows.Forms.Label();
            this.label_S = new System.Windows.Forms.Label();
            this.textBox_E = new System.Windows.Forms.TextBox();
            this.textBox_N = new System.Windows.Forms.TextBox();
            this.textBox_W = new System.Windows.Forms.TextBox();
            this.textBox_S = new System.Windows.Forms.TextBox();
            this.button_Ok = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_E
            // 
            this.label_E.AutoSize = true;
            this.label_E.Location = new System.Drawing.Point(12, 9);
            this.label_E.Name = "label_E";
            this.label_E.Size = new System.Drawing.Size(17, 12);
            this.label_E.TabIndex = 0;
            this.label_E.Text = global::Mahjong.Properties.Settings.Default.East;
            // 
            // label_N
            // 
            this.label_N.AutoSize = true;
            this.label_N.Location = new System.Drawing.Point(12, 38);
            this.label_N.Name = "label_N";
            this.label_N.Size = new System.Drawing.Size(17, 12);
            this.label_N.TabIndex = 1;
            this.label_N.Text = global::Mahjong.Properties.Settings.Default.Nouth;
            // 
            // label_W
            // 
            this.label_W.AutoSize = true;
            this.label_W.Location = new System.Drawing.Point(12, 67);
            this.label_W.Name = "label_W";
            this.label_W.Size = new System.Drawing.Size(17, 12);
            this.label_W.TabIndex = 2;
            this.label_W.Text = global::Mahjong.Properties.Settings.Default.West;
            // 
            // label_S
            // 
            this.label_S.AutoSize = true;
            this.label_S.Location = new System.Drawing.Point(12, 98);
            this.label_S.Name = "label_S";
            this.label_S.Size = new System.Drawing.Size(17, 12);
            this.label_S.TabIndex = 3;
            this.label_S.Text = global::Mahjong.Properties.Settings.Default.South;
            // 
            // textBox_E
            // 
            this.textBox_E.Location = new System.Drawing.Point(35, 6);
            this.textBox_E.Name = "textBox_E";
            this.textBox_E.Size = new System.Drawing.Size(100, 22);
            this.textBox_E.TabIndex = 4;
            // 
            // textBox_N
            // 
            this.textBox_N.Location = new System.Drawing.Point(35, 34);
            this.textBox_N.Name = "textBox_N";
            this.textBox_N.Size = new System.Drawing.Size(100, 22);
            this.textBox_N.TabIndex = 5;
            // 
            // textBox_W
            // 
            this.textBox_W.Location = new System.Drawing.Point(35, 63);
            this.textBox_W.Name = "textBox_W";
            this.textBox_W.Size = new System.Drawing.Size(100, 22);
            this.textBox_W.TabIndex = 6;
            // 
            // textBox_S
            // 
            this.textBox_S.Location = new System.Drawing.Point(35, 95);
            this.textBox_S.Name = "textBox_S";
            this.textBox_S.Size = new System.Drawing.Size(100, 22);
            this.textBox_S.TabIndex = 7;
            // 
            // button_Ok
            // 
            this.button_Ok.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Mahjong.Properties.Settings.Default, "Button_Ok", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_Ok.Location = new System.Drawing.Point(141, 6);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(75, 23);
            this.button_Ok.TabIndex = 8;
            this.button_Ok.Text = global::Mahjong.Properties.Settings.Default.Button_Ok;
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(141, 32);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 9;
            this.button_Cancel.Text = global::Mahjong.Properties.Settings.Default.Button_Cancel;
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = global::Mahjong.Properties.Settings.Default.ThisisPlayer;
            // 
            // InputName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 124);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Ok);
            this.Controls.Add(this.textBox_S);
            this.Controls.Add(this.textBox_W);
            this.Controls.Add(this.textBox_N);
            this.Controls.Add(this.textBox_E);
            this.Controls.Add(this.label_S);
            this.Controls.Add(this.label_W);
            this.Controls.Add(this.label_N);
            this.Controls.Add(this.label_E);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Mahjong.Properties.Settings.Default, "PlayerNameConfigTitle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = global::Mahjong.Properties.Settings.Default.PlayerNameConfigTitle;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_E;
        private System.Windows.Forms.Label label_N;
        private System.Windows.Forms.Label label_W;
        private System.Windows.Forms.Label label_S;
        private System.Windows.Forms.TextBox textBox_E;
        private System.Windows.Forms.TextBox textBox_N;
        private System.Windows.Forms.TextBox textBox_W;
        private System.Windows.Forms.TextBox textBox_S;
        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label5;
    }
}