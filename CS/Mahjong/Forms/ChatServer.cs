// Fig. 23.1: ChatServer.cs
// Set up a server that will receive a connection from a client, send a
// string to the client, chat with the client and close the connection.
using System;
using System.Windows.Forms;
using System.Threading;  
using System.Net;        
using System.Net.Sockets;
using System.IO;

namespace Mahjong.Forms
{
    public partial class ChatServerForm : Form
    {
        public ChatServerForm()
        {
            InitializeComponent();
            this.Show();
        } // end constructor
        private Player[] players;
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
        private int i = 0, n = 0;
        private string myMark; // player's mark on the board    
        private string myMarkname;
        // initialize thread for reading

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
        private void ChatServer()
        {
            players = new Player[3];
            playerThreads = new Thread[3];

            // accept connections on a different thread         
            getPlayers = new Thread(new ThreadStart(SetUp));
            getPlayers.Start();

        }
        private void ChatClient()
        {
            i = 1;
            IPtextBox.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            connection = new TcpClient(IPtextBox.Text, 50000);
            stream = connection.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
            outputThread = new Thread(new ThreadStart(Run));
            outputThread.Start();
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
                Invoke(new DisplayDelegate(DisplayMessage),
                   new object[] { message });
            }// end if

            else if (temp != message)// OK to modify displayTextBox in current thread
            {
                displayTextBox.AppendText("\r\n" + message);
                temp = message;

            }
            else
            { }

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

                    for (int i = 0; i < n; i++)
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
                 new TcpListener(serverIP, 50000);
            listener.Start();
            // accept first player and start a player thread

            players[0] = new Player(listener.AcceptSocket(), this, 0);
            playerThreads[0] =
               new Thread(new ThreadStart(players[0].Run));
            playerThreads[0].Start();

            if (listener.Server.Connected == false)
                n++;
            else
                n--;

            // accept second player and start another player thread       
            players[1] = new Player(listener.AcceptSocket(), this, 1);
            playerThreads[1] =
               new Thread(new ThreadStart(players[1].Run));
            playerThreads[1].Start();

            if (listener.Server.Connected == false)
                n++;
            else
                n--;

            players[2] = new Player(listener.AcceptSocket(), this, 2);
            playerThreads[2] =
               new Thread(new ThreadStart(players[2].Run));
            playerThreads[2].Start();

            // let the first player know that the other player has connected
            if (listener.Server.Connected == false)
                n++;
            else
                n--;            

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

            if (myMark == "0player")
            {
                ChatServer();
            }
            else
            {
                IPtextBox.Text = "127.0.0.1";
                ChatClient();
            }
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

        } // end method RunClient
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

        } // end constructor       

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

                string theReply = "";

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
/**************************************************************************
 * (C) Copyright 1992-2006 by Deitel & Associates, Inc. and               *
 * Pearson Education, Inc. All Rights Reserved.                           *
 *                                                                        *
 * DISCLAIMER: The authors and publisher of this book have used their     *
 * best efforts in preparing the book. These efforts include the          *
 * development, research, and testing of the theories and programs        *
 * to determine their effectiveness. The authors and publisher make       *
 * no warranty of any kind, expressed or implied, with regard to these    *
 * programs or to the documentation contained in these books. The authors *
 * and publisher shall not be liable in any event for incidental or       *
 * consequential damages in connection with, or arising out of, the       *
 * furnishing, performance, or use of these programs.                     *
 *************************************************************************/