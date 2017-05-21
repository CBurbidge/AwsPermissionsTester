using System.Collections.Generic;
using System.Linq;
using PermissionAnalyser.Permissions;

namespace PermissionAnalyser
{
    public class TypeConfig
    {
        public static List<Permission> GetPermissions()
        {
            var groups = new List<List<Permission>>
            {
                S3Permissions.All()
            };
            return groups.Aggregate(new List<Permission>(), (agg, elem) => agg.Concat(elem).ToList());
        }
    }
}