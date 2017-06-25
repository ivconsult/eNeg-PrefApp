#region → Usings   .


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using citPOINT.PrefApp.Data;
#endregion

#region → History  .

#endregion

#region → ToDos    .
#endregion


namespace citPOINT.PrefApp.Common
{

    /// <summary>
    /// eNeg RichTextBox Controls
    /// </summary>
    public partial class eNegRichTextBox : UserControl
    {
        #region → Fields         .

        bool HideLabel = false;

        private IEnumerable<string> Numbers = new List<string>() { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety", "hundred", "thousand", "million", "billion" };

        #region Dependency Property


        /// <summary>
        ///Using a DependencyProperty as the backing store for FormatedWords.  This enables animation, styling, binding, etc... 
        /// </summary>
        public static readonly DependencyProperty FormatedWordsProperty =
            DependencyProperty.Register("FormatedWords", typeof(List<FormatedWord>), typeof(eNegRichTextBox), new PropertyMetadata(new PropertyChangedCallback(OnChangingFormatedWords)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for TextValue.  
        /// This enables animation, styling, binding, etc... 
        /// </summary>
        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(eNegRichTextBox), new PropertyMetadata(new PropertyChangedCallback(OnChangingTextValue)));
        #endregion

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the text value.
        /// </summary>
        /// <value>The text value.</value>
        public string TextValue
        {
            get
            {
                return (string)GetValue(TextValueProperty);
            }

            set
            {
                SetValue(TextValueProperty, value);
                BuildRichTextBox();
            }
        }

        /// <summary>
        /// Gets or sets the formated words.
        /// </summary>
        /// <value>The formated words.</value>
        public List<FormatedWord> FormatedWords
        {
            get
            {
                return ((List<FormatedWord>)GetValue(FormatedWordsProperty)) == null ? new List<FormatedWord>() : (List<FormatedWord>)GetValue(FormatedWordsProperty);
            }

            set
            {
                SetValue(FormatedWordsProperty, value);
                BuildRichTextBox();
            }
        }


        /// <summary>
        /// Gets the main stack panel.
        /// </summary>
        /// <value>The main stack panel.</value>
        public Panel MainStackPanel
        {
            get
            {
                return uxMainStackPanel as Panel;
            }
        }


        /// <summary>
        /// Gets the drag label.
        /// </summary>
        /// <value>The drag label.</value>
        public TextBlock DragLabel
        {
            get
            {
                return uxLblUpper;
            }
        }


        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegRichTextBox" /> class.
        /// </summary>
        public eNegRichTextBox()
        {
            InitializeComponent();
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the uxLblUpper control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs" /> instance containing the event data.</param>
        private void uxLblUpper_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(uxLblUpper.Text) && !string.IsNullOrWhiteSpace(uxLblUpper.Text))
            {
                uxLblUpper.Visibility = HideLabel ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
                HideLabel = !HideLabel;
            }
            else
            {
                HideLabel = false;
                uxLblUpper.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        /// <summary>
        /// Handles the SelectionChanged event of the uxtxtRichTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void uxtxtRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            uxLblUpper.Text = uxtxtRichTextBox.Selection.Text;
            if (!string.IsNullOrEmpty(uxLblUpper.Text) && !string.IsNullOrWhiteSpace(uxLblUpper.Text))
                uxLblUpper.Visibility = System.Windows.Visibility.Visible;
            else
                uxLblUpper.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Called when [changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChangingTextValue(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as eNegRichTextBox).TextValue = string.IsNullOrEmpty(e.NewValue as string) ? string.Empty : e.NewValue.ToString();

        }

        /// <summary>
        /// Called when [changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChangingFormatedWords(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                (sender as eNegRichTextBox).FormatedWords = e.NewValue as List<FormatedWord>;
            }
            else
            {
                (sender as eNegRichTextBox).FormatedWords = new List<FormatedWord>();
            }

        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Builds the rich text box.
        /// </summary>
        private void BuildRichTextBox()
        {
            //Clear All Text
            uxtxtRichTextBox.Blocks.Clear();

            if (!string.IsNullOrEmpty(TextValue))
            {
                //Split the text Into Lines
                foreach (var line in TextValue.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line != null)
                    {
                        Paragraph para = new Paragraph();

                        //Get line as words Depend on some marks like space,dot, Comma,and So on..
                        var xx = LineDetails(line);

                        foreach (var Word in xx)
                        {
                            para.Inlines.Add(FormatWord(Word));
                        }

                        uxtxtRichTextBox.Blocks.Add(para);
                    }
                }
            }

            ResetAll();




        }

        /// <summary>
        /// Clears the selection.
        /// </summary>
        private void ResetAll()
        {

            //Create two arbitrary TextPointers to specify the range of content to select.
            TextPointer MyTP1 = uxtxtRichTextBox.ContentStart.GetPositionAtOffset(0, LogicalDirection.Forward);

            //Programmatically change the selection in the RichTextBox
            uxtxtRichTextBox.Selection.Select(MyTP1, MyTP1);

            //Focus
            uxtxtRichTextBox.Focus();

        }

        /// <summary>
        /// Lines the details.
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <returns></returns>
        private List<string> LineDetails(string Line)
        {
            List<string> _LineDetails = new List<string>();

            //\s mean Space
            Regex regex = new Regex(@"(\s|,|\.|:|-|;|_|\(|\)|\/)");
            foreach (string sub in regex.Split(Line))
            {
                if (!string.IsNullOrEmpty(sub))
                    _LineDetails.Add(sub);
            }

            return _LineDetails;
        }

        /// <summary>
        /// Formats the word.
        /// </summary>
        /// <param name="Word">The word.</param>
        /// <returns>new format</returns>
        private Run FormatWord(string Word)
        {
            double outValue = 0;


            if (string.IsNullOrEmpty(Word))
            {
                return new Run();
            }
            else if (string.IsNullOrWhiteSpace(Word))
            {
                return GetRun(Word, 12, new SolidColorBrush(System.Windows.Media.Colors.Blue), FontWeights.Bold);
            }
            else if (double.TryParse(Word, out outValue) || Numbers.Count(s => s.ToString().ToLower() == Word.ToLower()) > 0)
            {
                return GetRun(Word, 12, new SolidColorBrush(System.Windows.Media.Colors.Blue), FontWeights.Bold);
            }
            else
            {

                FormatedWord _world = GetFormatedWorld(Word);
                if (_world != null)
                {
                    return GetRun(Word, 12, new SolidColorBrush(_world.Foreground), _world.FontWeight);
                }
                else
                {
                    return GetRun(Word, 12, new SolidColorBrush(System.Windows.Media.Colors.Black), FontWeights.Normal);
                }
            }

        }

        /// <summary>
        /// Adds the run.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="forgroundColor">Color of the forground.</param>
        /// <param name="fontWeights">The font weights.</param>
        /// <returns></returns>
        Run GetRun(string Text, double fontSize, Brush forgroundColor, FontWeight fontWeights)
        {
            System.Windows.Documents.Run R = new System.Windows.Documents.Run();
            R.Text = Text;
            R.FontSize = fontSize;
            R.FontWeight = fontWeights;
            R.Foreground = forgroundColor;
            return R;
        }

        /// <summary>
        /// Gets the formated world.
        /// </summary>
        /// <param name="World">The world.</param>
        /// <returns></returns>
        FormatedWord GetFormatedWorld(string World)
        {
            foreach (var _world in FormatedWords)
            {

                if (RemoveSpace(_world.Word).ToLower() == RemoveSpace(World).ToLower())
                {
                    return _world;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes the space.
        /// </summary>
        /// <param name="Word">The word.</param>
        /// <returns></returns>
        private string RemoveSpace(string Word)
        {
            while (Word.IndexOf(" ") > -1)
            {
                Word = Word.Replace(" ", "");
            }

            return Word;
        }

        #endregion

        #endregion
    }



}
