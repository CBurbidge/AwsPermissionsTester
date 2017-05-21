namespace PermissionAnalyser.Permissions
{
    public class Actions
    {
        public class S3Actions
        {
            public static readonly string AbortMultipartUpload = "s3:AbortMultipartUpload";
            public static readonly string DeleteBucket = "s3:DeleteBucket";
            public static readonly string PutObject = "s3:PutObject";
            public static readonly string PutAnalyticsConfiguration = "s3:PutAnalyticsConfiguration";
        }
    }
}