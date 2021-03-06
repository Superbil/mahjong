using System;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Mahjong.Control;
using Mahjong.Brands;
using System.Runtime.Serialization.Formatters.Binary;
using Mahjong.Players;

namespace Mahjong.Forms
{
    public delegate void newGameHandler(object sender, EventArgs e);
    public delegate void allPlayerHandler(object sender, EventArgs e);
    public delegate void brandHandler(object sender, EventArgs e);
    public delegate void CheckHandler(object sender, EventArgs e);
    public delegate void CheckResultHandler(object sender, EventArgs e);
    public delegate void BrandplayersHandler(object sender, EventArgs e);
    public delegate void BrandplayerHandler(object sender, EventArgs e);
    public partial class ChatServerForm : Form
    {
        public ChatServerForm()
        {
            InitializeComponent();
        } // end constructor
        private NetPlayer[] players;
        private Thread getPlayers;
        private TcpListener listener;
        private NetworkStream socketStream; // network data stream 
        private BinaryWriter writer; // facilitates writing to the stream    
        private BinaryReader reader; // facilitates reading from the stream 
        //private Socket connection; // Socket for accepting a connection      
        private Thread[] playerThreads; // Thread for processing incoming messages
        private string temp;
        //
        private NetworkStream output; // stream for receiving data           
        private IPAddress serverIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        private string message = "";

        private Thread outputThread; // Thread for receiving data from server
        private TcpClient connection; // client to establish connection  

        private NetworkStream stream; // network data stream                 
        private int n = 0;
        private bool ckeckplayer = false;
        private string myMark; // player's mark on the board    
        private string myMarkname;
        public int Port = 50000;
        public const string AllPlayers_Head = "/allplaysize:";
        public const string newgameround = "/newgame";
        public const string Brand_Head = "/brand:";
        public const string Check_Head = "/check:";
        public const string BrandplayerArray_Head = "/brandplayerarray:";
        public const string Brandplayer_Head = "/brandplayer:";
        // initialize thread for reading
        //General g2;
        internal PC_Network PC;
        internal String[] name = new string[4];
        internal bool disconnected = false;
        
        public event newGameHandler getNewGame;
        public event allPlayerHandler getAllPlayer;
        public event brandHandler getBrand;
        public event CheckHandler getCheck;
        public event BrandplayersHandler getBrandplayers;
        public event BrandplayerHandler getBrandplayer;
        
        public ChatServerForm(int port)
        {
            this.Port = port;
        }
        public string ChatName
        {
            get
            {
                return myMarkname;
            }
        }
        public int HowMuchPlayer
        {
            get
            {
                return n;
            }
        }
        public string Mark
        {
            get
            {
                return myMark;
            }
        }

        public byte[] getByteArrayWithObject(AllPlayers all)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, all);
            return ms.ToArray();
        }
        public byte[] getByteArrayWithObject(BrandPlayer[] players)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, players);
            return ms.ToArray();
        }
        public byte[] getByteArrayWithObject(BrandPlayer players)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, players);
            return ms.ToArray();
        }
        public byte[] getByteArrayWithObject(Brand brand)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, brand);
            return ms.ToArray();
        }
        public byte[] getByteArrayWithObject(CheckUser check)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, check);
            return ms.ToArray();
        }

        public object getObjectWithByteArray(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;

            return bf1.Deserialize(ms);
        }
