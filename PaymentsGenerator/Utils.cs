using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using PaymentsGenerator.Abstracts;

namespace PaymentsGenerator
{
    public class Utils
    {
        public static string GetScript(string scriptRelPath)
        {
            return File.ReadAllText(scriptRelPath);
        }

        public static string GetCurrentAppPathDirectory()
        {
            
            return Directory.GetCurrentDirectory();
        }

        public static string ExportToCsv(IList<account> list) 
        {
            string fileName = "gen_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in list)
                {
                    sb.AppendLine(item.account_no);
                    //sb.Append(";");
                    //sb.Append(Environment.NewLine);
                }
                byte[] buffer = System.Text.Encoding.GetEncoding(1250).GetBytes(sb.ToString());

                fs.Write(buffer, 0, buffer.Length);
            }
            return fileName;
        }
    }
}
