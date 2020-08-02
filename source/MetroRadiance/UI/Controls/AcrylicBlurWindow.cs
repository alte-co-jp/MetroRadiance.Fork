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
			else if (IsAcrylicBlurEnabled)
			{
				this.ToAcrylicBlur(WindowsTheme.Transparency.Current);
			}
			else
			{
				this.ToBlur(WindowsTheme.Transparency.Current);
			}
		}

		private void ToAcrylicBlur(bool transparency)
		{
			Color background, foreground;
			this.GetColors(out background, out foreground);

			var wpfBackground = Color.FromArgb(1, 0, 0, 0);
			if (transparency)
			{
				WindowComposition.EnableAcrylicBlur(this, background, (byte)(255 * this.BlurOpacity), this.BordersFlag);
				this.ChangeProperties(wpfBackground, foreground, Colors.Transparent, new Thickness());
			}
			else
			{
				// Memo: It is impossible to reactivate the acrylic blur effect. So, its effect always turn on.
				WindowComposition.EnableAcrylicBlur(this, background, 255, this.BordersFlag);
				this.ChangeProperties(wpfBackground, foreground, Colors.Transparent, new Thickness());
			}
		}
	}
}
