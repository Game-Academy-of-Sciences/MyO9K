using System;
using System.Reflection;
using Ensage;
using O9K.Core.Exceptions;
using PlaySharp.Sentry;

namespace O9K.Core.Logger
{
	// Token: 0x02000085 RID: 133
	public static class StatsLogger
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		static StatsLogger()
		{
			StatsLogger.Client.Tags["hero"] = (() => ObjectManager.LocalHero.Name);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001DB4C File Offset: 0x0001BD4C
		public static void Log(string message, string info = null)
		{
			if (Game.ExpectedPlayers == 1 || Game.IsCustomGame || Game.GameMode == GameMode.Demo)
			{
				return;
			}
			O9KException ex = new O9KException(message);
			if (!string.IsNullOrEmpty(info))
			{
				ex.Data["Info"] = info;
			}
			GameExceptionData gameExceptionData = new GameExceptionData();
			if (string.IsNullOrEmpty(gameExceptionData.Hero))
			{
				return;
			}
			ex.Data["Game"] = gameExceptionData;
			StatsLogger.Client.Client.Logger = Assembly.GetCallingAssembly().GetName().Name;
			StatsLogger.Client.CaptureAsync(ex);
		}

		// Token: 0x040001DD RID: 477
		private static readonly SentryClient Client = new SentryClient(string.Format("https://{0}@sentry.io/{1}", "fd093fcea25949688ca04588e2b3e02d:4906e0f75d9a405bad1077a2dcad7480", "1311707"), null, null, null);
	}
}
