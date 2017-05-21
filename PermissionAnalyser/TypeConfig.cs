using System.Collections.Generic;

namespace PermissionAnalyser
{
    public class PermissionObj
    {
        public string Type { get; set; }
        public string Method { get; set; }
        public string Permission { get; set; }
    }
    public class MethodAndPermission
    {
        public string Service { get; set; }
        public List<PermissionObj> Permissions { get; set; }
    }
    /// <summary>
    /// Definition of types to search for.
    /// Could do this by linking to all of the sdks, but this way doesn't require having them all there.
    /// </summary>
    public class TypeConfig
    {
        public List<PermissionObj> MethodAndPermissions { get; set; }
    }
}