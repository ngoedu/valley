﻿/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/19
 * 时间: 23:56
 * 
 * 
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace NGO.Pad.AEther
{
	/// <summary>
	/// https://docs.microsoft.com/en-us/dotnet/framework/network-programming/using-an-asynchronous-client-socket
	/// 
	/// and aether client needs work in a producer-consumer pattern
	/// 
	/// https://www.infoworld.com/article/3090215/application-development/how-to-work-with-blockingcollection-in-c.html
	/// 
	/// 
	/// form background worker which can interact with UI
	/// https://stackoverflow.com/questions/6481304/how-to-use-a-backgroundworker
	/// 
	/// CROSS-THREAD update UI issue
	/// https://stackoverflow.com/questions/142003/cross-thread-operation-not-valid-control-accessed-from-a-thread-other-than-the
	///  
	/// https://blog.csdn.net/joem/article/details/1448198
	/// 
	/// 
	/// Socket disconnection detection
	/// https://stackoverflow.com/questions/2661764/how-to-check-if-a-socket-is-connected-disconnected-in-c
	/// </summary>
	public class Endpoint
	{
		//this is a unique ID in aether system.
	    // SwingConsole.ID = 12
	    // Pad.ID = 8;
	    // Bridge = 0;
		public static readonly int ID = 8;
		public static readonly int BRIDGE_ID = 0;
		
		private ICallback callback;
		private Socket client;
		//
		//https://docs.microsoft.com/en-us/dotnet/api/system.threading.manualresetevent?view=netframework-4.7.2
		//
		ManualResetEvent connectDone = new ManualResetEvent(false);
		ManualResetEvent sendDone = new ManualResetEvent(false);
		readonly ManualResetEvent receiveDone = new ManualResetEvent(false);
		
		public Endpoint(ICallback callback)
		{
			this.callback = callback;
		}
		
		public static void DebugDump(string message) {
			System.Diagnostics.Debug.WriteLine("[{0}] - {1}", Thread.CurrentThread.Name, message);
		}
		
		public void Connect(string ipAddress, int port)
		{
			//TODO: this main thread needs to be launched in a separate thread 
			//and     act as a producer for sendings
			//         act as a consumer for recevings
			DebugDump(string.Format("try connect to {0}", ipAddress));
			EndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), port);
			// Create a TCP/IP socket.  
			client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
			Connect(ep, client);
			Receive(client);
		}
        
		/// <summary>
		/// "GET / HTTP/1.1\r\nHost:www.bing.com\r\n\r\n"
		/// </summary>
		/// <param name="data"></param>
		public  void  SendData(string data, int dest)
		{
			Send(client, PackType.DAT.ToPackage(dest,data));
		}
		
		/// <summary>
		/// disconnect the socket to the remote peer
		/// TODO: one issue found - once disconnected then re-connected, not able to recieve new message, but sending works.
		/// </summary>
		public bool Disconnect() {
			// Release the socket.
			client.Shutdown(SocketShutdown.Both);
			client.Disconnect(false);
			if (client.Connected) {
			    System.Diagnostics.Debug.WriteLine("We're still connnected");
			    client = null;
			    return false;
			}
			else  {
				System.Diagnostics.Debug.WriteLine("We're disconnected");
				client = null;
				return true;
			}
			   	
			
		}
        
              
		private  void Connect(EndPoint remoteEP, Socket client)
		{  
			client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);  
        	connectDone.WaitOne();
        	
        	DebugDump(string.Format("connectDone signal recieved!"));
			this.callback.Connected();
		}
        
		private  void ConnectCallback(IAsyncResult ar)
		{  
			try {  
				// Retrieve the socket from the state object.  
				Socket client = (Socket)ar.AsyncState;  
        
				// Complete the connection.  
				client.EndConnect(ar);  
        
				DebugDump(string.Format("Socket connected to {0}",client.RemoteEndPoint.ToString()));
        
				// Signal that the connection has been made.  
				connectDone.Set();

				//send first REG to bridge
				var pack = PackType.REG.ToPackage(Endpoint.BRIDGE_ID,string.Empty);
				Send(client, pack );
				DebugDump(string.Format("<REG> sends to bridge - dest={0}", pack.Destination));
			} catch (Exception e) {  
				Console.WriteLine(e.ToString());  
			}  
		}
        
		private  void Send(Socket client, String data)
		{  
			// Convert the string data to byte data using ASCII encoding.  
			byte[] byteData = Encoding.ASCII.GetBytes(data);  
        
			// Begin sending the data to the remote device.  
			client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);  
		}
		
		private  void Send(Socket client, EtherPack package)
		{  
			// Convert the string data to byte data using ASCII encoding.  
			byte[] byteData = Encoder.Instance.Encode(package);
        
			// Begin sending the data to the remote device.  
			client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);  
		}
        
        
		private  void SendCallback(IAsyncResult ar)
		{  
			try {  
				// Retrieve the socket from the state object.  
				Socket client = (Socket)ar.AsyncState;  
        
				// Complete sending the data to the remote device.  
				int bytesSent = client.EndSend(ar);  
				DebugDump(string.Format("Sent {0} bytes to remote peer {1}", bytesSent, client.RemoteEndPoint));
        
				// Signal that all bytes have been sent.  
				sendDone.Set();
				this.callback.DataSent(string.Format("Sent {0} bytes to remote peer {1}.", bytesSent, client.RemoteEndPoint));
			} catch (Exception e) {  
				System.Diagnostics.Debug.WriteLine(e.ToString());  
			}  
		}
        
		private  void Receive(Socket client)
		{  
			try {  
				// Create the state object.  
				StateObject state = new StateObject();  
				state.workSocket = client;  
        
				// Begin receiving the data from the remote device.  
				client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,	new AsyncCallback(ReceiveCallback), state);  
			} catch (Exception e) {  
				DebugDump(e.ToString());  
			}  
		}
        
		private void HandlePackage(EtherPack package) {
			PackType etype = PackType.ValueOf(package.Type);
			switch (etype.Value)
			{
	        	case 5 : { //PackType.REGA
	        		DebugDump(string.Format("<REGA> received from bridge - src={0}", package.Source));
					this.callback.Connected();
					break;
	        	}
	        	case 3 : { //PackType.KA
	        		if (package.Destination == Endpoint.ID)
		        	{
	        			DebugDump(string.Format("<KA> received from bridge - src={0}", package.Source));
	        			var pack = PackType.KAA.ToPackage(Endpoint.BRIDGE_ID,string.Empty);
		        		Send(client, pack);
		        		DebugDump(string.Format("<KAA> sends to bridge - dest={0}", pack.Destination));
		        	}
	        		break;
	        	}
        		case 127 : { //PackType.DAT
        			//application DATA, deliver to callback
        			DebugDump(string.Format("<DAT> arrived from - src={0}", package.Source));
	        		callback.MessageReceived(package.ToString());
	        		break;
	        	}
	        }	           
		}		
        
		private  void ReceiveCallback(IAsyncResult ar)
		{  
			try { 
				// Retrieve the state object and the client socket   
				// from the asynchronous state object.  
				StateObject state = (StateObject)ar.AsyncState;  
				Socket client = state.workSocket;  
				// Read data from the remote device.  
				int bytesRead = client.EndReceive(ar);  
				if (bytesRead > 0) { 
					// There might be more data, so store the data received so far. 
					bool ready = Decoder.Instance.Decode(state, bytesRead, state.buffer);
					//needs human invention on whether the AEther package fully received or not.
					if (ready) {
						HandlePackage(state.package);
						state.package = null;
					}
					// Get the rest of the data.
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,	new AsyncCallback(ReceiveCallback), state);  
                
				} else {
					//TODO: seems here NEVER be invoked
					// All the data has arrived; put it in response.   
					// Signal that all bytes have been received.  
					receiveDone.Set();  
				}  
			} catch (Exception e) {  
				DebugDump(e.ToString());  
			}  
		}
        
	}
    
    
	public class StateObject
	{
		// Client socket.
		public Socket workSocket = null;
		// Size of receive buffer.
		public const int BufferSize = 128;//256;
		// Receive buffer.
		public byte[] buffer = new byte[BufferSize];
		// Received data string.
		public EtherPack package = null;
	}
}
