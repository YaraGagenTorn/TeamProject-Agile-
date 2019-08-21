using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoCartApp.Handlers;
using AutoCartApp.iOS.Handlers;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace AutoCartApp.iOS.Handlers
{
    public class SQLite_iOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string dbName = "Main.db3";
            string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", dbName);
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}