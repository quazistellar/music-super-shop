using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public static class FileManager
    {
        private static readonly string DataFilePath;
        private static readonly string DataFolderPath;
        private static readonly AppDbContext Context;

        static FileManager()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DataFolderPath = Path.Combine(desktopPath, "Data");
            DataFilePath = Path.Combine(DataFolderPath, "data.json");
            if (Directory.Exists(DataFolderPath) && File.Exists(DataFilePath))
            {
                var dbContextWrapper = Serializer.Deserialize<AppDbContextWrapper>(DataFilePath);
                Context = new AppDbContext(dbContextWrapper);
            }
            else
            {
                if (!Directory.Exists(DataFolderPath))
                {
                    Directory.CreateDirectory(DataFolderPath);
                }

                Context = new AppDbContext();
                SaveAppDbContext(Context);
            }
        }

        public static AppDbContext GetAppDbContext() => Context;

        public static void SaveAppDbContext(AppDbContext appContext)
        {
            Serializer.Serialize(appContext, DataFilePath);
        }
    }
}
