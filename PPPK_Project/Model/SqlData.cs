using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_Project.Model
{
    class SqlData
    {
        public DataSet Data { get; set; }
        public string Message { get; set; }

        public SqlData(DataSet data, string message)
        {
            Data = data;
            Message = message;
        }
    }
}
