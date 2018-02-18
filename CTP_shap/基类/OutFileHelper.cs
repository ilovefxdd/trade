using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CTP_交易.基类
{
    /// <summary>
    /// 输出文件帮助器
    /// </summary>
    public class OutFileHelper
    {
        /// <summary>
        /// 输出文件
        /// </summary>
        public string OutFile = @"c:\tmp.csv";

        public string Data = "";

        public List<string> Columns = new List<string>();

        public OutFileHelper(string _OutFile)
        {
            OutFile = _OutFile;
            Data = "";
            Columns = new List<string>();
        }

        public void Delete()
        {
            if (Exist())
            {
                File.Delete(OutFile);
            } 
        }

        public bool Exist()
        {
            return File.Exists(OutFile);
        }

        public void Init()
        {
            Data = ""; 
            //输出列头
            string head = "";
            foreach (string str in Columns)
            {
                head += str + ",";
            }
            head += "\r\n";

            Data += head;
        }

        public void AddColumn(string colName )
        {
            Columns.Add(colName);
        }

        public void AddRow(string str )
        {
            Data += str + ",";
        }

        public void Enter()
        {
            Data += "\r\n";
        }

        public void Write()
        {
            File.AppendAllText(OutFile, Data, Encoding.Default);
            Data = "";
        }
    }
}
