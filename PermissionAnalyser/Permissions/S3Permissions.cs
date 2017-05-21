using System.Collections.Generic;
using System.Linq;
using Amazon.S3;

namespace PermissionAnalyser.Permissions
{
    public class S3Permissions
    {
        public static List<Permission> All()
        {
            var groups = new List<List<Permission>>
            {
                FromMethod(nameof(AmazonS3Client.AbortMultipartUpload), Actions.S3Actions.AbortMultipartUpload),
                FromMethod(nameof(AmazonS3Client.AbortMultipartUploadAsync), Actions.S3Actions.AbortMultipartUpload),
                FromMethod(nameof(AmazonS3Client.CompleteMultipartUpload), Actions.S3Actions.PutObject),
                FromMethod(nameof(AmazonS3Client.CompleteMultipartUploadAsync), Actions.S3Actions.PutObject),
                FromMethod(nameof(AmazonS3Client.CopyObject), Actions.S3Actions.PutObject),
                FromMethod(nameof(AmazonS3Client.CopyObjectAsync), Actions.S3Actions.PutObject),
                FromMethod(nameof(AmazonS3Client.CopyPart), Actions.S3Actions.PutObject),
                FromMethod(nameof(AmazonS3Client.CopyPartAsync), Actions.S3Actions.PutObject),
                FromMethod(nameof(AmazonS3Client.DeleteBucket), Actions.S3Actions.DeleteBucket),
                FromMethod(nameof(AmazonS3Client.DeleteBucketAsync), Actions.S3Actions.DeleteBucket),
                FromMethod(nameof(AmazonS3Client.DeleteBucketAnalyticsConfiguration), Actions.S3Actions.PutAnalyticsConfiguration),
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