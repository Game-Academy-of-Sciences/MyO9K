using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Renderer;
using O9K.Hud.Core;

namespace O9K.Hud.Helpers
{
	// Token: 0x020000A4 RID: 164
	internal class TextureLoader : IDisposable, IHudModule
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00004546 File Offset: 0x00002746
		[ImportingConstructor]
		public TextureLoader(IContext9 context)
		{
			this.context = context;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		public void Activate()
		{
			this.textureManager = this.context.Renderer.TextureManager;
			foreach (Player player in ObjectManager.GetEntities<Player>())
			{
				HeroId selectedHeroId = player.SelectedHeroId;
				if (selectedHeroId != HeroId.npc_dota_hero_base)
				{
					this.textureManager.LoadFromDota(selectedHeroId, false, false);
					this.textureManager.LoadFromDota(selectedHeroId, true, false);
					this.textureManager.LoadFromDota(selectedHeroId, false, true);
					this.loaded.Add(selectedHeroId.ToString());
				}
			}
			EntityManager9.UnitAdded += this.OnUnitAdded;
			EntityManager9.AbilityAdded += this.OnAbilityAdded;
			this.LoadTextures();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00004560 File Offset: 0x00002760
		public void Dispose()
		{
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00004584 File Offset: 0x00002784
		private void LoadAbilityTexture(AbilityId id)
		{
			this.textureManager.LoadFromDota(id, false);
			this.textureManager.LoadFromDota(id, true);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000045A0 File Offset: 0x000027A0
		private void LoadTextures()
		{
			this.LoadAbilityTexture(AbilityId.item_smoke_of_deceit);
			this.LoadAbilityTexture(AbilityId.item_ward_sentry);
			this.LoadAbilityTexture(AbilityId.item_ward_observer);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (!this.loaded.Contains(ability.Name))
				{
					if (!ability.IsTalent)
					{
						this.textureManager.LoadFromDota(ability.Id, false);
						this.textureManager.LoadFromDota(ability.Id, true);
					}
					this.loaded.Add(ability.Name);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (!this.loaded.Contains(unit.Name))
				{
					Hero9 hero;
					if ((hero = (unit as Hero9)) != null)
					{
						this.textureManager.LoadFromDota(hero.Id, false, false);
						this.textureManager.LoadFromDota(hero.Id, true, false);
						this.textureManager.LoadFromDota(hero.Id, false, true);
					}
					else if (unit.IsUnit && !unit.IsCreep)
					{
						this.textureManager.LoadFromDota(unit.DefaultName, false);
					}
					this.loaded.Add(unit.Name);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0400025B RID: 603
		private readonly IContext9 context;

		// Token: 0x0400025C RID: 604
		private readonly HashSet<string> loaded = new HashSet<string>();

		// Token: 0x0400025D RID: 605
		private ITextureManager textureManager;
	}
}
