using System;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Base
{
	// Token: 0x0200015B RID: 347
	internal class ControllableUnitMenu
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x00020DEC File Offset: 0x0001EFEC
		public ControllableUnitMenu(Unit9 owner, Menu rootMenu)
		{
			string text;
			Menu menu;
			if (owner.IsIllusion)
			{
				text = owner.DefaultName + "illusion";
				menu = new Menu(owner.DisplayName + " (illusion)", text).SetTexture(owner.DefaultName);
			}
			else
			{
				text = owner.DefaultName;
				menu = new Menu(owner.DisplayName, text).SetTexture(owner.DefaultName);
			}
			bool defaultOrbwalk = ControllableUnitMenu.DefaultOrbwalkValue(owner);
			this.MenuCreate(rootMenu, menu, text, defaultOrbwalk);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00005757 File Offset: 0x00003957
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0000575F File Offset: 0x0000395F
		public MenuSelector OrbwalkingMode { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00005768 File Offset: 0x00003968
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00005770 File Offset: 0x00003970
		public MenuSwitcher OrbwalkerStopOnStanding { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00005779 File Offset: 0x00003979
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00005781 File Offset: 0x00003981
		public MenuSwitcher BodyBlock { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000578A File Offset: 0x0000398A
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00005792 File Offset: 0x00003992
		public MenuSwitcher Orbwalk { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000579B File Offset: 0x0000399B
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x000057A3 File Offset: 0x000039A3
		public MenuSwitcher DangerMoveToMouse { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x000057AC File Offset: 0x000039AC
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x000057B4 File Offset: 0x000039B4
		public MenuSwitcher Control { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x000057BD File Offset: 0x000039BD
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x000057C5 File Offset: 0x000039C5
		public MenuSlider DangerRange { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x000057CE File Offset: 0x000039CE
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x000057D6 File Offset: 0x000039D6
		public MenuSlider AdditionalDelay { get; private set; }

		// Token: 0x060006F0 RID: 1776 RVA: 0x000057DF File Offset: 0x000039DF
		private static bool DefaultOrbwalkValue(Unit9 owner)
		{
			return (!owner.IsIllusion || !(owner.Name == "npc_dota_hero_phantom_lancer")) && (owner.IsHero || !(owner.DefaultName == "npc_dota_shadow_shaman_ward"));
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00020E6C File Offset: 0x0001F06C
		private void MenuCreate(Menu rootMenu, Menu menu, string menuName, bool defaultOrbwalk)
		{
			this.Control = new MenuSwitcher("Control", "orbwalkControl" + menuName, true, false);
			this.Control.Tooltip = "Control unit in combo";
			menu.Add<MenuSwitcher>(this.Control);
			this.Orbwalk = new MenuSwitcher("Orbwalk", "orbwalk" + menuName, defaultOrbwalk, false);
			this.Orbwalk.Tooltip = "Orbwalk or just attack";
			menu.Add<MenuSwitcher>(this.Orbwalk);
			this.OrbwalkingMode = new MenuSelector("Orbwalk mode", "orbwalkMode" + menuName, new string[]
			{
				"Move to mouse",
				"Move to target"
			}, false);
			menu.Add<MenuSelector>(this.OrbwalkingMode);
			this.BodyBlock = new MenuSwitcher("Body block", "orbBodyBlock" + menuName, false, false);
			this.BodyBlock.Tooltip = "Unit(s) will try to body block the target in combo";
			menu.Add<MenuSwitcher>(this.BodyBlock);
			this.OrbwalkerStopOnStanding = new MenuSwitcher("Stop orbwalk if standing", "orbwalkStopStanding" + menuName, false, false);
			this.OrbwalkerStopOnStanding.Tooltip = "Unit will not orbwalk if target is standing";
			menu.Add<MenuSwitcher>(this.OrbwalkerStopOnStanding);
			this.DangerRange = new MenuSlider("Danger range", "orbwalkerDanger" + menuName, 0, 0, 1200, false);
			this.DangerRange.Tooltip = "Unit will not move closer to the target";
			menu.Add<MenuSlider>(this.DangerRange);
			this.DangerMoveToMouse = new MenuSwitcher("Danger move to mouse", "orbwalkerDangerMove" + menuName, false, false);
			this.DangerMoveToMouse.Tooltip = "Unit will move to mouse without attacking when in danger range";
			menu.Add<MenuSwitcher>(this.DangerMoveToMouse);
			this.AdditionalDelay = new MenuSlider("Additional delay", "orbwalkerDelay" + menuName, 0, 0, 500, false);
			this.AdditionalDelay.Tooltip = "Set additional delay if unit cancels auto attack";
			menu.Add<MenuSlider>(this.AdditionalDelay);
			rootMenu.Add<Menu>(menu);
		}
	}
}
