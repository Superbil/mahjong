using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Mahjong.Control
{
    public partial class ProgramControl
    {
        private BinaryFormatter formatter = new BinaryFormatter();
        private FileStream output, input;
        public void loadgame()
        {
            OpenFileDialog o = new OpenFileDialog();
            o.InitialDirectory = ".";
            o.Filter = "麻將存檔 (*.mahjong)|*.mahjong|所有檔案 (*.*)|*.*";
            o.ShowDialog();
            try
            {
                //設定檔案流
                input = new FileStream(o.FileName, FileMode.Open, FileAccess.ReadWrite);
                //讀檔並且解序列化                
                all = (AllPlayers)formatter.Deserialize(input);
                //關閉檔案
                input.Close();
                //更新設定畫面
                table.clearAll();
                table.Setup(all);
                table.addImage();
                setInforamtion();
            }
            catch
            {
                MessageBox.Show("開啟檔案錯誤！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public virtual void savegame()
        {
            SaveFileDialog s = new SaveFileDialog();
            s.InitialDirectory = ".";
            s.Filter = "麻將存檔 (*.mahjong)|*.mahjong|所有檔案 (*.*)|*.*";
            s.ShowDialog();
            try
            {
                output = new FileStream(s.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                formatter.Serialize(output, all);
                output.Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("檔案名稱錯誤！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
