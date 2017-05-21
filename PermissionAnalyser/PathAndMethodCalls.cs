using System.Collections.Generic;

namespace PermissionAnalyser
{
    public class PathAndMethodCalls
    {
        public PathAndMethodCalls(string path, List<TypeAndMethod> calls)
        {
            Path = path;
            Calls = calls;
        }

        public string Path { get; }
        public List<TypeAndMethod> Calls { get; }
    }
}