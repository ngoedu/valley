/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/15
 * Time: 6:59
 * 
 * 
 */
using System;
using System.Net.Sockets;
using System.Text;

namespace NGO.Pad.Catalina
{
	/// <summary>
	/// Description of Shutdown.
	/// </summary>
	public class Server
	{
		public static Server Instance = new Server();
		private Server()
		{
		}
		
		public void Shutdown() {
			
			const int PORT_NO = 6002;
        	const string SERVER_IP = "127.0.0.1";
        	string command = "NGO_CATA_BYE";

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(command);

            //---send the text---
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            client.Close();
		}
	}
}
