
#region → Usings   .

using System;
using citPOINT.PrefApp.Common.Test.Mocks;
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
    /// Numeric Calculation Test
    /// </summary>
    [TestClass]
    public class NumericCalculationTest
    {
        #region → Methods        .

        #region → Public         .

        #region → Optimal Boundary Type  .

        /// <summary>
        /// Calcs the numeric optimal boundary type value NULL score be zero.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValueNULL_ScoreBeZero()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value Null as expected is Zero");

            #endregion
        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value minus20 score be zero.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValue_Minus20_ScoreBeZero()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("-20.00");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value -20.00 as expected is Zero");

            #endregion

        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value zero score be zero.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValueZero_ScoreBeZero()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("0");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value Zero as expected is Zero");

            #endregion

            //This set to be showen in the pannel of Data matching (Status Label)
            //msgIssue.NumericRate = numericCalculation.CurrentNumericRate;

        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value20 score be80.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValue20_ScoreBe80()
        {
            #region → Arrange .

            decimal Expected = 80;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("20");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value 20 as expected is 80");

            #endregion

            //This set to be showen in the pannel of Data matching (Status Label)
            //msgIssue.NumericRate = numericCalculation.CurrentNumericRate;

        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value25 score be100.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValue25_ScoreBe100()
        {
            #region → Arrange .

            decimal Expected = 100;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("25");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value 25 as expected is 100");

            #endregion
        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value50 score be100.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValue50_ScoreBe100()
        {
            #region → Arrange .

            decimal Expected = 100;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("50");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value 50 as expected is 100");

            #endregion
        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value75 score be100.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValue75_ScoreBe100()
        {
            #region → Arrange .

            decimal Expected = 100;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("75");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value 75 as expected is 100");

            #endregion
        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value90 score be100.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValue90_ScoreBe100()
        {
            #region → Arrange .

            decimal Expected = 40;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("90");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value 90 as expected is 40");

            #endregion
        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value100 score be zero.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValue100_ScoreBeZero()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("100");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value 100 as expected is Zero");

            #endregion
        }

        /// <summary>
        /// Calcs the numeric optimal boundary type value110 score be zero.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_OptimalBoundaryTypeValue110_ScoreBeZero()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 25,
                OptimumValueEnd = 75
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };

            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            #region → Act     .

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("110");

            #endregion

            #region → Assert  .

            Assert.IsTrue(Expected == actual, "Please review Boundary type in case of value 110 as expected is Zero");

            #endregion
        }

        #endregion

        #region → Maximum Up To          .

        /// <summary>
        /// Calcs the numeric up to maximum type better range minus20 to120 score as expected.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_UpToMaximumTypeBetter_RangeMinus20To120_ScoreAsExpected()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 100,
                OptimumValueEnd = 100,
                MaximumOperator = 0 /*better*/,
                MaxOperatorBetter = true
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };



            #endregion

            for (int i = -20; i < 120; i = i + 10)
            {

                #region → Act     .

                if (i < 0)
                {
                    Expected = 0;
                }
                else if (i >= 0 && i <= 100)
                {
                    Expected = i;
                }
                else
                {
                    Expected = 100;
                }

                NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, (i > 100 ? i : 100));

                NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);


                //Calc score for numeric
                decimal actual = numericCalculation.CalcScore(i.ToString());

                #endregion

                #region → Assert  .

                Assert.IsTrue(Expected == actual, "Please review Up To Maximum Type Better type in case of value " + i.ToString() + "+ as expected is " + Expected.ToString());

                #endregion
            }
        }

        /// <summary>
        /// Calcs the numeric up to maximum type better max120 test value110 score expected91_67.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_UpToMaximumTypeBetter_Max120_TestValue110_ScoreExpected91_67()
        {
            #region → Arrange .
                       
            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 100,
                OptimumValueEnd = 100,
                MaximumOperator = 0 /*better*/,
                MaxOperatorBetter = true
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };



            #endregion


                NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 120);

                NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);
            
                //Calc score for numeric
                decimal actual = numericCalculation.CalcScore("110");
                          
                Assert.IsTrue(91.67M == actual, "Please review Up To Maximum Type Better type in case of value 110 as expected is 91.67 ");

        }

        /// <summary>
        /// Calcs the numeric up to maximum type equal range minus20 to120 score as expected.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_UpToMaximumTypeEqual_RangeMinus20To120_ScoreAsExpected()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 100,
                OptimumValueEnd = 100,
                MaximumOperator = 1 /*Equal*/,
                MaxOperatorEqual = true
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };




            #endregion

            for (int i = -20; i < 120; i = i + 10)
            {
                #region → Act     .

                if (i < 0)
                {
                    Expected = 0;
                }
                else if (i >= 0 && i <= 100)
                {
                    Expected = i;
                }
                else
                {
                    Expected = 100;
                }

                NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, (i > 100 ? i : 100));

                NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

                //Calc score for numeric
                decimal actual = numericCalculation.CalcScore(i.ToString());

                #endregion

                #region → Assert  .

                Assert.IsTrue(Expected == actual, "Please review Up To Maximum Type Equal type in case of value " + i.ToString() + "+ as expected is " + Expected.ToString());

                #endregion
            }
        }

        /// <summary>
        /// Calcs the numeric up to maximum type worse range minus20 to120 score as expected.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_UpToMaximumTypeWorse_RangeMinus20To120_ScoreAsExpected()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 100,
                OptimumValueEnd = 100,
                MaximumOperator = 2 /*Worse*/,
                MaxOperatorWorse = true
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };


            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, 0, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            #endregion

            for (int i = -20; i < 120; i = i + 10)
            {

                #region → Act     .

                if (i < 0)
                {
                    Expected = 0;
                }
                else if (i >= 0 && i <= 100)
                {
                    Expected = i;
                }
                else
                {
                    Expected = 0;
                }


                //Calc score for numeric
                decimal actual = numericCalculation.CalcScore(i.ToString());

                #endregion

                #region → Assert  .

                Assert.IsTrue(Expected == actual, "Please review Up To Maximum Type Worse type in case of value " + i.ToString() + "+ as expected is " + Expected.ToString());

                #endregion
            }
        }

        #endregion

        #region → Minimum Up To          .

        /// <summary>
        /// Calcs the numeric up to minimum type better range minus20 to120 score as expected.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_UpToMinimumTypeBetter_RangeMinus20To120_ScoreAsExpected()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 0,
                OptimumValueEnd = 0,
                MinimumOperator = 0 /*better*/,
                MinOperatorBetter = true
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };



            #endregion

            for (int i = 120; i > -20; i = i - 10)
            {
                #region → Act     .

                if (i > 100)
                {
                    Expected = 0;
                }
                else if (i >= 0 && i <= 100)
                {
                    Expected = 100 - i;
                }
                else
                {
                    Expected = 100;
                }

                NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, (i < 0 ? i : 0), 100);

                NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);


                //Calc score for numeric
                decimal actual = numericCalculation.CalcScore(i.ToString());

                #endregion

                #region → Assert  .

                Assert.IsTrue(Expected == actual, "Please review Up To Minimum Type Better type in case of value " + i.ToString() + "+ as expected is " + Expected.ToString());

                #endregion
            }
        }

        /// <summary>
        /// Calcs the numeric up to minimum type equal range minus20 to120 score as expected.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_UpToMinimumTypeEqual_RangeMinus20To120_ScoreAsExpected()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 0,
                OptimumValueEnd = 0,
                MinimumOperator = 1 /*Equal*/,
                MinOperatorEqual = true
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };



            #endregion

            for (int i = 120; i > -20; i = i - 10)
            {

                #region → Act     .

                if (i > 100)
                {
                    Expected = 0;
                }
                else if (i >= 0 && i <= 100)
                {
                    Expected = 100 - i;
                }
                else
                {
                    Expected = 100;
                }

                NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, (i < 0 ? i : 0), 100);

                NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);


                //Calc score for numeric
                decimal actual = numericCalculation.CalcScore(i.ToString());

                #endregion

                #region → Assert  .

                Assert.IsTrue(Expected == actual, "Please review Up To Minimum Type Equal type in case of value " + i.ToString() + "+ as expected is " + Expected.ToString());

                #endregion
            }
        }

        /// <summary>
        /// Calcs the numeric up to minimum type worse range minus20 to120 score as expected.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_UpToMinimumTypeWorse_RangeMinus20To120_ScoreAsExpected()
        {
            #region → Arrange .

            decimal Expected = 0;

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 0,
                OptimumValueEnd = 0,
                MinimumOperator = 2 /*Worse*/,
                MinOperatorWorse = true
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };



            #endregion

            for (int i = 120; i > -20; i = i - 10)
            {

                #region → Act     .


                if (i >= 0 && i <= 100)
                {
                    Expected = 100 - i;
                }
                else
                {
                    Expected = 0;
                }

                NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, (i < 0 ? i : 0), 100);

                NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);


                //Calc score for numeric
                decimal actual = numericCalculation.CalcScore(i.ToString());

                #endregion

                #region → Assert  .

                Assert.IsTrue(Expected == actual, "Please review Up To Minimum Type Equal type in case of value " + i.ToString() + "+ as expected is " + Expected.ToString());

                #endregion
            }
        }

        /// <summary>
        /// Calcs the numeric up to minimum type better min minus10 test value minus20 score expected91_67.
        /// </summary>
        [TestMethod]
        public void CalcNumeric_UpToMinimumTypeBetter_MinMinus10_TestValueMinus20_ScoreExpected91_67()
        {
            #region → Arrange .

            NumericIssue numericIssues = new NumericIssue()
            {
                NumericIssueID = Guid.NewGuid(),
                MinimumValue = 0,
                MaximumValue = 100,
                OptimumValueStart = 0,
                OptimumValueEnd =0,
                MinimumOperator = 0 /*better*/,
                MinOperatorBetter = true
            };


            Issue issue = new Issue()
            {
                IssueWeight = 100
            };



            #endregion
            
            NumericBoundaryMock numericBoundary = new NumericBoundaryMock(numericIssues, -20, 100);

            NumericCalculation numericCalculation = new NumericCalculation(issue.IssueWeight, numericIssues, numericBoundary);

            //Calc score for numeric
            decimal actual = numericCalculation.CalcScore("-10");

            Assert.IsTrue(91.67M == actual, "Please review Up To Maximum Type Better type in case of value -10 as expected is 91.67 ");
                    }

        #endregion

        #endregion

        #endregion
    }
}
