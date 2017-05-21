namespace PermissionAnalyser
{
    internal class TypeAndMethodParser
    {
        public TypeAndMethodParseResult Parse(string fullName)
        {
            var parts = fullName.Split(' ');
            var nameAndParameters = parts[1];
            var typeAndMethod = nameAndParameters.Split('(')[0].Split(':');
            var type = typeAndMethod[0];
            var method = typeAndMethod[2];

            return new TypeAndMethodParseResult(TypeAndMethodParseResult.ParseResult.Success, new TypeAndMethod(type, method));
        }

        internal class TypeAndMethodParseResult
        {
            public TypeAndMethodParseResult(ParseResult outcome, TypeAndMethod result)
            {
                Outcome = outcome;
                Result = result;
            }

            public enum ParseResult
            {
                Success, Failure
            }

            public ParseResult Outcome { get; }
            public TypeAndMethod Result { get; }
        }
    }
}