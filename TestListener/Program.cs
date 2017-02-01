using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class serv
{
    private static string ObtnerRespuesta(Socket s)
    {
        byte[] b = new byte[100];
        int k = s.Receive(b);
        Console.WriteLine("Recieved...");
        string nombre = System.Text.Encoding.UTF8.GetString(b); ;

        for (int i = 0; i < k; i++)
        {
            Console.Write(Convert.ToChar(b[i]));

        }
        return nombre;
    }

    private static string ObtnerRespuesta(TcpClient client )
    {
        byte[] b = new byte[256];
        NetworkStream stream = client.GetStream();
        stream.Read(b, 0, b.Length);

        Console.WriteLine("Recieved..."); 
        string nombre = System.Text.Encoding.UTF8.GetString(b); ;

        for (int i = 0; i < b.Length; i++)
        {
            Console.Write(Convert.ToChar(b[i]));

        }
        return nombre;
    }
    public static void Main()
    {
        try
        {
            IPAddress ipAd = IPAddress.Parse("127.0.0.1");
            // use local m/c IP address, and 
            // use the same in the client

            /* Initializes the Listener */
            TcpListener myList = new TcpListener(ipAd, 8001);

            /* Start Listeneting at the specified port */
            myList.Start();

            Console.WriteLine("The server is running at port 8001...");
            Console.WriteLine("The local End point is  :" +
                              myList.LocalEndpoint);
            Console.WriteLine("Waiting for a connection.....");

            //Socket s = myList.AcceptSocket();
            TcpClient client = myList.AcceptTcpClient();
            

            //string nombre = ObtnerRespuesta(s);
            string nombre = ObtnerRespuesta(client);

            ASCIIEncoding asen = new ASCIIEncoding();
            //s.Send(asen.GetBytes("hola " + nombre + ", como estas?"));
            NetworkStream stream = client.GetStream();
            stream.Write(asen.GetBytes("hola " + nombre + ", como estas?"), 0, asen.GetBytes("hola " + nombre + ", como estas?").Length);
            //string respuesta = ObtnerRespuesta(s);
            string respuesta = ObtnerRespuesta(client);
            Console.WriteLine(nombre + " dice " + respuesta);

            Console.WriteLine("\nSent Acknowledgement");
            Console.ReadKey();

            /* clean up */
            client.Close();
            myList.Stop();

        }
        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.StackTrace);
            Console.ReadKey();
        }
    }

}