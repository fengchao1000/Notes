
using Volo.Abp.Reflection;

namespace FC.Notes.Permissions
{
    public static class NotesPermissions
    {
        public const string GroupName = "Notes";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
          
        public static class Notes
        {
            public const string Default = GroupName + ".Notes";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManageFeatures = Default + ".ManageFeatures";
            public const string ManageConnectionStrings = Default + ".ManageConnectionStrings";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(NotesPermissions));
        }

    }
}