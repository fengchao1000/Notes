using FC.Notes.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FC.Notes.Permissions
{
    public class NotesPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(NotesPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(NotesPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NotesResource>(name);
        }
    }
}
