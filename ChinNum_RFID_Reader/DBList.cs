using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinNum_RFID_Reader
{
    class DBList
    {
    }
    public class reader_data_list
    {
        public string date;
        public string time;
        public string TagID;

    }

    public class text_data_list
    {
        public string date;
        public string time;
        public string TagID;
        public string result;
        public int chinID;

    }


    public class Search_data_list
    {
        public List<string> txtData;
        public string txtName;
    }


    public class excelList
    {
        [Description("序號")]
        public string index { get; set; }
        [Description("日期")]
        public string date { get; set; }
        [Description("時間")]
        public string time { get; set; }
        [Description("ID")]
        public string tagID { get; set; }
        [Description("Remark")]
        public string remark { get; set; }
    }


}
