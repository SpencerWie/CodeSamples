using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ConfigurationSettings = System.Configuration.ConfigurationManager;

namespace SampleApplicationConsole
{
    public static class ConfigItems
    {
        public static string JSONStorageLocation = ConfigurationSettings.AppSettings["JSONStorageLocation"];
        public static string BLOBStorageLocation = ConfigurationSettings.AppSettings["BlobStorageLocation"];
        public static string BLOBConnectionString = ConfigurationSettings.AppSettings["BlobConnectionString"];
    }

    /// <summary>
    /// Main Class to display "Hello World", also does a basic run through each Storage Class.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();

            DisplayBaseStorageSample();
            DisplayJSONStorageSample();
            DisplayBLOBStorageSample();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        // Displays basic information of newly created Storage Object
        public static void DisplayStorageHeader(Storage.Storage storage)
        {
            Console.WriteLine($"-- {storage.GetName()} Storage Sample --");
            Console.WriteLine($"Created: Name={storage.GetName()}, Loc={storage.GetLocation()}, Conn={storage.GetConnectionString()}");
            Console.WriteLine("Creating data...");
        }

        // Removes the Data from the storage.
        public static void DisplayStorageFooter(Storage.Storage storage)
        {
            List<string> keyList = storage.GetKeyList();

            Console.WriteLine("Data Contains:");
            foreach (string key in keyList)
                Console.WriteLine($"key: {key}, value: {storage.Read(key)}");

            Console.WriteLine("Deleting data...");
            foreach (string key in keyList)
                storage.Delete(key);

            Console.WriteLine("Remaining Items (Expecting 0): " + storage.GetKeyList().Count());

            Console.WriteLine($"-- END {storage.GetName()} Storage Sample --");
        }

        public static void DisplayBaseStorageSample()
        {
            Storage.Storage storage = new Storage.Storage("Base", "SomeLocation");

            DisplayStorageHeader(storage);

            storage.Create("key1", "foo");
            storage.Create("key2", "bar");

            DisplayStorageFooter(storage);
        }

        public static void DisplayJSONStorageSample()
        {
            Storage.JSONStorage storage = new Storage.JSONStorage("JSONStorage", ConfigItems.JSONStorageLocation);

            DisplayStorageHeader(storage);

            storage.Create("obj1", "value1");
            storage.Create("obj2", "value2");

            storage.WriteFile();

            // Delete Data currently in memory to prove new Data is pulled from file.
            foreach (string key in storage.GetKeyList())    
                storage.Delete(key);

            // Get new data from the created file, then remove it.
            storage.ReadFile();
            storage.RemoveFile();

            DisplayStorageFooter(storage);
        }

        public static void DisplayBLOBStorageSample()
        {
            Storage.BlobStorage storage = new Storage.BlobStorage("BLOStorage", ConfigItems.BLOBStorageLocation, ConfigItems.BLOBConnectionString);

            DisplayStorageHeader(storage);

            storage.Create("blob1", "val1");
            storage.Create("blob2", "val2");

            storage.WriteFile();

            // Delete Data currently in memory to prove new Data is pulled from blob.
            foreach (string key in storage.GetKeyList())
                storage.Delete(key);

            storage.ReadFile();

            Console.WriteLine("Data Contains:");
            foreach (string key in storage.GetKeyList())
                Console.WriteLine($"key: {key}, value: {storage.Read(key)}");

            // Delte data in memory, then remove data on Azure as well and re-pull.
            Console.WriteLine("Deleting data...");
            foreach (string key in storage.GetKeyList())
                storage.Delete(key);

            storage.WriteFile();   // Write to File and Azure
            storage.ReadFile();    // Pull new data from Azure. 

            Console.WriteLine("Remaining Items (Expecting 0): " + storage.GetKeyList().Count());

            Console.WriteLine($"-- END {storage.GetName()} Storage Sample --");
        }
    }
}
