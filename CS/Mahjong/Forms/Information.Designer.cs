namespace Mahjong.Forms
{
    partial class Information
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
            this.Up_label = new System.Windows.Forms.Label();
            this.Down_label = new System.Windows.Forms.Label();
            this.Left_label = new System.Windows.Forms.Label();
            this.Right_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Left_money = new System.Windows.Forms.Label();
            this.Right_money = new System.Windows.Forms.Label();
            this.Down_money = new System.Windows.Forms.Label();
            this.Up_money = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Up_label
            // 
            this.Up_label.AutoSize = true;
            this.Up_label.Location = new System.Drawing.Point(74, 9);
            this.Up_label.Name = "Up_label";
            this.Up_label.Size = new System.Drawing.Size(40, 12);
            this.Up_label.TabIndex = 0;
            this.Up_label.Text = "Player1";
            // 
            // Down_label
            // 
            this.Down_label.AutoSize = true;
            this.Down_label.Location = new System.Drawing.Point(74, 105);
            this.Down_label.Name = "Down_label";
            this.Down_label.Size = new System.Drawing.Size(40, 12);
            this.Down_label.TabIndex = 1;
            this.Down_label.Text = "Player3";
            // 
            // Left_label
            // 
            this.Left_label.AutoSize = true;
            this.Left_label.Location = new System.Drawing.Point(2, 61);
            this.Left_label.Name = "Left_label";
            this.Left_label.Size = new System.Drawing.Size(40, 12);
            this.Left_label.TabIndex = 1;
            this.Left_label.Text = "Player4";
            // 
            // Right_label
            // 
            this.Right_label.AutoSize = true;
            this.Right_label.Location = new System.Drawing.Point(140, 61);
            this.Right_label.Name = "Right_label";
            this.Right_label.Size = new System.Drawing.Size(40, 12);
            this.Right_label.TabIndex = 1;
            this.Right_label.Text = "Player2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PaleGreen;
            this.panel1.Controls.Add(this.Left_money);
            this.panel1.Controls.Add(this.Right_money);
            this.panel1.Controls.Add(this.Down_money);
            this.panel1.Controls.Add(this.Up_money);
            this.panel1.Location = new System.Drawing.Point(62, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(72, 72);
            this.panel1.TabIndex = 2;
            // 
            // Left_money
            // 
            this.Left_money.AutoSize = true;
            this.Left_money.Location = new System.Drawing.Point(3, 31);
            this.Left_money.Name = "Left_money";
            this.Left_money.Size = new System.Drawing.Size(11, 12);
            this.Left_money.TabIndex = 3;
            this.Left_money.Text = "0";
            // 
            // Right_money
            // 
            this.Right_money.AutoSize = true;
            this.Right_money.Location = new System.Drawing.Point(41, 31);
            this.Right_money.Name = "Right_money";
            this.Right_money.Size = new System.Drawing.Size(11, 12);
            this.Right_money.TabIndex = 3;
            this.Right_money.Text = "0";
            // 
            // Down_money
            // 
            this.Down_money.AutoSize = true;
            this.Down_money.Location = new System.Drawing.Point(28, 60);
            this.Down_money.Name = "Down_money";
            this.Down_money.Size = new System.Drawing.Size(11, 12);
            this.Down_money.TabIndex = 3;
            this.Down_money.Text = "0";
            // 
            // Up_money
            // 
            this.Up_money.AutoSize = true;
            this.Up_money.Location = new System.Drawing.Point(28, 0);
            this.Up_money.Name = "Up_money";
            this.Up_money.Size = new System.Drawing.Size(11, 12);
            this.Up_money.TabIndex = 3;
            this.Up_money.Text = "0";
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 124);
            this.ControlBox = false;
            this.Controls.Add(this.Down_label);
            this.Controls.Add(this.Up_label);
            this.Controls.Add(this.Right_label);
            this.Controls.Add(this.Left_label);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Information";
            this.ShowIcon = false;
            this.Text = "Information";
            this.Load += new System.EventHandler(this.Information_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Up_label;
        private System.Windows.Forms.Label Down_label;
        private System.Windows.Forms.Label Left_label;
        private System.Windows.Forms.Label Right_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Left_money;
        private System.Windows.Forms.Label Right_money;
        private System.Windows.Forms.Label Down_money;
        private System.Windows.Forms.Label Up_money;
    }
}