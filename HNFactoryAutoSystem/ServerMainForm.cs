﻿using HNFactoryAutoSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HNFactoryAutoSystem
{
    public partial class ServerMainForm : Form
    {

        private static byte[] result = new byte[1024];
        private static int myProt = Convert.ToInt32(appString.GetAppsettingStr("StockPort"));   //端口
        static Socket serverSocket;

        /// <summary>
        /// 声明 socketSend 用于等待客户端的连接 并且创建与之通信用的SocketSend，//等客户端连接//接受到client连接，为此连接建立新的socket，并接受信息
        /// </summary>
        Socket socketSend;
        /// <summary>
        /// 将远程连接的客户端的IP地址和Socket存入集合中
        /// </summary>
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();


        public ServerMainForm()
        {
            InitializeComponent();
        }

        private void btnStartPLCServer_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(appString.GetAppsettingStr("StockIP"));
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口           
                serverSocket.Listen(10);    //设定最多10个排队连接请求            
                txt_Log.AppendText(string.Format("{1:yyyyMMdd HH:mm:ss}启动监听{0}成功 \r \n", serverSocket.LocalEndPoint.ToString(), DateTime.Now));
                //通过Clientsoket发送数据            
                Thread myThread = new Thread(ListenClientConnect);
                myThread.Start();

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private void ListenClientConnect()
        {
            while (true)
            {
                //等待客户端的连接 并且创建一个负责通信的Socket
                socketSend = serverSocket.Accept();
                //将远程连接的客户端的IP地址和Socket存入集合中
                dicSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend);//
                //将远程连接的客户端的IP地址和端口号存储下拉框中
                //cboUsers.Items.Add(socketSend.RemoteEndPoint.ToString());//
                //192.168.11.78：连接成功
                //ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功");//
                //开启 一个新线程不停的接受客户端发送过来的消息
                Thread th = new Thread(ReceiveMessage);
                th.IsBackground = true;
                th.Start(socketSend);

                //Socket clientSocket = serverSocket.Accept();
                //clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                //Thread receiveThread = new Thread(ReceiveMessage);
                //receiveThread.Start(clientSocket);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket; 
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    //实际接受到的有效字节数
                    int receiveNumber = myClientSocket.Receive(buffer);
                    if (receiveNumber == 0)
                    {
                        break;
                    }
                    //string strInfo = string.Format("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));

                    string strInfo = Encoding.UTF8.GetString(buffer, 0, receiveNumber);

                    string[] strTexts = strInfo.Split('|');

                    ////通过clientSocket接收数据                    
                    //int receiveNumber = myClientSocket.Receive(result);
                    //string strInfo = string.Format("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
                    ////MessageBox.Show(strInfo);
                    DataLogHelper logHelper = new DataLogHelper();
                    foreach (string strText in strTexts)
                    {
                        if (!string.IsNullOrEmpty(strText))
                        {
                            logHelper.StockLog(strText, receiveNumber);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    //ServerMainFormtxt_Log.AppendText(ex.Message + "\r \n");
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }

        private void SendMessage(string strText)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(strText);
            foreach (KeyValuePair<string, Socket> socket in dicSocket)
            {
                socket.Value.Send(buffer);
            }
        }

        private void ServerMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭页面前需要关闭服务
            DialogResult result = MessageBox.Show("是否要退出程序？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                if(serverSocket != null)
                {
                    //serverSocket.Shutdown(SocketShutdown.Both);
                    SafeClose(serverSocket);
                }

                e.Cancel = true;
                return;
            }
        }

        private void btnClosePLCServer_Click(object sender, EventArgs e)
        {
            if (serverSocket != null)
            {
                //serverSocket.Shutdown(SocketShutdown.Both);
                SafeClose(serverSocket);
                MessageBox.Show("服务关闭成功！");
            }
            else
            {
                MessageBox.Show("服务尚未启动！");
            }
        }

        private void SafeClose(Socket socket)
        {
            if (socket == null)
                return;

            if (!socket.Connected)
                return;

            try
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            catch
            {
            }

            try
            {
                socket.Close();
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strText = "这是服务器发来的信息！";
            SendMessage(strText);
        }
    }
}
