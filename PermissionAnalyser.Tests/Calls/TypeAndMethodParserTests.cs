using NUnit.Framework;
using PermissionAnalyser.Calls;

namespace PermissionAnalyser.Tests.Calls
{
    [TestFixture]
    public class TypeAndMethodParserTests
    {
        [Test]
        public void Parse_OnCorrectName_ReturnsTypeAndMethod()
        {
            string type = "Amazon.S3.IAmazonS3";
            string method = "GetObject";
            var input = $"Amazon.S3.Model.GetObjectResponse {type}::{method}(System.String,System.String)";
            var parser = new TypeAndMethodParser();

            var result = parser.Parse(input);

            Assert.That(result.Outcome, Is.EqualTo(TypeAndMethodParser.Result.ParseOutcome.Success));
            Assert.That(result.Value.Method, Is.EqualTo(method));
            Assert.That(result.Value.Type, Is.EqualTo(type));
        }

        [Test]
        public void Parse_OnJunkName_ReturnsFailure()
        {
            var input = "sadfsadfw dsflsj fsad:  .sdfdsadsf";
            var parser = new TypeAndMethodParser();

            var result = parser.Parse(input);

            Assert.That(result.Outcome, Is.EqualTo(TypeAndMethodParser.Result.ParseOutcome.Failure));
        }
    }
}
