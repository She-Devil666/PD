using PD.Droid;
using PD.Services;
using SQLite;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]

namespace PD.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "dating.db";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}