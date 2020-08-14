using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MetroRadiance.Interop.Win32;

namespace MetroRadiance.Themes
{
	partial class Generic_CaptionButton : ResourceDictionary
	{
		static Generic_CaptionButton()
		{
			try
			{
				var user32Path = Path.Combine(Environment.SystemDirectory, "user32.dll");
				var hInst = Kernel32.LoadLibraryEx(user32Path, IntPtr.Zero, LoadLibraryExFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE);
				if (hInst != null)
				{
					CaptionButton_Minimize = User32.LoadString(hInst, 900);  // Minimize
					CaptionButton_Maximize = User32.LoadString(hInst, 901);  // Maximize
					CaptionButton_RestoreUp = User32.LoadString(hInst, 902);  // Restore Up
					CaptionButton_RestoreDown = User32.LoadString(hInst, 903);  // Restore Down
					CaptionButton_Help = User32.LoadString(hInst, 904);  // Help
					CaptionButton_Close = User32.LoadString(hInst, 905);  // Close
					Kernel32.FreeLibrary(hInst);
				}
			}
			catch (Exception ex)
			{
			}
			// fallback
			CaptionButton_Minimize = CaptionButton_Minimize ?? "Minimize";
			CaptionButton_Maximize = CaptionButton_Maximize ?? "Maximize";
			CaptionButton_RestoreUp = CaptionButton_RestoreUp ?? "Restore Up";
			CaptionButton_RestoreDown = CaptionButton_RestoreDown ?? "Restore Down";
			CaptionButton_Help = CaptionButton_Help ?? "Help";
			CaptionButton_Close = CaptionButton_Close ?? "Close";
		}

		public static string CaptionButton_Minimize { get; }
		public static string CaptionButton_Maximize { get; }
		public static string CaptionButton_RestoreUp { get; }
		public static string CaptionButton_RestoreDown { get; }
		public static string CaptionButton_Help { get; }
		public static string CaptionButton_Close { get; }
	}
}
