

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

class ClientEstablishConnection {
    private static Socket socket;
    private static string socketAddress = "10.0.0.30";
    private static int socketPort = 5500;
    private static string appDataPath = "NIKKIT/AutoLogin/credentials.JSON";
    private static int iterateRequestsDelay =  10000; //10s


    static void Main(String[] args) {
        int successCode = EstablishConnection();
        bool wasSuccessful = SuccessVerification(successCode);
        if (!wasSuccessful) DeniedConnection();
        bool isLoggedIn = verifyLoggedIn();
        if (!isLoggedIn) LoginModule();

        byte[] request = package(); // TO DO - VERIFY LOGIN DETAILS (USE .JSON FORMATTING)
        WriteToServer(request);
        


        socket.Close();

    }






    private static bool verifyLoggedIn()
    {
         string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NIKKIT\\"; //TO DO IMPLEMENT VERIFICATION SCANNING FOR JSON

    }
    private static bool SuccessVerification(int i)
    {
        if (i == 0) return true;
        else return false;
    }
    private static int EstablishConnection()
    {
        int returnValue = 0;
        IPEndPoint endPoint;

        // Attempting to connect to the socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try { 
            endPoint = new IPEndPoint(IPAddress.Parse(socketAddress), socketPort);    // Socket details
            socket.Connect(endPoint);
        } catch (Exception DeniedConnectionException) { returnValue = -1; } // If connection fails Method returns -1

        return returnValue;
    }
    private static void WriteToServer(byte[] message)
    {
        socket.Send(message);
    }


    private static void IterateConnectionRequests()
    {
        while(true) {
            Thread.Sleep(iterateRequestsDelay);
            int successCode = EstablishConnection();
            bool wasSuccessful = SuccessVerification(successCode);
            if (wasSuccessful) break;
        }

    }

    private static byte[] package(String requestIdentifier, String value)
    {
        var requestBody = new
        {
            Key1 = requestIdentifier,
            Key2 = value,
        };

        // Serialize the request body as a JSON string
        var jsonBody = JsonConvert.SerializeObject(requestBody);

        // Convert the JSON string to a byte array
        byte[] requestBytes = Encoding.UTF8.GetBytes(jsonBody);

        return requestBytes;
    }








    private static void DeniedConnection()
    {
        Console.WriteLine("Connection has been denied");
    }       //TO BE DEFINED
    private static void LoginModule()
    {
        Console.WriteLine("Login Module Called");
    }       //TO BE DEFINED

}

