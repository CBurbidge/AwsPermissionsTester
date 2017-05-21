using System.Collections.Generic;

namespace PermissionAnalyser.Calls
{
    public class PathAndActions
    {
        public PathAndActions(string path, List<string> actions)
        {
            Path = path;
            Actions = actions;
        }

        public string Path { get; }
        public List<string> Actions { get; }
    }
}