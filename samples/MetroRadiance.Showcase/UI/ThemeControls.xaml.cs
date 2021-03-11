using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Livet;
using MetroRadiance.UI;

namespace MetroRadiance.Showcase.UI
{
	public partial class ThemeControls
	{
		public ThemeControls()
		{
			this.InitializeComponent();
		}
	}

	public class ThemeViewModel : ViewModel
	{
		static ThemeViewModel()
		{
			_instance = new ThemeViewModel();
		}
		static ThemeViewModel _instance;
		public static ThemeViewModel Instance { get { return _instance; } }

		#region Theme property
		Theme _theme = ThemeService.Current.Theme;

		public Theme Theme
		{
			get { return this._theme; }
			set
			{
				if (this._theme != value)
				{
					this._theme = value;
					this.RaisePropertyChanged();
					this.RaisePropertyChanged(nameof(this.Windows));
					this.RaisePropertyChanged(nameof(this.Light));
					this.RaisePropertyChanged(nameof(this.Dark));

					ThemeService.Current.ChangeTheme(this._theme);
				}
			}
		}
		#endregion

		#region Windows 変更通知プロパティ

		public bool Windows
		{
			get { return this._theme == Theme.Windows; }
			set
			{
				if (value)
				{
					this.Theme = Theme.Windows;
				}
			}
		}

		#endregion

		#region Light 変更通知プロパティ

		public bool Light
		{
			get { return this._theme == Theme.Light; }
			set
			{
				if (value)
				{
					this.Theme = Theme.Light;
				}
			}
		}

		#endregion

		#region Dark 変更通知プロパティ

		public bool Dark
		{
			get { return this._theme == Theme.Dark; }
			set
			{
				if (value)
				{
					this.Theme = Theme.Dark;
				}
			}
		}

		#endregion
	}

	public class AccentViewModel : ViewModel
	{
		static AccentViewModel()
		{
			_instance = new AccentViewModel();
		}
		static AccentViewModel _instance;
		public static AccentViewModel Instance { get { return _instance; } }

		#region Accent property
		Accent _accent = ThemeService.Current.Accent;

		public Accent Accent
		{
			get { return this._accent; }
			set
			{
				if (this._accent != value)
				{
					this._accent = value;
					this.RaisePropertyChanged();
					this.RaisePropertyChanged(nameof(this.Windows));
					this.RaisePropertyChanged(nameof(this.Purple));
					this.RaisePropertyChanged(nameof(this.Blue));
					this.RaisePropertyChanged(nameof(this.Orange));
					this.RaisePropertyChanged(nameof(this.Red));

					ThemeService.Current.ChangeAccent(this._accent);
				}
			}
		}
		#endregion

		#region Windows 変更通知プロパティ

		public bool Windows
		{
			get { return this.Accent == Accent.Windows; }
			set
			{
				if (value)
				{
					this.Accent = Accent.Windows;
				}
			}
		}

		#endregion

		#region Purple 変更通知プロパティ

		public bool Purple
		{
			get { return this.Accent == Accent.Purple; }
			set
			{
				if (value)
				{
					this.Accent = Accent.Purple;
				}
			}
		}

		#endregion

		#region Blue 変更通知プロパティ

		public bool Blue
		{
			get { return this.Accent == Accent.Blue; }
			set
			{
				if (value)
				{
					this.Accent = Accent.Blue;
				}
			}
		}

		#endregion

		#region Orange 変更通知プロパティ

		public bool Orange
		{
			get { return this.Accent == Accent.Orange; }
			set
			{
				if (value)
				{
					this.Accent = Accent.Orange;
				}
			}
		}

		#endregion

		#region Red 変更通知プロパティ

		static Accent _accentRed = Accent.FromColor(Colors.Red);

		public bool Red
		{
			get { return this.Accent == _accentRed; }
			set
			{
				if (value)
				{
					this.Accent = _accentRed;
				}
			}
		}

		#endregion
	}
}
