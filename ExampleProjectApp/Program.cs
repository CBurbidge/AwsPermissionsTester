﻿using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.S3;

namespace ExampleProjectApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var s3Client = new AmazonS3Client();
            var dynamoClient = new AmazonDynamoDBClient();
            GetValue(s3Client, dynamoClient);
            var thing = 0;
            Console.WriteLine("Done.");
        }

        private static void GetValue(IAmazonS3 s3Client, IAmazonDynamoDB dynamoClient)
        {
            Action thing = () => s3Client.GetObject("some-bucket", "some-key");
            thing();
            Action thing2 = () => dynamoClient.GetItem(new GetItemRequest());
            Console.WriteLine(thing2);
        }
    }
}
