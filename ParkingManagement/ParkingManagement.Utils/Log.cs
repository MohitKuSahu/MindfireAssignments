using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Utils
{
    public class Log:ILog
    {
        private string _fileFolderPath;

        public Log(string fileFolderPath)
        {
            _fileFolderPath = fileFolderPath;
        }

        public void SetFileFolderPath(string fileFolderPath)
        {
            _fileFolderPath = fileFolderPath;
        }

        public void AddException(Exception inputData)
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string filePath = Path.Combine(_fileFolderPath, fileName);

            if (!Directory.Exists(_fileFolderPath))
            {
                Directory.CreateDirectory(_fileFolderPath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                 writer.WriteLine(DateTime.Now + "==>" + inputData.Message);

                if (inputData.InnerException!=null)
                {
                    writer.WriteLine(inputData.InnerException.Message);
                }

                writer.WriteLine();
            }

        
        }  

    }
}
