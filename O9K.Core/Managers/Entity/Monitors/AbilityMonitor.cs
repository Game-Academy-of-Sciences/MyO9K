using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;

namespace O9K.Core.Managers.Entity.Monitors
{
	// Token: 0x0200006E RID: 110
	public sealed class AbilityMonitor : IDisposable
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0001BB28 File Offset: 0x00019D28
		public AbilityMonitor()
		{
			Entity.OnBoolPropertyChange += this.OnBoolPropertyChange;
			Entity.OnFloatPropertyChange += this.OnFloatPropertyChange;
			Entity.OnInt32PropertyChange += this.OnInt32PropertyChange;
			ObjectManager.OnAddEntity += AbilityMonitor.OnAddEntity;
			ObjectManager.OnRemoveEntity += AbilityMonitor.OnRemoveEntity;
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000389 RID: 905 RVA: 0x0001BBCC File Offset: 0x00019DCC
		// (remove) Token: 0x0600038A RID: 906 RVA: 0x0001BC04 File Offset: 0x00019E04
		public event AbilityMonitor.EventHandler AbilityCastChange;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600038B RID: 907 RVA: 0x0001BC3C File Offset: 0x00019E3C
		// (remove) Token: 0x0600038C RID: 908 RVA: 0x0001BC74 File Offset: 0x00019E74
		public event AbilityMonitor.EventHandler AbilityCasted;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600038D RID: 909 RVA: 0x0001BCAC File Offset: 0x00019EAC
		// (remove) Token: 0x0600038E RID: 910 RVA: 0x0001BCE4 File Offset: 0x00019EE4
		public event AbilityMonitor.EventHandler AbilityChannel;

		// Token: 0x0600038F RID: 911 RVA: 0x0001BD1C File Offset: 0x00019F1C
		public void Dispose()
		{
			Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
			Entity.OnFloatPropertyChange -= this.OnFloatPropertyChange;
			Entity.OnInt32PropertyChange -= this.OnInt32PropertyChange;
			ObjectManager.OnAddEntity -= AbilityMonitor.OnAddEntity;
			ObjectManager.OnRemoveEntity -= AbilityMonitor.OnRemoveEntity;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001BD80 File Offset: 0x00019F80
		internal void SetOwner(Ability9 ability, Unit9 owner)
		{
			if (!ability.IsItem)
			{
				ability.SetOwner(owner);
				return;
			}
			Item baseItem = ability.BaseItem;
			Player purchaser = baseItem.Purchaser;
			Hero hero = (purchaser != null) ? purchaser.Hero : null;
			if (baseItem.Shareability != Shareability.Full && !owner.IsIllusion && hero != null && hero.IsValid)
			{
				Unit9 owner2 = owner.Owner;
				if (owner2 != null && owner2.IsValid && (!this.canUseOwnerItems.Contains(owner.Name) || hero.Handle != owner.Owner.Handle))
				{
					ability.SetOwner(EntityManager9.AddUnit(hero));
					goto IL_A0;
				}
			}
			ability.SetOwner(owner);
			IL_A0:
			AbilityMonitor.UpdateItemState(ability);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001BE34 File Offset: 0x0001A034
		private static void OnAddEntity(EntityEventArgs args)
		{
			try
			{
				PhysicalItem physicalItem = args.Entity as PhysicalItem;
				if (!(physicalItem == null) && !(physicalItem.Item == null))
				{
					Ability9 abilityFast = EntityManager9.GetAbilityFast(physicalItem.Item.Handle);
					if (!(abilityFast == null))
					{
						abilityFast.IsActive = false;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		private static void OnRemoveEntity(EntityEventArgs args)
		{
			try
			{
				PhysicalItem physicalItem = args.Entity as PhysicalItem;
				if (!(physicalItem == null) && !(physicalItem.Item == null))
				{
					Ability9 abilityFast = EntityManager9.GetAbilityFast(physicalItem.Item.Handle);
					if (!(abilityFast == null))
					{
						abilityFast.IsActive = true;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000045C4 File Offset: 0x000027C4
		private static void UpdateItemState(Ability9 ability)
		{
			Func<Item, bool> <>9__1;
			UpdateManager.BeginInvoke(delegate
			{
				try
				{
					Unit9 owner = ability.Owner;
					if (owner != null && owner.IsValid)
					{
						Ability9 ability2 = ability;
						Inventory baseInventory = owner.BaseInventory;
						bool isActive;
						if (baseInventory == null)
						{
							isActive = false;
						}
						else
						{
							IEnumerable<Item> items = baseInventory.Items;
							Func<Item, bool> predicate;
							if ((predicate = <>9__1) == null)
							{
								predicate = (<>9__1 = ((Item x) => x.Handle == ability.Handle));
							}
							isActive = items.Any(predicate);
						}
						ability2.IsActive = isActive;
					}
				}
				catch (Exception exception)
				{
					Logger.Error(exception, null);
				}
			}, 500);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001BF1C File Offset: 0x0001A11C
		private void OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
		{
			bool newValue = args.NewValue;
			if (newValue == args.OldValue)
			{
				return;
			}
			try
			{
				Ability9 abilityFast = EntityManager9.GetAbilityFast(sender.Handle);
				if (!(abilityFast == null))
				{
					string propertyName = args.PropertyName;
					if (!(propertyName == "m_bActivated"))
					{
						if (!(propertyName == "m_bHidden"))
						{
							if (!(propertyName == "m_bToggleState"))
							{
								if (propertyName == "m_bInAbilityPhase")
								{
									abilityFast.IsCasting = newValue;
									abilityFast.Owner.IsCasting = newValue;
									AbilityMonitor.EventHandler abilityCastChange = this.AbilityCastChange;
									if (abilityCastChange != null)
									{
										abilityCastChange(abilityFast);
									}
								}
							}
							else if (newValue)
							{
								AbilityMonitor.EventHandler abilityCasted = this.AbilityCasted;
								if (abilityCasted != null)
								{
									abilityCasted(abilityFast);
								}
							}
						}
						else
						{
							abilityFast.IsActive = !newValue;
						}
					}
					else
					{
						abilityFast.IsActive = newValue;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001C004 File Offset: 0x0001A204
		private void OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
		{
			float newValue = args.NewValue;
			if (newValue == args.OldValue)
			{
				return;
			}
			try
			{
				Ability9 abilityFast = EntityManager9.GetAbilityFast(sender.Handle);
				if (!(abilityFast == null))
				{
					string propertyName = args.PropertyName;
					if (!(propertyName == "m_flEnableTime"))
					{
						if (!(propertyName == "m_fCooldown"))
						{
							if (propertyName == "m_flChannelStartTime")
							{
								IChanneled channeled;
								if ((channeled = (abilityFast as IChanneled)) != null)
								{
									if (newValue > 0f)
									{
										abilityFast.IsChanneling = true;
										channeled.Owner.ChannelEndTime = newValue + channeled.ChannelTime;
										channeled.Owner.ChannelActivatesOnCast = channeled.IsActivatesOnChannelStart;
										AbilityMonitor.EventHandler abilityChannel = this.AbilityChannel;
										if (abilityChannel != null)
										{
											abilityChannel(abilityFast);
										}
									}
									else
									{
										abilityFast.IsChanneling = false;
										channeled.Owner.ChannelEndTime = 0f;
										channeled.Owner.ChannelActivatesOnCast = false;
									}
								}
							}
						}
						else if (this.AbilityCasted != null && newValue > args.OldValue && args.OldValue <= 0f && newValue >= abilityFast.Cooldown - 0.5f)
						{
							this.AbilityCasted(abilityFast);
						}
					}
					else
					{
						abilityFast.ItemEnableTimeSleeper.Sleep(newValue - Game.RawGameTime);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001C170 File Offset: 0x0001A370
		private void OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
		{
			if (args.NewValue == args.OldValue || args.PropertyName != "m_iParity")
			{
				return;
			}
			Unit9 owner = EntityManager9.GetUnitFast(sender.Handle);
			if (owner == null)
			{
				return;
			}
			UpdateManager.BeginInvoke(delegate
			{
				try
				{
					Inventory baseInventory = owner.BaseInventory;
					foreach (EntityHandle handle in from x in baseInventory.Items
					select x.Handle)
					{
						Ability9 abilityFast = EntityManager9.GetAbilityFast(handle);
						if (!(abilityFast == null))
						{
							if (abilityFast.Owner == owner)
							{
								abilityFast.IsActive = true;
							}
							else
							{
								EntityManager9.RemoveAbility(abilityFast.BaseAbility);
								EntityManager9.AddAbility(abilityFast.BaseAbility);
							}
						}
					}
					foreach (EntityHandle handle2 in from x in baseInventory.Stash.Concat(baseInventory.Backpack)
					select x.Handle)
					{
						Ability9 abilityFast2 = EntityManager9.GetAbilityFast(handle2);
						if (!(abilityFast2 == null))
						{
							if (abilityFast2.Owner == owner)
							{
								abilityFast2.IsActive = false;
							}
							else
							{
								EntityManager9.RemoveAbility(abilityFast2.BaseAbility);
								EntityManager9.AddAbility(abilityFast2.BaseAbility);
							}
						}
					}
				}
				catch (Exception exception)
				{
					Logger.Error(exception, null);
				}
			}, 0);
		}

		// Token: 0x04000196 RID: 406
		private readonly HashSet<string> canUseOwnerItems = new HashSet<string>
		{
			"npc_dota_lone_druid_bear1",
			"npc_dota_lone_druid_bear2",
			"npc_dota_lone_druid_bear3",
			"npc_dota_lone_druid_bear4"
		};

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x06000398 RID: 920
		public delegate void EventHandler(Ability9 ability);
	}
}
