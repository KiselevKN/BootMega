using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BootMega.Theme.Core
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public abstract class RangeBase<T> : Control 
        where T: struct, IComparable<T>
    {
        #region dependency properties

        #region public T Minimum

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(T), typeof(RangeBase<T>),
            new FrameworkPropertyMetadata((object)default(T), new PropertyChangedCallback(RangeBase<T>.OnMinimumChanged)));

        [Category("Behavior")]
        [Bindable(true)]
        public T Minimum
        {
            get
            {
                return (T)GetValue(RangeBase<T>.MinimumProperty);
            }
            set
            {
                SetValue(RangeBase<T>.MinimumProperty, value);
            }
        }

        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBase<T> rangeBase = (RangeBase<T>)d;
            rangeBase.CoerceValue(RangeBase<T>.MaximumProperty);
            rangeBase.CoerceValue(RangeBase<T>.ValueProperty);
            rangeBase.OnMinimumChanged((T)e.OldValue, (T)e.NewValue);
        }

        protected virtual void OnMinimumChanged(T oldMinimum, T newMinimum)
        {

        }

        #endregion

        #region public T Maximumy

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(T), typeof(RangeBase<T>),
            new FrameworkPropertyMetadata((object)default(T), new PropertyChangedCallback(RangeBase<T>.OnMaximumChanged),
                new CoerceValueCallback(RangeBase<T>.CoerceMaximum)));

        [Category("Behavior")]
        [Bindable(true)]
        public T Maximum
        {
            get
            {
                return (T)GetValue(RangeBase<T>.MaximumProperty);
            }
            set
            {
                SetValue(RangeBase<T>.MaximumProperty, value);
            }
        }

        private static object CoerceMaximum(DependencyObject d, object value)
        {
            T minimum = ((RangeBase<T>)d).Minimum;
            if (((T)value).CompareTo(minimum) < 0)
                return minimum;
            else
                return value;
        }

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBase<T> rangeBase = (RangeBase<T>)d;
            rangeBase.CoerceValue(RangeBase<T>.ValueProperty);
            rangeBase.OnMaximumChanged((T)e.OldValue, (T)e.NewValue);
        }

        protected virtual void OnMaximumChanged(T oldMaximum, T newMaximum)
        {

        }

        #endregion

        #region public T Value

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<T>), typeof(RangeBase<T>));
        
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(T), typeof(RangeBase<T>),
            new FrameworkPropertyMetadata((object)default(T),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                new PropertyChangedCallback(RangeBase<T>.OnValueChanged), new CoerceValueCallback(RangeBase<T>.ConstrainToRange)));

        [Category("Behavior")]
        [Bindable(true)]
        public T Value
        {
            get
            {
                return (T)GetValue(RangeBase<T>.ValueProperty);
            }
            set
            {
                SetValue(RangeBase<T>.ValueProperty, value);
            }
        }

        [Category("Behavior")]
        public event RoutedPropertyChangedEventHandler<T> ValueChanged
        {
            add
            {
                AddHandler(RangeBase<T>.ValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(RangeBase<T>.ValueChangedEvent, value);
            }
        }

        internal static object ConstrainToRange(DependencyObject d, object value)
        {
            RangeBase<T> rangeBase = (RangeBase<T>)d;
            T minimum = rangeBase.Minimum;
            T num = (T)value;
            if (num.CompareTo(minimum) < 0)
                return minimum;
            T maximum = rangeBase.Maximum;
            if (num.CompareTo(maximum) > 0)
                return maximum;
            else
                return value;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeBase<T> rangeBase = (RangeBase<T>)d;
            rangeBase.OnValueChanged((T)e.OldValue, (T)e.NewValue);
        }

        protected virtual void OnValueChanged(T oldValue, T newValue)
        {
            RoutedPropertyChangedEventArgs<T> changedEventArgs = new RoutedPropertyChangedEventArgs<T>(oldValue, newValue);
            changedEventArgs.RoutedEvent = RangeBase<T>.ValueChangedEvent;
            RaiseEvent(changedEventArgs);
        }

        #endregion

        #endregion
    } 
}
