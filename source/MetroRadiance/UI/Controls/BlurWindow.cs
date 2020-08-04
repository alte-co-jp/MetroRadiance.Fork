using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MetroRadiance.Interop;
using MetroRadiance.Interop.Win32;
using MetroRadiance.Platform;

namespace MetroRadiance.UI.Controls
{
	public class BlurWindow : Window
	{
		internal protected static bool IsWindows10 { get; }

		private static bool HasSystemTheme { get; }

		static BlurWindow()
		{
			IsWindows10 = Environment.OSVersion.Version.Major == 10;
			HasSystemTheme = IsWindows10 && Environment.OSVersion.Version.Build >= 18282;

			DefaultStyleKeyProperty.OverrideMetadata(typeof(BlurWindow), new FrameworkPropertyMetadata(typeof(BlurWindow)));
			WindowStyleProperty.OverrideMetadata(typeof(BlurWindow), new FrameworkPropertyMetadata(WindowStyle.None));
			if (IsWindows10)
			{
				AllowsTransparencyProperty.OverrideMetadata(typeof(BlurWindow), new FrameworkPropertyMetadata(true));
			}
		}

		#region ThemeMode 依存関係プロパティ

		public BlurWindowThemeMode ThemeMode
		{
			get { return (BlurWindowThemeMode)this.GetValue(ThemeModeProperty); }
			set { this.SetValue(ThemeModeProperty, value); }
		}
		public static readonly DependencyProperty ThemeModeProperty =
			DependencyProperty.Register("ThemeMode", typeof(BlurWindowThemeMode), typeof(BlurWindow), new UIPropertyMetadata(BlurWindowThemeMode.Default, ThemeModeChangedCallback));

		private static void ThemeModeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var instance = (BlurWindow)d;
			instance.RemoveThemeCallback((BlurWindowThemeMode)e.OldValue);
			instance.AddThemeCallback((BlurWindowThemeMode)e.NewValue);
			instance.HandleThemeChanged();
		}

		#endregion

		#region BlurOpacity 依存関係プロパティ

		public double BlurOpacity
		{
			get { return (double)this.GetValue(BlurOpacityProperty); }
			set { this.SetValue(BlurOpacityProperty, value); }
		}
		public static readonly DependencyProperty BlurOpacityProperty =
			DependencyProperty.Register("BlurOpacity", typeof(double), typeof(BlurWindow), new UIPropertyMetadata(0.8, BlurOpacityChangedCallback));

