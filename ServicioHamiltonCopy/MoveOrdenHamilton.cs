using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
 

namespace ServicioHamiltonCopy
{
    partial class MoveOrdenHamilton : ServiceBase
    {
        bool blBandera = false;
        public MoveOrdenHamilton()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            stLapso.Start();
        }

        protected override void OnStop()
        {
            stLapso.Stop();

            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        private void stLapso_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (blBandera) return;

            try
            {
                blBandera = true;
                EventLog.WriteEntry("Se inicio el proceso de copiado", EventLogEntryType.Information);

                string strRutaDestino = ConfigurationSettings.AppSettings["strRutaDestino"].ToString();
                string strRutaOrigen1 = ConfigurationSettings.AppSettings["strRutaOrigen1"].ToString();
                string strRutaOrigen2 = ConfigurationSettings.AppSettings["strRutaOrigen2"].ToString();
                string strRutaOrigen3 = ConfigurationSettings.AppSettings["strRutaOrigen3"].ToString();

                DirectoryInfo di = new DirectoryInfo(strRutaOrigen1);
                DirectoryInfo di2 = new DirectoryInfo(strRutaOrigen2);
                DirectoryInfo di3 = new DirectoryInfo(strRutaOrigen3);


                foreach (var archivo in di.GetFiles("*", SearchOption.AllDirectories))
                {
                    if(File.Exists(strRutaDestino + archivo.Name))
                    {
                        File.SetAttributes(strRutaDestino + archivo.Name, FileAttributes.Normal);
                        File.Delete(strRutaDestino + archivo.Name);
                    }
                    File.Move(strRutaOrigen1 + archivo.Name, strRutaDestino + archivo.Name);
                    File.SetAttributes(strRutaDestino + archivo.Name, FileAttributes.Normal);

                    if(File.Exists(strRutaDestino + archivo.Name))
                    {
                        EventLog.WriteEntry("Se Copio Archivo con exito del directorio 1", EventLogEntryType.Information);

                    }
                    else
                        EventLog.WriteEntry("No se copio el archivo", EventLogEntryType.Information);



                }

                    foreach (var archivo in di2.GetFiles("*", SearchOption.AllDirectories))
                {
                    if(File.Exists(strRutaDestino + archivo.Name))
                    {
                        File.SetAttributes(strRutaDestino + archivo.Name, FileAttributes.Normal);
                        File.Delete(strRutaDestino + archivo.Name);
                    }
                    File.Move(strRutaOrigen2 + archivo.Name, strRutaDestino + archivo.Name);
                    File.SetAttributes(strRutaDestino + archivo.Name, FileAttributes.Normal);

                    if(File.Exists(strRutaDestino + archivo.Name))
                    {
                        EventLog.WriteEntry("Se Copio Archivo con exito del directorio 2", EventLogEntryType.Information);

                    }
                    else
                        EventLog.WriteEntry("No se copio el archivo", EventLogEntryType.Information);



                }

                foreach (var archivo in di3.GetFiles("*", SearchOption.AllDirectories))
                {
                    if (File.Exists(strRutaDestino + archivo.Name))
                    {
                        File.SetAttributes(strRutaDestino + archivo.Name, FileAttributes.Normal);
                        File.Delete(strRutaDestino + archivo.Name);
                    }
                    File.Move(strRutaOrigen3 + archivo.Name, strRutaDestino + archivo.Name);
                    File.SetAttributes(strRutaDestino + archivo.Name, FileAttributes.Normal);

                    if (File.Exists(strRutaDestino + archivo.Name))
                    {
                        EventLog.WriteEntry("Se Copio Archivo con exito del directorio 3", EventLogEntryType.Information);

                    }
                    else
                        EventLog.WriteEntry("No se copio el archivo", EventLogEntryType.Information);



                }


                EventLog.WriteEntry("Finalizo el proceso de copiado ", EventLogEntryType.Information);


            }

            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
            blBandera = false;
        }
    }
}
