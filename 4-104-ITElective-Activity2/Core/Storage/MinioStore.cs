using Minio;
using Minio.DataModel.Args;
using System.Configuration;

namespace _4_104_ITElective_Activity2.Core.Storage
{
    /// <summary>
    /// Handles all MinIO object storage operations.
    /// Upload images here; store the returned object name in the database as image_path.
    /// </summary>
    public class MinioStore
    {
        private readonly IMinioClient _client;
        private readonly string _bucket;

        public MinioStore()
        {
            var endpoint  = ConfigurationManager.AppSettings["Minio:Endpoint"]!;
            var accessKey = ConfigurationManager.AppSettings["Minio:AccessKey"]!;
            var secretKey = ConfigurationManager.AppSettings["Minio:SecretKey"]!;
            _bucket       = ConfigurationManager.AppSettings["Minio:Bucket"]!;

            _client = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .Build();
            Console.WriteLine($"Minio Service Running Healthy -- {endpoint}");
        }

        /// <summary>
        /// Ensures the bucket exists. Call once on startup.
        /// </summary>
        public async Task EnsureBucketAsync()
        {
            var exists = await _client.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(_bucket));

            if (!exists)
                await _client.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(_bucket));
        }

        /// <summary>
        /// Uploads a file stream and returns the object name stored in MinIO.
        /// Store this object name as image_path in the database.
        /// </summary>
        public async Task<string> UploadAsync(Stream data, string fileName, string contentType)
        {
            var objectName = $"{Guid.NewGuid()}_{fileName}";

            await _client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_bucket)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType));

            return objectName;
        }

        /// <summary>
        /// Returns a presigned URL valid for 1 hour to display the image.
        /// </summary>
        public async Task<string> GetUrlAsync(string objectName)
        {
            return await _client.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                .WithBucket(_bucket)
                .WithObject(objectName)
                .WithExpiry(3600));
        }

        /// <summary>
        /// Deletes an object from the bucket.
        /// </summary>
        public async Task DeleteAsync(string objectName)
        {
            await _client.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(_bucket)
                .WithObject(objectName));
        }
    }
}
