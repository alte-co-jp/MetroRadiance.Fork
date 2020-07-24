using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using MetroRadiance.Interop.Win32;

namespace MetroRadiance.Platform
{
	public static class WindowComposition
	{
		public static void Disable(Window window)
		{
			var hwndSource = PresentationSource.FromVisual(window) as HwndSource;
			if (hwndSource == null) return;

			var accent = new AccentPolicy
			{
				AccentState = AccentState.ACCENT_DISABLED,
			};
			Set(hwndSource, accent);
		}

		public static void EnableBlur(Window window, AccentFlags accentFlags)
		{
			var hwndSource = PresentationSource.FromVisual(window) as HwndSource;
			if (hwndSource == null) return;

			var accent = new AccentPolicy
			{
				AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
				AccentFlags = accentFlags,
			};
			Set(hwndSource, accent);
		}

			{
			};
		private static void Set(HwndSource hwndSource, AccentPolicy accentPolicy)
		{
			var accentStructSize = Marshal.SizeOf(accentPolicy);
			var accentPtr = IntPtr.Zero;
			try
			{
				accentPtr = Marshal.AllocHGlobal(accentStructSize);
				Marshal.StructureToPtr(accentPolicy, accentPtr, false);

				var data = new WindowCompositionAttributeData
				{
					Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
					SizeOfData = accentStructSize,
					Data = accentPtr,
				};
				User32.SetWindowCompositionAttribute(hwndSource.Handle, ref data);
			}
			finally
			{
				if (accentPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(accentPtr);
				}
			}
		}
	}
}
