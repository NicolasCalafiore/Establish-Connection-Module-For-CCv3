using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Community_Connect_v3_Client_Login_Module.Assets.Networking
{
    public class SocketConnector
    {
        private Socket socket;
        private static int iterateRequestsDelay = 10000; //10s
        private string socketAddress;
        private int socketPort;

        public SocketConnector(String socketAddress, int socketPort) { 
            this.socketPort = socketPort;
            this.socketAddress = socketAddress;
        }



        public Boolean EstablishConnection()
        {
            Boolean isConnected = false;
            int returnValue = 0;
            IPEndPoint endPoint;

            // Attempting to connect to the socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                endPoint = new IPEndPoint(IPAddress.Parse(socketAddress), socketPort);    // Socket details
                socket.Connect(endPoint);
                isConnected = true;
            }
            catch (Exception DeniedConnectionException) { Console.WriteLine("Connection could not be established"); } // If connection fails Method returns -1

            return isConnected;
        }


        public void CloseConnection()
        {
            socket.Close();
        }











    }
}
