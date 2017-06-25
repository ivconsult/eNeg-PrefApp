#region → Usings   .

using System.Windows;
using System.Windows.Media;
#endregion

#region → History  .

#endregion

#region → ToDos    .

#endregion

namespace citPOINT.PrefApp.Data
{
    /// <summary>
    /// Formated Word
    /// </summary>
    public class FormatedWord
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets the word.
        /// </summary>
        /// <value>The word.</value>
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets the color of the forground.
        /// </summary>
        /// <value>The color of the forground.</value>
        public Color Foreground { get; set; }

        /// <summary>
        /// Gets or sets the font weight.
        /// </summary>
        /// <value>The font weight.</value>
        public FontWeight FontWeight { get; set; }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatedWord"/> class.
        /// </summary>
        public FormatedWord()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatedWord"/> class.
        /// </summary>
        /// <param name="Word">The word.</param>
        /// <param name="ForgroundColor">Color of the forground.</param>
        /// <param name="FontWeight">The font weight.</param>
        public FormatedWord(string Word, Color ForgroundColor, FontWeight FontWeight)
        {
            this.Word = Word;
            this.Foreground = ForgroundColor;
            this.FontWeight = FontWeight;
        }
        
        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Word.ToString();
        }

        #endregion

        #endregion

    }
}