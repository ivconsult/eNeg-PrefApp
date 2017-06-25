#region → Usings   .
using System;
using System.Collections.Generic;

#endregion

#region → History  .

/* Date         User            change
 * 
 * 23.01.11     M.Wahab         * creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Common
{

    #region → Word Weight Pair  .

    /// <summary>
    /// Word Weight Pair
    /// </summary>
    class WordWeightPair : IComparable
    {

        #region → Fields         .

        public int probabillity_weight;
        public string word;

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="WordWeightPair"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="w">The w.</param>
        public WordWeightPair(int p, string w)
        {
            probabillity_weight = p;
            word = w;
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="cl">The cl.</param>
        /// <returns></returns>
        public int CompareTo(object cl)
        {
            if (typeof(WordWeightPair) == cl.GetType())
            {
                return this.probabillity_weight - ((WordWeightPair)cl).probabillity_weight;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #endregion

    }

    #endregion


    /// <summary>
    /// spell Checker Class
    /// </summary>
    public class SpellChecker
    {
        #region → Fields         .


        private Dictionary<string, int> nWords = new Dictionary<string, int>();
        private List<char> alphabets;


        private bool CheckByDeepWay = false;

        #endregion

        #region → Contructor     .

        /// <summary>
        /// Initializes a new instance of the <see cref="spellChecker"/> class.
        /// </summary>
        /// <param name="dict">The dict.</param>
        public SpellChecker(List<String> dict)
        {
            foreach (var item in dict)
            {
                nWords.Add(item.ToLower(), 0);
            }

            Buildalphabet();
        }

        #endregion

        #region → Methods        .


        #region → Private        .


        /// <summary>
        /// Buildalphabets this instance.
        /// </summary>
        private void Buildalphabet()
        {
            alphabets = new List<char>();
            for (char c = 'a'; c <= 'z'; c++)
            {
                alphabets.Add(c);
            }
        }

        /// <summary>
        /// Editses the specified word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        private List<string> Edits(string word)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < word.Length; ++i)
            {
                /*Delete*/
                result.Add(word.Substring(0, i) + word.Substring(i + 1));
            }
            for (int i = 0; i < word.Length - 1; ++i)
            {
                /*transpose*/
                result.Add(word.Substring(0, i) + word.Substring(i + 1, 1) + word.Substring(i, 1) + word.Substring(i + 2));
            }
            for (int i = 0; i < word.Length; ++i)
            {
                /*alter*/
                foreach (char c in alphabets)
                {
                    result.Add(word.Substring(0, i) + c + word.Substring(i + 1));
                }
            }
            for (int i = 0; i <= word.Length; ++i)
            {
                /*insert*/
                foreach (char c in alphabets)
                {
                    result.Add(word.Substring(0, i) + c + word.Substring(i));
                }
            }
            return result;
        }


        #endregion

        #region → Public         .


        /// <summary>
        /// Corrects the specified word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public string Correct(string word)
        {
            word = word.ToLower();

            if (nWords.ContainsKey(word))
            {
                return word;
            }
            List<string> list = Edits(word);

            List<WordWeightPair> candidates = new List<WordWeightPair>();

            foreach (string s in list)
            {
                if (nWords.ContainsKey(s))
                {
                    candidates.Add(new WordWeightPair(nWords[s], s));
                }
            }
            if (candidates.Count > 0)
            {
                candidates.Sort();
                return candidates[0].word;
            }

            if (CheckByDeepWay)
            {
                foreach (string s in list)
                {
                    List<string> list2 = Edits(s);/*second level of edits*/

                    foreach (string ss in list2)
                    {
                        if (nWords.ContainsKey(ss))
                        {
                            candidates.Add(new WordWeightPair(nWords[ss], ss));
                        }
                    }

                }
                if (candidates.Count > 0)
                {
                    candidates.Sort();
                    return candidates[0].word;
                }
            }
            return null;
        }

        #endregion
        #endregion

    }
}
