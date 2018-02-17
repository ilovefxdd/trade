using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace PIVASWork
{
    public class sqlc
    {
        public static string Login_id = ""; //定义全局变量，记录当前登录的用户编号
        public static string Login_Name = "";  //定义全局变量，记录当前登录的用户名
        public static string Mean_SQL = "", Mean_Table = "", Mean_Field = "";  //定义全局变量，记录“基础信息”各窗体中的表名及SQL语句
        public static SqlConnection My_con;  //定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
   //    public static string localco = "Data Source=192.168.1.100;Initial Catalog=Barcode_data1;Persist Security Info=True;User ID=sa;Password=azcom";
       public static string localco = "Data Source=192.168.10.9;Persist Security Info=True;User ID=sa;Password=WM3319tg;Initial Catalog=Barcode_data1";
          //public static string localco = "Data Source=192.168.100.19;Initial Catalog=Barcode_data1;;Persist Security Info=True;User ID=sa;Password=WM3319tg";
      //  public static string mycont = "Persist Security Info=False;Integrated Security=true;Initial Catalog=myhis;server=(local)";
 //        public static string localco = "Persist Security Info=False;Integrated Security=true;Initial Catalog=Barcode_data1;server=(local);";
        public static int Login_n = 0;  //用户登录与重新登录的标识  
        public sqlc()
        {


        }
        #region  建立数据库连接

        public static SqlConnection getcon()
        {
            My_con = new SqlConnection(localco);
            //用SqlConnection对象与指定的数据库相连接
            My_con.Open();  //打开数据库连接
            return My_con;  //返回SqlConnection对象的信息
        }
        #endregion

        #region  测试数据库是

        public void con_open()
        {
            getcon();
        }
        #endregion

        #region  关闭数据库连接

        public void con_close()
        {
            if (My_con.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                My_con.Close();   //关闭数据库的连接
                My_con.Dispose();   //释放My_con变量的所有空间
            }
        }
        #endregion

        #region  读取指定表中的信息

        public SqlDataReader getcom(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            SqlCommand My_com = My_con.CreateCommand(); //创建一个SqlCommand对象，用于执行SQL语句
            My_com.CommandText = SQLstr;    //获取指定的SQL语句
            SqlDataReader My_read = My_com.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象
            return My_read;
        }
        #endregion

        #region 执行SqlCommand命令
        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void getsqlcom(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            SqlCommand SQLcom = new SqlCommand(SQLstr, My_con); //创建一个SqlCommand对象，用于执行SQL语句
            SQLcom.ExecuteNonQuery();   //执行SQL语句
            SQLcom.Dispose();   //释放所有空间
            con_close();    //调用con_close()方法，关闭与数据库的连接
        }
        #endregion

        #region  创建DataSet对象

        public DataSet getDataSet(string SQLstr, string tableName)
        {
            getcon();   //打开与数据库的连接
            SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, My_con);  //创建一个SqlDataAdapter对象，并获取指定数据表的信息
            DataSet My_DataSet = new DataSet(); //创建DataSet对象
            SQLda.Fill(My_DataSet, tableName);  //通过SqlDataAdapter对象的Fill()方法，将数据表信息添加到DataSet对象中
            con_close();    //关闭数据库的连接
            return My_DataSet;  //返回DataSet对象的信息

            //WritePrivateProfileString(string section, string key, string val, string filePath);
        }
        #endregion

        #region  创建读取对象
        /*
                   SqlConnection conn = new SqlConnection(gy_class.class1.mycon);
                   conn.Open();

                   string sql1 = "select fieldnam ,pox ,poy,font ,size,txt from printt ";
                  
                   SqlCommand cmd = new SqlCommand(sql1, conn);

                   cmd.CommandType = CommandType.Text;
                   SqlDataReader sdr = cmd.ExecuteReader();
                  
                   int ari = 0;
                   while (sdr.Read())
                   {
                       print_zd(i, e, sdr, sta[ari]);
                       ari ++;
                       
                   }*/
        #endregion
        #region  建datagridview cloumn
        /*
             DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();

                column.Name = "Det";
                column.HeaderText = "审核";
                column.CellTemplate = new DataGridViewCheckBoxCell();
                column.CellTemplate.Style.BackColor = Color.Beige;

                this.yp_infodgv.Columns.Insert(0, column);
         */
        #endregion
        #region  插入数据
        /*
                SqlConnection conn;
            try
            {
                conn = new SqlConnection(gy_class.class1.mycon);
                conn.Open();

                string sql1 = "INSERT INTO printt(fieldnam ,pox ,poy,font ,size,txt)  VALUES ";
                string sql2 = "(@cw_id ,@bm ,@ch,@hs ,@br_id,@txt)";
                SqlCommand cmd = new SqlCommand(sql1 + sql2, conn);
                SqlParameter spm_id = new SqlParameter("@cw_id", txt_zd.Text);
                cmd.Parameters.Add(spm_id);

                SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@bm", Convert.ToInt16(txt_x.Text)), 
                    new SqlParameter("@ch", Convert.ToInt16(txt_y.Text))
                ,new SqlParameter("@hs",cmb_fn.Text),new SqlParameter("@br_id",Convert.ToInt16(txt_size.Text)),
                new SqlParameter("@txt", txt_txt.Text)};
                cmd.Parameters.AddRange(paras);
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("成功添加了字段信息！");
                    opentable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
         */
        #endregion
        #region 修改记录
        /*
         *   SqlConnection conn1 = new SqlConnection(gy_class.class1.mycon);
            conn1.Open();
            string childst = "update pivas set 处理标志=@lx  where 所属病区=@bq and 所属科室=@ks and 床位号=@id ";
            SqlCommand cmdc = new SqlCommand(childst, conn1);
            
            SqlParameter spm_bq = new SqlParameter("@bq",txtbq.Text.Trim());
            cmdc.Parameters.Add(spm_bq);
            SqlParameter spm_ks = new SqlParameter("@ks", txtks.Text.Trim());
            cmdc.Parameters.Add(spm_ks);
            SqlParameter spm_id = new SqlParameter("@id", txtid.Text.Trim());
            cmdc.Parameters.Add(spm_id);
            string czlx="0";
            switch (cmblx.SelectedIndex)
            {
                case 0: czlx = "0"; MessageBox.Show(cmblx.Items[0].ToString()); break;
                case 1: czlx = "1"; MessageBox.Show(cmblx.Items[1].ToString()); break;
                case 2: czlx = "2"; MessageBox.Show(cmblx.Items[2].ToString()); break;
                case 3: czlx = "3"; MessageBox.Show(cmblx.Items[3].ToString()); break;
                case 4: czlx = "4"; MessageBox.Show(cmblx.Items[4].ToString()); break;
                case 5: czlx = "5"; MessageBox.Show(cmblx.Items[5].ToString()); break;
                case 6: czlx = "6"; MessageBox.Show(cmblx.Items[6].ToString()); break;
                case 7: czlx = "7"; MessageBox.Show(cmblx.Items[7].ToString()); break;
                case 8: czlx = "8"; MessageBox.Show(cmblx.Items[8].ToString()); break;
                case 9: czlx = "9"; MessageBox.Show(cmblx.Items[9].ToString()); break;
                case 10: czlx = "a"; MessageBox.Show(cmblx.Items[10].ToString()); break;
            }
            SqlParameter spm_lx = new SqlParameter("@lx",czlx);
            cmdc.Parameters.Add(spm_lx);
            cmdc.ExecuteNonQuery();
            conn1.Close();
        }
         */
        #endregion
        #region 读行数据
        /*
         *   private void yp_infodgv_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != -1 && !yp_infodgv.Rows[e.RowIndex].IsNewRow)
            {
               
                    txtbq.Text = yp_infodgv[2, e.RowIndex].Value.ToString();
                    txtks.Text = yp_infodgv[3, e.RowIndex].Value.ToString();
                    txtid.Text = yp_infodgv[4, e.RowIndex].Value.ToString();
                   


            }
        }
         */
        #endregion
    }
}