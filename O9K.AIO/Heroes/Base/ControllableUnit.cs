using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.FailSafe;
using O9K.AIO.Modes.Combo;
using O9K.AIO.Modes.MoveCombo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using PlaySharp.Toolkit.Helper.Annotations;
using SharpDX;

namespace O9K.AIO.Heroes.Base
{
	// Token: 0x0200015C RID: 348
	internal class ControllableUnit
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x00021064 File Offset: 0x0001F264
		public ControllableUnit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
		{
			this.Owner = owner;
			this.abilitySleeper = abilitySleeper;
			this.OrbwalkSleeper = orbwalkSleeper;
			this.Menu = menu;
			this.Handle = owner.Handle;
			this.MoveComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.moveBlink = new BlinkAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.moveForceStaff = new ForceStaff(x)
				},
				{
					AbilityId.item_hurricane_pike,
					(ActiveAbility x) => this.movePike = new ForceStaff(x)
				},
				{
					AbilityId.item_butterfly,
					(ActiveAbility x) => this.movePhaseBoots = new MoveBuffAbility(x)
				},
				{
					AbilityId.item_invis_sword,
					(ActiveAbility x) => this.moveShadowBlade = new MoveBuffAbility(x)
				},
				{
					AbilityId.item_silver_edge,
					(ActiveAbility x) => this.moveSilverEdge = new MoveBuffAbility(x)
				},
				{
					AbilityId.item_glimmer_cape,
					(ActiveAbility x) => this.moveGlimmer = new ShieldAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.moveBkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.moveBladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_hood_of_defiance,
					(ActiveAbility x) => this.moveHood = new ShieldAbility(x)
				},
				{
					AbilityId.item_lotus_orb,
					(ActiveAbility x) => this.moveLotus = new ShieldAbility(x)
				},
				{
					AbilityId.item_diffusal_blade,
					(ActiveAbility x) => this.moveDiffusal = new DebuffAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.moveAbyssal = new DisableAbility(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.moveAtos = new DisableAbility(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.moveOrchid = new DisableAbility(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.moveHex = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.moveBloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_ethereal_blade,
					(ActiveAbility x) => this.moveEthereal = new DebuffAbility(x)
				}
			};
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000581A File Offset: 0x00003A1A
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00005822 File Offset: 0x00003A22
		public string MorphedUnitName { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0000582B File Offset: 0x00003A2B
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00005833 File Offset: 0x00003A33
		public FailSafe FailSafe { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0000583C File Offset: 0x00003A3C
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00005844 File Offset: 0x00003A44
		public Vector3 LastMovePosition { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0000584D File Offset: 0x00003A4D
		public Sleeper ComboSleeper { get; } = new Sleeper();

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00005855 File Offset: 0x00003A55
		public uint Handle { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000585D File Offset: 0x00003A5D
		public Unit9 Owner { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00005865 File Offset: 0x00003A65
		public Sleeper AttackSleeper { get; } = new Sleeper();

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0000586D File Offset: 0x00003A6D
		public Sleeper MoveSleeper { get; } = new Sleeper();

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00005875 File Offset: 0x00003A75
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0000587D File Offset: 0x00003A7D
		public Unit9 LastTarget { get; protected set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00005886 File Offset: 0x00003A86
		public bool CanBeControlled
		{
			get
			{
				return this.Menu.Control && (this.Owner.UnitState & UnitState.CommandRestricted) == (UnitState)0UL;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x000058B2 File Offset: 0x00003AB2
		public bool IsValid
		{
			get
			{
				return this.Owner.IsValid && this.Owner.IsAlive;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x000058CE File Offset: 0x00003ACE
		public virtual bool ShouldControl
		{
			get
			{
				return !this.Owner.IsCasting;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00021264 File Offset: 0x0001F464
		public bool CanBodyBlock
		{
			get
			{
				return this.Menu.BodyBlock && (this.Owner.UnitState & UnitState.NoCollision) == (UnitState)0UL && this.Owner.MoveCapability == MoveCapability.Ground && this.Owner.CanMove(true) && !this.Owner.IsInvulnerable;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x000058DE File Offset: 0x00003ADE
		public virtual bool OrbwalkEnabled
		{
			get
			{
				return this.Menu.Orbwalk;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x000058F0 File Offset: 0x00003AF0
		public Sleeper OrbwalkSleeper { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x000058F8 File Offset: 0x00003AF8
		public virtual bool IsInvisible
		{
			get
			{
				return this.Owner.IsInvisible;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00005905 File Offset: 0x00003B05
		protected virtual int BodyBlockRange { get; } = 150;

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000590D File Offset: 0x00003B0D
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x00005915 File Offset: 0x00003B15
		protected Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>> ComboAbilities { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000591E File Offset: 0x00003B1E
		protected ControllableUnitMenu Menu { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00005926 File Offset: 0x00003B26
		protected Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>> MoveComboAbilities { get; }

		// Token: 0x0600070C RID: 1804 RVA: 0x000212C4 File Offset: 0x0001F4C4
		public virtual void AddAbility(ActiveAbility ability, IEnumerable<ComboModeMenu> comboMenus, MoveComboModeMenu moveMenu)
		{
			Func<ActiveAbility, UsableAbility> func;
			if (this.ComboAbilities != null && this.ComboAbilities.TryGetValue(ability.Id, out func))
			{
				UsableAbility usableAbility = func(ability);
				usableAbility.Sleeper = this.abilitySleeper[usableAbility.Ability.Handle];
				usableAbility.OrbwalkSleeper = this.OrbwalkSleeper;
				foreach (ComboModeMenu comboModeMenu in comboMenus)
				{
					comboModeMenu.AddComboAbility(usableAbility);
				}
			}
			this.AddMoveComboAbility(ability, moveMenu);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00021360 File Offset: 0x0001F560
		public bool? BodyBlock(TargetManager targetManager, Vector3 blockPosition, ComboModeMenu menu)
		{
			if (!this.Owner.CanMove(true) || this.MoveSleeper.IsSleeping)
			{
				return new bool?(false);
			}
			Unit9 target = targetManager.Target;
			float angle = target.GetAngle(this.Owner.Position, false);
			if ((double)angle > 1.2)
			{
				if (this.Owner.Speed <= target.Speed + 35f)
				{
					return null;
				}
				float num = angle * 0.6f;
				Vector3 vector = target.InFront((float)this.BodyBlockRange, MathUtil.RadiansToDegrees(num), true);
				Vector3 vector2 = target.InFront((float)this.BodyBlockRange, MathUtil.RadiansToDegrees(-num), true);
				if (this.Move((this.Owner.Distance(vector) < this.Owner.Distance(vector2)) ? vector : vector2))
				{
					this.MoveSleeper.Sleep(0.1f);
					return new bool?(true);
				}
				return new bool?(false);
			}
			else if ((double)angle < 0.36)
			{
				if (!target.IsMoving && this.CanAttack(target, 400f))
				{
					return new bool?(this.Attack(target, menu));
				}
				if (this.Owner.IsMoving && !this.AttackSleeper.IsSleeping)
				{
					this.MoveSleeper.Sleep(0.2f);
					return new bool?(this.Owner.BaseUnit.Stop());
				}
				return new bool?(false);
			}
			else
			{
				if (this.Move(Vector3Extensions.Extend2D(target.Position, blockPosition, (float)this.BodyBlockRange)))
				{
					this.MoveSleeper.Sleep(0.1f);
					return new bool?(true);
				}
				return new bool?(false);
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00021508 File Offset: 0x0001F708
		public virtual bool CanAttack(Unit9 target, float additionalRange = 0f)
		{
			if (target == null)
			{
				return false;
			}
			if (!this.Owner.CanAttack(target, additionalRange))
			{
				return false;
			}
			if (this.Menu.DangerRange > 0 && this.Menu.DangerMoveToMouse && this.Owner.Distance(target) < (float)Math.Min((int)this.Owner.GetAttackRange(null, 0f), this.Menu.DangerRange))
			{
				return false;
			}
			float num = this.Owner.GetTurnTime(target.Position) + Game.Ping / 2000f;
			if (num <= 0f)
			{
				return !this.AttackSleeper.IsSleeping;
			}
			return this.AttackSleeper.RemainingSleepTime <= num;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000592E File Offset: 0x00003B2E
		public virtual bool CanMove()
		{
			return this.Owner.CanMove(true) && !this.MoveSleeper.IsSleeping;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00003880 File Offset: 0x00001A80
		public virtual bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			return false;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000594E File Offset: 0x00003B4E
		public virtual void EndCombo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			this.LastMovePosition = Vector3.Zero;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000595B File Offset: 0x00003B5B
		public bool Move(Vector3 movePosition)
		{
			if (!this.CanMove())
			{
				return false;
			}
			if (movePosition == this.LastMovePosition)
			{
				return false;
			}
			if (!this.Owner.BaseUnit.Move(movePosition))
			{
				return false;
			}
			this.LastMovePosition = movePosition;
			return true;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000215D4 File Offset: 0x0001F7D4
		public virtual bool MoveCombo(TargetManager targetManager, MoveComboModeMenu comboModeMenu)
		{
			if (!this.Owner.CanUseAbilities || this.Owner.IsInvisible)
			{
				return false;
			}
			if (this.Owner.IsMyHero)
			{
				Unit9 unit = (from x in targetManager.EnemyHeroes
				where !x.IsStunned && !x.IsHexed && !x.IsSilenced && !x.IsRooted
				orderby x.Distance(this.Owner)
				select x).FirstOrDefault((Unit9 x) => x.Distance(this.Owner) < 1000f);
				if (unit != null)
				{
					targetManager.ForceSetTarget(unit);
				}
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return this.MoveComboUseBlinks(abilityHelper) || (targetManager.HasValidTarget && this.MoveComboUseDisables(abilityHelper)) || this.MoveComboUseBuffs(abilityHelper) || (targetManager.HasValidTarget && targetManager.Target.Distance(this.Owner) < 600f && this.MoveComboUseShields(abilityHelper));
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000216C8 File Offset: 0x0001F8C8
		public void OnAttackStart()
		{
			float num = Game.Ping / 2000f;
			float num2 = this.Owner.GetAttackPoint(this.LastTarget);
			if (this.Owner.Abilities.Any((Ability9 x) => x.Id == AbilityId.item_echo_sabre && x.CanBeCasted(true)))
			{
				num2 *= 2.5f;
			}
			this.MoveSleeper.Sleep(num2 - num + 0.06f + this.Menu.AdditionalDelay / 1000f);
			this.AttackSleeper.Sleep(num2 + this.Owner.GetAttackBackswing(this.LastTarget) - num - 0.06f);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00021780 File Offset: 0x0001F980
		public virtual bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (this.OrbwalkSleeper.IsSleeping)
			{
				return false;
			}
			this.LastTarget = target;
			if (attack && this.CanAttack(target, 0f))
			{
				this.LastMovePosition = Vector3.Zero;
				return this.Attack(target, comboMenu);
			}
			return (!(target != null) || !this.Menu.OrbwalkerStopOnStanding || target.IsMoving || this.Owner.Distance(target) >= this.Owner.GetAttackRange(target, 0f)) && (move && this.CanMove()) && this.ForceMove(target, attack);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00021824 File Offset: 0x0001FA24
		public bool Orbwalk(Unit9 target, ComboModeMenu comboMenu)
		{
			bool move = comboMenu.Move.IsEnabled;
			if (target != null && this.Owner.IsRanged && this.Owner.HasModifier("modifier_item_hurricane_pike_range"))
			{
				move = false;
			}
			return this.Orbwalk(target, comboMenu.Attack, move, comboMenu);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00002B3D File Offset: 0x00000D3D
		public virtual void RemoveAbility(ActiveAbility ability)
		{
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0002187C File Offset: 0x0001FA7C
		protected void AddMoveComboAbility(ActiveAbility ability, MoveComboModeMenu moveMenu)
		{
			Func<ActiveAbility, UsableAbility> func;
			if (this.MoveComboAbilities.TryGetValue(ability.Id, out func))
			{
				UsableAbility usableAbility = func(ability);
				usableAbility.Sleeper = this.abilitySleeper[usableAbility.Ability.Handle];
				usableAbility.OrbwalkSleeper = this.OrbwalkSleeper;
				moveMenu.AddComboAbility(usableAbility);
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000218D8 File Offset: 0x0001FAD8
		protected virtual bool Attack(Unit9 target, [CanBeNull] ComboModeMenu comboMenu)
		{
			if (this.Owner.Name == "npc_dota_hero_rubick")
			{
				IActiveAbility activeAbility = (IActiveAbility)this.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.rubick_telekinesis);
				if (activeAbility != null && activeAbility.CanBeCasted(true) && comboMenu != null && comboMenu.IsAbilityEnabled(activeAbility))
				{
					return false;
				}
			}
			if (!this.UseOrbAbility(target, comboMenu) && !this.Owner.BaseUnit.Attack(target.BaseUnit))
			{
				return false;
			}
			float num = Game.Ping / 2000f + 0.06f;
			float turnTime = this.Owner.GetTurnTime(target.Position);
			float num2 = Math.Max(this.Owner.Distance(target) - this.Owner.GetAttackRange(target, 0f), 0f) / (float)this.Owner.BaseUnit.MovementSpeed;
			float num3 = turnTime + num2 + num;
			float num4 = this.Owner.GetAttackPoint(target);
			if (this.Owner.Abilities.Any((Ability9 x) => x.Id == AbilityId.item_echo_sabre && x.CanBeCasted(true)))
			{
				num4 *= 2.5f;
			}
			this.AttackSleeper.Sleep(this.Owner.GetAttackPoint(target) + this.Owner.GetAttackBackswing(target) + num3 - 0.1f);
			this.MoveSleeper.Sleep(num4 + num3 + 0.25f + this.Menu.AdditionalDelay / 1000f);
			return true;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00021A74 File Offset: 0x0001FC74
		protected virtual bool ForceMove(Unit9 target, bool attack)
		{
			Vector3 mousePosition = Game.MousePosition;
			Vector3 vector = mousePosition;
			if (target != null && attack)
			{
				Vector3 position = target.Position;
				if (this.Menu.OrbwalkingMode == "Move to target" || this.CanAttack(target, 400f))
				{
					vector = position;
				}
				if (this.Menu.DangerRange > 0)
				{
					int num = Math.Min((int)this.Owner.GetAttackRange(null, 0f), this.Menu.DangerRange);
					float num2 = this.Owner.Distance(target);
					if (this.Menu.DangerMoveToMouse)
					{
						if (num2 < (float)num)
						{
							vector = mousePosition;
						}
					}
					else if (num2 < (float)num)
					{
						float num3 = (position - this.Owner.Position).AngleBetween(vector - position);
						if (num3 < 90f)
						{
							if (num3 < 30f)
							{
								vector = Vector3Extensions.Extend2D(position, vector, (float)((num - 25) * -1));
							}
							else
							{
								Vector3 vector2 = (mousePosition - position).Rotated(MathUtil.DegreesToRadians(90f)).Normalized() * (float)(num - 25);
								Vector3 vector3 = position + vector2;
								Vector3 vector4 = position - vector2;
								vector = ((this.Owner.Distance(vector3) < this.Owner.Distance(vector4)) ? vector3 : vector4);
							}
						}
						else if (target.Distance(vector) < (float)num)
						{
							vector = Vector3Extensions.Extend2D(position, vector, (float)(num - 25));
						}
					}
				}
			}
			if (this.Menu.OrbwalkingMode == "Move to target")
			{
				if (this.Owner.Distance(vector) < 100f)
				{
					return false;
				}
			}
			else if (this.Owner.Distance(vector) < 10f)
			{
				return false;
			}
			if (vector == this.LastMovePosition && this.AttackSleeper.IsSleeping)
			{
				return false;
			}
			if (!this.Owner.BaseUnit.Move(vector))
			{
				return false;
			}
			this.LastMovePosition = vector;
			return true;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00005994 File Offset: 0x00003B94
		protected virtual bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return abilityHelper.UseMoveAbility(this.moveBlink) || abilityHelper.UseMoveAbility(this.moveForceStaff) || abilityHelper.UseMoveAbility(this.movePike);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000059C7 File Offset: 0x00003BC7
		protected virtual bool MoveComboUseBuffs(AbilityHelper abilityHelper)
		{
			return abilityHelper.UseMoveAbility(this.movePhaseBoots) || abilityHelper.UseMoveAbility(this.moveSilverEdge) || abilityHelper.UseMoveAbility(this.moveShadowBlade);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00021C7C File Offset: 0x0001FE7C
		protected virtual bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return abilityHelper.UseAbility(this.moveHex, true) || abilityHelper.UseAbility(this.moveAbyssal, true) || abilityHelper.UseAbility(this.moveEthereal, true) || abilityHelper.UseAbility(this.moveAtos, true) || abilityHelper.UseAbility(this.moveBloodthorn, true) || abilityHelper.UseAbility(this.moveOrchid, true) || abilityHelper.UseAbility(this.moveDiffusal, true);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00021D04 File Offset: 0x0001FF04
		protected virtual bool MoveComboUseShields(AbilityHelper abilityHelper)
		{
			return abilityHelper.UseMoveAbility(this.moveBkb) || abilityHelper.UseMoveAbility(this.moveGlimmer) || abilityHelper.UseMoveAbility(this.moveBladeMail) || abilityHelper.UseMoveAbility(this.moveHood) || abilityHelper.UseMoveAbility(this.moveLotus);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00021D64 File Offset: 0x0001FF64
		protected virtual bool UseOrbAbility(Unit9 target, ComboModeMenu comboMenu)
		{
			if (!this.Owner.CanUseAbilities)
			{
				return false;
			}
			OrbAbility orbAbility = this.Owner.Abilities.OfType<OrbAbility>().FirstOrDefault(delegate(OrbAbility x)
			{
				ComboModeMenu comboMenu2 = comboMenu;
				return comboMenu2 != null && comboMenu2.IsAbilityEnabled(x) && !x.Enabled && x.CanBeCasted(true) && x.CanHit(target);
			});
			if (orbAbility != null)
			{
				if (string.IsNullOrEmpty(orbAbility.OrbModifier))
				{
					return orbAbility.UseAbility(target, false, false);
				}
				if ((double)Game.RawGameTime < (double)this.orbHitTime - 0.1)
				{
					return false;
				}
				Modifier modifier = target.GetModifier(orbAbility.OrbModifier);
				if (((modifier != null) ? modifier.RemainingTime : 0f) <= (this.Owner.GetAttackPoint(target) + this.Owner.Distance(target) / (float)this.Owner.ProjectileSpeed) * 2f + this.Owner.GetAttackBackswing(target))
				{
					this.orbHitTime = Game.RawGameTime + orbAbility.GetHitTime(target);
					return orbAbility.UseAbility(target, false, false);
				}
			}
			return false;
		}

		// Token: 0x040003CB RID: 971
		private readonly MultiSleeper abilitySleeper;

		// Token: 0x040003CC RID: 972
		private DisableAbility moveAbyssal;

		// Token: 0x040003CD RID: 973
		private DisableAbility moveAtos;

		// Token: 0x040003CE RID: 974
		private ShieldAbility moveBkb;

		// Token: 0x040003CF RID: 975
		private ShieldAbility moveBladeMail;

		// Token: 0x040003D0 RID: 976
		private BlinkAbility moveBlink;

		// Token: 0x040003D1 RID: 977
		private DisableAbility moveBloodthorn;

		// Token: 0x040003D2 RID: 978
		private DebuffAbility moveDiffusal;

		// Token: 0x040003D3 RID: 979
		private DebuffAbility moveEthereal;

		// Token: 0x040003D4 RID: 980
		private BlinkAbility moveForceStaff;

		// Token: 0x040003D5 RID: 981
		private ShieldAbility moveGlimmer;

		// Token: 0x040003D6 RID: 982
		private DisableAbility moveHex;

		// Token: 0x040003D7 RID: 983
		private ShieldAbility moveHood;

		// Token: 0x040003D8 RID: 984
		private ShieldAbility moveLotus;

		// Token: 0x040003D9 RID: 985
		private DisableAbility moveOrchid;

		// Token: 0x040003DA RID: 986
		private MoveBuffAbility movePhaseBoots;

		// Token: 0x040003DB RID: 987
		private BlinkAbility movePike;

		// Token: 0x040003DC RID: 988
		private MoveBuffAbility moveShadowBlade;

		// Token: 0x040003DD RID: 989
		private MoveBuffAbility moveSilverEdge;

		// Token: 0x040003DE RID: 990
		private float orbHitTime;
	}
}
