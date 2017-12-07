using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApplicationConsole;
using SampleApplicationConsole.Storage;

namespace UnitTestForSample
{
    // Unit Test Class, contains some basic unit test Azure API's Blob Storage. A real Unit Test Project would contain more.
    [TestClass]
    public class UnitTest
    {
        public void BaseTest(Storage storage)
        {
            storage.Create("unit_base_key1", "unit_value_1");

            // Item was added.
            Assert.AreEqual(storage.GetKeyList().Count, 1);

            // Note: Duplicate key cannot be added.
            storage.Create("unit_base_key1", "invalid_key");

            // The duplicate was not added.
            Assert.AreEqual(storage.GetKeyList().Count, 1);

            storage.Create("unit_base_key2", "unit_value_2");

            // New Item added.
            Assert.AreEqual(storage.GetKeyList().Count, 2);

            // Key "unit_base_key1" contains value "unit_value_1"
            Assert.AreEqual(storage.Read("unit_base_key1"), "unit_value_1");

            // Key "unit_base_key2" contains value "unit_value_2"
            Assert.AreEqual(storage.Read("unit_base_key2"), "unit_value_2");

            storage.Update("unit_base_key2", "new_value");

            // Key "unit_base_key2" contains value "new_value"
            Assert.AreEqual(storage.Read("unit_base_key2"), "new_value");

            storage.Delete("unit_base_key2");

            // The key "unit_base_key2" no longer exist, null expected
            Assert.AreEqual(storage.Read("unit_base_key2"), null);

            storage.Delete("unit_base_key1");

            // Ensure all items are new deleted
            Assert.AreEqual(storage.GetKeyList().Count, 0);
        }

        // Unit Test for the Base Storage Class
        [TestMethod]
        public void TestsForBaseStorage()
        {
            Storage storage = new Storage("UnitBase", "None");
            BaseTest(storage);
        }

        // Unit Test for the JSON Storage Class
        [TestMethod]
        public void TestsForJSONStorage()
        {
            JSONStorage storage = new JSONStorage("UnitJSON", @"C:\Temp\JsonStorage_unit.json");
            BaseTest(storage);

            storage.Create("json_key1", "json_val1");
            storage.Create("json_key2", "json_val2");

            // Write to file and remove data
            storage.WriteFile();

            // Remove memory data
            foreach (string key in storage.GetKeyList())
                storage.Delete(key);

            storage.ReadFile();

            Assert.AreEqual(storage.GetKeyList().Count, 2);
            Assert.AreEqual(storage.Read("json_key1"), "json_val1");
            Assert.AreEqual(storage.Read("json_key2"), "json_val2");

            // Remove memory data, then write to file
            foreach (string key in storage.GetKeyList())
                storage.Delete(key);

            storage.WriteFile();

            storage.ReadFile();
            Assert.AreEqual(storage.GetKeyList().Count, 0);
        }

        // Unit Test for Blob Class using Azure Client API
        [TestMethod]
        public void TestForBlobStorage()
        {
            // For Test use direct values rather than config
            ConfigItems.BLOBStorageLocation = @"C:\Temp\BlobStorage_unit.json";
            ConfigItems.BLOBConnectionString = "DefaultEndpointsProtocol=https;AccountName=storagefiles;AccountKey=M9OrtxX40K90DkD7IIgDmaBwoGsZ5zxtMvNCNNMIrOQQHUFQ5K7ERHh9FnO5dTDaFsZPla01mG+qvfF0PAryzA==;EndpointSuffix=core.windows.net";

            BlobStorage storage = new BlobStorage("BLOBstorage", ConfigItems.BLOBStorageLocation, ConfigItems.BLOBConnectionString);
            BaseTest(storage);

            // Cloud Storage Based Test
            storage.Create("cloud_key", "cloud_value");

            storage.WriteFile();

            // Remove memory data and local file
            foreach (string key in storage.GetKeyList())
                storage.Delete(key);

            storage.RemoveFile();

            // Make sure there is no data for storage
            Assert.AreEqual(storage.GetKeyList().Count, 0);

            // Read back from Cloud
            storage.ReadFile();

            // Check if data was populated
            Assert.AreEqual(storage.GetKeyList().Count, 1);
            Assert.AreEqual(storage.Read("cloud_key"), "cloud_value");

            // Now remove the data and push that to the cloud data
            foreach (string key in storage.GetKeyList())
                storage.Delete(key);

            storage.WriteFile();
            storage.RemoveFile();

            // Read back from Cloud
            storage.ReadFile();
            Assert.AreEqual(storage.GetKeyList().Count, 0);
        }
    }
}
