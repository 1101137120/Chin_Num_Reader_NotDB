using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMC5052B
{
    public class Language {

        public LanguageData Company { get; set; }

    public LanguageData LanguageTCN()
    {
            LanguageData LanguageData = new LanguageData();
            LanguageData.button1 = "查詢Com";
            LanguageData.button2 = "連接";
            LanguageData.button3 = "匯入 txt";
            LanguageData.button4 = "刪除";
            LanguageData.button5 = "匯出EXCEL";
            LanguageData.label2 = "合格";
            LanguageData.label3 = "無國碼重碼";
            LanguageData.label4 = "重碼";
            LanguageData.label5 = "無國碼";
            LanguageData.autoRead = "連續讀取";
            LanguageData.autoReadStop = "中止讀取";
            LanguageData.Column1 = "序號";
            LanguageData.Column2 = "日期";
            LanguageData.Column3 = "時間";
            LanguageData.Column4 = "ID";
            LanguageData.Column5 = "Remark";
            LanguageData.TagMore = "重碼";
            LanguageData.TagNotDB = "無國碼";
            LanguageData.ConnectStatusS = "連接成功";
            LanguageData.ConnectStatusF = "連接失敗";
            LanguageData.ConnectButtonL = "連接";
            LanguageData.ConnectButtonNL = "中斷連接";
            LanguageData.NotDBHeader = "國碼";
            LanguageData.NotDBContent = "無國碼";
            LanguageData.MoreHeader = "重碼";
            LanguageData.MoreContent = "已重複";
            LanguageData.ImportDialogHeader = "匯入";
            LanguageData.ImportDialogContent = "匯入成功";
            LanguageData.dataGridClearHeader = "表單";
            LanguageData.dataGridClearContent = "表單資料確認清空";
            LanguageData.exitHeader = "關閉";
            LanguageData.exitContent = "是否匯出表單?";
            LanguageData.txtImportHeader = "提示";
            LanguageData.txtImportContent = "請先匯入txt";
            LanguageData.TagIsOK = "合格";
            LanguageData.checkChinContent = "是否載入未匯出資料?";
            LanguageData.checkChinHeader = "提示";
            LanguageData.selectChinTag = "查詢Tag";
            return LanguageData;
    }

    public LanguageData LanguageSCN()
    {
            LanguageData LanguageData = new LanguageData();
            LanguageData.button1 = "查询Com";
            LanguageData.button2 = "连接";
            LanguageData.button3 = "汇入 txt";
            LanguageData.button4 = "删除";
            LanguageData.button5 = "汇出EXCEL";
            LanguageData.label2 ="合格";
            LanguageData.label3 ="无国码重码";
            LanguageData.label4 ="重码";
            LanguageData.label5 ="无国码";
            LanguageData.autoRead = "连续读取";
            LanguageData.autoReadStop = "中止读取";
            LanguageData.Column1 = "序号";
            LanguageData.Column2 = "日期";
            LanguageData.Column3 = "时间";
            LanguageData.Column4 = "ID";
            LanguageData.Column5 = "Remark";
            LanguageData.TagMore = "重码";
            LanguageData.TagNotDB = "无国码";
            LanguageData.ConnectStatusS = "连接成功";
            LanguageData.ConnectStatusF = "连接失败";
            LanguageData.ConnectButtonL = "连接";
            LanguageData.ConnectButtonNL = "中断连接";
            LanguageData.NotDBHeader = "国码";
            LanguageData.NotDBContent = "无国码";
            LanguageData.MoreHeader = "重码";
            LanguageData.MoreContent = "已重复";
            LanguageData.ImportDialogHeader = "汇入";
            LanguageData.ImportDialogContent = "汇入成功";
            LanguageData.dataGridClearHeader = "表单";
            LanguageData.dataGridClearContent = "表单资料确认清空";
            LanguageData.exitHeader = "关闭";
            LanguageData.exitContent = "是否汇出表单?";
            LanguageData.txtImportHeader = "提示";
            LanguageData.txtImportContent = "请先汇入txt";
            LanguageData.TagIsOK = "合格";
            LanguageData.checkChinContent = "是否载入未汇出资料？";
            LanguageData.checkChinHeader = "提示";
            LanguageData.selectChinTag = "查询Tag";
            return LanguageData;
    }

        public LanguageData LanguageEN()
        {
            LanguageData LanguageData = new LanguageData();
            LanguageData.button1 = "Search Com";
            LanguageData.button2 = "Connect";
            LanguageData.button3 = "Import txt";
            LanguageData.button4 = "Delete";
            LanguageData.button5 = "Export Excel";
            LanguageData.label2 = "Qualified";
            LanguageData.label3 = "No country code re-code";
            LanguageData.label4 = "Recode";
            LanguageData.label5 = "No country code";
            LanguageData.autoRead = "autoRead";
            LanguageData.autoReadStop = "stopRead";
            LanguageData.Column1 = "Index";
            LanguageData.Column2 = "Date";
            LanguageData.Column3 = "Time";
            LanguageData.Column4 = "ID";
            LanguageData.Column5 = "Remark";
            LanguageData.TagMore = "Recode";
            LanguageData.TagNotDB = "Data Null";
            LanguageData.ConnectStatusS = "Connect OK";
            LanguageData.ConnectStatusF = "Connect Fail";
            LanguageData.ConnectButtonL = "Link";
            LanguageData.ConnectButtonNL = "Stop";
            LanguageData.NotDBHeader = "Error";
            LanguageData.NotDBContent = " No country code";
            LanguageData.MoreHeader = "Error";
            LanguageData.MoreContent = "been repeated";
            LanguageData.ImportDialogHeader = "Import";
            LanguageData.ImportDialogContent = "Import Success";
            LanguageData.dataGridClearHeader = "Form";
            LanguageData.dataGridClearContent = "Form data confirmation clear";
            LanguageData.exitHeader = "Close";
            LanguageData.exitContent = "Whether to export the form?";
            LanguageData.txtImportHeader = "prompt";
            LanguageData.txtImportContent = "Please import txt first";
            LanguageData.TagIsOK = "qualified";
            LanguageData.checkChinContent = "Do you want to load unexported data?";
            LanguageData.checkChinHeader = "prompt";
            LanguageData.selectChinTag = "Query Tag";

            return LanguageData;
        }




    }

    public class LanguageData
    {
        public string button1 { get; set; }
        public string button2 { get; set; }

        public string Column1 { get; set; }

        public string Column2 { get; set; }

        public string Column3 { get; set; }

        public string Column4 { get; set; }

        public string Column5 { get; set; }

        public string button3 { get; set; }
        public string button4 { get; set; }

        public string button5 { get; set; }
        public string autoRead { get; set; }
        public string autoReadStop { get; set; }
        
        public string TagMore { get; set; }
        public string TagNotDB { get; set; }
        public string ConnectStatusS { get; set; }
        public string ConnectStatusF { get; set; }
        public string ConnectButtonL { get; set; }
        public string ConnectButtonNL{ get; set; }
        public string NotDBHeader { get; set; }
        public string NotDBContent { get; set; }
        public string MoreHeader { get; set; }
        public string MoreContent { get; set; }
        public string ImportDialogHeader { get; set; }
        public string ImportDialogContent { get; set; }
        public string dataGridClearHeader { get; set; }
        public string dataGridClearContent { get; set; }
        public string exitHeader { get; set; }
        public string exitContent { get; set; }
        public string txtImportHeader { get; set; }
        public string txtImportContent { get; set; }
        public string TagIsOK { get; set; }
        public string checkChinContent { get; set; }
        public string checkChinHeader { get; set; }
        public string label2 { get; set; }
        public string label3 { get; set; }
        public string label4 { get; set; }
        public string label5 { get; set; }
        public string selectChinTag { get; set; }
        public string ClearGridViewHeader { get; set; }
        public string ClearGridViewContent { get; set; }
    }
}
