using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.ServiceRuntime;


namespace DT.PCP.Utils
{
    public class LocalStorageWriter
    {
        public static void WriteToLocalStorage(string text)
        {
            // TODO получть из настроек
            String workerRoleStorageName = "WorkerRoleStorage";
            String workerRoleFileName = "WorkerRoleStorage.txt";
            LocalResource localResource =
                 RoleEnvironment.GetLocalResource(
                      workerRoleStorageName);
            String path = localResource.RootPath +
                  workerRoleFileName;
            FileStream writeFileStream = File.Create(path);
            using (var streamWriter = new StreamWriter(writeFileStream))
            {
                streamWriter.Write(text);
            }

           
        }
    }
}
