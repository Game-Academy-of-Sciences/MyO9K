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
	// Token: 0x02000081 RID: 129
	public static class ParticleLogger
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0001D8C4 File Offset: 0x0001BAC4
		static ParticleLogger()
		{
			ParticleLogger.Client = new SentryClient(string.Format("https://{0}@sentry.io/{1}", "743ecf4e824d4137905b805cd882e2aa:3f8b8e732e9f4d8eb8557d4141e7ccd4", "1408003"), null, null, null);
			AssemblyMetadata metadata = Assembly.GetExecutingAssembly().GetMetadata();
			if (metadata == null)
			{
				return;
			}
			ParticleLogger.Client.Client.Release = metadata.Commit;
			ParticleLogger.Client.Tags["core"] = (() => metadata.Version);
			ParticleLogger.Client.Tags["mode"] = (() => Game.GameMode.ToString());
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001D984 File Offset: 0x0001BB84
		public static void Error(string message, string info = null)
		{
			O9KException ex = new O9KException(message);
			if (!string.IsNullOrEmpty(info))
			{
				ex.Data["Info"] = info;
			}
			ParticleLogger.CaptureException(ex, Assembly.GetCallingAssembly());
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000489F File Offset: 0x00002A9F
		public static void Error(Exception exception, string info = null)
		{
			if (!string.IsNullOrEmpty(info))
			{
				exception.Data["Info"] = info;
			}
			ParticleLogger.CaptureException(exception, Assembly.GetCallingAssembly());
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001D9BC File Offset: 0x0001BBBC
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
			ParticleLogger.CaptureException(exception, Assembly.GetCallingAssembly());
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000048C5 File Offset: 0x00002AC5
		public static void Warn(string text)
		{
			ParticleLogger.NLog.Warn(text);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001DA2C File Offset: 0x0001BC2C
		private static void CaptureException(Exception exception, Assembly assembly)
		{
			if (!Game.IsInGame || Game.ExpectedPlayers == 1 || Game.IsCustomGame || Game.GameMode == GameMode.Demo)
			{
				return;
			}
			if (!ParticleLogger.Cache.Add(exception.ToString().GetHashCode()))
			{
				return;
			}
			GameExceptionData gameExceptionData = new GameExceptionData();
			if (string.IsNullOrEmpty(gameExceptionData.Hero))
			{
				return;
			}
			exception.Data["Game"] = gameExceptionData;
			ParticleLogger.Client.Client.Logger = assembly.GetName().Name;
			AssemblyMetadata metadata = assembly.GetMetadata();
			if (metadata != null)
			{
				ParticleLogger.Client.Tags["version"] = (() => metadata.Version);
			}
			ParticleLogger.Client.CaptureAsync(exception);
		}

		// Token: 0x040001D7 RID: 471
		private static readonly HashSet<int> Cache = new HashSet<int>();

		// Token: 0x040001D8 RID: 472
		private static readonly SentryClient Client;

		// Token: 0x040001D9 RID: 473
		private static readonly Logger NLog = LogManager.GetCurrentClassLogger();
	}
}
