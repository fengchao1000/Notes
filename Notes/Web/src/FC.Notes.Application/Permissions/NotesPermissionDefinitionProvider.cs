using FC.Notes.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace FC.Notes.Permissions
{
    public class NotesPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var notesGroup = context.AddGroup(NotesPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(NotesPermissions.MyPermission1, L("Permission:MyPermission1"));
             
            var notesPermission = notesGroup.AddPermission(NotesPermissions.Notes.Default, L("Notes:Notes"), multiTenancySide: MultiTenancySides.Both);
            notesPermission.AddChild(NotesPermissions.Notes.Create, L("Notes:Create"), multiTenancySide: MultiTenancySides.Both);
            notesPermission.AddChild(NotesPermissions.Notes.Update, L("Notes:Edit"), multiTenancySide: MultiTenancySides.Both);
            notesPermission.AddChild(NotesPermissions.Notes.Delete, L("Notes:Delete"), multiTenancySide: MultiTenancySides.Both);

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NotesResource>(name);
        }
    }
}
