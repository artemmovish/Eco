using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Converters
{
    public class TransactionTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString()?.ToLower() == "income" ? "↑" : "↓";

            if (value is decimal amount)
            {
                // Positive amount = income (green), negative = expense (red)
                return amount >= 0 ? "↑" : "↓";
            }

            // Default color if value isn't decimal
            return Color.FromArgb("#000000");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
