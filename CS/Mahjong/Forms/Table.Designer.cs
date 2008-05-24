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
            this.ShowMessageBox_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.速度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Slow = new System.Windows.Forms.ToolStripMenuItem();
            this.Normal = new System.Windows.Forms.ToolStripMenuItem();
            this.Quick = new System.Windows.Forms.ToolStripMenuItem();
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
            this.新遊戲ToolStripMenuItem.Text = "新遊戲";
            this.新遊戲ToolStripMenuItem.Click += new System.EventHandler(this.新遊戲ToolStripMenuItem_Click);
            // 
            // 網路ToolStripMenuItem1
            // 
            this.網路ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開新遊戲ToolStripMenuItem});
            this.網路ToolStripMenuItem1.Name = "網路ToolStripMenuItem1";
            this.網路ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.網路ToolStripMenuItem1.Text = "網路遊戲";
            // 
            // 開新遊戲ToolStripMenuItem
            // 
            this.開新遊戲ToolStripMenuItem.Name = "開新遊戲ToolStripMenuItem";
            this.開新遊戲ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.開新遊戲ToolStripMenuItem.Text = "新增/加入網路遊戲";
            this.開新遊戲ToolStripMenuItem.Click += new System.EventHandler(this.開新遊戲ToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem1
            // 
            this.設定ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowMessageBox_Menu,
            this.速度ToolStripMenuItem});
            this.設定ToolStripMenuItem1.Name = "設定ToolStripMenuItem1";
            this.設定ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.設定ToolStripMenuItem1.Text = "設定";
            this.設定ToolStripMenuItem1.Click += new System.EventHandler(this.設定ToolStripMenuItem1_Click);
            // 
            // ShowMessageBox_Menu
            // 
            this.ShowMessageBox_Menu.Checked = true;
            this.ShowMessageBox_Menu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowMessageBox_Menu.Name = "ShowMessageBox_Menu";
            this.ShowMessageBox_Menu.Size = new System.Drawing.Size(152, 22);
            this.ShowMessageBox_Menu.Text = "顯示提示";
            this.ShowMessageBox_Menu.Click += new System.EventHandler(this.ShowMessageBox_Menu_Click);
            // 
            // 速度ToolStripMenuItem
            // 
            this.速度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slow,
            this.Normal,
            this.Quick});
            this.速度ToolStripMenuItem.Name = "速度ToolStripMenuItem";
            this.速度ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.速度ToolStripMenuItem.Text = "速度";
            // 
            // Slow
            // 
            this.Slow.Name = "Slow";
            this.Slow.Size = new System.Drawing.Size(110, 22);
            this.Slow.Text = "慢速";
            this.Slow.Click += new System.EventHandler(this.非常慢ToolStripMenuItem_Click);
            // 
            // Normal
            // 
            this.Normal.Checked = true;
            this.Normal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Normal.Name = "Normal";
            this.Normal.Size = new System.Drawing.Size(110, 22);
            this.Normal.Text = "正常速";
            this.Normal.Click += new System.EventHandler(this.正常ToolStripMenuItem_Click);
            // 
            // Quick
            // 
            this.Quick.Name = "Quick";
            this.Quick.Size = new System.Drawing.Size(110, 22);
            this.Quick.Text = "超快速";
            this.Quick.Click += new System.EventHandler(this.非常快ToolStripMenuItem_Click);
            // 
            // 關於ToolStripMenuItem1
            // 
            this.關於ToolStripMenuItem1.Name = "關於ToolStripMenuItem1";
            this.關於ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.關於ToolStripMenuItem1.Text = "關於";
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
            this.結束ToolStripMenuItem1.Text = "結束";
            this.結束ToolStripMenuItem1.Click += new System.EventHandler(this.結束ToolStripMenuItem1_Click);
            // 
            // Table
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(662, 640);
            this.ContextMenuStrip = this.contextMenu;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Table";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "十六張麻將遊戲";
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
        private System.Windows.Forms.ToolStripMenuItem ShowMessageBox_Menu;
        private System.Windows.Forms.ToolStripMenuItem 速度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Slow;
        private System.Windows.Forms.ToolStripMenuItem Normal;
        private System.Windows.Forms.ToolStripMenuItem Quick;

        
    }
}

