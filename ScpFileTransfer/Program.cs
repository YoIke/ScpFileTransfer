using Renci.SshNet;
using System;
using System.IO;

namespace ScpFileTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("使い方: ScpFileTransfer.exe <接続先IP> <ユーザー名> <パスワード> <リモートファイルのパス> <ローカルの保存先のパス>");
                Console.ReadLine();
                return;
            }

            string host = args[0];
            string username = args[1];
            string password = args[2];
            string remoteFilePath = args[3];
            string localDestinationPath = args[4];

            try
            {
                using (var client = new ScpClient(new ConnectionInfo(host, 22, username, new PasswordAuthenticationMethod(username, password))))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        var localFile = new FileInfo(localDestinationPath);
                        client.Download(remoteFilePath, localFile);
                        Console.WriteLine("ファイルが正常に転送されました。");
                    }
                    else
                    {
                        Console.WriteLine("リモートサーバーに接続できませんでした。");
                    }
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラーが発生しました: " + ex.Message);
            }
        }
    }
}
