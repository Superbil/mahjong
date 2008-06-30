// Fig. 23.1: ChatServer.cs
// Set up a server that will receive a connection from a client, send a
// string to the client, chat with the client and close the connection.
using System;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Mahjong.Control;
using System.Runtime.Serialization.Formatters.Binary;



namespace Mahjong.Forms
{
    public partial class ChatServerForm : Form
    {
        public ChatServerForm()
        {
            InitializeComponent();
        } // end constructor
        private Player[] players;
        private Thread getPlayers, getPlayersobject;
        private TcpListener listener, listenerobject;
        private NetworkStream socketStream; // network data stream 
        private BinaryWriter writer, writerobject; // facilitates writing to the stream    
        private BinaryReader reader, readerobject; // facilitates reading from the stream 
        //private Socket connection; // Socket for accepting a connection      
        private Thread[] playerThreads; // Thread for processing incoming messages
        private string temp;
        //
        private NetworkStream output, outputobject; // stream for receiving data           
        private IPAddress serverIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        private string message = "";

        private Thread outputThread, outputThreadobject; // Thread for receiving data from server
        private TcpClient connection, connectionobject; // client to establish connection  

        private NetworkStream stream, streamobject; // network data stream                 
        private int i = 0, n = 0;
        private string myMark; // player's mark on the board    
        private string myMarkname;
        public int Port = 50000;
        private const string Allplayer_Head = "ALLPLAYERS:";
        public int size;
        // initialize thread for reading

        public ChatServerForm(int port)
        {
            this.Port = port;
        }

        public byte[] getByteArrayWithObject(AllPlayers g1)
        {

           // General g1 = new General("¼B§Ó«T", "c:\\liu.bmp",60, 99, 80, 95, true, true, false);


            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, g1);
            return ms.ToArray();
        }

        public object getObjectWithByteArray(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;

            return bf1.Deserialize(ms);
        }



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

        private void ChatServerForm_Load(object sender, EventArgs e)
        {
            if (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length < 2)
                IPButton.Enabled = false;
            else if (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length < 1)
                lanButton.Enabled = false;
            displayTextBox.AppendText(getByteArrayWithObject(this.all).Length.ToString());

            /*   players = new Player[3];
               playerThreads = new Thread[3];
       
               // accept connections on a different thread         
               getPlayers = new Thread(new ThreadStart(SetUp));
               getPlayers.Start();
            */
        } // end method CharServerForm_Load
        public void ChatServer()
        {
            players = new Player[6];
            playerThreads = new Thread[6];

            // accept connections on a different thread         
            getPlayers = new Thread(new ThreadStart(SetUp));
            getPlayers.Start();

            getPlayersobject = new Thread(new ThreadStart(SetUpObject));
            getPlayersobject.Start();


        }
        public void ChatClient()
        {
            i = 1;

            IPtextBox.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            connection = new TcpClient(IPtextBox.Text, Port);
            connectionobject = new TcpClient(IPtextBox.Text, 50001);
            stream = connection.GetStream();
            streamobject = connectionobject.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
            writerobject = new BinaryWriter(streamobject);
            readerobject = new BinaryReader(streamobject);
            outputThread = new Thread(new ThreadStart(Run));
            outputThread.Start();
            outputThreadobject = new Thread(new ThreadStart(RunObject));
            outputThreadobject.Start();
        }
        // close all threads associated with this application
        private void ChatServerForm_FormClosing(object sender,
           FormClosingEventArgs e)
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
            if (i == 1)
            {
                myMarkname = nametextBox.Text;
                myMark = message;
                i--;
            }
            // if modifying displayTextBox is not thread safe


            
           
            if (displayTextBox.InvokeRequired)
            {
                // use inherited method Invoke to execute DisplayMessage
                // via a delegate                                       
                Invoke(new DisplayDelegate(DisplayMessage),new object[] { message });
            }// end if
            else if (message.Contains(Allplayer_Head))
            {
                size = int.Parse(message.Remove(0, Allplayer_Head.Length));
            }
            else if (temp != message)// OK to modify displayTextBox in current thread
            {
                displayTextBox.AppendText("\r\n" + message);
                temp = message;

            }

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