		private static void BlurOpacityChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var instance = (BlurWindow)d;
			instance.HandleThemeChanged();
		}

		#endregion

		#region DrawBorders 依存関係プロパティ

		public AccentFlags BordersFlag
		{
			get { return (AccentFlags)this.GetValue(BordersFlagProperty); }
			set { this.SetValue(BordersFlagProperty, value); }
		}
		public static readonly DependencyProperty BordersFlagProperty =
			DependencyProperty.Register("BordersFlag", typeof(AccentFlags), typeof(BlurWindow), new UIPropertyMetadata(AccentFlags.DrawAllBorders, BordersFlagChangedCallback));

		private static void BordersFlagChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var instance = (BlurWindow)d;
			instance.HandleThemeChanged();
		}

		#endregion

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			WindowsTheme.HighContrast.Changed += this.HandleThemeChanged1;
			WindowsTheme.Transparency.Changed += this.HandleThemeChanged1;
			AddThemeCallback(this.ThemeMode);

			this.HandleThemeChanged();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			WindowsTheme.HighContrast.Changed -= this.HandleThemeChanged1;
			WindowsTheme.Transparency.Changed -= this.HandleThemeChanged1;
			RemoveThemeCallback(this.ThemeMode);
		}

		private void AddThemeCallback(BlurWindowThemeMode themeMode)
		{
			switch (themeMode)
			{
				case BlurWindowThemeMode.Light:
				case BlurWindowThemeMode.Dark:
				case BlurWindowThemeMode.Accent:
					break;

				case BlurWindowThemeMode.System:
					if (HasSystemTheme)
					{
						WindowsTheme.ColorPrevalence.Changed += this.HandleThemeChanged1;
						WindowsTheme.SystemTheme.Changed += this.HandleThemeValueChanged;
					}
					break;

				default:
					WindowsTheme.Theme.Changed += this.HandleThemeValueChanged;
					break;
			}
		}

		private void RemoveThemeCallback(BlurWindowThemeMode themeMode)
		{
			switch (themeMode)
			{
				case BlurWindowThemeMode.Light:
				case BlurWindowThemeMode.Dark:
				case BlurWindowThemeMode.Accent:
					break;

				case BlurWindowThemeMode.System:
					if (HasSystemTheme)
					{
						WindowsTheme.ColorPrevalence.Changed -= this.HandleThemeChanged1;
						WindowsTheme.SystemTheme.Changed -= this.HandleThemeValueChanged;
					}
					break;

				default:
					WindowsTheme.Theme.Changed -= this.HandleThemeValueChanged;
					break;
			}
		}

		private void HandleThemeChanged1(object sender, bool value)
			=> this.HandleThemeChanged();

		private void HandleThemeValueChanged(object sender, Platform.Theme value)
			=> this.HandleThemeChanged();

		internal protected virtual void HandleThemeChanged()
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
			else
			{
				this.ToBlur();
			}
		}

		internal protected void ToHighContrast()
		{
			WindowComposition.Disable(this);
			this.ChangeProperties(
				ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.ApplicationBackground),
				ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemText),
				SystemColors.WindowFrameColor,
				this.GetBordersFlagAsThickness(2));
		}

		internal protected void ToCompatibility()
		{
			if (this.ThemeMode == BlurWindowThemeMode.Dark)
			{
				this.ChangeProperties(
					SystemColors.WindowTextColor,
					SystemColors.WindowColor,
					SystemColors.WindowFrameColor,
					new Thickness());
			}
			else
			{
				this.ChangeProperties(
					SystemColors.WindowColor,
					SystemColors.WindowTextColor,
					SystemColors.WindowFrameColor,
					new Thickness());
			}
		}

		internal protected void GetColors(out Color background, out Color foreground)
		{
			var colorPrevalence = WindowsTheme.ColorPrevalence.Current;
			switch (this.ThemeMode)
			{
				case BlurWindowThemeMode.Light:
					background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.LightChromeMedium);
					foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextLightTheme);
					break;

				case BlurWindowThemeMode.Dark:
					background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.DarkChromeMedium);
					foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextDarkTheme);
					break;

				case BlurWindowThemeMode.Accent:
					background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemAccentDark1);
					foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextDarkTheme);
					break;

				case BlurWindowThemeMode.System:
					if (HasSystemTheme)
					{
						if (colorPrevalence)
						{
							background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemAccentDark1);
							foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextDarkTheme);
						}
						else if (WindowsTheme.SystemTheme.Current == Platform.Theme.Light)
						{
							background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.LightChromeMedium);
							foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextLightTheme);
						}
						else
						{
							background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.DarkChromeMedium);
							foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextDarkTheme);
						}
					}
					else
					{
						background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.DarkChromeMedium);
						foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextDarkTheme);
					}
					break;

				default:
					if (WindowsTheme.Theme.Current == Platform.Theme.Dark)
					{
						background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.DarkChromeMedium);
						foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextDarkTheme);
					}
					else
					{
						background = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.LightChromeMedium);
						foreground = ImmersiveColor.GetColorByTypeName(ImmersiveColorNames.SystemTextLightTheme);
					}
					break;
			}
		}

		internal protected void ToBlur()
		{
			Color background, foreground;
			this.GetColors(out background, out foreground);

			background.A = (byte)(background.A * this.BlurOpacity);
			WindowComposition.EnableBlur(this, this.BordersFlag);
			this.ChangeProperties(background, foreground, Colors.Transparent, new Thickness());
		}

		internal protected void ToDefault()
		{
			Color background, foreground;
			this.GetColors(out background, out foreground);

			WindowComposition.Disable(this);
			this.ChangeProperties(background, foreground, SystemColors.WindowFrameColor, this.GetBordersFlagAsThickness(1));
		}

		internal protected void ChangeProperties(Color background, Color foreground, Color border, Thickness borderThickness)
		{
			this.Background = new SolidColorBrush(background);
			this.Foreground = new SolidColorBrush(foreground);
			this.BorderBrush = new SolidColorBrush(border);
			this.BorderThickness = borderThickness;
		}

		private Thickness GetBordersFlagAsThickness(double width)
		{
			return new Thickness(
				this.BordersFlag.HasFlag(AccentFlags.DrawLeftBorder) ? width : 0.0,
				this.BordersFlag.HasFlag(AccentFlags.DrawTopBorder) ? width : 0.0,
				this.BordersFlag.HasFlag(AccentFlags.DrawRightBorder) ? width : 0.0,
				this.BordersFlag.HasFlag(AccentFlags.DrawBottomBorder) ? width : 0.0);
		}
	}
}
