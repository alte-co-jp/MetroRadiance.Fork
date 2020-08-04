using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MetroRadiance.Interop;
using MetroRadiance.Interop.Win32;
using MetroRadiance.Platform;

namespace MetroRadiance.UI.Controls
{
	public class AcrylicBlurWindow : BlurWindow
	{
		private static bool IsAcrylicBlurEnabled { get; }

		static AcrylicBlurWindow()
		{
			IsAcrylicBlurEnabled = IsWindows10 && Environment.OSVersion.Version.Build >= 17004;
		}

		internal protected override void HandleThemeChanged()
		{
			if (WindowsTheme.HighContrast.Current)
			{
				this.ToHighContrast();
			}
			else if (!IsWindows10)
			{
				this.ToCompatibility();
			}
			else if (!WindowsTheme.Transparency.Current)
			{
				this.ToDefault();
			}
			else if (IsAcrylicBlurEnabled)
			{
				this.ToAcrylicBlur();
			}
			else
			{
				this.ToBlur();
			}
		}

		private void ToAcrylicBlur()
		{
			Color background, foreground;
			this.GetColors(out background, out foreground);

			var wpfBackground = Color.FromArgb(1, 0, 0, 0);
			WindowComposition.EnableAcrylicBlur(this, background, (byte)(255 * this.BlurOpacity), this.BordersFlag);
			this.ChangeProperties(wpfBackground, foreground, Colors.Transparent, new Thickness());
		}
	}
}
