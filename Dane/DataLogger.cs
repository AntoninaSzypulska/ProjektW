using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace Dane
{
    public class DataLogger
    {

        private readonly object fileLock = new object();

        public DataLogger() { }

        public void LogData(object data)
        {
            string serializedData = JsonSerializer.Serialize(data);

            WriteToFile(serializedData);
        }

        private void WriteToFile(string data)
        {
            lock (fileLock)
            {
                try
                {
                    StreamWriter sw = new StreamWriter($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}/Data_Log.txt", append: true);
                    sw.WriteLine(data);
                    sw.Close();
                }
                catch
                {
                }
            }
        }
    }
}
