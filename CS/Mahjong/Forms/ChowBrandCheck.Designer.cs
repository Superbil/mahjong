namespace Mahjong.Forms
{
    partial class ChowBrandCheck
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
            this.flowLayout1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayout2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayout3 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayout1
            // 
            this.flowLayout1.AutoSize = true;
            this.flowLayout1.Location = new System.Drawing.Point(12, 12);
            this.flowLayout1.Name = "flowLayout1";
            this.flowLayout1.Size = new System.Drawing.Size(132, 60);
            this.flowLayout1.TabIndex = 0;
            this.flowLayout1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayout1_Paint);
            // 
            // flowLayout2
            // 
            this.flowLayout2.AutoSize = true;
            this.flowLayout2.Location = new System.Drawing.Point(12, 78);
            this.flowLayout2.Name = "flowLayout2";
            this.flowLayout2.Size = new System.Drawing.Size(132, 60);
            this.flowLayout2.TabIndex = 0;
            this.flowLayout2.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayout2_Paint);
            // 
            // flowLayout3
            // 
            this.flowLayout3.AutoSize = true;
            this.flowLayout3.Location = new System.Drawing.Point(12, 144);
            this.flowLayout3.Name = "flowLayout3";
            this.flowLayout3.Size = new System.Drawing.Size(132, 60);
            this.flowLayout3.TabIndex = 0;
            this.flowLayout3.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayout3_Paint);
            // 
            // ChowBrandCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(156, 218);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayout3);
            this.Controls.Add(this.flowLayout2);
            this.Controls.Add(this.flowLayout1);
            this.Name = "ChowBrandCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = global::Mahjong.Properties.Settings.Default.ChowBrandCheck;
            this.Load += new System.EventHandler(this.ChowBrandCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayout1;
        private System.Windows.Forms.FlowLayoutPanel flowLayout2;
        private System.Windows.Forms.FlowLayoutPanel flowLayout3;
    }
}