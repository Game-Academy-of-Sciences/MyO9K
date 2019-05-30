using System;
using Ensage.SDK.Handlers;

namespace O9K.Core.Extensions
{
	// Token: 0x020000A7 RID: 167
	public static class UpdateHandlerExtensions
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0001F830 File Offset: 0x0001DA30
		public static void SetUpdateRate(this IUpdateHandler updateHandler, int rate)
		{
			TimeoutHandler timeoutHandler;
			if ((timeoutHandler = (updateHandler.Executor as TimeoutHandler)) != null)
			{
				timeoutHandler.Timeout = rate;
			}
		}
	}
}
