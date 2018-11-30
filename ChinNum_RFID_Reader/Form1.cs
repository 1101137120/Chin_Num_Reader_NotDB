using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO.Ports;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using SMC5052B;

namespace ChinNum_RFID_Reader
{


    public partial class Form1 : Form
    {


        private delegate void UpdateUiTextStringDelegate(byte[] bArrary); // UI讀取用
        private delegate void GridViewAddRows(byte[] str);
        private delegate void GridViewRemoveRows(int Index);
        private delegate void UpdateUiTextStringDelegate2(string ID); // UI讀取用
        Boolean isConnect;
        string ConnectStatusS = "連接成功";
        string ConnectStatusF = "連接失敗";
        string ConnectButtonL = "連線";
        string ConnectButtonNL = "中斷連線";
        string TagMore = "重碼";
        string TagNotDB = "無國碼"; 
        string ConnectStatusDefault = "未連線";
        string ImportDialogHeader = "匯入";
        string ImportDialogContent = "匯入成功";
        string MoreHeader = "重碼";
        string MoreContent = "已重複";
        string NotDBHeader = "國碼";
        string NotDBContent = "無國碼";
        string dataGridClearHeader = "表單";
        string dataGridClearContent = "表單資料確認清空?";
        string exitHeader = "關閉";
        string exitContent = "是否匯出表單?";
        string txtImportHeader = "提示";
        string txtImportContent = "請先匯入txt";
        string txtPath = "";
        string TagIsOK = "合格";
        string checkChinContent = "是否載入未匯出資料?";
        string checkChinHeader = "提示";
    /*    string ClearGridViewHeader = "提示";
        string ClearGridViewContent = "是否清空表單查詢Tag";*/
        string autoLoad = "連續讀取";
        string autoLoadStop = "終止讀取";
        string readTagLog = "";
        Boolean autoReadclick = false;
        int chinDefault = 0;
        bool errorDialogIsShow = false;


        string IP = "";
        string DBName = "";
        string Ac = "";
        string Pw = "";
        string tableName = "";
        string tableID = "";
        string tableSumID = "";
        string dateField = "";
        string timeField = "";
        string tagField = "";
        string resultField = "";

        string tableName2 = "";
        string tableID2 = "";
        string dateField2 = "";
        string timeField2 = "";
        string statusField2 = "";

        bool remenber = true;
        
        //  Language LgDefault = new Language();
        //LgDefault
        List<SerialPort> port = new List<SerialPort>();
        List<text_data_list> InsertDataList = new List<text_data_list>();

        List<Search_data_list> SearchTagDataList = new List<Search_data_list>();
        static Timer autoLoadTagID = new Timer();
      //  MySqlConnect DB = new MySqlConnect();
        public Form1()
        {
            
            InitializeComponent();
            ComPortBaudrate.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            ConnectStatus.Text = ConnectStatusDefault;
            comboBox1.SelectedIndex = 1;
            button4.Visible=false;
            ComPortList.Visible = false;
            button1.Visible = false;
            ComPortBaudrate.Visible = false;
            button6.Visible = false;
            autoLoadTagID.Interval = 100;
            autoLoadTagID.Tick += new EventHandler(autoLoadTagIDTimer);
            
         /*   string str = System.AppDomain.CurrentDomain.BaseDirectory;
            string txtName = str + "test.txt";


            using (StreamReader sr = File.OpenText(txtName))
            {
                //listBox_Battery.Items.Clear();


                String input;


                int j = 0;
                while ((input = sr.ReadLine()) != null)
                {
                    Console.WriteLine("ddd"+j);
                    if (j == 0)
                    {
                         IP = input;
                    }
                    else if (j == 1)
                    {
                        DBName = input;
                    }
                    else if (j == 2)
                    {
                         Ac = input;
                    }
                    else if (j == 3)
                    {
                         Pw = input;
                    }
                    else if (j == 4)
                    {
                         tableName = input;
                    }
                    else if (j == 5)
                    {
                        tableID = input;
                    }
                    else if (j == 6)
                    {
                        tableSumID = input;
                    }
                    else if (j == 7)
                    {
                         dateField = input;
                    }
                    else if (j == 8)
                    {
                         timeField = input;
                    }
                    else if (j == 9)
                    {
                         tagField = input;
                    }
                    else if (j == 10)
                    {
                         resultField = input;
                    }
                    else if (j == 11)
                    {
                        tableName2 = input;
                    }
                    else if (j == 12)
                    {
                        tableID2 = input;
                    }
                    else if (j == 13)
                    {
                        dateField2 = input;
                    }
                    else if (j == 14)
                    {
                        timeField2 = input;
                    }
                    else if (j == 15)
                    {
                        statusField2 = input;
                    }

                    j++;
                }
                sr.Close();
            }

            if (InputBox( IP, DBName, Ac,Pw,tableName, tableID, tableSumID, dateField,timeField,tagField,resultField, tableName2, tableID2, dateField2, timeField2, statusField2) == DialogResult.OK)
            {
                if (remenber)
                {
                    string path = System.AppDomain.CurrentDomain.BaseDirectory;
                    string txtNamePath = str + "test.txt";
                    StreamWriter sw = new StreamWriter(txtNamePath, false, System.Text.Encoding.Default);
                    sw.Write(IP);
                    sw.WriteLine();
                    sw.Write(DBName);
                    sw.WriteLine();
                    sw.Write(Ac);
                    sw.WriteLine();
                    sw.Write(Pw);
                    sw.WriteLine();
                    sw.Write(tableName);
                    sw.WriteLine();
                    sw.Write(tableID);
                    sw.WriteLine();
                    sw.Write(tableSumID);
                    sw.WriteLine();
                    sw.Write(dateField);
                    sw.WriteLine();
                    sw.Write(timeField);
                    sw.WriteLine();
                    sw.Write(tagField);
                    sw.WriteLine();
                    sw.Write(resultField);
                    sw.WriteLine();
                    sw.Write(tableName2);
                    sw.WriteLine();
                    sw.Write(tableID2);
                    sw.WriteLine();
                    sw.Write(dateField2);
                    sw.WriteLine();
                    sw.Write(timeField2);
                    sw.WriteLine();
                    sw.Write(statusField2);
                    sw.WriteLine();
                    //清空緩衝區
                    sw.Flush();
                    //關閉流
                    sw.Close();
                }
              //  DB.Initialize(IP, DBName, Ac, Pw, tableName, tableID, tableSumID, dateField, timeField, tagField, resultField, tableName2, tableID2, dateField2, timeField2, statusField2);
            }*/

            //button6.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadComport();
        }


