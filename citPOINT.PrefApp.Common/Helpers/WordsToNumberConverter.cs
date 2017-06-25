#region → Usings   .

using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

#region → History  .

#endregion

#region → ToDos    .

#endregion

namespace citPOINT.PrefApp.Common
{

    /// <summary>
    /// Words ToNumber Converter
    /// </summary>
    public class WordsToNumberConverter
    {
        #region → Fields         .
        /// <summary>
        /// String Value
        /// </summary>
        private class StringValue
        {
            #region → Properties     .

            /// <summary>
            /// Gets or sets the number as string.
            /// </summary>
            /// <value>The number as string.</value>
            public string NumberAsString { get; set; }

            /// <summary>
            /// Gets or sets the number as value.
            /// </summary>
            /// <value>The number as value.</value>
            public long NumberAsValue { get; set; }

            #endregion


            #region → Constructors   .
            /// <summary>
            /// Initializes a new instance of the <see cref="StringValue" /> class.
            /// </summary>
            /// <param name="NumberAsString">The number as string.</param>
            /// <param name="NumberAsValue">The number as value.</param>
            public StringValue(string NumberAsString, long NumberAsValue)
            {
                this.NumberAsString = NumberAsString;
                this.NumberAsValue = NumberAsValue;
            }
            #endregion


        }

        private static List<StringValue> Small = new List<StringValue>(){
                                                 new StringValue("zero", 0),
                                                 new StringValue("one", 1),
                                                 new StringValue("two", 2),
                                                 new StringValue("three", 3),
                                                 new StringValue("four", 4),
                                                 new StringValue("five", 5),
                                                 new StringValue("six", 6),
                                                 new StringValue("seven", 7),
                                                 new StringValue("eight", 8),
                                                 new StringValue("nine", 9),
                                                 new StringValue("ten", 10),
                                                 new StringValue("eleven", 11),
                                                 new StringValue("twelve", 12),
                                                 new StringValue("thirteen", 13),
                                                 new StringValue("fourteen", 14),
                                                 new StringValue("fifteen", 15),
                                                 new StringValue("sixteen", 16),
                                                 new StringValue("seventeen", 17),
                                                 new StringValue("eighteen", 18),
                                                 new StringValue("nineteen", 19),
                                                 new StringValue("twenty", 20),
                                                 new StringValue("thirty", 30),
                                                 new StringValue("forty", 40),
                                                 new StringValue("fifty", 50),
                                                 new StringValue("sixty", 60),
                                                 new StringValue("seventy", 70),
                                                 new StringValue("eighty", 80),
                                                 new StringValue("ninety", 90)
};

        private static List<StringValue> Magnitude = new List<StringValue>(){
                                                     new StringValue("thousand",     1000),
                                                     new StringValue("million",      1000000),
                                                     new StringValue("billion",      1000000000),
                                                     new StringValue("trillion",     1000000000000),
                                                     new StringValue("quadrillion",  1000000000000000),
                                                     new StringValue("quintillion",  1000000000000000000)/* ,
                                                     new StringValue("sexillion",    1000000000000000000000),
                                                     new StringValue("septillion",   1000000000000000000000000),
                                                     new StringValue("octillion",    1000000000000000000000000000),
                                                     new StringValue("nonillion",    1000000000000000000000000000000),
                                                     new StringValue("decillion",    1000000000000000000000000000000000)
*/};


        private static List<string> mAllNumbersWords;

        /// <summary>
        /// Gets all numbers words.
        /// </summary>
        /// <value>All numbers words.</value>
        private static List<string> AllNumbersWords
        {
            get
            {
                if (mAllNumbersWords == null)
                {
                    mAllNumbersWords = new List<String>();

                    foreach (var word in Small)
                    {
                        mAllNumbersWords.Add(word.NumberAsString);
                    }


                    foreach (var word in Magnitude)
                    {
                        mAllNumbersWords.Add(word.NumberAsString);
                    }

                    mAllNumbersWords.Add("hundred");

                }

                return mAllNumbersWords;
            }
        }

        /// <summary>
        /// Spell Checker to Correct your Words As Thwee is not three
        /// </summary>
        static SpellChecker spellChecker = new SpellChecker(AllNumbersWords);

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Text 2 number.
        /// </summary>
        /// <param name="NumbersAsString">The numbers as string.</param>
        /// <returns></returns>
        public static double? Text2Number(string NumbersAsString)
        {
            try
            {
                //Split String By any Numer OF Spaces( ),(   ) and by Comma(,) and dash(-).
                Regex regex = new Regex(@"(\s+|,|-|_)");
                string[] NumberAsParts = regex.Split(NumbersAsString);


                double n = 0;
                double g = 0;

                //Indicating that the Converting happen one Tiome at least.
                bool OneTimeHappen = false;


                NumberAsParts = AdjustAndFilterWords(NumberAsParts);

                foreach (var word in NumberAsParts)
                {




                    var SmallNumber = Small.FirstOrDefault<StringValue>(ss => ss.NumberAsString.ToLower() == word.ToLower());


                    if (SmallNumber != null)
                    {
                        g += SmallNumber.NumberAsValue;

                        OneTimeHappen = true;
                    }

                    else if (word.ToLower() == "hundred")
                    {
                        if (g == 0)
                            g = 1; //base for multiplication

                        g *= 100;

                        OneTimeHappen = true;
                    }
                    else
                    {
                        var MagnitudeNumber = Magnitude.FirstOrDefault<StringValue>(ss => ss.NumberAsString.ToLower() == word.ToLower());
                        if (MagnitudeNumber != null)
                        {
                            if (g == 0)
                                g = 1; //base for multiplication

                            n += g * MagnitudeNumber.NumberAsValue;
                            g = 0;

                            OneTimeHappen = true;

                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if (OneTimeHappen)
                {
                    return n + g;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {


            }
            return null;
        }


        private static string[] AdjustAndFilterWords(string[] Words)
        {

            List<string> FilterdWords = new List<string>();


            foreach (var word in Words)
            {
                //in case choice is space or null or , or "and"
                // word then we continue without doing any thing.
                if (string.IsNullOrEmpty(word) ||
                    string.IsNullOrWhiteSpace(word) ||
                    word == "and" ||
                    word.IndexOf(" ") != -1 ||
                    word.IndexOf(",") != -1 ||
                    word.Length < 3)
                {
                    continue;
                }
                else
                {
                    string ReturnWord = spellChecker.Correct(word);
                    if (!string.IsNullOrEmpty(ReturnWord))
                    {
                        FilterdWords.Add(ReturnWord);    
                    }
                    
                }
            }



            return FilterdWords.ToArray();

        }
        #endregion
        #endregion
    }
}
