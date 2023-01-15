

using System.Runtime.CompilerServices;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Nodes;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Community_Connect_v3_Client_Login_Module.Assets.Networking;

class ClientEstablishConnection {
    
    private static string socketAddress = "10.0.0.30";
    private static int socketPort = 5500;
    private static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NIKKIT"; //TO DO IMPLEMENT VERIFICATION SCANNING FOR JSON
    private static string CredentialsPath = AppDataPath + "\\credentials.JSON";
    private static Boolean isConnectedToSocket = false;

    static void Main(String[] args) {

        // Object used to handle connection to server
        SocketConnector socketConnector = new SocketConnector(socketAddress, socketPort);

        // Object used to handle logins/auto-logins
        CredentialsHandler credentialsHandler = new CredentialsHandler();

        //Connection is attempted
        isConnectedToSocket = socketConnector.EstablishConnection();
        if (!isConnectedToSocket) return; 
        bool isLoggedIn = verifyLoggedIn();
        if (!isLoggedIn) LoginModule();
        socketConnector.CloseConnection();

    }






    private static bool verifyLoggedIn()
    {
        

        if (File.Exists(CredentialsPath))
        {
            Console.WriteLine("The file exists.");
            return true;
        }
        return false;

    }
    private static void LoginModule()
    {
        Console.WriteLine("Login Module Called");
    }       //TO BE DEFINED

}

