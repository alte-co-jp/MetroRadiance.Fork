﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using MetroRadiance.Chrome.Primitives;

namespace MetroRadiance.Chrome
{
	[TemplatePart(Name = PART_GradientBrush, Type = typeof(GradientBrush))]
	public abstract class GlowingEdge : Control, IValueConverter
	{
#pragma warning disable IDE1006
		private const string PART_GradientBrush = nameof(PART_GradientBrush);
#pragma warning restore IDE1006

		#region Infrastructures
#pragma warning disable IDE1006
		// ReSharper disable InconsistentNaming

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static GridLength __Thickness => new GridLength(ChromeWindow.Thickness);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static double __CornerThickness => ChromeWindow.Thickness * 2;

		// ReSharper restore InconsistentNaming
#pragma warning restore IDE1006
		#endregion

		#region CanResize dependency property

		public static readonly DependencyProperty CanResizeProperty = DependencyProperty.Register(
			nameof(CanResize), typeof(bool), typeof(GlowingEdge), new PropertyMetadata(true));

		public bool CanResize
		{
			get { return (bool)this.GetValue(CanResizeProperty); }
			set { this.SetValue(CanResizeProperty, value); }
		}

		#endregion

		protected GlowingEdge()
		{ }

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			var num = 1;
			if (this.GetTemplateChild(PART_GradientBrush) is GradientBrush brush)
			{
				this.SetGradientStops(brush);
				num++;
			}

			while ((brush = this.GetTemplateChild(PART_GradientBrush + num) as GradientBrush) != null)
			{
				this.SetGradientStops(brush);
				num++;
			}
		}

		private void SetGradientStops(GradientBrush brush)
		{
			var stops = new GradientStopCollection();
			var options = new[]
			{
				// Offset, Opacity
				Tuple.Create(1.0, 0.005),
				Tuple.Create(0.8, 0.020),
				Tuple.Create(0.6, 0.040),
				Tuple.Create(0.4, 0.080),
				Tuple.Create(0.2, 0.160),
				Tuple.Create(0.1, 0.260),
				Tuple.Create(0.0, 0.360),
			};

			foreach (var tuple in options)
			{
				var stop = new GradientStop { Offset = tuple.Item1, };
				var binding = new Binding(nameof(this.BorderBrush))
				{
					Source = this,
					Converter = this,
					ConverterParameter = tuple.Item2,
				};
				BindingOperations.SetBinding(stop, GradientStop.ColorProperty, binding);
				stops.Add(stop);
			}

			brush.GradientStops = stops;
		}

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Color color;
			if (value is Color color2)
			{
				color = color2;
			}
			else if (value is SolidColorBrush brush)
			{
				color = brush.Color;
			}
			else
			{
				return Colors.Transparent;
			}

			if (!double.TryParse(parameter.ToString(), out var opacity))
			{
				return color;
			}

			color.A = (byte)(color.A * opacity);
			return color;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
