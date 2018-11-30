using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMC5052B
{
    public class XSLXHelper
    {

        public XLWorkbook Export<T>(List<T> data)
        {
            Console.WriteLine("LIST T:"+ data.Count);
            //建立 excel 物件
            XLWorkbook workbook = new XLWorkbook();
            //加入 excel 工作表名為 `Report`
            var sheet = workbook.Worksheets.Add("Report");
            //欄位起啟位置
            int colIdx = 1;
            //使用 reflection 將物件屬性取出當作工作表欄位名稱
            Console.WriteLine("ypeof(T).GetProperties()" + typeof(T).GetProperties().Length);
            foreach (var item in typeof(T).GetProperties())
            {
                #region - 可以使用 DescriptionAttribute 設定，找不到 DescriptionAttribute 時改用屬性名稱 -
                //可以使用 DescriptionAttribute 設定，找不到 DescriptionAttribute 時改用屬性名稱
                //DescriptionAttribute description = item.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                //if (description != null)
                //{
                //    sheet.Cell(1, colIdx++).Value=description.Description;
                //    continue;
                //}
                //sheet.Cell(1, colIdx++).Value = item.Name;
                #endregion
                #region - 直接使用物件屬性名稱 -
                //或是直接使用物件屬性名稱
                Console.WriteLine("ITEM"+item.Name);
                sheet.Cell(1, colIdx++).Value = item.Name;
                #endregion

            }
            //資料起始列位置
            int rowIdx = 2;
            foreach (var item in data)
            {
                //每筆資料欄位起始位置
                int conlumnIndex = 1;
                foreach (var jtem in item.GetType().GetProperties())
                {
                    //將資料內容加上 "'" 避免受到 excel 預設格式影響，並依 row 及 column 填入
                    sheet.Cell(rowIdx, conlumnIndex).Value = string.Concat("'", Convert.ToString(jtem.GetValue(item, null)));
                    conlumnIndex++;
                }
                rowIdx++;
            }
            return workbook;
        }
    
}
}
