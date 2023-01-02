import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

// ...

public class Main {
    private static final int PORT = 5500;
    private static final int MAX_CONNECTIONS = 5;

    public static void main(String[] args) throws IOException {
        // Create a thread pool to handle incoming connections
        ExecutorService executor = Executors.newFixedThreadPool(MAX_CONNECTIONS);

        // Create a ServerSocket to listen for incoming connections
        ServerSocket serverSocket = new ServerSocket(PORT);
        System.out.println("Server listening on port " + PORT);

        while (true) {
            // Accept an incoming connection
            Socket clientSocket = serverSocket.accept();
            System.out.println("New client connected");

            // Start a new thread to handle the connection
            executor.submit(() -> {
                try {
                    // Get the input and output streams for the socket
                    BufferedReader in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
                    PrintWriter out = new PrintWriter(clientSocket.getOutputStream(), true);

                    // Read data from the client
                    String inputLine;
                    while ((inputLine = in.readLine()) != null) {
                        // Process the data
                        // ...
                        System.out.println(inputLine);
                        // Write a response to the client           // TO-DO DERIVE KEYS AND VALUES
                        out.println("Response from server");




























                    }
                } catch (IOException e) {
                    e.printStackTrace();
                } finally {
                    try {
                        // Close the socket when finished
                        clientSocket.close();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            });
        }
    }
}