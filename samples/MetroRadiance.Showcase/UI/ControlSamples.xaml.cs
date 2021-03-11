using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Livet;

namespace MetroRadiance.Showcase.UI
{
	partial class ControlSamples
	{
		public ControlSamples()
		{
			this.InitializeComponent();
			this.DataContext = new SampleValues();
		}

		private void HandleSystemWindowButtonClicked(object sender, RoutedEventArgs e)
		{
			new MainSystemWindow().Show();
		}

		private void HandleBlurWindowButtonClicked(object sender, RoutedEventArgs e)
		{
			new BlurWindowSample().Show();
		}

		private void HandleAcrylicBlurWindowButtonClicked(object sender, RoutedEventArgs e)
		{
			new AcrylicBlurWindowSample().Show();
		}
	}

	public class SampleValues : NotificationObject
	{
		ushort _uint16= 16;
		int _int32 = 32;
		double _double = 7.4;

		public ushort UInt16
		{
			get { return this._uint16; }
			set
			{
				if (this._uint16 != value)
				{
					this._uint16 = value;
					this.RaisePropertyChanged();
				}
			}
		}

		public int Int32
		{
			get { return this._int32; }
			set
			{
				if (this._int32 != value)
				{
					this._int32 = value;
					this.RaisePropertyChanged();
				}
			}
		}

		public double Double
		{
			get { return this._double; }
			set
			{
				if (this._double != value)
				{
					this._double = value;
					this.RaisePropertyChanged();
				}
			}
		}
	}
}
