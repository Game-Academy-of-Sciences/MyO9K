using System;
using System.Collections.Generic;
using System.Reflection;
using Ensage;
using Ensage.SDK.Logger;
using NLog;
using O9K.Core.Exceptions;
using PlaySharp.Sentry;

namespace O9K.Core.Logger
{
	// Token: 0x0200007D RID: 125
	public static class Logger
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0001D658 File Offset: 0x0001B858
		static Logger()
		{
			Logger.Client = new SentryClient(string.Format("https://{0}@sentry.io/{1}", "ed70f139a30e4c3e8481e629e541dec6:c07ecbe5b1c243aeb9b19b8d40b73461", "1277552"), null, null, null);
			AssemblyMetadata metadata = Assembly.GetExecutingAssembly().GetMetadata();
			if (metadata == null)
			{
				return;
			}
			Logger.Client.Client.Release = metadata.Commit;
			Logger.Client.Tags["core"] = (() => metadata.Version);
			Logger.Client.Tags["mode"] = (() => Game.GameMode.ToString());
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001D718 File Offset: 0x0001B918
		public static void Error(string message, string info = null)
		{
			O9KException ex = new O9KException(message);
			if (!string.IsNullOrEmpty(info))
			{
				ex.Data["Info"] = info;
			}
			Logger.CaptureException(ex, Assembly.GetCallingAssembly());
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00004836 File Offset: 0x00002A36
		public static void Error(Exception exception, string info = null)
		{
			if (!string.IsNullOrEmpty(info))
			{
				exception.Data["Info"] = info;
			}
			Logger.NLog.Error(exception.ToString());
			Logger.CaptureException(exception, Assembly.GetCallingAssembly());
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001D750 File Offset: 0x0001B950
		public static void Error(Exception exception, Entity entity, string info = null)
		{
			if (entity != null)
			{
				exception.Data[entity.GetType().Name] = new EntityExceptionData(entity);
			}
			else
			{
				exception.Data["Entity"] = "null";
			}
			if (!string.IsNullOrEmpty(info))
			{
				exception.Data["Info"] = info;
			}
			Logger.NLog.Error(exception.ToString());
			Logger.CaptureException(exception, Assembly.GetCallingAssembly());
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000486C File Offset: 0x00002A6C
		public static void Warn(string text)
		{
			Logger.NLog.Warn(text);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		private static void CaptureException(Exception exception, Assembly assembly)
		{
			if (!Game.IsInGame || Game.ExpectedPlayers == 1 || Game.IsCustomGame || Game.GameMode == GameMode.Demo)
			{
				return;
			}
			if (!Logger.Cache.Add(exception.ToString().GetHashCode()))
			{
				return;
			}
			if (exception is ParticleEffectNotCreatedException)
			{
				return;
			}
			GameExceptionData gameExceptionData = new GameExceptionData();
			if (string.IsNullOrEmpty(gameExceptionData.Hero))
			{
				return;
			}
			exception.Data["Game"] = gameExceptionData;
			Logger.Client.Client.Logger = assembly.GetName().Name;
			AssemblyMetadata metadata = assembly.GetMetadata();
			if (metadata != null)
			{
				Logger.Client.Tags["version"] = (() => metadata.Version);
			}
			Logger.Client.CaptureAsync(exception);
		}

		// Token: 0x040001D1 RID: 465
		private static readonly HashSet<int> Cache = new HashSet<int>();

		// Token: 0x040001D2 RID: 466
		private static readonly SentryClient Client;

		// Token: 0x040001D3 RID: 467
		private static readonly Logger NLog = LogManager.GetCurrentClassLogger();
	}
}
