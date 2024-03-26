using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Utils
{
    public interface ILog
    {
        public void AddException(Exception inputData);
    }
}