        //取得Comport
        private void LoadComport()
        {
            List<string> port = Load_AllComPortName();
            string s = button1.Text;
            ComPortList.Items.Clear();
            if (port.Count == 0)
                return;

            int index = 0;
            for (int i = 0; i < port.Count; i++)
            {
                ComPortList.Items.Add(port[i]);
                if (port[i] == s) index = i;
            }

            ComPortList.SelectedIndex = index;
        }
        

        public List<string> Load_AllComPortName()
        {
            List<string> strs = new List<string>();
            string[] portNames = SerialPort.GetPortNames();
            for (int i = 0; i < (int)portNames.Length; i++)
            {
                strs.Add(portNames[i]);
            }
            return strs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //連接Comport
            if (port.Count==0)
            {
               // port = null;
                List<string> portList = Load_AllComPortName();
                for (int sd=0;sd< portList.Count;sd++) {
                    SerialPort ddd = new SerialPort(portList[sd].ToString(), 19200, Parity.None, 8, StopBits.One);
                    ddd.DataReceived += new SerialDataReceivedEventHandler(port1_DataReceived);
                    port.Add(ddd);
                }
//port = new SerialPort(ComPortList.Text, Convert.ToInt32(ComPortBaudrate.Text), Parity.None, 8, StopBits.Two);
              //  port.DataReceived += new SerialDataReceivedEventHandler(port1_DataReceived);


            }
            else if (!port[0].IsOpen)
            {
                port.Clear();
                List<string> portList = Load_AllComPortName();
                for (int sd = 0; sd < portList.Count; sd++)
                {
                    SerialPort ddd = new SerialPort(portList[sd],19200, Parity.None, 8, StopBits.One);
                    
                    ddd.DataReceived += new SerialDataReceivedEventHandler(port1_DataReceived);
                    port.Add(ddd);
                }
                // port = null;
                // port = new SerialPort(ComPortList.Text, Convert.ToInt32(ComPortBaudrate.Text), Parity.None, 8, StopBits.Two);
                //port.DataReceived += new SerialDataReceivedEventHandler(port1_DataReceived);
            }


            if (!port[0].IsOpen)
            {
                try
                {
                    for (int sd = 0; sd < port.Count; sd++)
                    {
                        port[sd].Open();
                    }
                        
                }
                catch (Exception ex)
                {
                    autoLoadTagID.Stop();
                    autoRead.Text = autoLoad;
                    autoRead.ForeColor = Color.Black;
                    autoReadclick = false;
                    Console.WriteLine("ex" + ex);
                    for (int sd = 0; sd < port.Count; sd++)
                    {
                        port[sd].Dispose();
                        port[sd].Close();
                    }
                    
                    // MessageBox.Show("串口出問題請重新啟動程式");
                }
            }
            else
            {
                autoLoadTagID.Stop();
                autoRead.Text = autoLoad;
                autoRead.ForeColor = Color.Black;
                autoReadclick = false;
                for (int sd = 0; sd < port.Count; sd++)
                {
                    port[sd].Dispose();
                    port[sd].Close();
                }
            }
            if (port[0].IsOpen == true)
            {
                ConnectStatus.Text = ConnectStatusS;
                button2.Text = ConnectButtonNL;
                ConnectStatus.ForeColor = Color.Green;
                isConnect = true;
            }
            else
            {
                ConnectStatus.Text = ConnectStatusF;
                button2.Text = ConnectButtonL;
                ConnectStatus.ForeColor = Color.Red;
                isConnect = false;
                autoLoadTagID.Stop();
                autoRead.Text = autoLoad;
                autoRead.ForeColor = Color.Black;
                autoReadclick = false;
                for (int sd = 0; sd < port.Count; sd++)
                {
                    port[sd].Dispose();
                    port[sd].Close();
                }
            }
        }

        private void port1_DataReceived(object sender, EventArgs e)
        {

            Console.WriteLine("port1_DataReceived");

            List<byte> packet = new List<byte>();
            for (int i =0;i<port.Count;i++)
            {
                while (port[i].BytesToRead != 0)
                {

                    packet.Add((byte)port[i].ReadByte());


                }
            }

            if (MyMessageBox.Showed == true)
            {
                SoundPlayer Player = new SoundPlayer();
                string str = System.AppDomain.CurrentDomain.BaseDirectory;
                string filename = str + "error1.wav";
                packet.Clear();
                Player.SoundLocation = @filename;
                Player.Play();
                return;
            }
            byte[] bArrary = packet.ToArray();

            for (int i=0;i<bArrary.Length;i++)
            {
         //       Console.WriteLine("bArrary[i]" + bArrary[i]);
            }
            if (bArrary != null && bArrary.Length>3)
            {
                Console.WriteLine("errorDialogIsShow" + errorDialogIsShow);

                    InvokeGridViewAddRows(bArrary);
               
               
            }
            
            /*if (bArrary[bArrary.Length - 1] == 0x03)//比對結尾碼
            {
                foreach (DataGridViewRow dr in this.dataGridView1.Rows)
                {
                    if (dr.Cells[1].Value != null)
                    {
                        dr.Cells[1].Style.ForeColor = Color.BlueViolet;
                    }


                }


                //Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextStringDelegate(DisplayTextString), packet.ToArray());
            }*/

    }

        int aaa = 0;

