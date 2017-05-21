namespace PermissionAnalyser.Calls
{
    public class TypeAndMethod
    {
        public static TypeAndMethod Failed = new TypeAndMethod("", "");
        public string Type { get; }
        public string Method { get; }

        public TypeAndMethod(string type, string method)
        {
            Type = type;
            Method = method;
        }
    }
}