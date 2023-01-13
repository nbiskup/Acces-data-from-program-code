using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Azure.Storage.Blobs;

namespace BlobStorage.Dao
{
    static class Repository
    {
        private const string ContainerName = "blobcontainer";
        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static readonly Lazy<BlobContainerClient> container = new Lazy<BlobContainerClient>(() => new BlobServiceClient(cs).GetBlobContainerClient(ContainerName));
        public static BlobContainerClient Container = container.Value;
    }
}
