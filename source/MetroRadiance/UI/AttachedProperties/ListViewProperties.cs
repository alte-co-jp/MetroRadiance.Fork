/* ==============================
** Copyright 2021 nishy software
**
**		Create : 2021/08/13
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

    public class ListViewProperties : DependencyObject
    {
        static readonly object s_lockObject = new object();
        static readonly Dictionary<int, List<Tuple<WeakReference<DependencyObject>, Dictionary<DependencyProperty, PropertyChangeNotifier>>>> s_attachedObjects = new Dictionary<int, List<Tuple<WeakReference<DependencyObject>, Dictionary<DependencyProperty, PropertyChangeNotifier>>>>();

        #region ColumnHeaderDefaultContainerStyle Property

        /// <summary>
        /// Using a DependencyProperty as the backing store for ColumnHeaderDefaultContainerStyle.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ColumnHeaderDefaultContainerStyleProperty =
            DependencyProperty.RegisterAttached("ColumnHeaderDefaultContainerStyle", typeof(Style), typeof(ListViewProperties),
                new PropertyMetadata(null, OnColumnHeaderDefaultContainerStyleChanged));

        public static void SetColumnHeaderDefaultContainerStyle(DependencyObject d, Style style)
        {
            d.SetValue(ColumnHeaderDefaultContainerStyleProperty, style);
        }

        static void OnColumnHeaderDefaultContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnListViewGridViewDefaultStyleChanged<GridView>(d, e, ColumnHeaderDefaultContainerStyleProperty, GridView.ColumnHeaderContainerStyleProperty);
        }

        #endregion

        static void OnListViewGridViewDefaultStyleChanged<TargetClassT>(DependencyObject d, DependencyPropertyChangedEventArgs e,
            DependencyProperty sourceProperty, DependencyProperty targetProperty) where TargetClassT : ViewBase
        {
            if (d is ListView listView)
            {
                void ListView_ViewChanged(object sender, EventArgs ev)
                {
                    System.Diagnostics.Debug.WriteLine("ListView_ViewChanged: " + sender.ToString() + sender.GetHashCode().ToString() + ":" + ev.ToString());
                    UpdateStyles<TargetClassT>(listView, sourceProperty, targetProperty);
                }

                if (e.OldValue == null && e.NewValue != null)
                {
#if true
                    lock (s_lockObject)
                    {
                        var hashCode = listView.GetHashCode();
                        if (!s_attachedObjects.TryGetValue(hashCode, out var list))
                        {
                            var weakRef = new WeakReference<DependencyObject>(listView);
                            list = new List<Tuple<WeakReference<DependencyObject>, Dictionary<DependencyProperty, PropertyChangeNotifier>>>();
                            s_attachedObjects.Add(hashCode, list);
                        }

                        Tuple<WeakReference<DependencyObject>, Dictionary<DependencyProperty, PropertyChangeNotifier>> value = null;
                        foreach (var item in list)
                        {
                            if (item.Item1.TryGetTarget(out var target)
                                && target == listView)
                            {
                                value = item;
                            }
                        }

                        if (value == null)
                        {
                            value = new Tuple<WeakReference<DependencyObject>, Dictionary<DependencyProperty, PropertyChangeNotifier>>(new WeakReference<DependencyObject>(listView), new Dictionary<DependencyProperty, PropertyChangeNotifier>());
                            list.Add(value);
                        }
                        if (!value.Item2.TryGetValue(sourceProperty, out var notifier))
                        {
                            notifier = new PropertyChangeNotifier(listView, ListView.ViewProperty);
                            value.Item2.Add(sourceProperty, notifier);
                            notifier.ValueChanged += ListView_ViewChanged;
                        }
                    }
#else
                    var desc = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(ListView.ViewProperty, typeof(ViewBase));
                    desc.AddValueChanged(listView, ListView_ViewChanged);
#endif
                }
                if (e.OldValue != null && e.NewValue == null)
                {
#if true
                    PropertyChangeNotifier notifier = null;
                    lock (s_lockObject)
                    {
                        var hashCode = listView.GetHashCode();
                        if (s_attachedObjects.TryGetValue(hashCode, out var list))
                        {
                            Tuple<WeakReference<DependencyObject>, Dictionary<DependencyProperty, PropertyChangeNotifier>> value = null;
                            foreach (var item in list.ToList())
                            {
                                if (item.Item1.TryGetTarget(out var target))
                                {
                                    if (target == listView)
                                    {
                                        value = item;
                                        break;
                                    }
                                }
                                else
                                {
                                    list.Remove(item);
                                }
                            }

                            if (value != null)
                            {
                                if (value.Item2.TryGetValue(sourceProperty, out notifier))
                                {
                                    value.Item2.Remove(sourceProperty);
                                    notifier.ValueChanged -= ListView_ViewChanged;
                                }
                                if (value.Item2.Count <= 0)
                                {
                                    list.Remove(value);
                                }
                                if (list.Count <= 0)
                                {
                                    s_attachedObjects.Remove(hashCode);
                                }
                            }
                        }
                    }
                    notifier?.Dispose();
#else
                    var desc = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(ListView.ViewProperty, typeof(ViewBase));
                    desc.RemoveValueChanged(listView, ListView_ViewChanged);
#endif
                }
                UpdateStyles<TargetClassT>(listView, sourceProperty, targetProperty);
            }
        }

        static void UpdateStyles<TargetClassT>(ListView listView, DependencyProperty sourceProperty, DependencyProperty targetProperty) where TargetClassT : ViewBase
        {
            if (listView.GetValue(ListView.ViewProperty) is ViewBase viewBase)
            {
                var source = DependencyPropertyHelper.GetValueSource(viewBase, targetProperty);
                if (source.BaseValueSource == BaseValueSource.Default)
                {
                    if (listView.GetValue(sourceProperty) is Style style)
                    {
                        // override current value
                        viewBase.SetCurrentValue(targetProperty, style);
                    }
                    else
                    {
                        // restore original value
                        var original = viewBase.GetValue(targetProperty);
                        viewBase.SetCurrentValue(targetProperty, original);
                    }
                }
            }
        }
    }
}
