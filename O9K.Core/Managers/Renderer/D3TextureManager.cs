using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Ensage;
using O9K.Core.Logger;
using O9K.Core.Managers.Renderer.VPK;
using SharpDX;

namespace O9K.Core.Managers.Renderer
{
	// Token: 0x0200002B RID: 43
	public abstract class D3TextureManager<T> : IDisposable, ITextureManager where T : D3Texture
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00010C88 File Offset: 0x0000EE88
		protected D3TextureManager(VpkBrowser9 vpkBrowser)
		{
			this.VpkBrowser = vpkBrowser;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000298C File Offset: 0x00000B8C
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002994 File Offset: 0x00000B94
		protected bool ChangeBrightness { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000299D File Offset: 0x00000B9D
		protected IDictionary<string, T> TextureCache { get; } = new ConcurrentDictionary<string, T>();

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000029A5 File Offset: 0x00000BA5
		protected VpkBrowser9 VpkBrowser { get; }

		// Token: 0x060000D9 RID: 217 RVA: 0x000029AD File Offset: 0x00000BAD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000111C8 File Offset: 0x0000F3C8
		public T GetTexture(string textureKey)
		{
			T t;
			if (this.TextureCache.TryGetValue(textureKey, out t) && t != null)
			{
				return t;
			}
			return this.TextureCache["invoker_empty1"];
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000029BC File Offset: 0x00000BBC
		public bool IsTextureLoaded(string textureKey)
		{
			return this.TextureCache.ContainsKey(textureKey);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00011200 File Offset: 0x0000F400
		public void LoadFromDota(string textureKey, string file, int w = 0, int h = 0, bool rounded = false, int brightness = 0, Vector4? colorRatio = null)
		{
			if (this.TextureCache.ContainsKey(textureKey))
			{
				return;
			}
			this.TextureCache[textureKey] = default(T);
			Task.Run(delegate
			{
				try
				{
					Bitmap bitmap = this.VpkBrowser.FindImage(file);
					if (bitmap == null)
					{
						Logger.Warn(string.Format("O9K // Can't find Dota Texture: {0}", file));
					}
					else
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							if (w > 0 && h > 0 && (w != bitmap.Width || h != bitmap.Height))
							{
								bitmap = bitmap.Clone(new Rectangle(0, 0, Math.Min(bitmap.Width, w), Math.Min(bitmap.Height, h)), bitmap.PixelFormat);
							}
							if (rounded)
							{
								float xRatio;
								if (!this.heroRoundAdjustments.TryGetValue(textureKey, out xRatio))
								{
									xRatio = 0.5f;
								}
								bitmap = D3TextureManager<T>.RoundImage(bitmap, xRatio);
							}
							if (this.ChangeBrightness)
							{
								bitmap = D3TextureManager<T>.AdjustBrightness(bitmap, -35);
							}
							if (colorRatio != null)
							{
								bitmap = D3TextureManager<T>.AdjustColor(bitmap, colorRatio.Value);
							}
							if (brightness != 0)
							{
								bitmap = D3TextureManager<T>.AdjustBrightness(bitmap, brightness);
							}
							bitmap.Save(memoryStream, ImageFormat.Png);
							bitmap.Dispose();
							this.LoadFromStream(textureKey, memoryStream);
						}
					}
				}
				catch (Exception exception)
				{
					Logger.Error(exception, file);
				}
			});
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00011290 File Offset: 0x0000F490
		public void LoadFromDota(AbilityId abilityId, bool rounded = false)
		{
			try
			{
				string text = abilityId.ToString();
				bool flag = abilityId < AbilityId.attribute_bonus;
				string arg = flag ? "items" : "spellicons";
				string arg2 = flag ? text.Substring("item_".Length) : text;
				string file = string.Format("panorama\\images\\{0}\\{1}_png.vtex_c", arg, arg2);
				if (rounded)
				{
					text += "_rounded";
				}
				this.LoadFromDota(text, file, flag ? 86 : 128, flag ? 64 : 128, rounded, 0, null);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00011340 File Offset: 0x0000F540
		public void LoadFromDota(HeroId heroId, bool rounded = false, bool icon = false)
		{
			string text = heroId.ToString();
			if (icon)
			{
				string file = string.Format("panorama\\images\\heroes\\icons\\{0}_png.vtex_c", text);
				this.LoadFromDota(text + "_icon", file, 0, 0, false, 0, null);
				return;
			}
			this.LoadFromDota(text, rounded);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00011394 File Offset: 0x0000F594
		public void LoadFromDota(string unitName, bool rounded = false)
		{
			try
			{
				string file = string.Format("panorama\\images\\heroes\\{0}_png.vtex_c", unitName);
				if (rounded)
				{
					unitName += "_rounded";
				}
				this.LoadFromDota(unitName, file, 128, 72, rounded, 0, null);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000113F4 File Offset: 0x0000F5F4
		public void LoadFromFile(string textureKey, string file)
		{
			try
			{
				if (!this.TextureCache.ContainsKey(textureKey))
				{
					if (!File.Exists(file))
					{
						throw new FileNotFoundException(file);
					}
					using (FileStream fileStream = File.OpenRead(file))
					{
						string extension = Path.GetExtension(file);
						if (extension == ".png")
						{
							this.LoadFromStream(textureKey, fileStream);
							this.TextureCache[textureKey].File = file;
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0001148C File Offset: 0x0000F68C
		public void LoadFromMemory(string textureKey, byte[] data)
		{
			try
			{
				if (!this.TextureCache.ContainsKey(textureKey))
				{
					using (MemoryStream memoryStream = new MemoryStream(data))
					{
						this.LoadFromStream(textureKey, memoryStream);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000114EC File Offset: 0x0000F6EC
		public void LoadFromResource(string textureKey, string file)
		{
			try
			{
				if (!this.TextureCache.ContainsKey(textureKey))
				{
					Assembly callingAssembly = Assembly.GetCallingAssembly();
					string text = Array.Find<string>(callingAssembly.GetManifestResourceNames(), (string f) => f.EndsWith(file));
					if (text == null)
					{
						Logger.Warn(string.Format("O9K // Can't find Resource Texture: {0}", file));
					}
					else
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							Stream manifestResourceStream = callingAssembly.GetManifestResourceStream(text);
							if (manifestResourceStream != null)
							{
								manifestResourceStream.CopyTo(memoryStream);
							}
							this.LoadFromStream(textureKey, memoryStream);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000E3 RID: 227
		public abstract void LoadFromStream(string textureKey, Stream stream);

		// Token: 0x060000E4 RID: 228 RVA: 0x000115A4 File Offset: 0x0000F7A4
		public void LoadOutlineFromDota(string textureKey, string file, int brightness = 0, Vector4? colorRatio = null)
		{
			if (this.TextureCache.ContainsKey(textureKey))
			{
				return;
			}
			this.TextureCache[textureKey] = default(T);
			Task.Run(delegate
			{
				try
				{
					Bitmap bitmap = this.VpkBrowser.FindImage(file);
					if (bitmap == null)
					{
						Logger.Warn(string.Format("O9K // Can't find Dota Texture: {0}", file));
					}
					else
					{
						if (this.ChangeBrightness)
						{
							bitmap = D3TextureManager<T>.AdjustBrightness(bitmap, -35);
						}
						if (colorRatio != null)
						{
							bitmap = D3TextureManager<T>.AdjustColor(bitmap, colorRatio.Value);
						}
						if (brightness != 0)
						{
							bitmap = D3TextureManager<T>.AdjustBrightness(bitmap, brightness);
						}
						for (int i = 0; i <= 100; i++)
						{
							using (MemoryStream memoryStream = new MemoryStream())
							{
								Bitmap bitmap2 = D3TextureManager<T>.PieImage(bitmap, i);
								bitmap2.Save(memoryStream, ImageFormat.Png);
								bitmap2.Dispose();
								this.LoadFromStream(textureKey + i, memoryStream);
							}
						}
						bitmap.Dispose();
					}
				}
				catch (Exception exception)
				{
					Logger.Error(exception, file);
				}
			});
		}

		// Token: 0x060000E5 RID: 229
		protected abstract void Dispose(bool disposing);

		// Token: 0x060000E6 RID: 230 RVA: 0x0001161C File Offset: 0x0000F81C
		private static Bitmap AdjustBrightness(Image image, int value)
		{
			float num = (float)value / 255f;
			float[][] array = new float[5][];
			int num2 = 0;
			float[] array2 = new float[5];
			array2[0] = 1f;
			array[num2] = array2;
			int num3 = 1;
			float[] array3 = new float[5];
			array3[1] = 1f;
			array[num3] = array3;
			int num4 = 2;
			float[] array4 = new float[5];
			array4[2] = 1f;
			array[num4] = array4;
			int num5 = 3;
			float[] array5 = new float[5];
			array5[3] = 1f;
			array[num5] = array5;
			array[4] = new float[]
			{
				num,
				num,
				num,
				0f,
				1f
			};
			ColorMatrix colorMatrix = new ColorMatrix(array);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(colorMatrix);
			Point[] destPoints = new Point[]
			{
				new Point(0, 0),
				new Point(image.Width, 0),
				new Point(0, image.Height)
			};
			Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
			Bitmap bitmap = new Bitmap(image.Width, image.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.DrawImage(image, destPoints, srcRect, GraphicsUnit.Pixel, imageAttributes);
			}
			image.Dispose();
			return bitmap;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0001174C File Offset: 0x0000F94C
		private static Bitmap AdjustColor(Image image, Vector4 colorRatio)
		{
			float[][] array = new float[5][];
			int num = 0;
			float[] array2 = new float[5];
			array2[0] = 1f * colorRatio.X;
			array[num] = array2;
			int num2 = 1;
			float[] array3 = new float[5];
			array3[1] = 1f * colorRatio.Y;
			array[num2] = array3;
			int num3 = 2;
			float[] array4 = new float[5];
			array4[2] = 1f * colorRatio.Z;
			array[num3] = array4;
			int num4 = 3;
			float[] array5 = new float[5];
			array5[3] = 1f * colorRatio.W;
			array[num4] = array5;
			array[4] = new float[]
			{
				0f,
				0f,
				0f,
				0f,
				1f
			};
			ColorMatrix colorMatrix = new ColorMatrix(array);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(colorMatrix);
			Point[] destPoints = new Point[]
			{
				new Point(0, 0),
				new Point(image.Width, 0),
				new Point(0, image.Height)
			};
			Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
			Bitmap bitmap = new Bitmap(image.Width, image.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.DrawImage(image, destPoints, srcRect, GraphicsUnit.Pixel, imageAttributes);
			}
			image.Dispose();
			return bitmap;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00011884 File Offset: 0x0000FA84
		private static Bitmap PieImage(Image source, int pct)
		{
			Bitmap bitmap = new Bitmap(source.Width, source.Height);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddPie(0, 0, source.Width, source.Height, -90f, 3.6f * (float)pct);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.SetClip(graphicsPath);
					graphics.DrawImage(source, new PointF(0f, 0f));
				}
			}
			return bitmap;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00011924 File Offset: 0x0000FB24
		private static Bitmap RoundImage(Image source, float xRatio)
		{
			int num = Math.Min(source.Width, source.Height);
			Bitmap bitmap = new Bitmap(num, num);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddEllipse(0, 0, num, num);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.SetClip(graphicsPath);
					graphics.DrawImage(source, new RectangleF((float)(source.Width - num) * -xRatio, (float)(source.Height - num) * -0.5f, (float)source.Width, (float)source.Height));
				}
			}
			source.Dispose();
			return bitmap;
		}

		// Token: 0x0400005F RID: 95
		private readonly Dictionary<string, float> heroRoundAdjustments = new Dictionary<string, float>
		{
			{
				"npc_dota_hero_antimage_rounded",
				0.8f
			},
			{
				"npc_dota_hero_axe_rounded",
				0.7f
			},
			{
				"npc_dota_hero_bloodseeker_rounded",
				0.6f
			},
			{
				"npc_dota_hero_crystal_maiden_rounded",
				0.7f
			},
			{
				"npc_dota_hero_drow_ranger_rounded",
				0.3f
			},
			{
				"npc_dota_hero_earthshaker_rounded",
				0.4f
			},
			{
				"npc_dota_hero_juggernaut_rounded",
				0.4f
			},
			{
				"npc_dota_hero_morphling_rounded",
				0.2f
			},
			{
				"npc_dota_hero_phantom_lancer_rounded",
				0.2f
			},
			{
				"npc_dota_hero_puck_rounded",
				0.9f
			},
			{
				"npc_dota_hero_pudge_rounded",
				0.3f
			},
			{
				"npc_dota_hero_razor_rounded",
				0.9f
			},
			{
				"npc_dota_hero_sven_rounded",
				0.7f
			},
			{
				"npc_dota_hero_zuus_rounded",
				0.6f
			},
			{
				"npc_dota_hero_kunkka_rounded",
				0.3f
			},
			{
				"npc_dota_hero_lina_rounded",
				0.2f
			},
			{
				"npc_dota_hero_lion_rounded",
				0.4f
			},
			{
				"npc_dota_hero_shadow_shaman_rounded",
				0.1f
			},
			{
				"npc_dota_hero_tidehunter_rounded",
				0.1f
			},
			{
				"npc_dota_hero_witch_doctor_rounded",
				0.3f
			},
			{
				"npc_dota_hero_lich_rounded",
				0.3f
			},
			{
				"npc_dota_hero_riki_rounded",
				0.8f
			},
			{
				"npc_dota_hero_enigma_rounded",
				0.7f
			},
			{
				"npc_dota_hero_tinker_rounded",
				0.5f
			},
			{
				"npc_dota_hero_sniper_rounded",
				0.4f
			},
			{
				"npc_dota_hero_necrolyte_rounded",
				0.8f
			},
			{
				"npc_dota_hero_venomancer_rounded",
				0.7f
			},
			{
				"npc_dota_hero_dragon_knight_rounded",
				0.9f
			},
			{
				"npc_dota_hero_skeleton_king_rounded",
				0.3f
			},
			{
				"npc_dota_hero_death_prophet_rounded",
				1f
			},
			{
				"npc_dota_hero_pugna_rounded",
				0.6f
			},
			{
				"npc_dota_hero_templar_assassin_rounded",
				0.3f
			},
			{
				"npc_dota_hero_rattletrap_rounded",
				0.6f
			},
			{
				"npc_dota_hero_leshrac_rounded",
				0.8f
			},
			{
				"npc_dota_hero_life_stealer_rounded",
				0.75f
			},
			{
				"npc_dota_hero_clinkz_rounded",
				0.4f
			},
			{
				"npc_dota_hero_omniknight_rounded",
				0.3f
			},
			{
				"npc_dota_hero_huskar_rounded",
				0.3f
			},
			{
				"npc_dota_hero_night_stalker_rounded",
				0.7f
			},
			{
				"npc_dota_hero_broodmother_rounded",
				0.4f
			},
			{
				"npc_dota_hero_ancient_apparition_rounded",
				0.6f
			},
			{
				"npc_dota_hero_ursa_rounded",
				0.2f
			},
			{
				"npc_dota_hero_jakiro_rounded",
				0.9f
			},
			{
				"npc_dota_hero_weaver_rounded",
				0.6f
			},
			{
				"npc_dota_hero_batrider_rounded",
				0.7f
			},
			{
				"npc_dota_hero_spectre_rounded",
				0.8f
			},
			{
				"npc_dota_hero_spirit_breaker_rounded",
				0.6f
			},
			{
				"npc_dota_hero_gyrocopter_rounded",
				0.3f
			},
			{
				"npc_dota_hero_alchemist_rounded",
				0f
			},
			{
				"npc_dota_hero_invoker_rounded",
				0.4f
			},
			{
				"npc_dota_hero_lycan_rounded",
				0.65f
			},
			{
				"npc_dota_hero_brewmaster_rounded",
				0.75f
			},
			{
				"npc_dota_hero_shadow_demon_rounded",
				0.6f
			},
			{
				"npc_dota_hero_lone_druid_rounded",
				1f
			},
			{
				"npc_dota_hero_chaos_knight_rounded",
				0.4f
			},
			{
				"npc_dota_hero_meepo_rounded",
				0.8f
			},
			{
				"npc_dota_hero_treant_rounded",
				0.4f
			},
			{
				"npc_dota_hero_ogre_magi_rounded",
				0.2f
			},
			{
				"npc_dota_hero_undying_rounded",
				0.3f
			},
			{
				"npc_dota_hero_disruptor_rounded",
				0.3f
			},
			{
				"npc_dota_hero_naga_siren_rounded",
				0.3f
			},
			{
				"npc_dota_hero_keeper_of_the_light_rounded",
				0.2f
			},
			{
				"npc_dota_hero_wisp_rounded",
				0.3f
			},
			{
				"npc_dota_hero_slark_rounded",
				0.4f
			},
			{
				"npc_dota_hero_medusa_rounded",
				0.4f
			},
			{
				"npc_dota_hero_troll_warlord_rounded",
				0.2f
			},
			{
				"npc_dota_hero_magnataur_rounded",
				0.6f
			},
			{
				"npc_dota_hero_shredder_rounded",
				0.7f
			},
			{
				"npc_dota_hero_bristleback_rounded",
				0.3f
			},
			{
				"npc_dota_hero_tusk_rounded",
				0.3f
			},
			{
				"npc_dota_hero_skywrath_mage_rounded",
				0.2f
			},
			{
				"npc_dota_hero_abaddon_rounded",
				0.4f
			},
			{
				"npc_dota_hero_legion_commander_rounded",
				0.3f
			},
			{
				"npc_dota_hero_techies_rounded",
				0.1f
			},
			{
				"npc_dota_hero_ember_spirit_rounded",
				0.2f
			},
			{
				"npc_dota_hero_oracle_rounded",
				0.7f
			},
			{
				"npc_dota_hero_winter_wyvern_rounded",
				0.6f
			},
			{
				"npc_dota_hero_monkey_king_rounded",
				0.2f
			},
			{
				"npc_dota_hero_dark_willow_rounded",
				0.4f
			},
			{
				"npc_dota_hero_pangolier_rounded",
				0.8f
			},
			{
				"npc_dota_hero_grimstroke_rounded",
				0.9f
			}
		};
	}
}
