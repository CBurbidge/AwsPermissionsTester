using System.Collections.Generic;
using System.Linq;
using Amazon.S3;
using static PermissionAnalyser.Permissions.Actions.S3Actions;
namespace PermissionAnalyser.Permissions
{
    public class S3Permissions
    {
        public static List<Permission> All()
        {
            var groups = new List<List<Permission>>
            {
                FromMethod(nameof(AmazonS3Client.AbortMultipartUpload), AbortMultipartUpload),
                FromMethod(nameof(AmazonS3Client.AbortMultipartUploadAsync), AbortMultipartUpload),
                FromMethod(nameof(AmazonS3Client.CompleteMultipartUpload), PutObject),
                FromMethod(nameof(AmazonS3Client.CompleteMultipartUploadAsync), PutObject),
                FromMethod(nameof(AmazonS3Client.CopyObject), PutObject),
                FromMethod(nameof(AmazonS3Client.CopyObjectAsync), PutObject),
                FromMethod(nameof(AmazonS3Client.CopyPart), PutObject),
                FromMethod(nameof(AmazonS3Client.CopyPartAsync), PutObject),
                FromMethod(nameof(AmazonS3Client.DeleteBucket), DeleteBucket),
                FromMethod(nameof(AmazonS3Client.DeleteBucketAsync), DeleteBucket),
                FromMethod(nameof(AmazonS3Client.DeleteBucketAnalyticsConfiguration), PutAnalyticsConfiguration),


                FromMethod(nameof(AmazonS3Client.GetObject), GetObject),
            };
            return groups.Aggregate(new List<Permission>(), (agg, elem) => agg.Concat(elem).ToList());
        }
        public static List<Permission> FromMethod(string method, string action)
        {
            return new List<Permission>
            {
                new Permission("Amazon.S3.IAmazonS3", method, action),
                new Permission("Amazon.S3.AmazonS3Client", method, action)
            };
        }
    }
}