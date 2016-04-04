using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Web;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;

namespace CarSales.Models
{
        /// <summary>
        /// Class to Store BLOB Info
        /// </summary>



        /// <summary>
        /// Class to Work with Blob
        /// </summary>
        public class BlobOperations
        {
            private static CloudBlobContainer carImagesBlobContainer;

            /// <summary>
            /// Initialize BLOB and Queue Here
            /// </summary>
            public BlobOperations()
            {
                var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["webjobstorage"].ToString());

                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get the blob container reference.
                carImagesBlobContainer = blobClient.GetContainerReference("CarImages");
                //Create Blob Container if not exist
                carImagesBlobContainer.CreateIfNotExists();
            }


            /// <summary>
            /// Method to Upload the BLOB
            /// </summary>
            /// <param name='"careImageFile"'>
            /// <returns></returns>
            public async Task<CloudBlockBlob> UploadBlob(HttpPostedFileBase careImageFile)
            {
                string blobName = Guid.NewGuid().ToString() + Path.GetExtension(careImageFile.FileName);
                // GET a blob reference. 
                CloudBlockBlob carImagesBlob = carImagesBlobContainer.GetBlockBlobReference(blobName);
                // Uploading a local file and Create the blob.
                using (var fs = careImageFile.InputStream)
                {
                    await carImagesBlob.UploadFromStreamAsync(fs);
                }
                return carImagesBlob;
            }

        }

    }
