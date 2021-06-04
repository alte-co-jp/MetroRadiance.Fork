/* ==============================
** Copyright 2021 nishy software
**
**		Create : 2021/06/03
** ============================== */

namespace MetroRadiance.UI.AttachedProperties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    class DataGridHelper : DependencyObject
    {
        #region TextColumnDefaultElementStyle Property

        /// <summary>
        /// Using a DependencyProperty as the backing store for TextColumnDefaultElementStyle.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TextColumnDefaultElementStyleProperty =
            DependencyProperty.RegisterAttached("TextColumnDefaultElementStyle", typeof(Style), typeof(DataGridHelper),
                new PropertyMetadata(null, OnTextColumnDefaultElementStyleChanged));

        public static void SetTextColumnDefaultElementStyle(DependencyObject d, Style style)
        {
            d.SetValue(TextColumnDefaultElementStyleProperty, style);
        }

        static void OnTextColumnDefaultElementStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnDataGridColumnDefaultStyleChanged<DataGridTextColumn>(d, e, TextColumnDefaultElementStyleProperty, DataGridTextColumn.ElementStyleProperty);
        }
        #endregion

        #region TextColumnDefaultEditingElementStyle Property

        /// <summary>
        /// Using a DependencyProperty as the backing store for TextColumnDefaultEditingElementStyle.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TextColumnDefaultEditingElementStyleProperty =
            DependencyProperty.RegisterAttached("TextColumnDefaultEditingElementStyle", typeof(Style), typeof(DataGridHelper),
                new PropertyMetadata(null, OnTextColumnDefaultEditingElementStyleChanged));

        public static void SetTextColumnDefaultEditingElementStyle(DependencyObject d, Style style)
        {
            d.SetValue(TextColumnDefaultEditingElementStyleProperty, style);
        }

        static void OnTextColumnDefaultEditingElementStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnDataGridColumnDefaultStyleChanged<DataGridTextColumn>(d, e, TextColumnDefaultEditingElementStyleProperty, DataGridTextColumn.EditingElementStyleProperty);
        }
        #endregion

        #region CheckBoxColumnDefaultElementStyle Property

        /// <summary>
        /// Using a DependencyProperty as the backing store for CheckBoxColumnDefaultElementStyle.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty CheckBoxColumnDefaultElementStyleProperty =
            DependencyProperty.RegisterAttached("CheckBoxColumnDefaultElementStyle", typeof(Style), typeof(DataGridHelper),
                new PropertyMetadata(null, OnCheckBoxColumnDefaultElementStyleChanged));

        public static void SetCheckBoxColumnDefaultElementStyle(DependencyObject d, Style style)
        {
            d.SetValue(CheckBoxColumnDefaultElementStyleProperty, style);
        }

        static void OnCheckBoxColumnDefaultElementStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnDataGridColumnDefaultStyleChanged<DataGridCheckBoxColumn>(d, e, CheckBoxColumnDefaultElementStyleProperty, DataGridCheckBoxColumn.ElementStyleProperty);
        }
        #endregion

        #region CheckBoxColumnDefaultEditingElementStyle Property

        /// <summary>
        /// Using a DependencyProperty as the backing store for CheckBoxColumnDefaultEditingElementStyle.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty CheckBoxColumnDefaultEditingElementStyleProperty =
            DependencyProperty.RegisterAttached("CheckBoxColumnDefaultEditingElementStyle", typeof(Style), typeof(DataGridHelper),
                new PropertyMetadata(null, OnCheckBoxColumnDefaultEditingElementStyleChanged));

        public static void SetCheckBoxColumnDefaultEditingElementStyle(DependencyObject d, Style style)
        {
            d.SetValue(CheckBoxColumnDefaultEditingElementStyleProperty, style);
        }

        static void OnCheckBoxColumnDefaultEditingElementStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnDataGridColumnDefaultStyleChanged<DataGridCheckBoxColumn>(d, e, CheckBoxColumnDefaultEditingElementStyleProperty, DataGridCheckBoxColumn.EditingElementStyleProperty);
        }
        #endregion

        #region ComboBoxColumnDefaultElementStyle Property

        /// <summary>
        /// Using a DependencyProperty as the backing store for ComboBoxColumnDefaultElementStyle.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ComboBoxColumnDefaultElementStyleProperty =
            DependencyProperty.RegisterAttached("ComboBoxColumnDefaultElementStyle", typeof(Style), typeof(DataGridHelper),
                new PropertyMetadata(null, OnComboBoxColumnDefaultElementStyleChanged));

        public static void SetComboBoxColumnDefaultElementStyle(DependencyObject d, Style style)
        {
            d.SetValue(ComboBoxColumnDefaultElementStyleProperty, style);
        }

        static void OnComboBoxColumnDefaultElementStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnDataGridColumnDefaultStyleChanged<DataGridComboBoxColumn>(d, e, ComboBoxColumnDefaultElementStyleProperty, DataGridComboBoxColumn.ElementStyleProperty);
        }
        #endregion

        #region ComboBoxColumnDefaultEditingElementStyle Property

        /// <summary>
        /// Using a DependencyProperty as the backing store for ComboBoxColumnDefaultEditingElementStyle.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ComboBoxColumnDefaultEditingElementStyleProperty =
            DependencyProperty.RegisterAttached("ComboBoxColumnDefaultEditingElementStyle", typeof(Style), typeof(DataGridHelper),
                new PropertyMetadata(null, OnComboBoxColumnDefaultEditingElementStyleChanged));

        public static void SetComboBoxColumnDefaultEditingElementStyle(DependencyObject d, Style style)
        {
            d.SetValue(ComboBoxColumnDefaultEditingElementStyleProperty, style);
        }

        static void OnComboBoxColumnDefaultEditingElementStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnDataGridColumnDefaultStyleChanged<DataGridComboBoxColumn>(d, e, ComboBoxColumnDefaultEditingElementStyleProperty, DataGridComboBoxColumn.EditingElementStyleProperty);
        }
        #endregion

        static void OnDataGridColumnDefaultStyleChanged<TargetColumnClassT>(DependencyObject d, DependencyPropertyChangedEventArgs e,
            DependencyProperty sourceProperty, DependencyProperty targetProperty) where TargetColumnClassT : DataGridColumn
        {
            if (d is DataGrid grid)
            {
                void Columns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs ev)
                {
                    switch (ev.Action)
                    {
                        case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                            UpdateStyles<TargetColumnClassT>(grid, sourceProperty, targetProperty);
                            break;
                    }
                }

                if (e.OldValue == null && e.NewValue != null)
                {
                    grid.Columns.CollectionChanged += Columns_CollectionChanged;
                }
                if (e.OldValue != null && e.NewValue == null)
                {
                    grid.Columns.CollectionChanged -= Columns_CollectionChanged;
                }
                UpdateStyles<TargetColumnClassT>(grid, sourceProperty, targetProperty);
            }
        }

        static void UpdateStyles<TargetColumnClassT>(DataGrid grid, DependencyProperty sourceProperty, DependencyProperty targetProperty) where TargetColumnClassT : DataGridColumn
        {
            var style = grid.GetValue(sourceProperty) as Style;
            foreach (TargetColumnClassT column in grid.Columns.OfType<TargetColumnClassT>())
            {
                var source = DependencyPropertyHelper.GetValueSource(column, targetProperty);
                if(source.BaseValueSource == BaseValueSource.Default)
                {
                    if (style != null)
                    {
                        // override current value
                        column.SetCurrentValue(targetProperty, style);
                    }
                    else
                    {
                        // restore original value
                        var original = column.GetValue(targetProperty);
                        column.SetCurrentValue(targetProperty, original);
                    }
                }
            }
        }
    }
}
