using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.Hud.Modules.Units.Modifiers
{
	// Token: 0x0200001B RID: 27
	internal class ModifierUnit
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00002548 File Offset: 0x00000748
		public ModifierUnit(Unit9 unit)
		{
			this.Unit = unit;
			this.IsAlly = unit.IsAlly();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000256E File Offset: 0x0000076E
		public bool IsAlly { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002576 File Offset: 0x00000776
		public Unit9 Unit { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000257E File Offset: 0x0000077E
		public IEnumerable<DrawableModifier> Modifiers
		{
			get
			{
				return from x in this.modifiers
				where x.ShouldDraw
				select x;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000025AA File Offset: 0x000007AA
		public Vector2 HealthBarPosition
		{
			get
			{
				return this.Unit.HealthBarPosition;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000025B7 File Offset: 0x000007B7
		public void AddModifier(DrawableModifier modifier)
		{
			this.modifiers.Add(modifier);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006F34 File Offset: 0x00005134
		public void CheckModifiers()
		{
			foreach (DrawableModifier drawableModifier in this.modifiers.ToList<DrawableModifier>())
			{
				if (!drawableModifier.Modifier.IsValid)
				{
					this.modifiers.Remove(drawableModifier);
				}
				else
				{
					drawableModifier.UpdateTimings();
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006FA8 File Offset: 0x000051A8
		public bool IsValid(bool showAlly)
		{
			if (this.IsAlly)
			{
				return showAlly && this.Unit.IsValid && this.Unit.IsAlive;
			}
			return this.Unit.IsValid && this.Unit.IsVisible && this.Unit.IsAlive;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007004 File Offset: 0x00005204
		public void RemoveModifier(Modifier modifier)
		{
			this.modifiers.RemoveAll((DrawableModifier x) => x.Handle == modifier.Handle);
		}

		// Token: 0x04000058 RID: 88
		private readonly List<DrawableModifier> modifiers = new List<DrawableModifier>();
	}
}
