# Changelog for MetroRadiance.Fork

## 3.1.0 MetroRadiance

- ### Breaking changes
  - Change then name of the resource keys added in Version 3.0.
If you have been using the style keys added in Version 3.0 directly, you will need to change them.

    |Control    | style key name in Ver. 3.1     | style key name in Ver. 3.0     |
    | ---       | ---                            | ---                            |
    |ComboBox   |MetroRadiance.Styles.ComboBox   |MetroRadianceComboBoxStyleKey   |
    |ContextMenu|MetroRadiance.Styles.ContextMenu|MetroRadianceContextMenuStyleKey|
    |GroupBox   |MetroRadiance.Styles.GroupBox   |MetroRadianceGroupBoxStyleKey   |
    |Label      |MetroRadiance.Styles.Label      |MetroRadianceLabelStyleKey      |
    |Menu       |MetroRadiance.Styles.Menu       |MetroRadianceMenuStyleKey       |
    |MenuItem   |MetroRadiance.Styles.MenuItem   |MetroRadianceMenuItemStyleKey   |
    |TextBoxBase|MetroRadiance.Styles.TextBoxBase|MetroRadianceTextBoxBaseStyleKey|

- ### Enhancements/Features
  - Add standard control styles (DataGrid)
  - Add attached properties (DataGridProperties)

- ### Bug fixes
  - [#1](../../issues/1) - Error occurs when displaying ToolTip in System.Windows.Window (System.Windows.WindowÇ≈ToolTipÇï\é¶Ç∑ÇÈÇ∆Ç´Ç…ÉGÉâÅ[Ç™î≠ê∂Ç∑ÇÈ)
  - [#2](../../issues/2) - Bug: ComboBox appearance and UWP brushes may not follow dynamic changes in theme

- ### Limitations
  - When moving windows using AcrylicBlurWindow class, the move process is very slow on Windows 10 1903 (Build 18362, May 2019 Update) or later


## 3.0.0 MetroRadiance
- ### Changes
  - Support .NET Core 3.1 (WPF)
  - Add UWP compatible resources (colors/brushes)
  - Add AcrylicBlurWindow class
  - Add a style key for each style defined by MetroRadiance
  - Add standard control styles (ComboBox/GroupBox/Label/TextBox)
  - Add standard menu styles (Menu/MenuItem/ContextMenu)
  - Add number validation rules (Int16Rule/UInt16Rule/Int32Rule/UInt32Rule/SingleRule/DoubleRule)
  - Add toottip text to Caption buttons (Minimize/Maximize/Restore/Close).
  - Localize the text string used in the Int32Rule class (en/ja/de/fr/ko/zh-Hans/zh-Hant)
  - Apply dark theme to the system menu
  - Improve CaptionIcon behavior to work with Window.
  - Improve the appearance of radio button
  - Adjust the foreground color of the tooltip control
  - Fix an issue where brush values would not update as the theme changed after the control was reloaded

- ### Limitations
  - When moving windows using AcrylicBlurWindow class, the move process is very slow on Windows 10 1903 (Build 18362, May 2019 Update) or later

## 3.0.0 MetroRadiance.Chrome
- ### Changes
  - Support .NET Core 3.1 (WPF)
  - Adjust for High DPI enviroment
  - Fix misalignment issues of some parts
  - [internal change]Update the formatting of source code

## 3.0.0 MetroRadiance.Core
- ### Changes
  - Support .NET Core 3.1 (WPF)
  - Add HslColor class for HSL color model
  - Add text scale factor support.
  - Improve interop methods
  - Add new DPIHelper class for DPI / Per-Monitor DPI support

- ### Breaking changes
  - The type of properties in the MetroRadiance.Platform.WindowsTheme class has changed. Affected if the received variable declaration is used an explicitly specified type instead of using "var".
