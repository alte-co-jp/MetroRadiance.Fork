using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MetroRadiance.UI;

namespace MetroRadiance.Showcase
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			this.ShutdownMode = ShutdownMode.OnMainWindowClose;

			ThemeService.Current.EnableUwpResoruces();
			ThemeService.Current.Register(this, Theme.Windows, Accent.Windows);
		}
	}
}
