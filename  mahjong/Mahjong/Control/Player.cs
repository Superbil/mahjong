using System;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Mahjong.Control;
using System.Runtime.Serialization.Formatters.Binary;
using Mahjong.Forms;

namespace Mahjong.Control
{
    public class NetPlayer
    {
        internal Socket connection; // Socket for accepting a connection  
        private NetworkStream socketStream; // network data stream 
        public ChatServerForm server; // reference to server 
        private BinaryWriter writer; // facilitates writing to the stream    
        private BinaryReader reader; // facilitates reading from the stream 
        public const string AllPlayers_Head = "/allplaysize:";
        General g2;        
        string theReply = "";
        public string number; // player number                                
        //private string mark; // player mark on the board     

        internal NetPlayer(Socket socket, ChatServerForm serverValue, int newNumber)
        {

            connection = socket;
            server = serverValue;
            number = newNumber + ".player";

            // create NetworkStream object for Socket      
            socketStream = new NetworkStream(connection);

            // create Streams for reading/writing bytes
            writer = new BinaryWriter(socketStream);
            reader = new BinaryReader(socketStream);



        } // end constructor       

        public object getObjectWithByteArray(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;

            return bf1.Deserialize(ms);
        }
        /// <summary>
        /// Server 端的接收
        /// </summary>
        /// <param name="s"></param>
        private void stringcheck(string s)
        {
            byte[] allplayer;
            if (s.Contains(AllPlayers_Head))
            {
                allplayer = reader.ReadBytes(int.Parse(s.Remove(0, AllPlayers_Head.Length)));
                g2 = (General)getObjectWithByteArray(allplayer);

                //server.PC.all = (AllPlayers)getObjectWithByteArray(allplayer);

                MessageBox.Show(g2.Name,"Server");

                server.servermessage(AllPlayers_Head + allplayer.Length.ToString());
                server.serverobject(allplayer);
            }
            else if (s.Contains("："))
            {
                theReply = s;
            }
            else if (s.Contains("."))
            {
                string[] sss = s.Split('.');
                server.name[int.Parse(sss[0])] = sss[2];
                MessageBox.Show(server.name[int.Parse(sss[0])]);
            }


        }

        internal void Run()
        {
            while (true)
            {
                server.DisplayMessage("Waiting for connection");
                // accept an incoming connection     

                server.DisplayMessage(number + " Connection " + " received.");

                // inform client that connection was successfull
                writer.Write(number);
                writer.Write("SERVER>>> Connection successful");

                //server.DisableInput(false); // enable inputTextBox




                // loop until server signals termination
                do
                {
                    // Step 3: processing phase
                    try
                    {
                        // read message from server        
                        stringcheck(reader.ReadString());
                        // display the message
                        server.DisplayMessage(theReply);
                        server.servermessage(theReply);

                    } // end try
                    catch (Exception)
                    {
                        // handle exception if error reading data
                        break;
                    } // end catch

                } while (connection.Connected);

                server.DisplayMessage("\r\nUser terminated connection\r\n");

                // Step 5: close connection  
                writer.Close();
                reader.Close();
                socketStream.Close();
                connection.Close();

                server.DisableInput(true); // disable InputTextBox

            }
        }




    }
}
