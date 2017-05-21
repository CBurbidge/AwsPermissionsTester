namespace PermissionAnalyser
{
    public class TypeAndMethod
    {
        public string Type { get; }
        public string Method { get; }

        public TypeAndMethod(string type, string method)
        {
            Type = type;
            Method = method;
        }
    }
}