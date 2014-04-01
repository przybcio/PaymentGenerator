using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

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

        /// <summary>
        /// TODO: zrobić to generycznie 
        /// </summary>
        /// <param name="list"></param>
        public static void ExportToCsv(IList list)
        {
            using (FileStream fs = new FileStream("gen.csv", FileMode.Create))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in list)
                {
                    sb.AppendLine(((account)item).account_no);
                    //sb.Append(";");
                    //sb.Append(Environment.NewLine);
                }
                byte[] buffer = System.Text.Encoding.GetEncoding(1250).GetBytes(sb.ToString());

                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
