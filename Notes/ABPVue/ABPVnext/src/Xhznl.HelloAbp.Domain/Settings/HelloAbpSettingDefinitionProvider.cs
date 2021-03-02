using EasyAbp.Abp.SettingUi;
using Volo.Abp.Localization;
using Volo.Abp.Settings;
using Xhznl.HelloAbp.Localization;

namespace Xhznl.HelloAbp.Settings
{
    public class HelloAbpSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
			//Define your own settings here. Example:
			//context.Add(new SettingDefinition(HelloAbpSettings.MySetting1));

			context.Add(
				// Set properties with "WithProperty" method
				new SettingDefinition(
						"Connection.Ip",
						"127.0.0.1",
						L("DisplayName:Connection.Ip"),
						L("Description:Connection.Ip"))
					.WithProperty("Group1", "Server")
					.WithProperty("Group2", "Connection"),

				// The properties are defined in the "MyAbpAppSettingProperties.json" file
				new SettingDefinition(
					"Connection.Port",
					8080.ToString(),
					L("DisplayName:Connection.Port"),
					L("Description:Connection.Port")
				),

				// If a setting's DisplayName and Description are not defined, we can still localize it by using our own localization resources.
				// Just add the localization resources to the `SettingUiResource`, see `ConfigureLocalizationServices` method in `MyAbpAppWebModule`
				new SettingDefinition("Connection.Protocol")
			);

			context.Add(
				new SettingDefinition(
						HelloAbpSettings.SettingExample.ASettings.Setting1,
						"setting 1 value")
					.WithProperty(SettingUiConst.Group1, HelloAbpSettings.SettingExample.Default)
					.WithProperty(SettingUiConst.Group2, HelloAbpSettings.SettingExample.ASettings.GroupName),
				new SettingDefinition(
						HelloAbpSettings.SettingExample.ASettings.Setting2,
						"500")
					.WithProperty(SettingUiConst.Group1, HelloAbpSettings.SettingExample.Default)
					.WithProperty(SettingUiConst.Group2, HelloAbpSettings.SettingExample.ASettings.GroupName)
					.WithProperty(SettingUiConst.Type, "number"),
				new SettingDefinition(
						HelloAbpSettings.SettingExample.ASettings.Setting3,
						"true")
					.WithProperty(SettingUiConst.Group1, HelloAbpSettings.SettingExample.Default)
					.WithProperty(SettingUiConst.Group2, HelloAbpSettings.SettingExample.ASettings.GroupName)
					.WithProperty(SettingUiConst.Type, "checkbox"),

				new SettingDefinition(
						HelloAbpSettings.SettingExample.BSettings.Setting1,
						"setting 1 value")
					.WithProperty(SettingUiConst.Group1, HelloAbpSettings.SettingExample.Default)
					.WithProperty(SettingUiConst.Group2, HelloAbpSettings.SettingExample.BSettings.GroupName)
			);

		}

		private static LocalizableString L(string name)
		{
			return LocalizableString.Create<HelloAbpResource>(name);
		}
	}
}
