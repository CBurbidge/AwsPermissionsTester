namespace PermissionAnalyser.Calls
{
    public class TypeAndMethodParser
    {
        public Result Parse(string fullName)
        {
            var parts = fullName.Split(' ');
            if(parts.Length < 2) return new Result(Result.ParseOutcome.Failure, TypeAndMethod.Failed);
            var nameAndParameters = parts[1];
            var methodNameSplit = nameAndParameters.Split('(');
            if (nameAndParameters.Length < 2) return new Result(Result.ParseOutcome.Failure, TypeAndMethod.Failed);
            var typeAndMethod = methodNameSplit[0].Split(':');
            if (typeAndMethod.Length < 3) return new Result(Result.ParseOutcome.Failure, TypeAndMethod.Failed);
            var type = typeAndMethod[0];
            var method = typeAndMethod[2];

            return new Result(Result.ParseOutcome.Success, new TypeAndMethod(type, method));
        }

        public class Result
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