        private void InvokeGridViewAddRows(byte[] bArrary)
        {

            try {
                if (this.InvokeRequired)
                {
                    //   Console.WriteLine("InvokeRequired");
                    GridViewAddRows addRows = new GridViewAddRows((InvokeGridViewAddRows));

                    this.Invoke(addRows, bArrary);


                }
                else
                {
                    // 在這裡寫入原本取到str後要對dataGridView做的事
                    //     Console.WriteLine(bArrary.Length);

                    
 
                    string stringresult = ReceviewDataChange(bArrary);

                    if (stringresult==null)
                        return;

                        bool isAdd = true;
                    //if (bArrary[2].ToString() == "22" && bArrary[3] < 128&& readTagLog!= stringresult)
                    //     if (bArrary[2].ToString() == "22" && bArrary[3] < 128)
                    //     {

                    //   byte[] commEnter = new byte[] { 0x07, 0x01, 0x14, 0x01,0x14,0x14,0xBA};   ///buf讀取
                    //    for (int i = 0; i < port.Count; i++)
                    //    {
                    //        port[i].Write(commEnter, 0, commEnter.Length);
                    //   }

                    // readTagLog = stringresult;
                     stringresult = stringresult.ToUpper();



                    if (selectChinTag.Checked)
                        {
                            bool isNull = false;
                            string txtName =null;
                            for (int i = 0; i < SearchTagDataList.Count; i++)
                            {
                                for (int a = 0; a < SearchTagDataList[i].txtData.Count; a++)
                                {
                                    if (SearchTagDataList[i].txtData[a] == stringresult)
                                    {
                                        isNull = true;
                                        if (txtName!=null)
                                            txtName = txtName + " , ";
                                        else
                                            txtName = txtName + SearchTagDataList[i].txtName;
                                    }
                                }
                            }

                            if (isNull)
                            {
                               /* dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = dataGridView1.Rows.Count.ToString();
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = DateTime.Now.ToString("yyyy/MM/dd");
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = DateTime.Now.ToString("HH: mm: ss");
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value =  stringresult;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = txtName;
                                */
                                string[] row1 = new string[] { dataGridView1.Rows.Count.ToString(), DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH: mm: ss"), stringresult, txtName };
                                dataGridView1.Rows.Add(row1);
                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[1];
                        }
                            else
                            {
                                string[] row1 = new string[] { dataGridView1.Rows.Count.ToString(), DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH: mm: ss"), stringresult, "null" };
                                dataGridView1.Rows.Add(row1);
                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[1];
                        }
                               
                           
                            return;
                        }

                        text_data_list data = new text_data_list();

                        /*if (InsertDataList.Count == 0)
                        {
                            MessageBox.Show(txtImportHeader, txtImportContent, MessageBoxButtons.OK);
                            return;
                        }*/
                  /*      if (dataGridView1.Rows.Count == 1)
                        {
                            chinDefault = DB.InsertChin();
                        }*/


                 /*       List<text_data_list>  result = DB.selectdoublecheck(stringresult);
                        if (result.Count != 0)
                        {
                            isAdd = false;
                        }
                        else
                        {
                            isAdd = true;
                        }
                        */

                              foreach (DataGridViewRow dr in this.dataGridView1.Rows)
                              {
                                  if (dr.Cells[3].Value != null && dr.Cells[3].Value.ToString() == stringresult)
                                  {


                                      isAdd = false;


                                  }

                              }
                            if (!isAdd)
                        {
                            //  dr.Cells[0].Selected = true;
                            SoundPlayer Player = new SoundPlayer();
                            string str = System.AppDomain.CurrentDomain.BaseDirectory;
                            string filename = str + "error1.wav";

                            Player.SoundLocation = @filename;
                            Player.Play();
                            data.date = DateTime.Now.ToString("yyyy/MM/dd");
                            data.time = DateTime.Now.ToString("HH:mm:ss");
                            data.TagID = stringresult;
                            string[] row1 = new string[] { dataGridView1.Rows.Count.ToString(), DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH: mm: ss"), stringresult };
                            dataGridView1.Rows.Add(row1);
                        }
                        else
                        {
                            data.date = DateTime.Now.ToString("yyyy/MM/dd");
                            data.time = DateTime.Now.ToString("HH:mm:ss");
                            data.TagID = stringresult;
                            string[] row1 = new string[] { dataGridView1.Rows.Count.ToString(), DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH: mm: ss"), stringresult };
                            dataGridView1.Rows.Add(row1);
                        }

                        bool isNULL = true;
                        for (int i = 0; i < InsertDataList.Count; i++)
                        {
                            if (InsertDataList[i].TagID == stringresult)
                            {
                                isNULL = false;

                            }
                        }


                        if (isNULL)
                        {
                            SoundPlayer Player = new SoundPlayer();
                            string str = System.AppDomain.CurrentDomain.BaseDirectory;
                            string filename = str + "error1.wav";

                            Player.SoundLocation = @filename;
                            Player.Play();




                            /*  if (errorResult == DialogResult.Yes)
                              {

                                  Write(txtPath, stringresult);
                                  text_data_list data = new text_data_list();
                                  data.date = DateTime.Now.ToString("yyyy/MM/dd");
                                  data.time = DateTime.Now.ToString("HH: mm: ss");
                                  data.TagID = stringresult;
                                  InsertDataList.Add(data);

                                  //   return;
                             }else
                              {
                                  dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                                  dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.ForeColor = Color.White;
                                  dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Value = TagNotDB;
                              }
                              */
                        }
                   /*     else
                        {
                            isAdd = true;
                            foreach (text_data_list aaa in result)
                            {
                                if (aaa.result == TagIsOK)
                                {
                                    isAdd = false;
                                }
                            }
                            
                        }*/


                        if (isAdd && isNULL)
                        {


                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.ForeColor = Color.White;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Value = TagNotDB;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Style.ForeColor = Color.Red;
                            data.result = TagNotDB;
                            label5.Text = count(label5.Text);
                            MyMessageBox.Show(NotDBContent);

                            /*errorDialogIsShow = true;
                             * DialogResult errorResult = MessageBox.Show(NotDBContent, NotDBHeader, MessageBoxButtons.OK);
                             if (errorResult == DialogResult.OK)
                             {
                                 errorDialogIsShow = false;
                             }*/


                        }
                        else if (isAdd && !isNULL)
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Value = TagIsOK;
                            data.result = TagIsOK;
                            label2.Text = count(label2.Text);
                        }
                        else if (!isAdd && isNULL)
                        {

                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.ForeColor = Color.White;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Value = TagNotDB + "、" + TagMore;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Style.ForeColor = Color.Gray;
                            data.result = TagNotDB + "、" + TagMore;
                            label3.Text = count(label3.Text);
                            MyMessageBox.Show(NotDBContent + MoreContent);
                            /*  errorDialogIsShow = true;
                              DialogResult errorResult = MessageBox.Show(NotDBContent + MoreContent, NotDBHeader + MoreHeader, MessageBoxButtons.OK);
                              if (errorResult == DialogResult.OK)
                              {
                                  errorDialogIsShow = false;
                              }*/

                        }
                        else if (!isAdd && !isNULL)
                        {

                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = SystemColors.Highlight;
                            dataGridView1.Rows[dataGridView1.RowCount - 2].DefaultCellStyle.ForeColor = Color.White;
                            dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[4].Value = TagMore;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Style.ForeColor = Color.Blue;
                            data.result = TagMore;
                            label4.Text = count(label4.Text);
                            MyMessageBox.Show(MoreContent);
                            /* errorDialogIsShow = true;
                             DialogResult errorResult = MessageBox.Show(MoreContent, MoreHeader, MessageBoxButtons.OK);
                             if (errorResult == DialogResult.OK)
                             {
                                 errorDialogIsShow = false;
                             }*/

                        }
                        List<text_data_list> chin = new List<text_data_list>();
                        chin.Add(data);
                       // DB.Insert(chin, chinDefault);
                        // stringresult = " test8" + aaa.ToString();
                        // aaa++;



                        /*    Console.WriteLine("stringresult" + stringresult);
                            string real = "";
                            for (int i = 1; i < stringresult.Length; i++)
                            {
                                Console.WriteLine("stringresult[i]" + stringresult[i]);
                                real = real + stringresult[i];
                            }*/



                        /*MySqlConnect DB = new MySqlConnect();

                       List<string> selectResult = DB.Select(stringresult);


                        if (selectResult.Count == 0)
                        {

                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.ForeColor = Color.White;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Value = TagNotDB;

                    }*/

                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[1];

             //   }

            }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "error",MessageBoxButtons.OK);
            }
           
        }


         private string ReceviewDataChange(byte[] data)
          {

             // byte[] datas = {0x00,0x00,0x11,0x00,0x71,0xF7,0x91,0x95,0xB7,0x0A,0x42,0xD6,0x00,0x00,0x00,0x12 };
              string result = null;
              string id = null;

              if (data[2] == 22 && data[3] > 128)
              {
                  readTagLog = "";
              }
              if (data[2] == 22&& data[3]<128)
              {

                  for (int i = 4; i < data.Length - 1; i++)
                  {

                      string dataTostrong = Convert.ToString(data[i], 16);
                      if (dataTostrong.Length < 2)
                      {
                          dataTostrong = "0" + dataTostrong ;
                      }

                      result = result + dataTostrong;
                      // result = result + Convert.ToString(data[i], 16);
                      //    Console.WriteLine("data[i]:" + data[i] +"  result:" + result);
                  }
              }


              return result;
          }

    /*    private string ReceviewDataChange(byte[] data)
        {
            string result = null;
            string id = null;
            for (int i = (int)data.Length - 3; i > 0; i--)
            {
           //     Console.WriteLine("data[i]" + data[i]);
                string dataTostrong = "";
                if (data[i] != 0)
                    dataTostrong = Convert.ToString((char)data[i]);
                else
                    dataTostrong = "00";
              //  Console.WriteLine("dataTostrong"+ dataTostrong);
             //   Console.WriteLine(string.Concat(new object[] { "data[i]:", data[i], "    dataTostrong:", dataTostrong }));
                result = string.Concat(result, dataTostrong);
            }
        //    Console.WriteLine("fd========="+ result);
            for (int i = 0; i < result.Length; i += 2)
            {
             //   Console.WriteLine(string.Concat("cahange", result.Substring(i + 1, 1), result.Substring(i, 1)));
                id = string.Concat(id, result.Substring(i + 1, 1), result.Substring(i, 1));
            }
            return id;
        }*/


        private void button3_Click(object sender, EventArgs e)
        {


            if (dataGridView1.RowCount > 1) {
                DialogResult data =  MessageBox.Show(dataGridClearContent,dataGridClearHeader,MessageBoxButtons.YesNoCancel);
                if (data == DialogResult.Yes)
                {
                    dataGridView1.Rows.Clear();
                }
                else if (data == DialogResult.Cancel)
                {
                    return;
                }

            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;    


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               // InsertDataList.Clear();
                //List<text_data_list> dataList = new List<text_data_list>();
                txtPath = openFileDialog1.FileName;
                string file = Path.GetExtension(openFileDialog1.FileName);
                Console.WriteLine("file:" + file);
                if (file == ".txt")
                {
                    using (StreamReader sr = File.OpenText(openFileDialog1.FileName))
                    {
                        //listBox_Battery.Items.Clear();


                        String input;


                        int j = 0;
                        while ((input = sr.ReadLine()) != null)
                        {
                            text_data_list data = new text_data_list();
                            data.date = DateTime.Now.ToString("yyyy/MM/dd");
                            data.time = DateTime.Now.ToString("HH: mm: ss");
                            data.TagID = input;
                            InsertDataList.Add(data);
                            //Console.WriteLine("S" + input);
                        }
                        sr.Close();
                        MessageBox.Show(ImportDialogContent, ImportDialogHeader);
                    }
                }
                else if (file == ".xlsx")
                {
                    Console.WriteLine("XXLSX");
                    mExcelData dd = new mExcelData();
                    DataTable a  = dd.GetExcelDataTable(openFileDialog1.FileName, "select * from[Sheet1$]");
                    foreach (DataRow dr in a.Rows)
                    {
   
                           if (dr[0] != null)
                           {
                              // Console.WriteLine("S"+ dr[0].ToString());
                               text_data_list data = new text_data_list();
                               data.date = DateTime.Now.ToString("yyyy/MM/dd");
                               data.time = DateTime.Now.ToString("HH: mm: ss");
                               data.TagID = dr[0].ToString();
                               InsertDataList.Add(data);
                           }
                    }
                    MessageBox.Show(ImportDialogContent, ImportDialogHeader);
                }
                
               // MySqlConnect DB = new MySqlConnect();
                
              //  DB.Insert(dataList, Path.GetFileNameWithoutExtension(openFileDialog1.FileName));

            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            /*
            MySqlConnect DB = new MySqlConnect();

            if (dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[3].Value == null)
                return;

            List<string> selectResult = DB.Select(dataGridView1.Rows[dataGridView1.Rows.Count-2].Cells[3].Value.ToString());


            if (selectResult.Count > 0)
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.PaleVioletRed;
            */
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value == null)
                return;

            
          //  List<string> selectResult = DB.Select(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString());

          //  Console.WriteLine("selectResult"+ selectResult);
      //      if (selectResult.Count == 0)
       //     {
         //       dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor = Color.PaleVioletRed;
           // }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            int index = dataGridView1.CurrentRow.Index;
        //    Console.WriteLine("index" + index);

            dataGridView1.Rows[index].Selected = true;
            if (dataGridView1.Rows[index].Cells[0].Value == null)
                return;

            dataGridView1.Rows.RemoveAt(index);
            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[1];
            if (dataGridView1.Rows.Count == 1)
                return;
            int num = 1;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = num;
                num++;
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Selected)
            {
                using (Pen pen = new Pen(Color.Black))
                {
                    int penWidth = 2;

                    pen.Width = penWidth;

                    int x = e.RowBounds.Left + (penWidth / 2);
                    int y = e.RowBounds.Top + (penWidth / 2);
                    int width = e.RowBounds.Width - penWidth;
                    int height = e.RowBounds.Height - penWidth;

                    e.Graphics.DrawRectangle(pen, x, y, width, height);
                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            int Index = 0;
            if (dataGridView1.CurrentRow != null)
            {
                Index = dataGridView1.CurrentRow.Index;
            }


         //   Console.WriteLine(dataGridView1.Rows[Index].DefaultCellStyle.SelectionBackColor + "ddddddddd" + dataGridView1.Rows[Index].DefaultCellStyle.BackColor);
            dataGridView1.Rows[Index].DefaultCellStyle.SelectionBackColor = dataGridView1.Rows[Index].DefaultCellStyle.BackColor;
            dataGridView1.Rows[Index].DefaultCellStyle.SelectionForeColor = dataGridView1.Rows[Index].DefaultCellStyle.ForeColor;
         //   Console.WriteLine(dataGridView1.Rows[Index].DefaultCellStyle.SelectionBackColor + "ddddddddd" + dataGridView1.Rows[Index].DefaultCellStyle.BackColor);

            /*     Console.WriteLine("Color:" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor);
                 if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Blue)
                 {
                     dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
                     Console.WriteLine("LightBlue"+ dataGridView1.DefaultCellStyle.SelectionBackColor);
                 }
                 else
                 {

                     Console.WriteLine("LightGray"+ dataGridView1.DefaultCellStyle.SelectionBackColor);
                 }*/
        }
        int ia = 0;
        private void button5_Click_1(object sender, EventArgs e)
        {
         
            //string[] row1 = new string[] { dataGridView1.Rows.Count.ToString(), DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH: mm: ss"), "44444444444"+ ia };
           // dataGridView1.Rows.Add(row1);



            /*    Console.WriteLine("stringresult" + stringresult);
                string real = "";
                for (int i = 1; i < stringresult.Length; i++)
                {
                    Console.WriteLine("stringresult[i]" + stringresult[i]);
                    real = real + stringresult[i];
                }*/
         /*   bool isNULL = false;
            for (int i = 0; i < InsertDataList.Count; i++)
            {
                if (InsertDataList[i].TagID == "44444444444"+ ia)
                {
                    isNULL = true;

                }
            }

            if (!isNULL)
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.ForeColor = Color.White;
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[4].Value = TagNotDB;
            }
            ia=ia+1;*/


            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HHmmss");
            saveFileDialog1.FileName = @""+date+ time+"imgone.CSV";

            saveFileDialog1.Filter = "Execl files (*.CSV)|*.CSV";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                //獲得文件路徑
               string  localFilePath = saveFileDialog1.FileName.ToString();


                //獲取文件名，不帶路徑
               string [] fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1).Split('.');


                //獲取文件路徑，不帶文件名
               string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                string txtName = FilePath + "/" + fileNameExt[0];
                aaaa(saveFileDialog1.FileName, txtName);
            }

        }
        private void aaaa(string FileName,string txtName)
        {
            string excelName = FileName;
            mExcelData mExcelData = new mExcelData();
        List<excelList> items = new List<excelList>();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                excelList item = new excelList();
                if (dr.Cells[3].Value != null)
                {
                    if (dr.Cells[0].Value != null)
                        item.index = dr.Cells[0].Value.ToString();
                    if (dr.Cells[1].Value != null)
                        item.date = dr.Cells[1].Value.ToString();
                    if (dr.Cells[2].Value != null)
                        item.time = dr.Cells[2].Value.ToString();
                    if (dr.Cells[3].Value != null)
                        item.tagID = dr.Cells[3].Value.ToString();
                    if(dr.Cells[4].Value != null)
                        item.remark = dr.Cells[4].Value.ToString();

                    items.Add(item);
                }

            }

            /*    //建立 xlxs 轉換物件
                SMC5052B.XSLXHelper helper = new SMC5052B.XSLXHelper();
                //取得轉為 xlsx 的物件
                var xlsx = helper.Export(items);

                //存檔至指定位置
                xlsx.SaveAs(FileName);*/
            mExcelData.ExportDataGridViewToCsv(dataGridView1,InsertDataList,excelName, txtName);
        //    mExcelData.SaveExcelFile(excelName, items);
        //    bool excelresult = mExcelData.ExportDataGridview(dataGridView1, false, excelName);
        //    DB.chinComplete();
     /*       if (excelresult)
            {
                int totalcount = dataGridView1.Rows.Count;
                for (int i = 0; i < totalcount - 1; i++)
                {
                    Console.WriteLine("i:" + i);
                    TagID.Add(dataGridView1.Rows[0].Cells[3].Value.ToString());
                    reader_data_list Data = new reader_data_list();
                    Data.date = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    Data.time = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    Data.TagID = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    readerData.Add(Data);
                    //dataGridView1.Rows.RemoveAt(0);
                    InvokeGridViewRemoveRows(0);
                }


                List<string> selectResult = DB.Select(TagID);


                int count = 1;
                foreach (DataGridViewRow dr in this.dataGridView1.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {

                        dr.Cells[0].Value = count;
                        count++;
                    }

                }



                if (selectResult != null)
                {
                    string errorTagID = null;
                    for (int a = 0; a < selectResult.Count; a++)
                    {
                        if (errorTagID != null)
                            errorTagID = errorTagID + ",";

                        errorTagID = errorTagID + selectResult[a];
                    }
                    DialogResult result = MessageBox.Show(excelName + "\n" + errorTagID, "重複資料", MessageBoxButtons.YesNo);

                }


                bool InsertResult = DB.Insert(readerData, excelName);
                if (InsertResult)
                {
                    Console.WriteLine("新增成功");
                }

            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Language aaaa = new Language();
            LanguageData LD = new LanguageData();
            if (comboBox1.SelectedIndex == 0)
            {


                LD = aaaa.LanguageTCN();
                if (ConnectStatus.Text == "未連線" || ConnectStatus.Text == "Not Connect"|| ConnectStatus.Text == "未连线")
                {
                    ConnectStatus.Text = "未連線";
                }

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                LD = aaaa.LanguageSCN();

                if (ConnectStatus.Text == "未連線" || ConnectStatus.Text == "Not Connect" || ConnectStatus.Text == "未连线")
                {
                    ConnectStatus.Text = "未连线";
                }
            }

            else if (comboBox1.SelectedIndex == 2)
            {
                LD = aaaa.LanguageEN();

                if (ConnectStatus.Text == "未連線" || ConnectStatus.Text == "Not Connect" || ConnectStatus.Text == "未连线")
                {
                    ConnectStatus.Text = "Not Connect";
                }
            }

            button1.Text = LD.button1;
            button2.Text = LD.button2;
            button3.Text = LD.button3;
            button4.Text = LD.button4;
            button5.Text = LD.button5;

            label2.Text = noCount(label2.Text, LD.label2);
            label5.Text = noCount(label5.Text, LD.label5);
            label4.Text = noCount(label4.Text, LD.label4);
            label3.Text = noCount(label3.Text, LD.label3);
            if (autoReadclick)
                autoRead.Text = LD.autoReadStop;
            else
                autoRead.Text = LD.autoRead;

            autoLoad = LD.autoRead;
            autoLoadStop = LD.autoReadStop;
            Column1.HeaderText = LD.Column1;
            Column2.HeaderText = LD.Column2;
            Column3.HeaderText = LD.Column3;
            Column4.HeaderText = LD.Column4;
            Column5.HeaderText = LD.Column5;
            TagMore = LD.TagMore;
            TagNotDB = LD.TagNotDB;
            ConnectButtonL = LD.ConnectButtonL;
            ConnectButtonNL = LD.ConnectButtonNL;
            ConnectStatusS = LD.ConnectStatusS;
            ConnectStatusF = LD.ConnectStatusF;
            NotDBHeader = LD.NotDBHeader;
            NotDBContent = LD.NotDBContent;
            MoreHeader = LD.MoreHeader;
            MoreContent = LD.MoreContent;
            dataGridClearHeader = LD.dataGridClearHeader;
            dataGridClearContent = LD.dataGridClearContent;
            ImportDialogHeader = LD.ImportDialogHeader;
            exitHeader = LD.exitHeader;
            exitContent = LD.exitContent;
            ImportDialogContent = LD.ImportDialogContent;
            txtImportHeader = LD.txtImportHeader;
            txtImportContent = LD.txtImportContent;
            TagIsOK = LD.TagIsOK;
            checkChinContent = LD.checkChinContent;
            checkChinHeader = LD.checkChinHeader;
            selectChinTag.Text = LD.selectChinTag;
        }


       public void Write(string path,string stringresult)
 {

            //  FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(path, true);
            //開始寫入 
            sw.WriteLine();
            sw.Write(stringresult);
            //清空緩衝區
            sw.Flush();
 //關閉流
 sw.Close();
 //fs.Close();
 }


        private void testtxtsave_Click(object sender, EventArgs e)
        {
            //Write(txtPath);
        }

        private void button6_Click(object sender, EventArgs e)
        {

                


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HHmmss");
            saveFileDialog1.FileName = @"" + date + time + "imgone.CSV";

            saveFileDialog1.Filter = "Execl files (*.CSV)|*.CSV";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                //獲得文件路徑
                string localFilePath = saveFileDialog1.FileName.ToString();


                //獲取文件名，不帶路徑
                string[] fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1).Split('.');


                //獲取文件路徑，不帶文件名
                string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                string txtName = FilePath + "/" + fileNameExt[0];
                aaaa(saveFileDialog1.FileName, txtName);
            }

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            SoundPlayer Player = new SoundPlayer();
            string str = System.AppDomain.CurrentDomain.BaseDirectory;
            string filename = str + "error1.wav";

            Player.SoundLocation = @filename;
            Player.Play();
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            byte[] commEnter = new byte[] { 0x10, 0x01, 0x16, 0x00, 0x05, 0x4F, 0x03, 0xB4, 0x07, 0xC2, 0xE3, 0xD0, 0x00, 0x00, 0x00,0x51 };   ///buf讀取
           string aaa =  ReceviewDataChange(commEnter);

          /*  Console.WriteLine("---------------"+ port.Count);
           //  byte[] commEnter = new byte[] {0x04, 0x01, 0x15, 0xE5 };  //version
           // byte[] commEnter = new byte[] {0x04, 0x01, 0x16, 0xE4 };  //UID
            byte[] commEnter = new byte[] { 0x04, 0x01, 0x12, 0xE8 };   ///buf讀取
           // byte[] commEnter = new byte[] { 0x05, 0x01, 0x16, 0xF3,0xF0 };   ///buf讀取
            for (int i = 0; i < port.Count; i++)
            {
                port[i].Write(commEnter, 0, commEnter.Length);
            }*/
            string[] row1 = new string[] { dataGridView1.Rows.Count.ToString(), DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH: mm: ss"), aaa};
            dataGridView1.Rows.Add(row1);


        }

        private string count(string dd)
        {
           int count =  Convert.ToInt32(dd.Split(':')[1])+1;
            string result = dd.Split(':')[0] +":  "+ count;
            return result;
        }

        private string noCount(string dd,string header)
        {
            int count = Convert.ToInt32(dd.Split(':')[1]);
            string result = header + ":  " + count;
            return result;
        }

        public  class MyMessageBox
        {

            public static bool Showed { get; set; }
            public delegate void OnOK();

            public static event OnOK OnOkClick;

            public static void Show(string text)
            {
                System.Threading.ParameterizedThreadStart pT = new System.Threading.ParameterizedThreadStart(InvokeShow);
                pT.BeginInvoke(text, null, null);
            }

            public static void InvokeShow(object text)
            {
                Showed = true;
                if (MessageBox.Show(text.ToString()) == DialogResult.OK)
                {
                    Showed = false;
                    if (OnOkClick != null)
                        OnOkClick();

                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public DialogResult InputBox(string ip,string db,string Ac,string pw,string table, string tableID, string tableSumID, string dateF,string timeF,string tag,string result,string table2, string tableID2, string dateF2, string timeF2, string statusF2)
        {
            Form form = new Form();
            Label labelIP = new Label();
            Label labelDB = new Label();
            Label labelA = new Label();
            Label labelP = new Label();


            Label labelTable = new Label();
            Label labelDF = new Label();
            Label labelTT = new Label();
            Label labelTID = new Label();
            Label labelRF = new Label();
            Label labelIDF = new Label();
            Label labelSumIDF = new Label();

            Label labelTable2 = new Label();
            Label labelIDF2 = new Label();
            Label labelDF2 = new Label();
            Label labelTT2 = new Label();
            Label labelSF2 = new Label();

            TextBox textBoxIP = new TextBox();
            TextBox textBoxDB = new TextBox();
            TextBox textBoxA = new TextBox();
            TextBox textBoxP = new TextBox();


            TextBox textBoxTable = new TextBox();
            TextBox textBoxDF = new TextBox();
            TextBox textBoxTT= new TextBox();
            TextBox textBoxTID= new TextBox();
            TextBox textBoxIDF = new TextBox();
            TextBox textBoxSumIDF = new TextBox();
            TextBox textBoxRF = new TextBox();

            TextBox textBoxTable2 = new TextBox();
            TextBox textBoxIDF2 = new TextBox();
            TextBox textBoxDF2 = new TextBox();
            TextBox textBoxTT2 = new TextBox();
            TextBox textBoxSF2 = new TextBox();

            GroupBox database = new GroupBox();
            GroupBox chinReader = new GroupBox();
            GroupBox chinDetail = new GroupBox();

            CheckBox remenber = new CheckBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = "資料庫";
          //  label.Text = promptText;    
            //textBox.Text = value;


            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            

            buttonOk.SetBounds(180, 250, 75, 23);
            buttonCancel.SetBounds(309, 350, 75, 23);
            remenber.Checked = true;
            remenber.SetBounds(60, 250, 75, 23);
            remenber.Text="記住設定";
            remenber.CheckedChanged += new EventHandler(remenber_checkedChange);

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(450, 280);

            form.ClientSize = new Size(Math.Max(450, labelIP.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            // form.CancelButton = buttonCancel;
            database.SetBounds(10, 20, 150, 160);
            database.Size = new Size(150,160);

            labelIP.SetBounds(19, 35, 30, 13);
            labelIP.AutoSize = true;
            labelIP.Text = "IP:";
            textBoxIP.Text = ip;
            textBoxIP.SetBounds(59, 30, 80, 20);
            textBoxIP.Anchor = textBoxIP.Anchor | AnchorStyles.Right;
            textBoxIP.TextChanged += new EventHandler(textBoxIP_TextChanged);


            labelDB.SetBounds(19, 65, 30, 13);
            labelDB.AutoSize = true;
            labelDB.Text = "資料庫";
            textBoxDB.Text = db;
            textBoxDB.SetBounds(59, 60, 80, 20);
            textBoxDB.Anchor = textBoxDB.Anchor | AnchorStyles.Right;
            textBoxDB.TextChanged += new EventHandler(textBoxDB_TextChanged);

            labelA.SetBounds(19, 95, 30, 13);
            labelA.AutoSize = true;
            labelA.Text = "帳號:";
            textBoxA.Text = Ac;
            textBoxA.SetBounds(59, 90, 80, 20);
            textBoxA.Anchor = textBoxA.Anchor | AnchorStyles.Right;
            textBoxA.TextChanged += new EventHandler(textBoxA_TextChanged);

            labelP.SetBounds(19, 125, 30, 13);
            labelP.AutoSize = true;
            labelP.Text = "密碼:";
            textBoxP.Text = pw;
            textBoxP.SetBounds(59, 120, 80, 20);
            textBoxP.Anchor = textBoxP.Anchor | AnchorStyles.Right;
            textBoxP.TextChanged += new EventHandler(textBoxP_TextChanged);

            labelTable.SetBounds(149, 35, 30, 13);
            labelTable.AutoSize = true;
            labelTable.Text = "table";
            textBoxTable.Text = table;
            textBoxTable.SetBounds(189, 30, 80, 20);
            textBoxTable.Anchor = textBoxTable.Anchor | AnchorStyles.Right;
            textBoxTable.TextChanged += new EventHandler(textBoxTable_TextChanged);

            labelDF.SetBounds(149, 65, 30, 13);
            labelDF.AutoSize = true;
            labelDF.Text = "日期";
            textBoxDF.Text = dateF;
            textBoxDF.SetBounds(189, 60, 80, 20);
            textBoxDF.Anchor = textBoxDF.Anchor | AnchorStyles.Right;
            textBoxDF.TextChanged += new EventHandler(textBoxDF_TextChanged);

            labelTT.SetBounds(149, 95, 30, 13);
            labelTT.AutoSize = true;
            labelTT.Text = "時間:";
            textBoxTT.Text = timeF;
            textBoxTT.SetBounds(189, 90, 80, 20);
            textBoxTT.Anchor = textBoxTT.Anchor | AnchorStyles.Right;
            textBoxTT.TextChanged += new EventHandler(textBoxTT_TextChanged);

            labelTID.SetBounds(149, 125, 30, 13);
            labelTID.AutoSize = true;
            labelTID.Text = "UID:";
            textBoxTID.Text = tag;
            textBoxTID.SetBounds(189,120, 80, 20);
            textBoxTID.Anchor = textBoxTID.Anchor | AnchorStyles.Right;
            textBoxTID.TextChanged += new EventHandler(textBoxTID_TextChanged);

            labelRF.SetBounds(149, 155, 30, 13);
            labelRF.AutoSize = true;
            labelRF.Text = "結果:";
            textBoxRF.Text = result;
            textBoxRF.SetBounds(189, 150, 80, 20);
            textBoxRF.Anchor = textBoxRF.Anchor | AnchorStyles.Right;
            textBoxRF.TextChanged += new EventHandler(textBoxRF_TextChanged);

            labelIDF.SetBounds(149, 185, 30, 13);
            labelIDF.AutoSize = true;
            labelIDF.Text = "id:";
            textBoxIDF.Text = tableID;
            textBoxIDF.SetBounds(189, 180, 80, 20);
            textBoxIDF.Anchor = textBoxIDF.Anchor | AnchorStyles.Right;
            textBoxIDF.TextChanged += new EventHandler(textBoxIDF_TextChanged);

            labelSumIDF.SetBounds(149, 215, 30, 13);
            labelSumIDF.AutoSize = true;
            labelSumIDF.Text = "表單id:";
            textBoxSumIDF.Text = tableSumID;
            textBoxSumIDF.SetBounds(189, 210, 80, 20);
            textBoxSumIDF.Anchor = textBoxSumIDF.Anchor | AnchorStyles.Right;
            textBoxSumIDF.TextChanged += new EventHandler(textBoxSumIDF_TextChanged);

            labelTable2.SetBounds(289, 35, 30, 13);
            labelTable2.AutoSize = true;
            labelTable2.Text = "table:";
            textBoxTable2.Text = table2;
            textBoxTable2.SetBounds(329, 30, 80, 20);
            textBoxTable2.Anchor = textBoxTable2.Anchor | AnchorStyles.Right;
            textBoxTable2.TextChanged += new EventHandler(textBoxTable2_TextChanged);

            labelIDF2.SetBounds(289, 65, 30, 13);
            labelIDF2.AutoSize = true;
            labelIDF2.Text = "id:";
            textBoxIDF2.Text = tableID2;
            textBoxIDF2.SetBounds(329, 60, 80, 20);
            textBoxIDF2.Anchor = textBoxIDF2.Anchor | AnchorStyles.Right;
            textBoxIDF2.TextChanged += new EventHandler(textBoxIDF2_TextChanged);


            labelDF2.SetBounds(289, 95, 30, 13);
            labelDF2.AutoSize = true;
            labelDF2.Text = "date:";
            textBoxDF2.Text = dateF2;
            textBoxDF2.SetBounds(329, 90, 80, 20);
            textBoxDF2.Anchor = textBoxDF2.Anchor | AnchorStyles.Right;
            textBoxDF2.TextChanged += new EventHandler(textBoxDF2_TextChanged);


            labelTT2.SetBounds(289, 125, 30, 13);
            labelTT2.AutoSize = true;
            labelTT2.Text = "time:";
            textBoxTT2.Text = timeF2;
            textBoxTT2.SetBounds(329, 120, 80, 20);
            textBoxTT2.Anchor = textBoxTT2.Anchor | AnchorStyles.Right;
            textBoxTT2.TextChanged += new EventHandler(textBoxTT2_TextChanged);


            labelSF2.SetBounds(289, 155, 30, 13);
            labelSF2.AutoSize = true;
            labelSF2.Text = "status:";
            textBoxSF2.Text = statusF2;
            textBoxSF2.SetBounds(329, 150, 80, 20);
            textBoxSF2.Anchor = textBoxSF2.Anchor | AnchorStyles.Right;
            textBoxSF2.TextChanged += new EventHandler(textBoxSF2_TextChanged);


            form.Controls.AddRange(new Control[] { labelIP, labelDB, labelA, labelP, labelTable, labelDF, labelTT, labelTID, labelRF, labelIDF, labelSumIDF, labelTable2, labelIDF2, labelDF2, labelTT2, labelSF2, textBoxIP, textBoxDB, textBoxA, textBoxP, textBoxTable, textBoxDF, textBoxTT, textBoxTID, textBoxRF, textBoxIDF, textBoxSumIDF, textBoxTable2, textBoxIDF2, textBoxDF2, textBoxTT2, textBoxSF2, buttonOk,remenber, database });
            DialogResult dialogResult = form.ShowDialog();
            return dialogResult;
        }

         private void remenber_checkedChange(object sender, EventArgs e)
      {
          CheckBox tb = sender as CheckBox;

             remenber= tb.Checked;
      }

     private void textBoxIP_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

              IP = tb.Text;
      }


      private void textBoxDB_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

         DBName = tb.Text;
      }

      private void textBoxA_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          Ac = tb.Text;
      }

      private void textBoxP_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          Pw = tb.Text;
      }

      private void textBoxTable_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          tableName = tb.Text;
      }

      private void textBoxIDF_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          tableID = tb.Text;
      }


