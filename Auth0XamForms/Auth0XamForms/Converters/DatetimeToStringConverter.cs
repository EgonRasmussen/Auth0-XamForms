using System;
using System.Globalization;
using Xamarin.Forms;

namespace Auth0XamForms.Converters;
public class DatetimeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null)
            return string.Empty;

        var datetime = (DateTime)value;

        return datetime.ToLocalTime().ToString(new CultureInfo("da-DK"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
