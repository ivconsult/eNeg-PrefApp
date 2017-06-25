
#region → Usings   .

using System;
using System.Collections.Generic;
using System.Linq;
using citPOINT.PrefApp.Data.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18/07/2011   mwahab         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.PrefApp.Common.Test
{
    /// <summary>
    /// Options Calculation Test
    /// </summary>
    [TestClass]
    public class OptionsCalculation_Test
    {

        #region → Methods        .

        #region → Public         .

        #region → Optimal Boundary Type  .

        /// <summary>
        /// Calcs the options green only score15.
        /// </summary>
        [TestMethod]
        public void CalcOptions_GreenOnly_Score15()
        {

            #region → Arrange .

            decimal Expected = 15;

            CalculationSamlpesMock calcMock = new CalculationSamlpesMock();

            OptionsCalculation optionsCalculation = new OptionsCalculation();

            //Message Options has Colour Green Only which is 30 % of issue
            IEnumerable<MessageOptionIssue> messageOptionIssue = calcMock.MessageOptionIssueSource
                                                                .Where(ss => ss.MessageIssueID == Guid.Parse("F49429D9-46C2-4B5C-A59B-D0B031B7EAEF"));

            #endregion

            #region → Act     .

            //Calc score for options
            decimal actual = optionsCalculation.CalcOptionScore(50, calcMock.OptionIssueSource, messageOptionIssue);

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Calculation options values when selecting one color green expect 15");

            #endregion
        }

        /// <summary>
        /// Calcs the options all colours score50.
        /// </summary>
        [TestMethod]
        public void CalcOptions_AllColours_Score50()
        {

            #region → Arrange .

            decimal Expected = 50;

            CalculationSamlpesMock calcMock = new CalculationSamlpesMock();

            OptionsCalculation optionsCalculation = new OptionsCalculation();

            //Message Options has Colour Green Only which is 30 % of issue
            IEnumerable<MessageOptionIssue> messageOptionIssue = calcMock.MessageOptionIssueSource
                                                                .Where(ss => ss.MessageIssueID == Guid.Parse("88999203-F113-486E-98E1-9267C91EE03B"));

            #endregion

            #region → Act     .

            //Calc score for options
            decimal actual = optionsCalculation.CalcOptionScore(50, calcMock.OptionIssueSource, messageOptionIssue);

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Calculation options values when selecting All color Red,Blue,Green expect 50");

            #endregion
        }

        #endregion
        
        #endregion

        #endregion
    }
}
