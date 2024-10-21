using System.Net.Sockets;
using System.Text;
class client
{
    static void Main(string[] args)
    {
        try
        {
            TcpClient client = new TcpClient("127.0.0.1", 7070);
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            var serverMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine(serverMessage);

            int attempts = 5;

            while (attempts > 0)
            {
                string userInput = Console.ReadLine();
                stream.Write(Encoding.UTF8.GetBytes(userInput), 0, userInput.Length);

                bytesRead = stream.Read(buffer, 0, buffer.Length);
                serverMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine(serverMessage);

                attempts--;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        } 
    }
}
