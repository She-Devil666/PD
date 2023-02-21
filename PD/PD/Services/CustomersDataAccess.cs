using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PD.Services
{
    public class CustomersDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<User> User { get; set; }

        public CustomersDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<User>();
            this.User = new ObservableCollection<User>(database.Table<User>());
        }

        public User GetUser(string login)
        {
            lock (collisionLock)
            {
                return database.Table<User>().FirstOrDefault(user => user.Login == login);
            }
        }
        public User GetPhone(string phone)
        {
            lock (collisionLock)
            {
                return database.Table<User>().FirstOrDefault(user => user.Phone == phone);
            }
        }
        public int SaveUser(User user)
        {
            lock (collisionLock)
            {
                if (user.Id != 0)
                {
                    database.Update(user);
                    return user.Id;
                }
                else
                {
                    database.Insert(user);
                    return user.Id;
                }
            }
        }
    }
}
