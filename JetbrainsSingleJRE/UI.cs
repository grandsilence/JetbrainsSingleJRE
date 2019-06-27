using System;

namespace JetbrainsSingleJRE
{
	public static class UI
	{
		public static Action<string> Log = Console.WriteLine;

		public static void LogBreakpoint(string message)
		{
			Log(message);
			Console.ReadKey();
		}

		public static void Panic(string message)
		{
			LogBreakpoint(message);
			Environment.Exit(-1);
		}

		public static void Success(string message)
		{
			LogBreakpoint(message);
			Environment.Exit(0);
		}
	}
}