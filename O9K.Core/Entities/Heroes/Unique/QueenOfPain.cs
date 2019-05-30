using System;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Talents;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Logger;

namespace O9K.Core.Entities.Heroes.Unique
{
	// Token: 0x020000D0 RID: 208
	[HeroId(HeroId.npc_dota_hero_queenofpain)]
	internal class QueenOfPain : Hero9, IDisposable
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x00006313 File Offset: 0x00004513
		public QueenOfPain(Hero baseHero) : base(baseHero)
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00006327 File Offset: 0x00004527
		public override bool IsLinkensProtected
		{
			get
			{
				if (!base.IsLinkensProtected)
				{
					SpellBlockTalent spellBlockTalent = this.linkensSphereTalent;
					return spellBlockTalent != null && spellBlockTalent.Level > 0u && !this.talentSleeper.IsSleeping;
				}
				return true;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000635A File Offset: 0x0000455A
		public void Dispose()
		{
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000636D File Offset: 0x0000456D
		internal override void Ability(Ability9 ability, bool added)
		{
			base.Ability(ability, added);
			if (added && ability.Id == AbilityId.special_bonus_spell_block_15)
			{
				this.linkensSphereTalent = (SpellBlockTalent)ability;
				Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002132C File Offset: 0x0001F52C
		private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			try
			{
				if (!(args.ParticleEffect.Name != "particles/items_fx/immunity_sphere.vpcf"))
				{
					SpellBlockTalent spellBlockTalent = this.linkensSphereTalent;
					if (spellBlockTalent != null && spellBlockTalent.IsValid && this.linkensSphereTalent.Level > 0u)
					{
						UpdateManager.BeginInvoke(delegate
						{
							try
							{
								if (this.IsValid && this.IsVisible && this.Distance(args.ParticleEffect.GetControlPoint(0u)) < 15f)
								{
									this.talentSleeper.Sleep(this.linkensSphereTalent.SpellBlockCooldown);
								}
							}
							catch (Exception exception2)
							{
								Logger.Error(exception2, null);
							}
						}, 0);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
				Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
			}
		}

		// Token: 0x040002D5 RID: 725
		private readonly Sleeper talentSleeper = new Sleeper();

		// Token: 0x040002D6 RID: 726
		private SpellBlockTalent linkensSphereTalent;
	}
}
