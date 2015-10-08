using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BootMega.Theme.Core;

namespace BootMega.Theme
{
    [TemplatePart(Name = "PART_Text", Type = typeof(TextBox))]
    public class AddressTextBox : RangeTextBase<ulong>
    {
        #region fields

        private TextBox textBox;

        #endregion

        #region ctors

        static AddressTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AddressTextBox),
                new FrameworkPropertyMetadata(typeof(AddressTextBox)));
        }

        #endregion

        #region overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            textBox = GetTemplateChild("PART_Text") as TextBox;

            if(textBox != null)
            {
                textBox.KeyDown += textBox_KeyDown;
                textBox.PreviewTextInput += textBox_PreviewTextInput;
                textBox.LostFocus += textBox_LostFocus;    
            }

            UpdateMaxLength();
            UpdateTextBox();
        }

        protected override void OnMaximumChanged(ulong oldMaximum, ulong newMaximum)
        {
            UpdateMaxLength();
            UpdateTextBox();
        }

        protected override void OnValueChanged(ulong oldValue, ulong newValue)
        {
            Value = (Value / 0x10) * 0x10;
            UpdateTextBox();
        }

        protected override void OnAsHexChanged(bool oldValue, bool newValue)
        {
            UpdateMaxLength();
            UpdateTextBox();
        }

        #endregion

        #region methods

        void textBox_LostFocus(object sender, RoutedEventArgs e)
        {            
            try
            {
                ulong value = Convert.ToUInt64(textBox.Text, AsHex == true ? 16 : 10);

                if (value >= Maximum)
                    value = Maximum;

                if (value <= Minimum)
                    value = Minimum;

                Value = value;
            }
            catch { }
            finally
            {
                UpdateTextBox();
            }
        }

        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789abcdefABCDEF".IndexOf(e.Text) < 0;
        }

        void textBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                TextBox tb = (TextBox)sender;
                tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void UpdateMaxLength()
        {
            if (textBox != null)
            {
                textBox.MaxLength = (AsHex == true) ? Maximum.ToString("X").Length : Maximum.ToString().Length;
            }
        }

        private void UpdateTextBox()
        {
            if (textBox != null)
                textBox.Text = (AsHex == true) ? Value.ToString("X" + textBox.MaxLength) : Value.ToString();
        }

        #endregion
    }
}
