using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using O9K.Core.Logger;
using O9K.Core.Managers.Menu.Items;

namespace O9K.Core.Managers.Menu
{
	// Token: 0x02000043 RID: 67
	internal sealed class MenuSerializer
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x000154F0 File Offset: 0x000136F0
		public MenuSerializer(params JsonConverter[] converters)
		{
			this.Settings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				DefaultValueHandling = DefaultValueHandling.Populate,
				NullValueHandling = NullValueHandling.Ignore,
				TypeNameHandling = TypeNameHandling.Auto,
				Converters = converters,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};
			this.JsonSerializer = JsonSerializer.Create(this.Settings);
			this.ConfigDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "game", "O9K");
			try
			{
				Directory.CreateDirectory(this.ConfigDirectory);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000332C File Offset: 0x0000152C
		public string ConfigDirectory { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00003334 File Offset: 0x00001534
		public JsonSerializer JsonSerializer { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000333C File Offset: 0x0000153C
		public JsonSerializerSettings Settings { get; }

		// Token: 0x060001C9 RID: 457 RVA: 0x00015594 File Offset: 0x00013794
		public JToken Deserialize(MenuItem menuItem)
		{
			string path = Path.Combine(this.ConfigDirectory, menuItem.Name + ".json");
			if (!File.Exists(path))
			{
				return null;
			}
			JToken result;
			try
			{
				result = JToken.Parse(File.ReadAllText(path));
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
				result = null;
			}
			return result;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000155F4 File Offset: 0x000137F4
		public void Serialize(MainMenu mainMenu)
		{
			List<MenuItem> list = mainMenu.MenuItems.ToList<MenuItem>();
			list.Add(mainMenu);
			foreach (MenuItem menuItem in list)
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
					{
						jsonTextWriter.Formatting = Formatting.Indented;
						jsonTextWriter.WriteStartObject();
						this.Serialize(jsonTextWriter, menuItem);
						jsonTextWriter.WriteEndObject();
					}
				}
				File.WriteAllText(Path.Combine(this.ConfigDirectory, menuItem.Name + ".json"), stringBuilder.ToString());
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000156DC File Offset: 0x000138DC
		private void Serialize(JsonWriter writer, MenuItem menuItem)
		{
			Menu menu;
			if ((menu = (menuItem as Menu)) != null && !menu.IsMainMenu)
			{
				List<string> list = new List<string>();
				writer.WritePropertyName(menuItem.Name);
				writer.WriteStartObject();
				foreach (MenuItem menuItem2 in menu.MenuItems)
				{
					this.Serialize(writer, menuItem2);
					list.Add(menuItem2.Name);
				}
				if (menu.Token != null)
				{
					foreach (KeyValuePair<string, JToken> keyValuePair in menu.Token.ToObject<JObject>())
					{
						if (!list.Contains(keyValuePair.Key))
						{
							writer.WritePropertyName(keyValuePair.Key);
							this.JsonSerializer.Serialize(writer, keyValuePair.Value);
						}
					}
				}
				writer.WriteEndObject();
				return;
			}
			object saveValue = menuItem.GetSaveValue();
			if (saveValue == null)
			{
				return;
			}
			writer.WritePropertyName(menuItem.Name);
			this.WritePropertyValue(writer, saveValue);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0001580C File Offset: 0x00013A0C
		private void WritePropertyValue(JsonWriter writer, object propertyValue)
		{
			object[] array;
			if (propertyValue.GetType().IsArray && (array = (propertyValue as object[])) != null)
			{
				writer.WriteStartArray();
				foreach (object value in array)
				{
					this.JsonSerializer.Serialize(writer, value);
				}
				writer.WriteEnd();
				return;
			}
			this.JsonSerializer.Serialize(writer, propertyValue);
		}
	}
}
