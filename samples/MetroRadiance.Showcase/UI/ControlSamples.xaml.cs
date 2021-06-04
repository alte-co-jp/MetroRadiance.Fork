using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

	public class SampleDataItem : NotificationObject
	{
		int _id;
		public int Id
		{
			get { return this._id; }
			set { RaisePropertyChangedIfSet(ref this._id, value); }
		}

		string _name;
		public string Name
		{
			get { return this._name; }
			set { RaisePropertyChangedIfSet(ref this._name, value); }
		}

		string _description;
		public string Description
		{
			get { return this._description; }
			set { RaisePropertyChangedIfSet(ref this._description, value); }
		}

		bool? _active;
		public bool? Active
		{
			get { return this._active; }
			set { RaisePropertyChangedIfSet(ref this._active, value); }
		}

		string _selected;
		public string Selected
		{
			get { return this._selected; }
			set { RaisePropertyChangedIfSet(ref this._selected, value); }
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
