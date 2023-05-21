using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLServerStudio
{
    /// <summary>
    /// Interaction logic for ScriptEditorControl.xaml
    /// </summary>
    public partial class ScriptEditorControl : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// Plain text context for RichTextBox control
        /// </summary>
        public string TextContent
        {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }

        // DependencyProperty is backing store for TextContent. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextContentProperty =
            DependencyProperty.Register("TextContent", typeof(string), typeof(ScriptEditorControl), new PropertyMetadata(String.Empty, new PropertyChangedCallback(OnTextContentChange)));

        #endregion

        #region Private members

        /// <summary>
        /// SQL reserved words with formatting colors
        /// </summary>
        private static readonly Dictionary<Brush, string> _sqlWords = new()
            {
                { Brushes.Blue, "select|from|where|order|by|group|having|insert|into|drop|table|drop|create|add|column" },
                { Brushes.Gray, "inner|left|right|join|outer" },
                { Brushes.Magenta, "count|sum|avg|cast|convert" }
            };

        #endregion

        #region Default Constructor

        public ScriptEditorControl()
        {
            InitializeComponent();
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Callback function for TextContent dependency property change
        /// </summary>
        /// <param name="d">Instance of <see cref="DependencyObject"/></param>
        /// <param name="e">Instance of <see cref="DependencyPropertyChangedEventArgs"/></param>
        private static void OnTextContentChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Get new text
            string text = (string)e.NewValue;

            if (text is not null)
            {
                var control = d as ScriptEditorControl;
                //Apply formatting and create new FlowDocument for RichTextBox control
                control.RichText.Document = FormatText(text);
            }
        }


        /// <summary>
        /// Change color for all comments and SQL reserved words like SQL server management studio
        /// </summary>
        /// <param name="text">Plain SQL script</param>
        /// <returns>Instance of <see cref="FlowDocument"/></returns>
        private static FlowDocument FormatText(string text)
        {
            var paragraph = new Paragraph();

            var words = _sqlWords.Select(x => x.Value).Aggregate((accumulate, source) => accumulate + "|" + source);

            //Create regular expression pattern with two blocks wrapped in parenteshis
            //Capturing parentheses are used to include captured text in the resulting array
            //1.Comment (starts with -- and goes until the end of a line), 2. SQL words 
            var pattern = @"("+ @"-{2}.+[\r\n]+|" + words + @")";

            //Split text
            var split = Regex.Split(text, pattern, RegexOptions.IgnoreCase);
            //Remove elements with empty string added during split
            var splitNotEmpty = split.Where(x => x != String.Empty);

            foreach (string item in splitNotEmpty)
            {
                //Text is comment
                if (item.StartsWith("--"))
                {
                    var run = CreateRun(item, Brushes.Green);
                    paragraph.Inlines.Add(run);
                }
                //Other text
                else
                {
                    //Get Brush for reserved words
                    var brush = _sqlWords
                        .Where(x => x.Value.Contains(item, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault().Key;
                    var run = CreateRun(item, brush);

                    paragraph.Inlines.Add(run);
                }
            }

            //Return new FolowDocument
            return CreateFlowDocument(paragraph);
        }

        /// <summary>
        /// Create new Run element for Paragraph element
        /// </summary>
        /// <param name="text">Plain text</param>
        /// <param name="foreground">Text foreground color</param>
        /// <returns>Instance of <see cref="Run"/></returns>
        private static Run CreateRun(string text, Brush foreground)
        {
            var run = new Run(text);
            if (foreground is not null)
                run.Foreground = foreground;
            return run;
        }

        /// <summary>
        /// Create new FlowDocument element for RichTextBox control document property
        /// </summary>
        /// <param name="paragraph">Paragraph element</param>
        /// <returns>Instance of <see cref="FlowDocument"/></returns>
        private static FlowDocument CreateFlowDocument(Paragraph paragraph)
        {
            var doc = new FlowDocument();
            doc.Blocks.Add(paragraph);
            return doc;
        }

        #endregion

    }
}
