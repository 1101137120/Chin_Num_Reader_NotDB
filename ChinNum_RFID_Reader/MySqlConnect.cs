using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ChinNum_RFID_Reader
{
    class MySqlConnect
    {
       private MySqlConnection connection; //MYSQL
        // private SqlConnection connection; // SQL server
        private string server;
        private string database;
        private string uid;
        private string password;
        private string table;
        private string tableID;
        private string tableSumID;
        private string dateField;
        private string timeField;
        private string tagField;
        private string reField;

        private string table2;
        private string tableID2;
        private string dateField2;
        private string timeField2;
        private string statusField2;
        private int Port;

        //Constructor
        public void MySqlConnectA()
        {
            OpenConnection();
        }

        //Initialize values
        public void Initialize(string ip,string db,string account,string pw, string tableName, string id, string sumid, string dateF, string timeF, string tagF, string reF, string tableName2, string id2, string dateF2, string timeF2, string statusF2)
        {
            Console.WriteLine("LOGINLOGINLOGINLOGIN");
            this.server = ip;
            this.database = db;
            this.uid = account;
            this.password = pw;
            this.table = tableName;
            this.tableID = id;
            this.tableSumID = sumid;
            this.dateField = dateF;
            this.timeField = timeF;
            this.tagField = tagF;
            this.reField = reF;


            this.table2 = tableName2;
            this.tableID2 = id2;
            this.dateField2 = dateF2;
            this.timeField2 = timeF2;
            this.statusField2 = statusF2;
            /*  server = "104.236.153.32";
              database = "esl_test";
              uid = "esltest";
              password = "smartchip";

              Port = 3306;
              string connectionString;
              connectionString = "server =" + server + ";"+ "port =" + Port + ";" + "database =" +
              database + ";" + "user id =" + uid + ";" + "password=" + password + ";";*/
            //連結資料庫
            // string  connectionString= "server=104.236.153.32;database=RFID_Reader;uid=root;password=isup155231;";//MYSQL connect
            string connectionString = "server="+ this.server + ";database=" + this.database + ";uid="+ this.uid + ";password="+ this.password+ ";charset=utf8;";//MYSQL connect

            try
            {
                connection = new MySqlConnection(connectionString); //MYSQL connect
            }
            catch (Exception ex)
            {
                Console.WriteLine("DDDDDDDDDDDDDDDDDDDD"+ ex);
            }

            string datasource = @"MIKE-NB-8\SQLEXPRESS,1433";

            string database = "RFID_Reader";
            string username = "sa";
            string password = "1234";

           // string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=RFID_Reader;User ID=testSQLServer;Password=smartchip"; //SQL Server connect

          //  connection = new SqlConnection(connectionString);//SQL Server connect
        }

        //open connection to database
        private bool OpenConnection()
        {
            Console.WriteLine("LINK");
            try
            {

                connection.Open();
                Console.WriteLine("LINK true");
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("LINK false" + ex.Number);
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }


        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Select statement
        public List<string> Select(string dgTagID)
        {

            MySqlConnectA();
            Console.WriteLine("dgTagID" + dgTagID);


            //   string SQL = "select DISTINCT(TagID) as TagID from ChinNum_Reader_data  where  TagID='" + dgTagID+"'";

            string SQL = "select DISTINCT("+tagField+") as TagID from "+table+"  where  "+ tagField + "='" + dgTagID + "'";

            // SqlDataAdapter da = new SqlDataAdapter(SQL,connection);
            MySqlCommand myCommand = new MySqlCommand(SQL, connection);
            MySqlDataReader myDataReader = myCommand.ExecuteReader();
            // SqlCommand myCommand = new SqlCommand(SQL, connection);
            // SqlDataReader myDataReader = myCommand.ExecuteReader();
            //讀取結果
            List<string> selectresult = new List<string>();

            while (myDataReader.Read())
            {
                if (myDataReader["TagID"].ToString() != "")
                {
                    selectresult.Add(myDataReader["TagID"].ToString());

                }
            }

            //close connection
            this.CloseConnection();
            return selectresult;
        }



        //Select statement
        public bool Insert(List<text_data_list> dgdata, int  Chin)
        {

            MySqlConnectA();

            // SqlCommand comm = new SqlCommand("INSERT INTO ChinNum_Reader_data  (date,time,TagID,textName) VALUES (@date,@time,@TagID,@textName)", connection);
            //  MySqlCommand comm = new MySqlCommand("INSERT INTO ChinNum_Reader_data  (date,time,TagID,chinID) VALUES (@date,@time,@TagID,@chinID)", connection);
            MySqlCommand comm = new MySqlCommand("INSERT INTO "+table+"  ("+dateField+","+timeField+","+tagField+","+tableSumID+ "," + reField + ") VALUES (@date,@time,@TagID,@chinID,@result)", connection);

            //以上都應該放在for迴圈外(除非你會變動=.=)

            int i;

            for (i = 0; i < dgdata.Count; i++)
            {
                comm.Parameters.Clear();//清除掉目前宣告出來的Parameters
                comm.Parameters.AddWithValue("date", dgdata[i].date);
                comm.Parameters.AddWithValue("time", dgdata[i].time);
                comm.Parameters.AddWithValue("TagID", dgdata[i].TagID);
                Console.WriteLine("dgdata[i].result" + dgdata[i].result);
                comm.Parameters.AddWithValue("result", dgdata[i].result);
                comm.Parameters.AddWithValue("chinID", Chin);
                comm.ExecuteNonQuery();
            }
            //close connection
            this.CloseConnection();
            return true;
        }


        public int InsertChin()
        {

            MySqlConnectA();

            // SqlCommand comm = new SqlCommand("INSERT INTO ChinNum_Reader_data  (date,time,TagID,textName) VALUES (@date,@time,@TagID,@textName)", connection);
            // MySqlCommand comm = new MySqlCommand("INSERT INTO chinNum_Reader  (date,time,status) VALUES (@date,@time,@status) ;SELECT LAST_INSERT_ID()", connection);

            MySqlCommand comm = new MySqlCommand("INSERT INTO "+table2+"  ("+dateField2+","+timeField2+","+statusField2+") VALUES (@date,@time,@status) ;SELECT LAST_INSERT_ID()", connection);
            //以上都應該放在for迴圈外(除非你會變動=.=)

            comm.Parameters.Clear();//清除掉目前宣告出來的Parameters
                comm.Parameters.AddWithValue("date",DateTime.Now.ToString("yyyy/MM/dd"));
                comm.Parameters.AddWithValue("time", DateTime.Now.ToString("HH : mm : ss"));
                comm.Parameters.AddWithValue("status", false);
                //comm.ExecuteNonQuery();
            int insertedID = Convert.ToInt32(comm.ExecuteScalar());
            //close connection
            this.CloseConnection();
            return insertedID;
        }

        public bool checkChinComplete()
        {

            MySqlConnectA();

            // SqlCommand comm = new SqlCommand("INSERT INTO ChinNum_Reader_data  (date,time,TagID,textName) VALUES (@date,@time,@TagID,@textName)", connection);
            // MySqlCommand comm = new MySqlCommand("SELECT status FROM chinNum_Reader ORDER BY id DESC LIMIT 1", connection);
            MySqlCommand comm = new MySqlCommand("SELECT "+statusField2+" FROM "+table2+" ORDER BY "+tableID2+" DESC LIMIT 1", connection);

            //comm.ExecuteNonQuery();
            bool insertedID = Convert.ToBoolean(comm.ExecuteScalar());
            //close connection
            this.CloseConnection();
            return insertedID;
        }

        public bool chinComplete()
        {

            MySqlConnectA();

            // SqlCommand comm = new SqlCommand("INSERT INTO ChinNum_Reader_data  (date,time,TagID,textName) VALUES (@date,@time,@TagID,@textName)", connection);
            //MySqlCommand comm = new MySqlCommand("UPDATE chinNum_Reader SET chinNum_Reader.status = true where chinNum_Reader.id = (SELECT Max(id) FROM(SELECT * FROM chinNum_Reader) AS something)", connection);
            MySqlCommand comm = new MySqlCommand("UPDATE "+table2+" SET "+table2+"."+statusField2+" = true where "+table2+"."+tableID2+" = (SELECT Max("+tableID2+") FROM(SELECT * FROM "+table2+") AS something)", connection);
            //comm.ExecuteNonQuery();
            bool insertedID = Convert.ToBoolean(comm.ExecuteScalar());
            //close connection
            this.CloseConnection();
            return insertedID;
        }


        //Select statement
        public List<text_data_list> chinLastestData()
        {

            MySqlConnectA();

            // SqlDataAdapter da = new SqlDataAdapter(SQL,connection);
            // MySqlCommand myCommand = new MySqlCommand("SELECT * FROM `ChinNum_Reader_data` WHERE chinID=(SELECT MAX(id) FROM chinNum_Reader)", connection);
            MySqlCommand myCommand = new MySqlCommand("SELECT * FROM `"+table+"` WHERE "+tableSumID+"=(SELECT MAX("+tableID2+") FROM "+table2+ ") ORDER BY "+dateField+","+timeField+"", connection);
            MySqlDataReader myDataReader = myCommand.ExecuteReader();
            // SqlCommand myCommand = new SqlCommand(SQL, connection);
            // SqlDataReader myDataReader = myCommand.ExecuteReader();
            //讀取結果
            List<text_data_list> selectresult = new List<text_data_list>();
            
            while (myDataReader.Read())
            {
                if (myDataReader["id"].ToString() != "")
                {

                    text_data_list data = new text_data_list();
                    if (myDataReader["date"].ToString() != "")
                        data.date = Convert.ToDateTime(myDataReader["date"]).ToString("yyyy/MM/dd");

                    if (myDataReader["time"].ToString() != "")
                        data.time = myDataReader["time"].ToString();
                    data.TagID = myDataReader["TagID"].ToString();
                    data.result = myDataReader["result"].ToString();
                    data.chinID = Convert.ToInt32(myDataReader["chinID"]);
                    selectresult.Add(data);

                }
            }

            //close connection
            this.CloseConnection();
            return selectresult;
        }



        //Select statement
        public List<text_data_list> selectdoublecheck(string tagID)
        {

            MySqlConnectA();

            // SqlDataAdapter da = new SqlDataAdapter(SQL,connection);
            // MySqlCommand myCommand = new MySqlCommand("SELECT * FROM `ChinNum_Reader_data` WHERE chinID=(SELECT MAX(id) FROM chinNum_Reader)", connection);
            Console.WriteLine("SELECT * FROM `" + table + "` WHERE " + tagField + "=" + tagID + "");
            MySqlCommand myCommand = new MySqlCommand("SELECT * FROM `" + table + "` WHERE " + tagField + "='"+ tagID+"'", connection);
            MySqlDataReader myDataReader = myCommand.ExecuteReader();
            // SqlCommand myCommand = new SqlCommand(SQL, connection);
            // SqlDataReader myDataReader = myCommand.ExecuteReader();
            //讀取結果
            List<text_data_list> selectresult = new List<text_data_list>();

            while (myDataReader.Read())
            {
                if (myDataReader["id"].ToString() != "")
                {

                    text_data_list data = new text_data_list();
                    if (myDataReader["date"].ToString() != "")
                        data.date = Convert.ToDateTime(myDataReader["date"]).ToString("yyyy/MM/dd");

                    if (myDataReader["time"].ToString() != "")
                        data.time = myDataReader["time"].ToString();
                    data.TagID = myDataReader["TagID"].ToString();
                    data.result = myDataReader["result"].ToString();
                    data.chinID = Convert.ToInt32(myDataReader["chinID"]);
                    selectresult.Add(data);

                }
            }

            //close connection
            this.CloseConnection();
            return selectresult;
        }


    }
}
