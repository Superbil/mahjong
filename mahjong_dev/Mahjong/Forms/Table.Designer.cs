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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Exit_ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.GameMenu_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Newgame_ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Savegame_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Loadgame_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlineGameMenu_ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlineGame_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetupMenu_ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowMessageBox_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.About_ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.GameSpeed_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Slow = new System.Windows.Forms.ToolStripMenuItem();
            this.Normal = new System.Windows.Forms.ToolStripMenuItem();
            this.Quick = new System.Windows.Forms.ToolStripMenuItem();
            this.PlaySound_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameMenu_ToolStripMenuItem,
            this.OnlineGameMenu_ToolStripMenuItem1,
            this.SetupMenu_ToolStripMenuItem1,
            this.About_ToolStripMenuItem1,
            this.toolStripSeparator1,
            this.Exit_ToolStripMenuItem1});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(119, 120);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // Exit_ToolStripMenuItem1
            // 
            this.Exit_ToolStripMenuItem1.Name = "Exit_ToolStripMenuItem1";
            this.Exit_ToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.Exit_ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.Exit;
            this.Exit_ToolStripMenuItem1.Click += new System.EventHandler(this.結束ToolStripMenuItem1_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(204, 140);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(75, 23);
            this.axWindowsMediaPlayer1.TabIndex = 1;
            this.axWindowsMediaPlayer1.Visible = false;
            // 
            // GameMenu_ToolStripMenuItem
            // 
            this.GameMenu_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Newgame_ToolStripMenuItem1,
            this.Savegame_ToolStripMenuItem,
            this.Loadgame_ToolStripMenuItem});
            this.GameMenu_ToolStripMenuItem.Name = "GameMenu_ToolStripMenuItem";
            this.GameMenu_ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.GameMenu_ToolStripMenuItem.Text = global::Mahjong.Properties.Settings.Default.GameMenu;
            // 
            // Newgame_ToolStripMenuItem1
            // 
            this.Newgame_ToolStripMenuItem1.Name = "Newgame_ToolStripMenuItem1";
            this.Newgame_ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.Newgame_ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.NewGame;
            this.Newgame_ToolStripMenuItem1.Click += new System.EventHandler(this.新遊戲ToolStripMenuItem_Click);
            // 
            // Savegame_ToolStripMenuItem
            // 
            this.Savegame_ToolStripMenuItem.Name = "Savegame_ToolStripMenuItem";
            this.Savegame_ToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.Savegame_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.Savegame_ToolStripMenuItem.Text = global::Mahjong.Properties.Settings.Default.SaveGame;
            this.Savegame_ToolStripMenuItem.Click += new System.EventHandler(this.儲存牌局ToolStripMenuItem_Click);
            // 
            // Loadgame_ToolStripMenuItem
            // 
            this.Loadgame_ToolStripMenuItem.Name = "Loadgame_ToolStripMenuItem";
            this.Loadgame_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.Loadgame_ToolStripMenuItem.Text = global::Mahjong.Properties.Settings.Default.LoadGame;
            this.Loadgame_ToolStripMenuItem.Click += new System.EventHandler(this.讀取牌局ToolStripMenuItem_Click);
            // 
            // OnlineGameMenu_ToolStripMenuItem1
            // 
            this.OnlineGameMenu_ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OnlineGame_ToolStripMenuItem});
            this.OnlineGameMenu_ToolStripMenuItem1.Name = "OnlineGameMenu_ToolStripMenuItem1";
            this.OnlineGameMenu_ToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.OnlineGameMenu_ToolStripMenuItem1.Tag = "";
            this.OnlineGameMenu_ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.OnlineGame;
            // 
            // OnlineGame_ToolStripMenuItem
            // 
            this.OnlineGame_ToolStripMenuItem.Name = "OnlineGame_ToolStripMenuItem";
            this.OnlineGame_ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.OnlineGame_ToolStripMenuItem.Text = global::Mahjong.Properties.Settings.Default.NewOnlineGame;
            this.OnlineGame_ToolStripMenuItem.Click += new System.EventHandler(this.開新遊戲ToolStripMenuItem_Click);
            // 
            // SetupMenu_ToolStripMenuItem1
            // 
            this.SetupMenu_ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowMessageBox_Menu,
            this.GameSpeed_ToolStripMenuItem,
            this.PlaySound_Menu});
            this.SetupMenu_ToolStripMenuItem1.Name = "SetupMenu_ToolStripMenuItem1";
            this.SetupMenu_ToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.SetupMenu_ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.Config;
            this.SetupMenu_ToolStripMenuItem1.Click += new System.EventHandler(this.設定ToolStripMenuItem1_Click);
            // 
            // ShowMessageBox_Menu
            // 
            this.ShowMessageBox_Menu.Checked = true;
            this.ShowMessageBox_Menu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowMessageBox_Menu.Name = "ShowMessageBox_Menu";
            this.ShowMessageBox_Menu.Size = new System.Drawing.Size(152, 22);
            this.ShowMessageBox_Menu.Text = global::Mahjong.Properties.Settings.Default.ShowMessageBox;
            this.ShowMessageBox_Menu.Click += new System.EventHandler(this.ShowMessageBox_Menu_Click);
            // 
            // About_ToolStripMenuItem1
            // 
            this.About_ToolStripMenuItem1.Name = "About_ToolStripMenuItem1";
            this.About_ToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.About_ToolStripMenuItem1.Text = global::Mahjong.Properties.Settings.Default.About;
            this.About_ToolStripMenuItem1.Click += new System.EventHandler(this.關於ToolStripMenuItem1_Click);
            // 
            // GameSpeed_ToolStripMenuItem
            // 
            this.GameSpeed_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Slow,
            this.Normal,
            this.Quick});
            this.GameSpeed_ToolStripMenuItem.Name = "GameSpeed_ToolStripMenuItem";
            this.GameSpeed_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.GameSpeed_ToolStripMenuItem.Text = global::Mahjong.Properties.Settings.Default.RunRoundTime_Menu;
            // 
            // Slow
            // 
            this.Slow.Name = "Slow";
            this.Slow.Size = new System.Drawing.Size(152, 22);
            this.Slow.Text = global::Mahjong.Properties.Settings.Default.RunRoundTime_Slow_Text;
            this.Slow.Click += new System.EventHandler(this.非常慢ToolStripMenuItem_Click);
            // 
            // Normal
            // 
            this.Normal.Checked = true;
            this.Normal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Normal.Name = "Normal";
            this.Normal.Size = new System.Drawing.Size(152, 22);
            this.Normal.Text = global::Mahjong.Properties.Settings.Default.RunRoundTime_Normal_Text;
            this.Normal.Click += new System.EventHandler(this.正常ToolStripMenuItem_Click);
            // 
            // Quick
            // 
            this.Quick.Name = "Quick";
            this.Quick.Size = new System.Drawing.Size(152, 22);
            this.Quick.Text = global::Mahjong.Properties.Settings.Default.RunRoundTime_Quick_Text;
            this.Quick.Click += new System.EventHandler(this.非常快ToolStripMenuItem_Click);
            // 
            // PlaySound_Menu
            // 
            this.PlaySound_Menu.Checked = true;
            this.PlaySound_Menu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PlaySound_Menu.Name = "PlaySound_Menu";
            this.PlaySound_Menu.Size = new System.Drawing.Size(152, 22);
            this.PlaySound_Menu.Text = global::Mahjong.Properties.Settings.Default.PlaySound_Menu;
            this.PlaySound_Menu.Click += new System.EventHandler(this.PlaySound_Menu_Click);
            // 
            // Table
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(662, 640);
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Table";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = global::Mahjong.Properties.Settings.Default.Title;
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem GameMenu_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OnlineGameMenu_ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem OnlineGame_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetupMenu_ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem About_ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem Exit_ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripMenuItem ShowMessageBox_Menu;
        private System.Windows.Forms.ToolStripMenuItem GameSpeed_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Slow;
        private System.Windows.Forms.ToolStripMenuItem Normal;
        private System.Windows.Forms.ToolStripMenuItem Quick;
        private System.Windows.Forms.ToolStripMenuItem Savegame_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Loadgame_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Newgame_ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem PlaySound_Menu;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;

        
    }
}

