namespace Mahjong.Forms
{
    partial class Tally
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
            this.Textlable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labeltally = new System.Windows.Forms.Label();
            this.player1 = new System.Windows.Forms.Label();
            this.player2 = new System.Windows.Forms.Label();
            this.player3 = new System.Windows.Forms.Label();
            this.player4 = new System.Windows.Forms.Label();
            this.sum1 = new System.Windows.Forms.Label();
            this.sum2 = new System.Windows.Forms.Label();
            this.sum3 = new System.Windows.Forms.Label();
            this.sum4 = new System.Windows.Forms.Label();
            this.score1 = new System.Windows.Forms.Label();
            this.score2 = new System.Windows.Forms.Label();
            this.score3 = new System.Windows.Forms.Label();
            this.score4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Textlable
            // 
            this.Textlable.AutoSize = true;
            this.Textlable.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Textlable.Location = new System.Drawing.Point(12, 9);
            this.Textlable.Name = "Textlable";
            this.Textlable.Size = new System.Drawing.Size(66, 19);
            this.Textlable.TabIndex = 0;
            this.Textlable.Text = "玩家：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "累計分數：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "本局得分：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(12, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "總計：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 189);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(219, 142);
            this.textBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(241, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "合計：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(335, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "台";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(397, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 39);
            this.button1.TabIndex = 7;
            this.button1.Text = "確定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labeltally
            // 
            this.labeltally.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labeltally.Location = new System.Drawing.Point(299, 198);
            this.labeltally.Name = "labeltally";
            this.labeltally.Size = new System.Drawing.Size(39, 19);
            this.labeltally.TabIndex = 8;
            // 
            // player1
            // 
            this.player1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.player1.Location = new System.Drawing.Point(116, 9);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(89, 23);
            this.player1.TabIndex = 9;
            // 
            // player2
            // 
            this.player2.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.player2.Location = new System.Drawing.Point(211, 9);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(100, 23);
            this.player2.TabIndex = 10;
            // 
            // player3
            // 
            this.player3.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.player3.Location = new System.Drawing.Point(317, 9);
            this.player3.Name = "player3";
            this.player3.Size = new System.Drawing.Size(100, 23);
            this.player3.TabIndex = 11;
            // 
            // player4
            // 
            this.player4.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.player4.Location = new System.Drawing.Point(424, 9);
            this.player4.Name = "player4";
            this.player4.Size = new System.Drawing.Size(100, 23);
            this.player4.TabIndex = 12;
            // 
            // sum1
            // 
            this.sum1.Location = new System.Drawing.Point(122, 45);
            this.sum1.Name = "sum1";
            this.sum1.Size = new System.Drawing.Size(69, 23);
            this.sum1.TabIndex = 13;
            // 
            // sum2
            // 
            this.sum2.Location = new System.Drawing.Point(211, 45);
            this.sum2.Name = "sum2";
            this.sum2.Size = new System.Drawing.Size(69, 23);
            this.sum2.TabIndex = 14;
            // 
            // sum3
            // 
            this.sum3.Location = new System.Drawing.Point(317, 45);
            this.sum3.Name = "sum3";
            this.sum3.Size = new System.Drawing.Size(69, 23);
            this.sum3.TabIndex = 15;
            // 
            // sum4
            // 
            this.sum4.Location = new System.Drawing.Point(424, 45);
            this.sum4.Name = "sum4";
            this.sum4.Size = new System.Drawing.Size(69, 23);
            this.sum4.TabIndex = 16;
            // 
            // score1
            // 
            this.score1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.score1.Location = new System.Drawing.Point(122, 75);
            this.score1.Name = "score1";
            this.score1.Size = new System.Drawing.Size(69, 23);
            this.score1.TabIndex = 17;
            // 
            // score2
            // 
            this.score2.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.score2.Location = new System.Drawing.Point(211, 75);
            this.score2.Name = "score2";
            this.score2.Size = new System.Drawing.Size(69, 23);
            this.score2.TabIndex = 18;
            // 
            // score3
            // 
            this.score3.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.score3.Location = new System.Drawing.Point(317, 75);
            this.score3.Name = "score3";
            this.score3.Size = new System.Drawing.Size(69, 23);
            this.score3.TabIndex = 19;
            // 
            // score4
            // 
            this.score4.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.score4.Location = new System.Drawing.Point(424, 75);
            this.score4.Name = "score4";
            this.score4.Size = new System.Drawing.Size(69, 23);
            this.score4.TabIndex = 20;
            // 
            // Tally
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 350);
            this.Controls.Add(this.score4);
            this.Controls.Add(this.score3);
            this.Controls.Add(this.score2);
            this.Controls.Add(this.score1);
            this.Controls.Add(this.sum4);
            this.Controls.Add(this.sum3);
            this.Controls.Add(this.sum2);
            this.Controls.Add(this.sum1);
            this.Controls.Add(this.player4);
            this.Controls.Add(this.player3);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.player1);
            this.Controls.Add(this.labeltally);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Textlable);
            this.Name = "Tally";
            this.Text = "麻將計分板";
            this.Load += new System.EventHandler(this.Tally_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Textlable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labeltally;
        private System.Windows.Forms.Label player1;
        private System.Windows.Forms.Label player2;
        private System.Windows.Forms.Label player3;
        private System.Windows.Forms.Label player4;
        private System.Windows.Forms.Label sum1;
        private System.Windows.Forms.Label sum2;
        private System.Windows.Forms.Label sum3;
        private System.Windows.Forms.Label sum4;
        private System.Windows.Forms.Label score1;
        private System.Windows.Forms.Label score2;
        private System.Windows.Forms.Label score3;
        private System.Windows.Forms.Label score4;
    }
}