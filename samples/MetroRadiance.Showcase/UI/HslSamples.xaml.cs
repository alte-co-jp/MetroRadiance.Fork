namespace MetroRadiance.Showcase.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using MetroRadiance.Media;


    /// <summary>
    /// Interaction logic for HslSamples.xaml
    /// </summary>
    public partial class HslSamples
    {
        public HslSamples()
        {
            InitializeComponent();

            this.hSlider.ValueChanged += (sender, e) => this.Update();
            this.sSlider.ValueChanged += (sender, e) => this.Update();
            this.lSlider.ValueChanged += (sender, e) => this.Update();

            this.Update();
        }

        private void Update()
        {
            var h = this.hSlider.Value;
            var s = this.sSlider.Value / 100.0;
            var l = this.lSlider.Value / 100.0;

            var hsl = HslColor.FromHsl(h, s, l);
            var c = hsl.ToRgb();

            var lu = Luminosity.FromRgb(c);
            var w = lu <= 128;

            this.colorbox.Background = new SolidColorBrush(c);
            this.colorbox.Foreground = w ? Brushes.White : Brushes.Black;
            this.colorbox.Text = $"Color: {c}({c.R},{c.G},{c.B}), Luminosity: {lu}";
        }
    }
}
