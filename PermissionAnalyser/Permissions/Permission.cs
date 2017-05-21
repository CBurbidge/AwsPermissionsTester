namespace PermissionAnalyser.Permissions
{
    public class Permission
    {
        public Permission(string type, string method, string action)
        {
            Type = type;
            Method = method;
            Action = action;
        }

        public string Type { get; }
        public string Method { get; }
        public string Action { get; }

        public override string ToString()
        {
            return $"{Type} - {Method} - {Action}";
        }
    }
}