using System;
using System.ComponentModel;
using System.Windows;

namespace BootMega.Theme.Core
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public abstract class RangeTextBase<T> : RangeBase<T> 
        where T: struct, IComparable<T>
    {
        #region dependency properties

        #region public bool AsHex

        public bool AsHex
        {
            get { return (bool)GetValue(AsHexProperty); }
            set { SetValue(AsHexProperty, value); }
        }

        public static readonly DependencyProperty AsHexProperty =
            DependencyProperty.Register("AsHex", typeof(bool), typeof(RangeTextBase<T>), new PropertyMetadata(false, OnAsHexChanged));

        private static void OnAsHexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeTextBase<T> rangeTextBoxBase = (RangeTextBase<T>)d;
            rangeTextBoxBase.OnAsHexChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnAsHexChanged(bool oldValue, bool newValue)
        {

        }

        #endregion

        #region public bool IsReadOnly

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(RangeTextBase<T>), new PropertyMetadata(false, OnIsReadOnlyChanged));

        private static void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeTextBase<T> rangeTextBoxBase = (RangeTextBase<T>)d;
            rangeTextBoxBase.OnAsHexChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsReadOnlyChanged(bool oldValue, bool newValue)
        {

        }

        #endregion

        #endregion
    } 
}
