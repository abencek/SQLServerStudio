
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace SQLServerStudio
{
    /// <summary>
    /// Converts provided control name to specific control
    /// </summary>
    [ValueConversion (typeof(string), typeof(UserControl))]
    internal class ActionTextToControlConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Static instance of this value converter
        /// </summary>
        private static ActionTextToControlConverter _instance;

        /// <summary>
        /// The method converts string type to it's control counterpart
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string)value;

            return text switch
            {
                "View Code" => new ScriptEditorControl(),
                "View Results" => new MultiDataGridControl(),
                _ => null,
            };
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

        /// <summary>
        /// Provides a static instance of this value converter 
        /// </summary>
        /// <param name="serviceProvider">A service provider that can provide services for markup extension</param>
        /// <returns>Instance of <see cref="ActionTextToControlConverter"/></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ??= new ActionTextToControlConverter();
        }
    }
}
