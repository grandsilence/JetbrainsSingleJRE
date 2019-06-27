using System;
using System.IO;

namespace JetbrainsSingleJRE
{
	internal class Program
	{
		private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		private static readonly string JetbrainsFolder = Path.Combine(AppData, "JetBrains\\Toolbox\\apps");
		private const string JreTarget = "C:\\jre64";

		private static void Main(string[] args)
		{
			if (!Directory.Exists(JetbrainsFolder))
				UI.Panic("JetBrains folder not found");

			if (!Directory.Exists(JreTarget))
				UI.Panic($"jre target not found: {JreTarget}");

			var apps = Directory.GetDirectories(JetbrainsFolder);
			foreach (string appPath in apps)
			{
				if (appPath.EndsWith("Toolbox", StringComparison.OrdinalIgnoreCase) || 
					appPath.EndsWith("ReSharper-U", StringComparison.OrdinalIgnoreCase))
					continue;

				ReplaceJreWithSymlink(appPath);
			}

			UI.Success("done!");
		}

		private static void ReplaceJreWithSymlink(string appPath)
		{
			var instances = Directory.GetDirectories(appPath);
			foreach (string instance in instances)
			{
				var versions = Directory.GetDirectories(instance);
				foreach (string version in versions)
				{
					string jrePath = Path.Combine(version, "jre64");
					if (Directory.Exists(jrePath))
						Directory.Delete(jrePath, true);

					WinApi.CreateSymbolicLink( jrePath, JreTarget, WinApi.SymbolicLink.Directory);
				}
			}
		}
	}
}
