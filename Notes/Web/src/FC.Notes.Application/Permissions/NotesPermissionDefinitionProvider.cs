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
             
            var notesPermission = notesGroup.AddPermission(NotesPermissions.Notes.Default, L("Permission:Notes"), multiTenancySide: MultiTenancySides.Host);
            notesPermission.AddChild(NotesPermissions.Notes.Create, L("Permission:Create"), multiTenancySide: MultiTenancySides.Host);
            notesPermission.AddChild(NotesPermissions.Notes.Update, L("Permission:Edit"), multiTenancySide: MultiTenancySides.Host);
            notesPermission.AddChild(NotesPermissions.Notes.Delete, L("Permission:Delete"), multiTenancySide: MultiTenancySides.Host);
            notesPermission.AddChild(NotesPermissions.Notes.ManageFeatures, L("Permission:ManageFeatures"), multiTenancySide: MultiTenancySides.Host);
            notesPermission.AddChild(NotesPermissions.Notes.ManageConnectionStrings, L("Permission:ManageConnectionStrings"), multiTenancySide: MultiTenancySides.Host);

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NotesResource>(name);
        }
    }
}
