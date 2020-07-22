# MetroRadiance.Fork

MetroRadiance.Fork: [![Build Status (develop)](https://nishy-software.visualstudio.com/MetroRadiance/_apis/build/status/nishy2000.MetroRadiance?branchName=develop&label=develop)](https://nishy-software.visualstudio.com/MetroRadiance/_build/latest?definitionId=6&branchName=develop)
[![Build Status (master)](https://nishy-software.visualstudio.com/MetroRadiance/_apis/build/status/nishy2000.MetroRadiance?branchName=master&label=master)](https://nishy-software.visualstudio.com/MetroRadiance/_build/latest?definitionId=6&branchName=master)

[![Downloads (.Core)](https://img.shields.io/nuget/dt/MetroRadiance.Fork.Core.svg?style=flat-square&label=downloads%20%28.Core%29)](https://www.nuget.org/packages/MetroRadiance.Fork.Core/)
[![NuGet](https://img.shields.io/nuget/v/MetroRadiance.Fork.svg?style=flat-square)](https://www.nuget.org/packages/MetroRadiance.Fork/)
[![NuGet (pre)](https://img.shields.io/nuget/vpre/MetroRadiance.Fork.svg?style=flat-square&label=nuget-pre)](https://www.nuget.org/packages/MetroRadiance.Fork/)
[![Release](https://img.shields.io/github/tag/nishy2000/MetroRadiance.svg?style=flat-square)](https://github.com/nishy2000/MetroRadiance/releases)
[![License](https://img.shields.io/github/license/nishy2000/MetroRadiance.svg?style=flat-square)](https://github.com/nishy2000/MetroRadiance/blob/master/LICENSE.txt)

[![Issues](https://img.shields.io/github/issues/nishy2000/MetroRadiance.svg?style=flat-square)](https://github.com/nishy2000/MetroRadiance/issues)
[![Issues](https://img.shields.io/github/issues-closed/nishy2000/MetroRadiance.svg?style=flat-square)](https://github.com/nishy2000/MetroRadiance/issues?q=is%3Aissue+is%3Aclosed)
[![Pull Requests](https://img.shields.io/github/issues-pr/nishy2000/MetroRadiance.svg?style=flat-square)](https://github.com/nishy2000/MetroRadiance/pulls)
[![Pull Requests](https://img.shields.io/github/issues-pr-closed/nishy2000/MetroRadiance.svg?style=flat-square)](https://github.com/nishy2000/MetroRadiance/pulls?q=is%3Apr+is%3Aclosed)

( MetroRadiance: [![Build status (master)](https://img.shields.io/appveyor/ci/Grabacr07/MetroRadiance/master.svg?style=flat-square&label=build%20(master))](https://ci.appveyor.com/project/Grabacr07/MetroRadiance)
[![Downloads](https://img.shields.io/nuget/dt/MetroRadiance.Core.svg?style=flat-square)](https://www.nuget.org/packages/MetroRadiance.Core/)
[![NuGet](https://img.shields.io/nuget/v/MetroRadiance.Core.svg?style=flat-square)](https://www.nuget.org/packages/MetroRadiance.Core/)
[![License](https://img.shields.io/github/license/Grabacr07/MetroRadiance.svg?style=flat-square)](https://github.com/Grabacr07/MetroRadiance/blob/master/LICENSE.txt) )

UI control libraries for create WPF window like Visual Studio 2012/2013/2015.
MetroRadiance.Fork is forked of MetroRadiance 2.4.0.

![ss150730085651kd](https://cloud.githubusercontent.com/assets/1779073/8972861/0e3eed28-3699-11e5-9bfe-18af42a6ed73.png)


# Installation

Install NuGet package(s).

```powershell
PM> Install-Package MetroRadiance.Fork
```

* [MetroRadiance.Fork.Core](https://www.nuget.org/packages/MetroRadiance.Fork.Core/) - MetroRadiance core library.
* [MetroRadiance.Fork.Chrome](https://www.nuget.org/packages/MetroRadiance.Fork.Chrome/) - Chrome library for WPF Window.
* [MetroRadiance.Fork](https://www.nuget.org/packages/MetroRadiance.Fork/) - WPF custom control library.


# Features / How to use

## MetroRadiance.Core

### DPI / Per-Monitor DPI support
  - Get system DPI
  - Get monitor DPI from HwndSource or window handle

```csharp
using MetroRadiance.Interop;
```

```csharp
// Get system dpi
var systemDpi = window.GetSystemDpi();

if (PerMonitorDpi.IsSupported)
{
    // Get monitor dpi.
    var hwndSource = (HwndSource)PresentationSource.FromVisual(this);
    var monitorDpi = hwndSource.GetDpi();
}
```

### Windows theme support
  - Get Windows system theme (Light or Dark, only Windows 10)
  - Get Windows app theme (Light or Dark, only Windows 10)
  - Get Windows accent color
  - Subscribe color change event from Windows

```csharp
using MetroRadiance.Platform;
```

```csharp
// Get Windows accent color
var color = WindowsTheme.GetAccentColor();

// Subscribe accent color change event from Windows theme.
var disposable = WindowsTheme.RegisterAccentColorListener(color =>
{
    // apply color to your app.
});

// Unsubscribe color change event.
disposable.Dispose();
```

### HSV color model support

```csharp
using MetroRadiance.Media;
```

```csharp
// Get Windows accent color (using MetroRadiance.Platform;)
var rgbColor = WindowsTheme.GetAccentColor();

// Convert from RGB to HSV color.
var hsvColor = rgbColor.ToHsv();
hsvColor.V *= 0.8;

// Convert from HSV to RGB color.
var newColor = hsvColor.ToRgb();
```

## MetroRadiance.Chrome

### Add window chrome like Visual Studio to WPF Window
  - `MetroRadiance.Chrome.WindowChrome`

```XAML
<Window xmlns:chrome="http://schemes.grabacr.net/winfx/2014/chrome">
    <chrome:WindowChrome.Instance>
        <chrome:WindowChrome />
    </chrome:WindowChrome.Instance>
</Window>
```

### Add any UI elements to window chrome
  - `MetroRadiance.Chrome.WindowChrome.Top` / `.Left` / `.Right` / `.Bottom`

```XAML
<Window xmlns:chrome="http://schemes.grabacr.net/winfx/2014/chrome">
    <chrome:WindowChrome.Instance>
        <chrome:WindowChrome>
            <chrome:WindowChrome.Top>
                <Border Background="DarkRed"
                        Padding="24,3"
                        Margin="8,0"
                        HorizontalAlignment="Right">
                    <TextBlock Text="any UI elements"
                               Foreground="White" />
                </Border>
            </chrome:WindowChrome.Top>
        </chrome:WindowChrome>
    </chrome:WindowChrome.Instance>
</Window>
```

![ss160128005316ls](https://cloud.githubusercontent.com/assets/1779073/12619010/9d8d6e24-c559-11e5-8be1-04aa0eba278f.png)

## MetroRadiance

### Theme support

```csharp
// Change theme.
ThemeService.Current.ChangeTheme(Theme.Dark);

// Change theme (sync Windows)
ThemeService.Current.ChangeTheme(Theme.Windows);

// Change Accent
ThemeService.Current.ChangeAccent(Accent.Blue);

// Change accent (sync Windows)
ThemeService.Current.ChangeAccent(Accent.Windows);

// Change accent (from RGB Color)
var accent = Colors.Red.ToAccent();
ThemeService.Current.ChangeAccent(accent);
```

### Colors and Brushes defined in MetroRadiance

MetroRadiance defines colors and brushes.
Naming rule is `[ColorName]ColorKey` and `[ColorName]BrushKey`.
If you use these as `DynamicResource` in your controls, the control will work with the theme color.

Table: Theme Color

|Color Name |Sample| Color Key | Brush Key|
|---|---|---|---|
|Theme|<span style="background:#EFEFF2">Light</span>/<span style="background:#2D2D30">Dark</span>|ThemeColorKey|ThemeBrushKey|
|Background|<span style="background:#D7D7DC">Light</span>/<span style="background:#1B1B1C">Dark</span>|BackgroundColorKey|BackgroundBrushKey|
|Border|<span style="background:#B9B9C0">Light</span>/<span style="background:#3F3F46">Dark</span>|BorderColorKey|BorderBrushKey|
|Foreground|<span style="background:#363636">Light</span>/<span style="background:#C8C8C8">Dark</span>|ForegroundColorKey|ForegroundBrushKey|
|SemiactiveBackground|<span style="background:#F5F5F8">Light</span>/<span style="background:#333337">Dark</span>|SemiactiveBackgroundColorKey|SemiactiveBackgroundBrushKey|
|SemiactiveBorder|<span style="background:#B9B9C0">Light</span>/<span style="background:#3F3F46">Dark</span>|SemiactiveBorderColorKey|SemiactiveBorderBrushKey|
|SemiActiveForeground|<span style="background:#101010">Light</span>/<span style="background:#A1A0A0">Dark</span>|SemiActiveForegroundColorKey|SemiActiveForegroundBrushKey|
|ActiveBackground|<span style="background:#FFFFFF">Light</span>/<span style="background:#3F3F46">Dark</span>|ActiveBackgroundColorKey|ActiveBackgroundBrushKey|
|ActiveBorder|<span style="background:#B9B9C0">Light</span>/<span style="background:#3F3F46">Dark</span>|ActiveBorderColorKey|ActiveBorderBrushKey|
|ActiveForeground|<span style="background:#000000">Light</span>/<span style="background:White">Dark</span>|ActiveForegroundColorKey|ActiveForegroundBrushKey|
|InactiveBackground|<span style="background:#EFEFF2">Light</span>/<span style="background:#2D2D30">Dark</span>|InactiveBackgroundColorKey|InactiveBackgroundBrushKey|
|InactiveBorder|<span style="background:#D7D7DE">Light</span>/<span style="background:#3C3C41">Dark</span>|InactiveBorderColorKey|InactiveBorderBrushKey|
|InactiveForeground|<span style="background:#808080">Light</span>/<span style="background:#787878">Dark</span>|InactiveForegroundColorKey|InactiveForegroundBrushKey|
|HighlightBackground|<span style="background:#A5A5AC">Light</span>/<span style="background:#686868">Dark</span>|HighlightBackgroundColorKey|HighlightBackgroundBrushKey|
|HighlightBorder|<span style="background:#A5A5AC">Light</span>/<span style="background:#686868">Dark</span>|HighlightBorderColorKey|HighlightBorderBrushKey|
|HighlightForeground|<span style="background:#000000">Light</span>/<span style="background:#FFFFFF">Dark</span>|HighlightForegroundColorKey|HighlightForegroundBrushKey|
|Highlight2Background|<span style="background:#696970">Light</span>/<span style="background:#A0A0A0">Dark</span>|Highlight2BackgroundColorKey|Highlight2BackgroundBrushKey|
|Highlight2Border|<span style="background:#696970">Light</span>/<span style="background:#A0A0A0">Dark</span>|Highlight2BorderColorKey|Highlight2BorderBrushKey|
|Highlight2Foreground|<span style="background:#FFFFFF">Light</span>/<span style="background:#000000">Dark</span>|Highlight2ForegroundColorKey|Highlight2ForegroundBrushKey|
|Highlight3Background|<span style="background:#4B4B52">Light</span>/<span style="background:#C8C8C8">Dark</span>|Highlight3BackgroundColorKey|Highlight3BackgroundBrushKey|
|Highlight3Border|<span style="background:#4B4B52">Light</span>/<span style="background:#C8C8C8">Dark</span>|Highlight3BorderColorKey|Highlight3BorderBrushKey|
|Highlight3Foreground|<span style="background:#FFFFFF">Light</span>/<span style="background:#000000">Dark</span>|Highlight3ForegroundColorKey|Highlight3ForegroundBrushKey|
|LinkForeground|<span style="background:#0000C8">Light</span>/<span style="background:#7AC1FF">Dark</span>|LinkForegroundColorKey|LinkForegroundBrushKey|
|ActiveLinkForeground|<span style="background:#007ACC">Light</span>/<span style="background:#007ACC">Dark</span>|ActiveLinkForegroundColorKey|ActiveLinkForegroundBrushKey|
|ValidationError|<span style="background:#C80000">Light</span>/<span style="background:#C80000">Dark</span>|ValidationErrorColorKey|ValidationErrorBrushKey|


Table: Accent Color
|Color Name |Sample| Color Key | Brush Key|
|---|---|---|---|
|Accent|<span style="background:#007ACC">Blue</span>/<span style="background:#CA5100">Orange</span>/<span style="background:#68217A">Purple</span>|AccentColorKey|AccentBrushKey|
|AccentHighlight|<span style="background:#28A0F0">Blue</span>/<span style="background:#F07828">Orange</span>/<span style="background:#8C46A0">Purple</span>|AccentHighlightColorKey|AccentHighlightBrushKey|
|AccentActive|<span style="background:#005AAA">Blue</span>/<span style="background:#B43C00">Orange</span>/<span style="background:#5A1464">Purple</span>|AccentActiveColorKey|AccentActiveBrushKey|
|AccentForeground|<span style="background:White">Blue</span>/<span style="background:White">Orange</span>/<span style="background:White">Purple</span>|AccentForegroundColorKey|AccentForegroundBrushKey|

Style definition sample
```csharp
<Style TargetType="{x:Type Button}">
    <Setter Property="Background"
            Value="{DynamicResource BackgroundBrushKey}" />
    <Setter Property="BorderBrush"
            Value="{DynamicResource BorderBrushKey}" />
    <Setter Property="Foreground"
            Value="{DynamicResource ActiveForegroundBrushKey}" />
    <Setter Property="BorderThickness"
            Value=".99" />
    <Setter Property="Padding"
            Value="8,2" />
    <Setter Property="FocusVisualStyle"
            Value="{DynamicResource {x:Static SystemParameters.FocusVisualStyleKey}}" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type Button}">
                <Border BorderBrush="{TemplateBinding BorderBrush}"
                        BorderThickness="{TemplateBinding BorderThickness}"
                        SnapsToDevicePixels="{TemplateBinding SnapsToDevicePixels}"
                        Background="{TemplateBinding Background}">
                    <ContentPresenter x:Name="contentPresenter"
                                        Margin="{TemplateBinding Padding}"
                                        HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                        VerticalAlignment="{TemplateBinding VerticalContentAlignment}" />
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
    <Style.Triggers>
        <Trigger Property="IsMouseOver"
                    Value="True">
            <Setter Property="Background"
                    Value="{DynamicResource ActiveBackgroundBrushKey}" />
            <Setter Property="BorderBrush"
                    Value="{DynamicResource ActiveBorderBrushKey}" />
        </Trigger>
        <Trigger Property="IsPressed"
                    Value="True">
            <Setter Property="Background"
                    Value="{DynamicResource AccentBrushKey}" />
            <Setter Property="BorderBrush"
                    Value="{DynamicResource ActiveBorderBrushKey}" />
            <Setter Property="Foreground"
                    Value="{DynamicResource AccentForegroundBrushKey}" />
        </Trigger>
        <Trigger Property="IsEnabled"
                    Value="False">
            <Setter Property="Background"
                    Value="{DynamicResource InactiveBackgroundBrushKey}" />
            <Setter Property="BorderBrush"
                    Value="{DynamicResource InactiveBorderBrushKey}" />
            <Setter Property="Foreground"
                    Value="{DynamicResource InactiveForegroundBrushKey}" />
        </Trigger>
    </Style.Triggers>
</Style>
```

### Standard control styles

You can use the standard control styles by merging "/Styles/Controls.xaml" of MetroRadiance into ```Application.Resource``` etc.
These styles use the colors and brushes defined in MetroRadiance. Therefore, it will be linked to the theme change and color setting change.

```csharp
<Application x:Class="MetroRadiance.Showcase.App"
			 xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
			 xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
			 StartupUri="UI/MainWindow.xaml">
	<Application.Resources>
		<ResourceDictionary>
			<ResourceDictionary.MergedDictionaries>
				<ResourceDictionary Source="pack://application:,,,/MetroRadiance;component/Styles/Controls.xaml" />
				<ResourceDictionary Source="pack://application:,,,/MetroRadiance;component/Themes/Dark.xaml" />
				<ResourceDictionary Source="pack://application:,,,/MetroRadiance;component/Themes/Accents/Blue.xaml" />
			</ResourceDictionary.MergedDictionaries>
		</ResourceDictionary>
	</Application.Resources>
</Application>
```

Styles are defined by "/Styles/Controls.xaml"
 - `<Style TargetType="{x:Type Button}">`
 - `<Style x:Key="CircleButtonStyleKey" TargetType="{x:Type Button}">`
 - `<Style TargetType="{x:Type CheckBox}">`
 - `<Style TargetType="{x:Type Expander}">`
 - `<Style x:Key="{x:Static SystemParameters.FocusVisualStyleKey}">`
 - `<Style TargetType="{x:Type PasswordBox}">`
 - `<Style TargetType="{x:Type RadioButton}">`
 - `<Style TargetType="{x:Type ScrollBar}">`
 - `<Style TargetType="{x:Type ToggleButton}">`
 - `<Style TargetType="{x:Type ToolTip}">`


### Custom controls
- Badge
- BindableRichTextBox
- BindableTextBlock
- BlurWindow
- CaptionButton
- CaptionIcon
- ExpanderButton
- LinkButton
- MetroWindow
- ProgressRing
- PromptComboBox
- PromptTextBox
- ResizeGrip
- SystemButtons
- TabView
- 

### Custom converters
- WindowStateToVisibilityConverter

### Custom behaviors


## License

This library is under [the MIT License (MIT)](LICENSE.txt).