            for (int i = 0; i < n; i += 2)
            {
                socketStream = new NetworkStream(players[i].connection);

                // create objects for transferring data across stream
                writer = new BinaryWriter(socketStream);
                reader = new BinaryReader(socketStream);
                writer.Write(reply);

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
                    displayTextBox.AppendText("\r\n" + myMarkname + "¡G" + inputTextBox.Text);
                    temp = myMarkname + "¡G" + inputTextBox.Text;

                    for (int i = 0; i < n; i += 2)
                    {
                        socketStream = new NetworkStream(players[i].connection);

                        // create objects for transferring data across stream
                        writer = new BinaryWriter(socketStream);
                        reader = new BinaryReader(socketStream);
                        writer.Write(myMarkname + "¡G" + inputTextBox.Text);

                    }
                    // if the user at the server signaled termination
                    // sever the connection to the client

                    inputTextBox.Clear(); // clear the userŠö input
                } // end if
                else if (e.KeyCode == Keys.Enter && inputTextBox.ReadOnly == false)
                {
                    writer.Write(myMarkname + "¡G" + inputTextBox.Text);
                    displayTextBox.AppendText("\r\n" + myMarkname + "¡G" + inputTextBox.Text);
                    temp = myMarkname + "¡G" + inputTextBox.Text;
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
            listener =
                 new TcpListener(serverIP, Port);
            listener.Start();
            // accept first player and start a player thread

            players[0] = new Player(listener.AcceptSocket(), this, 0);
            playerThreads[0] =
               new Thread(new ThreadStart(players[0].Run));
            playerThreads[0].Start();

            if (listener.Server.Connected == false)
                n += 2;
            else
                n -= 2;

            // accept second player and start another player thread       
            players[2] = new Player(listener.AcceptSocket(), this, 2);
            playerThreads[2] =
               new Thread(new ThreadStart(players[2].Run));
            playerThreads[2].Start();

            if (listener.Server.Connected == false)
                n += 2;
            else
                n -= 2;

            players[4] = new Player(listener.AcceptSocket(), this, 4);
            playerThreads[4] =
               new Thread(new ThreadStart(players[4].Run));
            playerThreads[4].Start();

            // let the first player know that the other player has connected
            if (listener.Server.Connected == false)
                n += 2;
            else
                n -= 2;

        }

        public void SetUpObject()
        {


            // set up Socket                                           
            listenerobject =
                 new TcpListener(serverIP, 50001);
            listenerobject.Start();
            // accept first player and start a player thread

            players[1] = new Player(listenerobject.AcceptSocket(), this, 1);
            playerThreads[1] =
               new Thread(new ThreadStart(players[1].RunObject));
            playerThreads[1].Start();



            // accept second player and start another player thread       
            players[3] = new Player(listenerobject.AcceptSocket(), this, 3);
            playerThreads[3] =
               new Thread(new ThreadStart(players[3].RunObject));
            playerThreads[3].Start();



            players[5] = new Player(listenerobject.AcceptSocket(), this, 5);
            playerThreads[5] =
               new Thread(new ThreadStart(players[5].RunObject));
            playerThreads[5].Start();

            // let the first player know that the other player has connected


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
            ChatServer();
        }

