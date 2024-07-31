using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace WebApplication1

{
    class ProgramaDeMensajería
    {
        static void Main(string[] args)
        {   
            //Para ingresar el puerto
            Console.WriteLine("Bienvenido al chat. Ingrese el número de puerto al que desea conectarse:");
            int port = int.Parse(Console.ReadLine());
            //Para intentar la coneccion
            var cliente = new TcpClient();
            cliente.Connect(IPAddress.Loopback, port);

            var stream = cliente.GetStream();

            // Parte para recibir mensajes
            var recibirHilo = new Thread(() =>
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Mensaje nuevo: " + message);
                }
            });
            recibirHilo.Start();

            // Parte para enviar mensajes
            var enviarHilo = new Thread(() =>
            {
                while (true)
                {
                    string input = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(input);
                    stream.Write(buffer, 0, buffer.Length);
                }
            });
            enviarHilo.Start();

            // Para mantener la aplicación en ejecución
            enviarHilo.Join();
        }
    }
}