/*
        public static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }
        */
        private void ChatServerForm_Load(object sender, EventArgs e)
        {
            if (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length < 2)
                IPButton.Enabled = false;
            else if (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length < 1)
                lanButton.Enabled = false;


            /*   players = new Player[3];
               playerThreads = new Thread[3];
       
               // accept connections on a different thread         
               getPlayers = new Thread(new ThreadStart(SetUp));
               getPlayers.Start();
            */
        } // end method CharServerForm_Load

        public void ChatServer()
        {
            players = new NetPlayer[3];
            playerThreads = new Thread[3];
            
            // accept connections on a different thread         
            getPlayers = new Thread(new ThreadStart(SetUp));
            getPlayers.Start();
        }
        public void ChatClient()
        {
            ckeckplayer = true;

            //IPtextBox.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            connection = new TcpClient(IPtextBox.Text, Port);

            stream = connection.GetStream();

            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);

            outputThread = new Thread(new ThreadStart(Run));
            outputThread.Start();

        }
        // close all threads associated with this application
        private void ChatServerForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        } // end method CharServerForm_FormClosing

        // delegate that allows method DisplayMessage to be called
        // in the thread that creates and maintains the GUI       
        private delegate void DisplayDelegate(string message);

        // method DisplayMessage sets displayTextBox's Text property
        // in a thread-safe manner
        internal void DisplayMessage(string message)
        {
            //           string tempp = message;
            if (ckeckplayer == true)
            {
                myMarkname = nametextBox.Text;
                myMark = message;
                // setup Mark and Name
                writer.Write(myMark + "." + myMarkname);
                ckeckplayer = false;

            }
            // if modifying displayTextBox is not thread safe
            if (displayTextBox.InvokeRequired)
            {
                // use inherited method Invoke to execute DisplayMessage
                // via a delegate                                       
                Invoke(new DisplayDelegate(DisplayMessage), new object[] { message });

            }// end if
            else if (temp != message)// OK to modify displayTextBox in current thread
            {
                displayTextBox.AppendText("\r\n" + message);
                temp = message;

            }// end if

        } // end method DisplayMessage

        // delegate that allows method DisableInput to be called 
        // in the thread that creates and maintains the GUI
        private delegate void DisableInputDelegate(bool value);

        // method DisableInput sets inputTextBox's ReadOnly property
        // in a thread-safe manner
        internal void DisableInput(bool value)
        {
            // if modifying inputTextBox is not thread safe
            if (inputTextBox.InvokeRequired)
            {
                // use inherited method Invoke to execute DisableInput
                // via a delegate                                     
                Invoke(new DisableInputDelegate(DisableInput),
                   new object[] { value });
            } // end if
            else // OK to modify inputTextBox in current thread
                inputTextBox.ReadOnly = value;
        } // end method DisableInput
        internal void servermessage(string reply)
        {
            for (int i = 0; i < n; i++)
            {
                socketStream = new NetworkStream(players[i].connection);

                // create objects for transferring data across stream
                writer = new BinaryWriter(socketStream);
                reader = new BinaryReader(socketStream);
                writer.Write(reply);
            }
        }
        internal void serverobject(byte[] date)
        {
            for (int i = 0; i < n; i++)
            {
                socketStream = new NetworkStream(players[i].connection);

                // create objects for transferring data across stream
                writer = new BinaryWriter(socketStream);
                reader = new BinaryReader(socketStream);
                writer.Write(date);
            }
        }
        // send the text typed at the server to the client
        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // send the text to the client
            try
            {
                if (myMark == "Server" && e.KeyCode == Keys.Enter && inputTextBox.ReadOnly == false)
                {
                    displayTextBox.AppendText("\r\n" + myMarkname + "�G" + inputTextBox.Text);
                    temp = myMarkname + "�G" + inputTextBox.Text;

                    for (int i = 0; i < n; i++)
                    {
                        socketStream = new NetworkStream(players[i].connection);

                        // create objects for transferring data across stream
                        writer = new BinaryWriter(socketStream);
                        reader = new BinaryReader(socketStream);
                        writer.Write(myMarkname + "�G" + inputTextBox.Text);

                    }
                    // if the user at the server signaled termination
                    // sever the connection to the client

                    inputTextBox.Clear(); // clear the user�� input
                } // end if
                else if (e.KeyCode == Keys.Enter && inputTextBox.ReadOnly == false)
                {
                    writer.Write(myMarkname + "�G" + inputTextBox.Text);
                    displayTextBox.AppendText("\r\n" + myMarkname + "�G" + inputTextBox.Text);
                    temp = myMarkname + "�G" + inputTextBox.Text;
                    inputTextBox.Clear();
                } // end if
            } // end try
            catch (SocketException)
            {
                displayTextBox.Text += "\nError writing object";
            } // end catch
        } // end method inputTextBox_KeyDown

        // allows a client to connect; displays text the client sends


        public void SetUp()
        {
            DisplayMessage("Waiting for players...");

            // set up Socket                                           
            listener = new TcpListener(serverIP, Port);
            listener.Start();
            // accept first player and start a player thread

            players[0] = new NetPlayer(listener.AcceptSocket(), this, 1);
            playerThreads[0] =
               new Thread(new ThreadStart(players[0].Run));
            playerThreads[0].Start();

            if (listener.Server.Connected == false)
                n++;
            else
                n--;
            lock (players[0])
            {
                players[0].threadSuspended = false;
                Monitor.Pulse(players[0]);
            } // end lock

            // accept second player and start another player thread       
            players[1] = new NetPlayer(listener.AcceptSocket(), this, 2);
            playerThreads[1] =
               new Thread(new ThreadStart(players[1].Run));
            playerThreads[1].Start();

            if (listener.Server.Connected == false)
                n++;
            else
                n--;

            lock (players[1])
            {
                players[1].threadSuspended = false;
                Monitor.Pulse(players[1]);
            } // end lock

            players[2] = new NetPlayer(listener.AcceptSocket(), this, 3);
            playerThreads[2] =
               new Thread(new ThreadStart(players[2].Run));
            playerThreads[2].Start();

            // let the first player know that the other player has connected
            if (listener.Server.Connected == false)
                n++;
            else
                n--;

            lock (players[2])
            {
                players[2].threadSuspended = false;
                Monitor.Pulse(players[2]);
            } // end lock

        }


        private void displayTextBox_TextChanged(object sender, EventArgs e)
        {
            //displayTextBox.ScrollBars = displayTextBox.SelectionLength;
        }

        private void createbutton_Click(object sender, EventArgs e)
        {
            myMarkname = nametextBox.Text;
            myMark = "Server";
            nametextBox.ReadOnly = true;
            IPtextBox.ReadOnly = true;
            name[0] = nametextBox.Text;
            connectbutton.Enabled = false;
            createbutton.Enabled = false;
            Text = Text + " - ���A����";
            DisableInput(false);
            ChatServer();
        }

        private void connectbutton_Click(object sender, EventArgs e)
        {
            Text = Text + " - �s�u��";
            createbutton.Enabled = false;
            startbutton.Enabled = false;
            connectbutton.Enabled = false;
            if (myMark != "Server")
            {
                nametextBox.ReadOnly = true;
                IPtextBox.ReadOnly = true;
                ChatClient();
            }
        } // end method SetUp
        /// <summary>
        /// Client 
        /// </summary>
        public void Run()
        {

            // Step 2: get NetworkStream associated with TcpClient
            output = connection.GetStream();

            // create objects for writing and reading across stream
            writer = new BinaryWriter(output);
            reader = new BinaryReader(output);

            DisableInput(false); // enable inputTextBox            

            // loop until server signals termination
            do
            {
                // Step 3: processing phase
                try
                {
                    // read message from server                       
                    stringcheck(reader.ReadString());

                    DisplayMessage(/*"\r\n" +*/ message);

                } // end try
                catch (IOException)
                {
                    break;
                    // handle exception if error in reading server data
                    //System.Environment.Exit(System.Environment.ExitCode);
                } // end catch
            } while (connection.Connected);

            // Step 4: close connection
            writer.Close();
            reader.Close();
            output.Close();
            connection.Close();
            outputThread.Abort();
            getPlayers.Abort();
        }
        /// <summary>
        /// Client �ݪ�����
        /// </summary>
        /// <param name="s">�r��</param>
        private void stringcheck(string s)
        {
            byte[] allplayer;

            if (s.Contains(newgameround))
            {
                if (myMark != "Server")
                {
                    //MessageBox.Show("Call "+myMark+"Run");
                    getNewGame(this, null);
                }
            }
            else if (s.Contains(Check_Head))
            {
                allplayer = reader.ReadBytes(int.Parse(s.Remove(0, Check_Head.Length)));
                getCheck(getObjectWithByteArray(allplayer), null);
            }
            else if (s.Contains(Brand_Head))
            {
                allplayer = reader.ReadBytes(int.Parse(s.Remove(0, Brand_Head.Length)));
                getBrand(getObjectWithByteArray(allplayer), null);

            }
            else if (s.Contains(AllPlayers_Head))
            {
                allplayer = reader.ReadBytes(int.Parse(s.Remove(0, AllPlayers_Head.Length)));
                //g2 = (General)getObjectWithByteArray(allplayer);
                // clinet ������allplayer���]�w

                //PC.all = (AllPlayers)getObjectWithByteArray(allplayer);

                getAllPlayer(getObjectWithByteArray(allplayer), null);

                //MessageBox.Show(g2.Name,myMark);
                //MessageBox.Show(PC.all.Name[PC.all.state],myMark);
            }
            else if (s.Contains(BrandplayerArray_Head))
            {
                allplayer = reader.ReadBytes(int.Parse(s.Remove(0, BrandplayerArray_Head.Length)));
                getBrandplayers(getObjectWithByteArray(allplayer), null);
            }
            else if (s.Contains(Brandplayer_Head))
            {
                allplayer = reader.ReadBytes(int.Parse(s.Remove(0, Brandplayer_Head.Length)));
                getBrandplayer(getObjectWithByteArray(allplayer), null);
            }
            else
                message = s;
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lanButton_CheckedChanged(object sender, EventArgs e)
        {
            if (lanButton.Checked == true && Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length > 0)
            {
                serverIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
                IPtextBox.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
                IPtextBox.Enabled = false;
            }
        }

        private void IPButton_CheckedChanged(object sender, EventArgs e)
        {
            if (IPButton.Checked == true && Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length > 1)
            {
                serverIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1];
                IPtextBox.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
                IPtextBox.Enabled = true;
            }

        }

        private void playgame_Click(object sender, EventArgs e)
        {
            createbutton.Enabled = false;
            startbutton.Enabled = false;
            
            try
            {
                for (int i = 0; i < n; i++)
                {
                    socketStream = new NetworkStream(players[i].connection);

                    // create objects for transferring data across stream
                    writer = new BinaryWriter(socketStream);
                    reader = new BinaryReader(socketStream);
                    writer.Write(newgameround);
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("!");
            }
            
        } // end method RunClient


        internal void SendObject(AllPlayers all)
        {
            try
            {
                if (myMark == "Server")
                {
                    for (int i = 0; i < n; i++)
                    {
                        socketStream = new NetworkStream(players[i].connection);

                        writer = new BinaryWriter(socketStream);
                        reader = new BinaryReader(socketStream);
                        writer.Write(AllPlayers_Head + getByteArrayWithObject(all).Length.ToString());
                        writer.Write(getByteArrayWithObject(all));
                    }

                }
                else
                {
                    writer.Write(AllPlayers_Head + getByteArrayWithObject(all).Length.ToString());
                    writer.Write(getByteArrayWithObject(all));
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("���f�]�w���~�I");
            }
        }
        internal void SendObject(Brand bd)
        {
            try
            {
                if (myMark == "Server")
                {
                    for (int i = 0; i < n; i++)
                    {
                        socketStream = new NetworkStream(players[i].connection);

                        writer = new BinaryWriter(socketStream);
                        reader = new BinaryReader(socketStream);
                        writer.Write(Brand_Head + getByteArrayWithObject(bd).Length.ToString());
                        writer.Write(getByteArrayWithObject(bd));
                    }

                }
                else
                {
                    writer.Write(Brand_Head + getByteArrayWithObject(bd).Length.ToString());
                    writer.Write(getByteArrayWithObject(bd));
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("���f�]�w���~�I");
            }
        }

        internal void SendObject(CheckUser cu)
        {
            try
            {
                if (myMark == "Server")
                {
                    for (int i = 0; i < n; i++)
                    {
                        socketStream = new NetworkStream(players[i].connection);

                        writer = new BinaryWriter(socketStream);
                        reader = new BinaryReader(socketStream);
                        writer.Write(Check_Head + getByteArrayWithObject(cu).Length.ToString());
                        writer.Write(getByteArrayWithObject(cu));
                    }

                }
                else
                {
                    writer.Write(Check_Head + getByteArrayWithObject(cu).Length.ToString());
                    writer.Write(getByteArrayWithObject(cu));
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("���f�]�w���~�I");
            }
        }
        internal void SendObject(BrandPlayer[] brandplayers)
        {
            try
            {
                if (myMark == "Server")
                {
                    for (int i = 0; i < n; i++)
                    {
                        socketStream = new NetworkStream(players[i].connection);

                        writer = new BinaryWriter(socketStream);
                        reader = new BinaryReader(socketStream);
                        writer.Write(BrandplayerArray_Head + getByteArrayWithObject(brandplayers).Length.ToString());
                        writer.Write(getByteArrayWithObject(brandplayers));
                    }

                }
                else
                {
                    writer.Write(BrandplayerArray_Head + getByteArrayWithObject(brandplayers).Length.ToString());
                    writer.Write(getByteArrayWithObject(brandplayers));
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("���f�]�w���~�I");
            }
        }
        internal void SendObject(BrandPlayer brandplayer)
        {
            try
            {
                if (myMark == "Server")
                {
                    for (int i = 0; i < n; i++)
                    {
                        socketStream = new NetworkStream(players[i].connection);

                        writer = new BinaryWriter(socketStream);
                        reader = new BinaryReader(socketStream);
                        writer.Write(Brandplayer_Head + getByteArrayWithObject(brandplayer).Length.ToString());
                        writer.Write(getByteArrayWithObject(brandplayer));
                    }

                }
                else
                {
                    writer.Write(Brandplayer_Head + getByteArrayWithObject(brandplayer).Length.ToString());
                    writer.Write(getByteArrayWithObject(brandplayer));
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("���f�]�w���~�I");
            }
        }
        internal void SendObject(int bi)
        {
            try
            {
                if (myMark == "Server")
                {
                    for (int i = 0; i < n; i++)
                    {
                        socketStream = new NetworkStream(players[i].connection);

                        writer = new BinaryWriter(socketStream);
                        reader = new BinaryReader(socketStream);
                        writer.Write(BrandplayerArray_Head);
                        writer.Write(bi);
                        
                    }

                }
                else
                {
                    writer.Write(BrandplayerArray_Head);
                    writer.Write(bi);
               
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("���f�]�w���~�I");
            }
        }
        protected void Server_Closing(object sender, FormClosedEventArgs e)
        {
            disconnected = true;
            System.Environment.Exit(System.Environment.ExitCode);
        }
        public bool GameOver()
        {
            return false;
        }
       
    } // end class ChatServerForm



}
