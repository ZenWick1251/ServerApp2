
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main(string[] args)
    {
        
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 7070);
        server.Start();
        Console.WriteLine("Server started");

        Random rand = new Random();
        int numberToGuess = rand.Next(1, 101);
        Console.WriteLine($"secret number: {numberToGuess}");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Player connected.");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            string responseMessage = "type number from 1 to 100";
            stream.Write(Encoding.UTF8.GetBytes(responseMessage), 0, responseMessage.Length);

            int attempts = 5;

            while (attempts > 0)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                int guessedNumber;

                if (int.TryParse(clientMessage, out guessedNumber))
                {
                    if (guessedNumber == numberToGuess)
                    {
                        responseMessage = "u guessed the number";
                        stream.Write(Encoding.UTF8.GetBytes(responseMessage), 0, responseMessage.Length);
                        break;
                    }
                    else if (guessedNumber < numberToGuess)
                    {
                        responseMessage = $"secret number is greater. u have attempts: {--attempts}";
                    }
                    else
                    {
                        responseMessage = $"guessed number is less. u have attempts: {--attempts}";
                    }
                }
                else
                {
                    responseMessage = "type number:";
                }

                
                stream.Write(Encoding.UTF8.GetBytes(responseMessage), 0, responseMessage.Length);

                if (attempts == 0)
                {
                    responseMessage = $"u dont have any attempts left. secret number was: {numberToGuess}";
                    stream.Write(Encoding.UTF8.GetBytes(responseMessage), 0, responseMessage.Length);
                }
            }

            
            stream.Close();
            client.Close();

            
            numberToGuess = rand.Next(1, 101);
            Console.WriteLine($"new secret number is: {numberToGuess}");
        }
    }
}
