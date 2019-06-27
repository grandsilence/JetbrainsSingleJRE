using System.Runtime.InteropServices;

namespace JetbrainsSingleJRE
{
	public static class WinApi
	{
		[DllImport("kernel32.dll")]
		public static extern bool CreateSymbolicLink(
			string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

		public enum SymbolicLink
		{
			File = 0,
			Directory = 1
		}
	}
}