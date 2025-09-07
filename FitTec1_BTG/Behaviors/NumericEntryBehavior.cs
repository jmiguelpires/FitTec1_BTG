using Microsoft.Maui.Controls;

namespace FitTec1_BTG.Behaviors
{
    public class NumericEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindableEntry)
        {
            bindableEntry.TextChanged += OnTextChanged;
            base.OnAttachedTo(bindableEntry);
        }

        protected override void OnDetachingFrom(Entry bindableEntry)
        {
            bindableEntry.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(bindableEntry);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (!string.IsNullOrEmpty(entry.Text))
            {
                string apenasNumeros = new string(entry.Text.Where(char.IsDigit).ToArray());
                if (entry.Text != apenasNumeros)
                    entry.Text = apenasNumeros;
            }
        }
    }
}
