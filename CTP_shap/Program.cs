using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace CTP_交易
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//update();
			Application.Run(new FormTrade());
		}
		private static void update()
		{
			try
			{
				FormUpdate update = new FormUpdate();
				//update.Show();

				//获取新的版本号
				WebClient wc = new WebClient();
				Stream sm = wc.OpenRead("");
				StreamReader sr = new StreamReader(sm);
				string newVer = sr.ReadLine();
				string url = sr.ReadLine();
				string msg = sr.ReadToEnd();
				//比较是否需要更新
				Version lastVer = new Version(newVer);
				Version curVer = Assembly.GetEntryAssembly().GetName().Version;//
				if (lastVer > curVer && MessageBox.Show("发现新版本:" + newVer + "\n\t\t是否更新?\n" + msg, "升级提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					//更新:下载	
					wc.DownloadProgressChanged += delegate(object wcsender, DownloadProgressChangedEventArgs ex) //委托下载数据时事件 
					{
						update.progressBar1.Value = Convert.ToInt32(ex.BytesReceived / ex.TotalBytesToReceive * 100);
						update.progressBar2.Value = ex.ProgressPercentage;
					};
					wc.DownloadFileCompleted += delegate(object wcsender, AsyncCompletedEventArgs ex)  //委托下载完成时事件 
					{
						//更新:运行安装解压程序
						System.Diagnostics.Process.Start(Application.StartupPath + @"\update\update.exe");
						//主程序退出
						Application.Exit();
					};
					if (!Directory.Exists(Application.StartupPath + @"\update"))
						Directory.CreateDirectory(Application.StartupPath + @"\update");
					wc.DownloadFileAsync(new Uri(url), Application.StartupPath + @"\update\update.exe");//下载
					//update.Hide();
					update.ShowDialog();
				}
				else
				{
					update.Close();
					Application.Run(new FormTrade());
				}
			}
			catch
			{
				if (MessageBox.Show("升级服务不可用.", "警告", MessageBoxButtons.OK) == DialogResult.OK)
					Application.Run(new FormTrade());
			}
		}
	}
}
