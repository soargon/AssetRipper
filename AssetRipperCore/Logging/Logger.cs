﻿using AssetRipper.Utils;
using System;
using System.Collections.Generic;

namespace AssetRipper.Logging
{
	public static class Logger
	{
		private static readonly List<ILogger> loggers = new List<ILogger>();

		static Logger()
		{
			Cpp2IL.Core.Logger.InfoOverride = new Action<string, string>((string message, string source) => LogCpp2IL(LogType.Info, message));
			Cpp2IL.Core.Logger.WarnOverride = new Action<string, string>((string message, string source) => LogCpp2IL(LogType.Warning, message));
			Cpp2IL.Core.Logger.ErrorOverride = new Action<string, string>((string message, string source) => LogCpp2IL(LogType.Error, message));
			Cpp2IL.Core.Logger.VerboseOverride = new Action<string, string>((string message, string source) => LogCpp2IL(LogType.Debug, message));
		}

		private static void LogCpp2IL(LogType logType, string message)
		{
			Log(logType, LogCategory.Cpp2IL, message.Trim());
		}

		public static void Log(LogType type, LogCategory category, string message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));
			foreach (ILogger instance in loggers)
				instance?.Log(type, category, message);
		}

		public static void Log(LogType type, LogCategory category, string[] messages)
		{
			if (messages == null) throw new ArgumentNullException(nameof(messages));
			foreach (string message in messages)
				Log(type, category, message);
		}

		public static void BlankLine() => BlankLine(1);

		public static void BlankLine(int numLines)
		{
			foreach (ILogger instance in loggers)
				instance?.BlankLine(numLines);
		}

		private static void LogReleaseInformation(string platformType)
		{
#if VIRTUAL
			Log(LogType.Info, LogCategory.System, $"AssetRipper Build Type: Virtual {platformType}");
#elif DEBUG
			Log(LogType.Info, LogCategory.System, $"AssetRipper Build Type: Debug {platformType}");
#else
			Log(LogType.Info, LogCategory.System, $"AssetRipper Build Type: Release {platformType}");
#endif
		}

		private static void LogOperatingSystemInformation()
		{
			Log(LogType.Info, LogCategory.System, $"System Version: {Environment.OSVersion.VersionString}");
			string osBitness = Environment.Is64BitOperatingSystem ? "x64" : "x86";
			string operatingSystem = RunetimeUtils.RuntimeOS.ToString();
			Log(LogType.Info, LogCategory.System, $"Operating System: {operatingSystem} {osBitness}");
		}

		public static void LogSystemInformation(string programName, string platformType)
		{
			Log(LogType.Info, LogCategory.System, programName);
			LogOperatingSystemInformation();
			Log(LogType.Info, LogCategory.System, $"AssetRipper Version: {BuildInfo.Version}");
			LogReleaseInformation(platformType);
			string processBitness = Environment.Is64BitProcess ? "x64" : "x86";
			Log(LogType.Info, LogCategory.System, $"AssetRipper Process: {processBitness}");
		}

		public static void Add(ILogger logger) => loggers.Add(logger);

		public static void Remove(ILogger logger) => loggers.Remove(logger);

		public static void Clear() => loggers.Clear();
	}
}
