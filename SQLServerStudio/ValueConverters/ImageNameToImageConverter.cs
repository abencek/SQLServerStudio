using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SQLServerStudio
{
    /// <summary>
    /// Converts provided image name to bitmap object
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class ImageNameToImageConverter : IValueConverter
    {
        /// <summary>
        /// Provides a static instance of this value converter
        /// </summary>
        public static ImageNameToImageConverter Instance = new();

        /// <summary>
        /// The method converts image name to bitmap object
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Instance of <see cref="BitmapImage"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/Images/{value}.png"));
        }

        /// <summary>
        /// The method converts a value back to it's source type
        /// </summary>
        /// <returns>Instance of <see cref="NotImplementedException"/></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
