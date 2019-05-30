using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Logger;
using SharpDX;

namespace O9K.Core.Helpers
{
	// Token: 0x0200008E RID: 142
	public static class Hud
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000049E0 File Offset: 0x00002BE0
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000049E7 File Offset: 0x00002BE7
		public static Vector3 CameraPosition
		{
			get
			{
				return Player.CameraPosition;
			}
			set
			{
				Game.ExecuteCommand(string.Concat(new object[]
				{
					"dota_camera_set_lookatpos ",
					value.X,
					" ",
					value.Y
				}));
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00004A25 File Offset: 0x00002C25
		public static void CenterCameraOnHero(bool enabled = true)
		{
			Game.ExecuteCommand((enabled ? "+" : "-") + "dota_camera_center_on_hero");
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001E76C File Offset: 0x0001C96C
		public static void DisplayWarning(string text, float time = 10f)
		{
			try
			{
				if (Hud.Messages.ContainsKey(text))
				{
					Hud.Messages[text] = Game.RawGameTime + time;
				}
				else
				{
					Hud.Messages.Add(text, Game.RawGameTime + time);
					if (Hud.Messages.Count == 1)
					{
						Drawing.OnDraw += Hud.OnDraw;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
		private static void OnDraw(EventArgs args)
		{
			try
			{
				if (Hud.Messages.Count == 0)
				{
					Drawing.OnDraw -= Hud.OnDraw;
				}
				Vector2 vector;
				vector..ctor(Hud.Info.ScreenSize.X * 0.13f, Hud.Info.ScreenSize.Y * 0.05f);
				foreach (KeyValuePair<string, float> keyValuePair in Hud.Messages.ToList<KeyValuePair<string, float>>())
				{
					string key = keyValuePair.Key;
					float value = keyValuePair.Value;
					if (Game.RawGameTime > value)
					{
						Hud.Messages.Remove(key);
					}
					else
					{
						vector += new Vector2(0f, 35f);
						Drawing.DrawText(key, "Calibri", vector, new Vector2(33f * Hud.Info.ScreenRatio), Color.OrangeRed, FontFlags.DropShadow);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x040001FA RID: 506
		private static readonly Dictionary<string, float> Messages = new Dictionary<string, float>();

		// Token: 0x0200008F RID: 143
		public static class Info
		{
			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000453 RID: 1107 RVA: 0x00004A51 File Offset: 0x00002C51
			public static Vector2 ScreenSize { get; } = new Vector2((float)Drawing.Width, (float)Drawing.Height);

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000454 RID: 1108 RVA: 0x00004A58 File Offset: 0x00002C58
			public static float ScreenRatio { get; } = (float)Drawing.Height / 1080f;

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000455 RID: 1109 RVA: 0x00004A5F File Offset: 0x00002C5F
			public static Vector2 GlyphPosition { get; } = new Vector2((float)Drawing.Width * 0.16f, (float)Drawing.Height * 0.965f);

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000456 RID: 1110 RVA: 0x00004A66 File Offset: 0x00002C66
			public static Vector2 ScanPosition { get; } = new Vector2((float)Drawing.Width * 0.16f, (float)Drawing.Height * 0.925f);
		}
	}
}
