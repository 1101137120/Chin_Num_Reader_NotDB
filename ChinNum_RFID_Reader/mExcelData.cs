using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace ChinNum_RFID_Reader
{
   public class mExcelData
    {
        private const string ProviderName = "Microsoft.Jet.OLEDB.4.0;";

        private const string ExtendedString = "'Excel 8.0;";

        private const string Hdr = "Yes;";

        private const string IMEX = "0';";

        private string cs = "Data Source=fileName;Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='Excel 8.0;HDR=Yes;IMEX=0';";

        public mExcelData()
        {
        }


        public DataTable GetExcelDataTable(string filePath, string sql)
        {
            //Office 2003
            // OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;Readonly=0'");

            //Office 2007
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES'");
            OleDbDataAdapter da;
            DataTable dt = new DataTable();
            try
            {
                da = new OleDbDataAdapter(sql, conn);
                da.Fill(dt);
                dt.TableName = "tmp";
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("錯誤，請關閉Excel");
                //       Console.WriteLine(e.ToString());
            }


            return dt;
        }


        /*     public bool ExportDataGridview(DataGridView gridView, bool isShowExcle, string filename)
             {
                 // if (gridView.Rows.Count == 0)
                 //     return false;
                 //建立Excel


                 Excel._Worksheet mySheet = null;


                 Excel.Application excel = new Excel.Application();
                 excel.Application.Workbooks.Add(true);

                 //  excel.Application.Sheets.Add(After: excel.Application.Sheets[excel.Application.Sheets.Count]);


                 mySheet = (Excel._Worksheet)excel.Worksheets[1];//引用第一張工作表
                                                                      //  mySheet2 = (Excel._Worksheet)excel.Worksheets["工作表2"];//引用第一張工作表


                 //    try
                 //  {
                 //標題
                 int a = 0;
                 for (int i = 0; i < gridView.ColumnCount; i++)
                 {
                     //濾掉選項欄位

                     mySheet.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
                     // excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
                 }
                 //數據資料
                 for (int i = 0; i < gridView.Rows.Count; i++)
                 {
                     for (int j = 0; j < gridView.ColumnCount; j++)
                     {
                         //濾掉選項欄位

                         //   mySheet.Cells[i + 1, j + 1] = gridView[j+1, i].Value.ToString();
                         if (gridView[j, i].Value != null)
                             mySheet.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
                         // }

                     }

                 }





                 //設定為按照內容自動調整欄寬
                 Excel.Range oRng;
                 oRng = mySheet.get_Range("A1", "N" + gridView.RowCount);
                 oRng.EntireColumn.AutoFit(); // 自動調整欄寬

                 oRng.EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter; //置中

                 oRng.EntireColumn.NumberFormatLocal = 0;
                 oRng = mySheet.get_Range("B1", "B" + gridView.RowCount);
                 oRng.EntireColumn.NumberFormatLocal = "yyyy/MM/dd";

                 oRng = mySheet.get_Range("C1", "C" + gridView.RowCount);
                 oRng.EntireColumn.NumberFormatLocal = "HH: mm: ss";

                 //excel.ActiveWorkbook.SaveCopyAs(filename);

                 // mySheet.Cells.NumberFormat = "0";
                 excel.ActiveWorkbook.SaveCopyAs(filename);
                 excel.Visible = isShowExcle;
                 // excel.Quit();//離開聯結 
                 return true;
             }

         */




        public void SaveExcelFile(string fileName, List<excelList> ltd)
        {
            bool flag;

            Console.WriteLine(fileName);
            this.cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=2;'";
            if (fileName.Substring(fileName.Length - 4, 4) != ".xls")
            {
                fileName = string.Concat(fileName, ".xls");
            }
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists)
            {
                fi.Delete();
            }
            string connectString = "CREATE TABLE [WorkPage] ([Serial Number] INTEGER,[ID] VarChar,[Country Code] VarChar,[String 1] VarChar,[String 2] VarChar,[String 3] VarChar)";
            OleDbConnection cn = new OleDbConnection(this.cs);
            Console.WriteLine("sfsdfsdffffffffffffff");
            try
            {
                cn.Open();
                try
                {
                    OleDbCommand cmd = new OleDbCommand(connectString, cn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        if (cmd != null)
                        {
                            ((IDisposable)cmd).Dispose();
                        }
                    }

                    Console.WriteLine("sfsdfsdff222222222222");
                    string qs = "";
                    foreach (excelList td in ltd)
                    {
                        qs = "INSERT INTO [WorkPage$] VALUES(";
                        qs = string.Concat(qs, td.index, ",'");
                        qs = string.Concat(qs, td.date, "','");
                        qs = string.Concat(qs, td.time, "','");
                        qs = string.Concat(qs, td.tagID, "','");
                        qs = string.Concat(qs, td.remark, "','");
                        cmd = new OleDbCommand(qs, cn);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        finally
                        {
                            if (cmd != null)
                            {
                                ((IDisposable)cmd).Dispose();
                            }
                        }
                    }
                    cn.Close();
                    flag = true;
                }
                catch
                {
                    cn.Close();
                    flag = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("aaaaaa"+ ex);
                if (cn != null)
                {
                    ((IDisposable)cn).Dispose();
                }
            }
            //return flag;
        }



        public void ExportDataGridViewToCsv(DataGridView DataGridView1, List<text_data_list> InsertDataList , string FileName,string txtName)
        {


        /*    Microsoft.Office.Interop.Excel._Worksheet mySheet = null;
            Microsoft.Office.Interop.Excel._Worksheet mySheet2 = null;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Application.Sheets.Add(After: excel.Application.Sheets[excel.Application.Sheets.Count]);


            mySheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.Worksheets[1];//引用第一張工作表
            mySheet2 = (Microsoft.Office.Interop.Excel._Worksheet)excel.Worksheets[2];//引用第二張工作表


            //    try
            //  {
            //標題
            int a = 0;
            for (int i = 0; i < DataGridView1.ColumnCount; i++)
            {
                //濾掉選項欄位

                mySheet.Cells[1, i + 1] = DataGridView1.Columns[i].HeaderText;
                // excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
            }
            //數據資料
            for (int i = 0; i < DataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < DataGridView1.ColumnCount; j++)
                {
                    //濾掉選項欄位

                    //   mySheet.Cells[i + 1, j + 1] = gridView[j+1, i].Value.ToString();
                    if (DataGridView1[j, i].Value != null)
                        mySheet.Cells[i + 2, j + 1] = DataGridView1[j, i].Value.ToString();
                    // }

                }

            }


                mySheet2.Cells[1,1] ="DB_TagID";

            //數據資料
            for (int i = 0; i < InsertDataList.Count; i++)
            {
                    //   mySheet.Cells[i + 1, j + 1] = gridView[j+1, i].Value.ToString();
                    if (InsertDataList[i].TagID != null)
                        mySheet2.Cells[i + 2,1] = InsertDataList[i].TagID;
                    // }


            }




            if (InsertDataList.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Range oRng2;
                oRng2 = mySheet2.get_Range("A1", "A" + InsertDataList.Count);
                oRng2.EntireColumn.AutoFit(); // 自動調整欄寬
            }

            if (DataGridView1.RowCount > 0)
            {
                //設定為按照內容自動調整欄寬
                Microsoft.Office.Interop.Excel.Range oRng;
                oRng = mySheet.get_Range("A1", "N" + DataGridView1.RowCount);
                oRng.EntireColumn.AutoFit(); // 自動調整欄寬

                oRng.EntireColumn.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter; //置中

                oRng.EntireColumn.NumberFormatLocal = 0;
                oRng = mySheet.get_Range("B1", "B" + DataGridView1.RowCount);
                oRng.EntireColumn.NumberFormatLocal = "yyyy/MM/dd";

                oRng = mySheet.get_Range("C1", "C" + DataGridView1.RowCount);
                oRng.EntireColumn.NumberFormatLocal = "HH: mm: ss";

            }


            //excel.ActiveWorkbook.SaveCopyAs(filename);

            // mySheet.Cells.NumberFormat = "0";
            excel.ActiveWorkbook.SaveCopyAs(FileName);
            excel.Visible = false;*/
            // excel.Quit();//離開聯結 
        

         //settings
         //string delimiter = "|";
         string delimiter = ",";

       //  string outputFilename = "fiverrComScrapedResult.csv";
       //  string fullFilename = Path.Combine(getSaveFolder(), outputFilename);

         StreamWriter csvStreamWriter = new StreamWriter(FileName, true, System.Text.Encoding.UTF8);

         //output header data
         string strHeader = "";
         for (int i = 0; i < DataGridView1.Columns.Count; i++)
         {
             strHeader += ((char)(9)).ToString() + DataGridView1.Columns[i].HeaderText + delimiter;
         }
         csvStreamWriter.WriteLine(strHeader);

         //output rows data
         for (int j = 0; j < DataGridView1.Rows.Count; j++)
         {
             string strRowValue = "";

             for (int k = 0; k < DataGridView1.Columns.Count; k++)
             {

                 strRowValue += ((char)(9)).ToString() + DataGridView1.Rows[j].Cells[k].Value + delimiter;


             }
             csvStreamWriter.WriteLine(strRowValue);
         }

         csvStreamWriter.Close();



            StreamWriter sw = new StreamWriter(txtName, false, System.Text.Encoding.Default);


            for (int i = 0; i < InsertDataList.Count; i++)
            {
                
                
                sw.Write(InsertDataList[i].TagID);
                sw.WriteLine();
            }
            //清空緩衝區
            sw.Flush();
            //關閉流
            sw.Close();
        }
}
}
