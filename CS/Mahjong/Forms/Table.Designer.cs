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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Table));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新遊戲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.網路ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.開新遊戲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.關於ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.結束ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新遊戲ToolStripMenuItem,
            this.網路ToolStripMenuItem1,
            this.設定ToolStripMenuItem1,
            this.關於ToolStripMenuItem1,
            this.toolStripSeparator1,
            this.結束ToolStripMenuItem1});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(153, 142);
            // 
            // 新遊戲ToolStripMenuItem
            // 
            this.新遊戲ToolStripMenuItem.Name = "新遊戲ToolStripMenuItem";
            this.新遊戲ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新遊戲ToolStripMenuItem.Text = global::Mahjong.Properties.Settings.Default.NewGame;
            this.新遊戲ToolStripMenuItem.Click += new System.EventHandler(this.新遊戲ToolStripMenuItem_Click);
            // 
            // 網路ToolStripMenuItem1
            // 
            this.網路ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開新遊戲ToolStripMenuItem});
            this.網路ToolStripMenuItem1.Name = "網路ToolStripMenuItem1";
            this.網路ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.網路ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.OnlineGame;
            // 
            // 開新遊戲ToolStripMenuItem
            // 
            this.開新遊戲ToolStripMenuItem.Name = "開新遊戲ToolStripMenuItem";
            this.開新遊戲ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.開新遊戲ToolStripMenuItem.Text = global::Mahjong.Properties.Settings.Default.NewOnlineGame;
            this.開新遊戲ToolStripMenuItem.Click += new System.EventHandler(this.開新遊戲ToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem1
            // 
            this.設定ToolStripMenuItem1.Name = "設定ToolStripMenuItem1";
            this.設定ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.設定ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.Config;
            this.設定ToolStripMenuItem1.Click += new System.EventHandler(this.設定ToolStripMenuItem1_Click);
            // 
            // 關於ToolStripMenuItem1
            // 
            this.關於ToolStripMenuItem1.Name = "關於ToolStripMenuItem1";
            this.關於ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.關於ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.About;
            this.關於ToolStripMenuItem1.Click += new System.EventHandler(this.關於ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 結束ToolStripMenuItem1
            // 
            this.結束ToolStripMenuItem1.Name = "結束ToolStripMenuItem1";
            this.結束ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.結束ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.Exit;
            this.結束ToolStripMenuItem1.Click += new System.EventHandler(this.結束ToolStripMenuItem1_Click);
            // 
            // Table
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(692, 680);
            this.ContextMenuStrip = this.contextMenu;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Table";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = global::Mahjong.Properties.Settings.Default.Title;
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem 新遊戲ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 網路ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 開新遊戲ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 關於ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 結束ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

        
    }
}

