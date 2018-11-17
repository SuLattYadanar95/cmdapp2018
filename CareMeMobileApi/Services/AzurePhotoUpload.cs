using CareMeMobileApi.IService;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Services
{
    public class AzurePhotoUpload: IPhoto
    {
        public string uploadPhoto(string stringInBase64)
        {
            List<string> bs64List = stringInBase64.Split(',').ToList();
            if(bs64List.Count() > 1)
            {
                stringInBase64 = bs64List[0];
            }
            string guid = Guid.NewGuid().ToString();
            byte[] file = System.Convert.FromBase64String(stringInBase64);  // ByteArrayToImage(stringInBase64);
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("yammo");
                BlobRequestOptions opt = new BlobRequestOptions
                {
                    SingleBlobUploadThresholdInBytes = 1048576,
                    ParallelOperationThreadCount = 4
                };
                blobClient.DefaultRequestOptions = opt;
                BlobContainerPermissions permissions = new BlobContainerPermissions()
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                container.SetPermissions(permissions);

                CloudBlockBlob blockBlob = container.GetBlockBlobReference("careme" + "/" + guid + ".png");

                blockBlob.Properties.ContentType = "image/png";

                blockBlob.UploadFromByteArrayAsync(file, 0, file.Length);

                return guid + ".png";
            }
            catch
            {
                return null;
            }
        }

        public static System.Drawing.Image ByteArrayToImage(string stringInBase64)
        {
            byte[] bArray = System.Convert.FromBase64String(stringInBase64);
            if (bArray == null)
                return null;

            System.Drawing.Image newImage;

            try
            {
                using (MemoryStream ms = new MemoryStream(bArray, 0, bArray.Length))
                {
                    ms.Write(bArray, 0, bArray.Length);
                    newImage = System.Drawing.Image.FromStream(ms, true);
                }
            }
            catch (Exception ex)
            {
                newImage = null;
            }
            return newImage;
        }

    }
}

