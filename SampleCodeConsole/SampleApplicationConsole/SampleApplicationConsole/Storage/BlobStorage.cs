using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.IO;

namespace SampleApplicationConsole.Storage
{
    /// <summary>
    /// Inheirted from JSON Storage, this class will also download and upload JSON to an Azure Cloud Storage.
    /// </summary>
    // Blob Storage Class using Azure Client API
    public class BlobStorage : JSONStorage
    {
        private CloudStorageAccount storage;
        private CloudBlobClient client;
        private string containerName = "sample-container";
        private string blobName = "blob";
        private CloudBlobContainer container;

        public BlobStorage(string name, string location, string connection) : base(name, location)
        {
            this._name = name;
            this._location = location;
            this._connection = connection;
            _Data = new Dictionary<string, string>();

            // Get instance from Azure Account
            storage = CloudStorageAccount.Parse(connection);

            // Setup Client for Blob storages
            client = storage.CreateCloudBlobClient();

            // Create container sample-cotainer if it doesn't exist already.
            container = client.GetContainerReference(this.containerName);
            container.CreateIfNotExists();

            // Allow Public Use
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
        }

        // Create File like normal to JSON File, but then uploade as blob to Azure
        public override void WriteFile()
        {
            base.WriteFile();

            CloudBlockBlob blob = container.GetBlockBlobReference(blobName); 
            using (var fileStream = System.IO.File.OpenRead(GetLocation()))
            {
                blob.UploadFromStream(fileStream);
            }
        }

        public override void ReadFile()
        {
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            string json;
            using (var memoryStream = new MemoryStream())
            {
                blob.DownloadToStream(memoryStream);
                json = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                this.AssignJSONStringToData(json);
            }
        }
    }
}
