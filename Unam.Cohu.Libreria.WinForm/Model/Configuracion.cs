using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.Cohu.Libreria.WinForm.Model
{
    public class Configuracion
    {
        public Configuracion() { }

        public static string ObtenerVariableConfig(string key)
        {
            string value = "";
            try
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                {
                    value = ConfigurationManager.AppSettings[key];
                }
            }
            catch (Exception)
            {
                value = string.Empty;
            }            
            return value;
        }

        public static string ObtenerCnnString(string key) {
            string cnnStr = Configuracion.ObtenerVariableConfig(key);
            if (String.IsNullOrEmpty(cnnStr)) {
                cnnStr = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=False;User ID={2};Password={3};Connect Timeout=30;Encrypt=False;TrustServerCertificate=False", 
                    Properties.Settings.Default.ServerInstance,Properties.Settings.Default.bd, Properties.Settings.Default.user, Properties.Settings.Default.pass );

            }
            return cnnStr;
        }

        public static string ObtenerRutaArchivos(string key)
        {
            string cnnStr = Configuracion.ObtenerVariableConfig(key);
            return (String.IsNullOrEmpty(cnnStr) ? "/FilesUpload" : cnnStr);
        }

        public static bool SaveConfig(string pathFile,string dataSource, string bd, string user, string pass, string urlPdf) {
            bool retorno = false;
            if (!String.IsNullOrEmpty(pathFile)) {
                string formatLine = "{0}:={1}{2}";
                string text = string.Empty;
                text = string.Format(formatLine, "Source", dataSource, "\n\r");
                text += string.Format(formatLine, "catalog", bd, "\n\r");
                text += string.Format(formatLine, "UID", user, "\n\r");
                text += string.Format(formatLine, "pw", pass, "\n\r");
                text += string.Format(formatLine, "pdf", urlPdf, "");

                byte[] data= System.Text.Encoding.Default.GetBytes(text);

                try
                {
                    if (File.Exists(pathFile))
                    {
                        using (FileStream fs = File.OpenWrite(pathFile))
                        {
                            fs.Write(data, 0, data.Length);
                            fs.Flush();
                        }
                        retorno = true;
                    }
                    else
                    {
                        using (FileStream fs = File.Create(pathFile))
                        {
                            fs.Write(data, 0, data.Length);
                            fs.Flush();
                        }
                        retorno = true;
                    }
                }
                catch (Exception ex)
                {
                }
                
            }
            return retorno;
        }

        public static string[] GetConfig(string pathFile)
        {
            string[] dataR = null;
            if (!String.IsNullOrEmpty(pathFile))
            {
                if (File.Exists(pathFile))
                {                    
                    string[] data = File.ReadAllLines(pathFile);
                    dataR = new string[data.Length];
                    int i = 0;
                    foreach (string  item in data)
                    {
                        string[] values =  item.Split(new string[] { ":=" }, StringSplitOptions.None);
                        dataR[i] = values[1];
                        i++;
                    }
                }
            }
            return dataR;
        }

    }
}
