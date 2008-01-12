namespace Mahjong.Forms
{
    partial class Table
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新遊戲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.網路ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.開新遊戲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.關於ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.結束ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新遊戲ToolStripMenuItem,
            this.網路ToolStripMenuItem1,
            this.設定ToolStripMenuItem1,
            this.關於ToolStripMenuItem1,
            this.toolStripSeparator1,
            this.結束ToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 120);
            // 
            // 新遊戲ToolStripMenuItem
            // 
            this.新遊戲ToolStripMenuItem.Name = "新遊戲ToolStripMenuItem";
            this.新遊戲ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新遊戲ToolStripMenuItem.Text = "新遊戲";
            this.新遊戲ToolStripMenuItem.Click += new System.EventHandler(this.新遊戲ToolStripMenuItem_Click);
            // 
            // 網路ToolStripMenuItem1
            // 
            this.網路ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開新遊戲ToolStripMenuItem});
            this.網路ToolStripMenuItem1.Name = "網路ToolStripMenuItem1";
            this.網路ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.網路ToolStripMenuItem1.Text = "網路遊戲";
            // 
            // 開新遊戲ToolStripMenuItem
            // 
            this.開新遊戲ToolStripMenuItem.Name = "開新遊戲ToolStripMenuItem";
            this.開新遊戲ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.開新遊戲ToolStripMenuItem.Text = "開新遊戲...";
            this.開新遊戲ToolStripMenuItem.Click += new System.EventHandler(this.開新遊戲ToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem1
            // 
            this.設定ToolStripMenuItem1.Name = "設定ToolStripMenuItem1";
            this.設定ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.設定ToolStripMenuItem1.Text = "設定";
            this.設定ToolStripMenuItem1.Click += new System.EventHandler(this.設定ToolStripMenuItem1_Click);
            // 
            // 關於ToolStripMenuItem1
            // 
            this.關於ToolStripMenuItem1.Name = "關於ToolStripMenuItem1";
            this.關於ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.關於ToolStripMenuItem1.Text = "關於";
            this.關於ToolStripMenuItem1.Click += new System.EventHandler(this.關於ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // 結束ToolStripMenuItem1
            // 
            this.結束ToolStripMenuItem1.Name = "結束ToolStripMenuItem1";
            this.結束ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.結束ToolStripMenuItem1.Text = "結束";
            this.結束ToolStripMenuItem1.Click += new System.EventHandler(this.結束ToolStripMenuItem1_Click);
            // 
            // Table
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(692, 670);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "Table";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "十六張麻將遊戲";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 新遊戲ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 網路ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 開新遊戲ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 關於ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 結束ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

        
    }
}