      private void textBoxSumIDF_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          tableSumID = tb.Text;
      }
        

      private void textBoxDF_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          dateField = tb.Text;
      }

      private void textBoxTT_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          timeField = tb.Text;
      }

      private void textBoxTID_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          tagField = tb.Text;
      }

      private void textBoxRF_TextChanged(object sender, EventArgs e)
      {
          TextBox tb = sender as TextBox;

          resultField = tb.Text;
      }



        private void textBoxTable2_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            tableName2 = tb.Text;
        }

        private void textBoxIDF2_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            tableID2 = tb.Text;
        }

        private void textBoxDF2_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            dateField2 = tb.Text;
        }

        private void textBoxTT2_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            timeField2 = tb.Text;
        }

        private void textBoxSF2_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            statusField2 = tb.Text;
        }


        private void autoLoadTagIDTimer(object sender, EventArgs e)
        {
            byte[] commEnter = new byte[] { 0x04, 0x01, 0x16, 0xE4 };
            for (int i = 0; i < port.Count; i++)
            {
                port[i].Write(commEnter, 0, commEnter.Length);
            }
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
       /*     if (!DB.checkChinComplete())
            {
                DialogResult re = MessageBox.Show(checkChinContent, checkChinHeader, MessageBoxButtons.YesNo);
                if (re == DialogResult.Yes)
                {
                    List<text_data_list> chinLastest = DB.chinLastestData();
                    for (int i = 0; i < chinLastest.Count; i++)
                    {
                        string[] row1 = new string[] { dataGridView1.Rows.Count.ToString(), chinLastest[i].date, chinLastest[i].time, chinLastest[i].TagID, chinLastest[i].result };
                        dataGridView1.Rows.Add(row1);
                        if (chinLastest[i].result == TagMore)
                        {
                            label4.Text = count(label4.Text);
                        }
                        else if (chinLastest[i].result == TagNotDB)
                        {
                            label5.Text = count(label5.Text);
                        }
                        else if (chinLastest[i].result == TagNotDB + "、" + TagMore)
                        {
                            label3.Text = count(label3.Text);
                        }
                        else if (chinLastest[i].result == TagIsOK)
                        {
                            label2.Text = count(label2.Text);
                        }
                    }
                    chinDefault = chinLastest[0].chinID;
                }
                if (re == DialogResult.No)
                {
                    DB.chinComplete();
                }
            }
            */
        }

        private void autoRead_Click(object sender, EventArgs e)
        {
            if (port.Count == 0||!port[0].IsOpen) {
                return;
            }

            if (autoReadclick)
            {
                autoLoadTagID.Stop();
                autoRead.Text = autoLoad;
                autoRead.ForeColor = Color.Black;
                autoReadclick = false;
            }
            else
            {
                autoLoadTagID.Start();
                autoRead.Text = autoLoadStop;
                autoRead.ForeColor = Color.Red;
                autoReadclick = true;
            }
        }

        private void BCC(byte[] data)
        {
            int total = 0;
            int t16 = 0x00;
            int t16back = 0xFF;
            for (int i = 0; i < data.Length; i++)
            {
                total = total + data[i];
            }
            t16 = Convert.ToInt32(Convert.ToString(total, 16).Substring(Convert.ToString(total, 16).Length - 2, 2),16);
            Console.WriteLine("t16 :" + t16);
            t16back = t16back - t16;
          
            Console.WriteLine("t16BYTE :" + Convert.ToString(t16back, 16));

        }

        private void selectChinTag_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectChinTag = (CheckBox)sender;


            if (selectChinTag.Checked)
            {
              //  DialogResult result = MessageBox.Show(ClearGridViewContent, ClearGridViewHeader, MessageBoxButtons.YesNo);



                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                //openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Multiselect = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int a = dataGridView1.Rows.Count;
                    for (int i = 0; i < a - 1; i++)
                    {
                        dataGridView1.Rows.RemoveAt(0);
                    }

                    SearchTagDataList.Clear();
                    for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                    {
                        txtPath = openFileDialog1.FileNames[i];
                        string file = Path.GetExtension(openFileDialog1.FileNames[i]);
                        Console.WriteLine("file:" + file);
                        if (file == ".txt")
                        {
                            using (StreamReader sr = File.OpenText(openFileDialog1.FileNames[i]))
                            {
                                //listBox_Battery.Items.Clear();


                                String input;
                                Search_data_list data = new Search_data_list();
                                List<string> inputData = new List<string>();
                               data.txtName = txtPath.Substring(txtPath.LastIndexOf("\\") + 1);
                                int j = 0;
                                while ((input = sr.ReadLine()) != null)
                                {

                                    inputData.Add(input);
                                    
                                    //Console.WriteLine("S" + input);
                                }
                                data.txtData = inputData;
                                SearchTagDataList.Add(data);

                                sr.Close();

                            }
                        }
                    }

                    MessageBox.Show(ImportDialogContent, ImportDialogHeader);


                }
                else
                {
                    selectChinTag.Checked = false;
                }

            }
            else
            {
                int a = dataGridView1.Rows.Count;
                for (int i = 0; i < a- 1; i++)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
            }
        }

        private void Cleartxt_Click(object sender, EventArgs e)
        {
            InsertDataList.Clear();
            MessageBox.Show("OK","success", MessageBoxButtons.OK);
        }
    }
}
