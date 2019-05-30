using System;
using Newtonsoft.Json.Linq;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x02000056 RID: 86
	public class MenuText : MenuItem
	{
		// Token: 0x060002DB RID: 731 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public MenuText(string displayName) : base(displayName, displayName, false)
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00003DC2 File Offset: 0x00001FC2
		public MenuText(string displayName, string name) : base(displayName, name, false)
		{
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000344F File Offset: 0x0000164F
		public MenuText SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00003D6D File Offset: 0x00001F6D
		internal override object GetSaveValue()
		{
			return null;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000372F File Offset: 0x0000192F
		internal override void Load(JToken token)
		{
		}
	}
}
