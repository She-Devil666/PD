using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace PD.Services
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
