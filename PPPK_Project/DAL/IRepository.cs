using PPPK_Project.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_Project.DAL
{
    interface IRepository
    {
        void LogIn(string server, string username, string password);
        IEnumerable<Column> GetColumns(DBEntity entity);
        IEnumerable<Database> GetDatabases();
        IEnumerable<DBEntity> GetDBEntities(Database database, DBEntityType entityType);
        IEnumerable<Parameter> GetParams(Procedure procedure);
        IEnumerable<Procedure> GetProcedures(Database database);
        SqlData CreateResults(String query);
    }
}