        private void connectbutton_Click(object sender, EventArgs e)
        {
            if (myMark == "Server")
            {
                DisableInput(false);
                pc.newgame();
            }
            else
            {
                nametextBox.ReadOnly = true;
                IPtextBox.ReadOnly = true;
                ChatClient();
            }
        } // end method SetUp
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
                    message = reader.ReadString();
                    DisplayMessage(/*"\r\n" +*/ message);
                } // end try
                catch (Exception)
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

        public void RunObject()
        {

            // Step 2: get NetworkStream associated with TcpClient
            outputobject = connectionobject.GetStream();

            // create objects for writing and reading across stream
            writerobject = new BinaryWriter(outputobject);
            readerobject = new BinaryReader(outputobject);



            byte[] allplayer;
            AllPlayers g2;
            // Step 4: read string data sent from client
            do
            {
                try
                {
                    // read the string sent to the server
                    allplayer = readerobject.ReadBytes(size);
                    g2 = (AllPlayers)getObjectWithByteArray(allplayer);
                    this.pc.all = g2;
                    //MessageBox.Show(size.ToString());
                } // end try
                catch (Exception)
                {
                    break;
                    // handle exception if error in reading server data
                    //System.Environment.Exit(System.Environment.ExitCode);
                } // end catch
            } while (connectionobject.Connected);

            // Step 4: close connection
            writerobject.Close();
            readerobject.Close();
            outputobject.Close();
            connectionobject.Close();
            outputThreadobject.Abort();
            getPlayersobject.Abort();


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

        private void button1_Click(object sender, EventArgs e)
        {
            //socketStream = new NetworkStream(players[1].connection);
            //writerobject = new BinaryWriter(socketStream);
            //writerobject.Write(getByteArrayWithObject(all));
            SendAllPlayer(all);
        } // end method RunClient


        AllPlayers all;
        internal void  SendAllPlayer(AllPlayers all)
        {
            this.all = all;
            if (myMark == "Server")
            {
                for (int i = 0; i < n; i += 2)
                {
                    socketStream = new NetworkStream(players[i].connection);
                    writer = new BinaryWriter(socketStream);
                    writer.Write(Allplayer_Head +size.ToString());
                }
                for (int i = 1; i < n; i += 2)
                {
                    socketStream = new NetworkStream(players[i].connection);
                    writerobject = new BinaryWriter(socketStream);
                    writerobject.Write(getByteArrayWithObject(all));
                }
            }
            else
            {
                
                    writer.Write(Allplayer_Head+size.ToString());

                    writerobject.Write(getByteArrayWithObject(all));
                
            }

        }
        internal AllPlayers AllPlayer
        {
            set
            {
                this.all = value;
                size = getByteArrayWithObject(this.all).Length;                
            }
            get
            {
                return this.all;
            }
        }
        ProgramControl pc;
        internal ProgramControl PC
        {
            set
            {
                pc = value;
            }
            get
            {
                return pc;
            }
        }


    } // end class ChatServerForm

    public class Player
    {
        internal Socket connection; // Socket for accepting a connection  
        private NetworkStream socketStream; // network data stream 
        private ChatServerForm server; // reference to server 
        private BinaryWriter writer; // facilitates writing to the stream    
        private BinaryReader reader; // facilitates reading from the stream 

        private string number; // player number                                
        //private string mark; // playerŠö mark on the board     
        public int size12;
        public string theReply = "";
        private const string Allplayer_Head = "ALLPLAYERS:";

        internal Player(Socket socket, ChatServerForm serverValue, int newNumber)
        {

            connection = socket;
            server = serverValue;
            number = newNumber + "player";

            // create NetworkStream object for Socket      
            socketStream = new NetworkStream(connection);

            // create Streams for reading/writing bytes
            writer = new BinaryWriter(socketStream);
            reader = new BinaryReader(socketStream);
            int size12;
        } // end constructor       

        

        public object getObjectWithByteArray(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;

            return bf1.Deserialize(ms);
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

                

                // Step 4: read string data sent from client
                do
                {
                    try
                    {
                        // read the string sent to the server
                        theReply = reader.ReadString();
                        
                        // display the message
                        server.DisplayMessage(theReply);
                        server.servermessage(theReply);
                        getobjectsize();

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

        public void getobjectsize()
        {
           if (theReply.Contains(Allplayer_Head))
           {
              this.server.size = int.Parse(theReply.Remove(0, Allplayer_Head.Length));
               //size12 = int.Parse(theReply);
           }

        }
        internal void RunObject()
        {
            while (true)
            {


                byte[] allplayer;
                AllPlayers g2;
                // Step 4: read string data sent from client
                do
                {
                    try
                    {
                        // read the string sent to the server
                        allplayer = reader.ReadBytes(this.server.size);
                        g2 = (AllPlayers)getObjectWithByteArray(allplayer);
                        MessageBox.Show(this.server.size.ToString());

                    } // end try
                    catch (IOException)
                    {
                        // handle exception if error reading data
                        //MessageBox.Show("Error");
                        break;
                    } // end catch

                } while (connection.Connected);



                // Step 5: close connection  
                writer.Close();
                reader.Close();
                socketStream.Close();
                connection.Close();



            }
        }

    }

}