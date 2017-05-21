namespace PermissionAnalyser.Calls
{
    internal class TypeAndMethodParser
    {
        public Result Parse(string fullName)
        {
            var parts = fullName.Split(' ');
            var nameAndParameters = parts[1];
            var typeAndMethod = nameAndParameters.Split('(')[0].Split(':');
            var type = typeAndMethod[0];
            var method = typeAndMethod[2];

            return new Result(Result.ParseOutcome.Success, new TypeAndMethod(type, method));
        }

        internal class Result
        {
            public Result(ParseOutcome outcome, TypeAndMethod value)
            {
                Outcome = outcome;
                Value = value;
            }

            public enum ParseOutcome
            {
                Success, Failure
            }

            public ParseOutcome Outcome { get; }
            public TypeAndMethod Value { get; }
        }
    }
}