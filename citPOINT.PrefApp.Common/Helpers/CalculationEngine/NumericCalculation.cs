

#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using citPOINT.PrefApp.Data.Web;


#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 06/06/2011   mwahab         • creation
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

namespace citPOINT.PrefApp.Common
{
    
    /// <summary>
    /// Class for Numeric Calculation 
    /// </summary>
     public class NumericCalculation
    {
        #region → Fields         .

        private INumericIssue mCurrentNumericIssue;
        private INumericBoundary mNumericBoundaries;
        private decimal mRate;
        private decimal mIssueWeight;
                
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the top minimum value.
        /// </summary>
        /// <value>The top minimum value.</value>
        public decimal TopMinimumValue
        {
            get { return NumericBoundaries.TopMinimumValue; }

        }

        /// <summary>
        /// Gets or sets the top maximum value.
        /// </summary>
        /// <value>The top maximum value.</value>
        public decimal TopMaximumValue
        {
            get { return NumericBoundaries.TopMaximumValue; }
        }

        /// <summary>
        /// Gets or sets the numeric boundaries.
        /// </summary>
        /// <value>The numeric boundaries.</value>
        public INumericBoundary NumericBoundaries
        {
            get { return mNumericBoundaries; }
            set { mNumericBoundaries = value; }
        }

        /// <summary>
        /// Gets or sets the current numeric issue.
        /// </summary>
        /// <value>The current numeric issue.</value>
        public INumericIssue CurrentNumericIssue
        {
            get { return mCurrentNumericIssue; }
            set { mCurrentNumericIssue = value; }
        }
              
        /// <summary>
        /// Gets or sets the current numeric rate.
        /// </summary>
        /// <value>The current numeric rate.</value>
        public decimal Rate
        {
            get
            {
                return mRate;
            }
            set
            {
                mRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the issue weight.
        /// </summary>
        /// <value>The issue weight.</value>
        private decimal IssueWeight
        {
            get { return mIssueWeight; }
            set { mIssueWeight = value; }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericCalculation"/> class.
        /// </summary>
        /// <param name="issueWeight">The issue weight.</param>
        /// <param name="NumericIssue">The numeric issue.</param>
        /// <param name="numericBoundaries">The numeric boundaries.</param>
        public NumericCalculation(Decimal issueWeight, INumericIssue NumericIssue, INumericBoundary numericBoundaries)
        {
            this.IssueWeight = issueWeight;
            this.CurrentNumericIssue = NumericIssue;
            this.NumericBoundaries = numericBoundaries;
        }

        #endregion

        #region → Methods        .

        #region → Private        .
        
        #endregion

        #region → Protected      .

        #endregion

        #region → Public         .

        /// <summary>
        /// Calculates the numric score.
        /// </summary>
        /// <param name="messageOffervalue">The message offervalue.</param>
        /// <returns>the actual score as decimal value from 0 to 100%</returns>
        public decimal CalcScore(string messageOffervalue)
        {


            decimal NumericRate = 0;
            decimal Xvalue = 0;


            //Checking If Value Is Null or No numeric Details
            if (string.IsNullOrEmpty(messageOffervalue) || CurrentNumericIssue == null)
            {
                Rate = 0;
                return  0;

            }

            //CurrentNumericIssue = CurrentNumericIssue;

            Xvalue = decimal.Parse(messageOffervalue);


            #region → Optimum = MinimumValue .

            if (CurrentNumericIssue.OptimumValueStart == CurrentNumericIssue.MinimumValue)
            {
                #region → Good Example (Comments)                      .

                /*
                 * Suppose We Have the Following arguments
                 * 
                 * Min=>0
                 * Max=>100
                 * 
                 * Opt.Start=0
                 * Opt.End  =50
                 * 
                 * 
                 * =>New Min Value=-10
                 * 
                 *    (TotalRangeofPoints)
                 *    
                 *=> First Step we Calc the Total Lenght of the X Axis as it will be 110 ??
                 *   this calculated by this equation MaximumValue - New Minimum Value
                 *                                    100          - -10=100 +10 =110              
                 * 
                 * =>Second Step Find the new Value in Y Axis for the Minimum Value (e.g here is (0))
                 *   this calculated as the Following 
                 *   =in normal Curve it will be (Last Point/total range or New Point) but here we have the reverse of Curve 
                 *   =so we make the same but we reverse the Curve by subtracting from 1
                 *   =so the Final Equation will be (1-(Diff New point To Min / Total Range of Points))*100
                 *   =e.g                          =(1-( (Min. - New Min.   ) / 110                  ))*100
                 *                                 =(1-( (0    - -10        ) / 110                  ))*100
                 *                                 =(1-( (10                ) / 110                  ))*100
                 *                                 =(1-( 0.9090                                      ))*100                                 
                 *                                 =(0.9090                                          ))*100
                 *                                 =90.90
                 *                                 
                 * before New minimum Value the X=0  Y=100
                 * After New Min (-10)Value     X=0  Y=90.90
                 * 
                 * 
                 * thanks.
                 */

                #endregion

                #region → Defining Some Important Variables            .

                //First Step we suppose no minimum less than the Actual Minimum.(Intial Value)
                decimal topMinimum = CurrentNumericIssue.MinimumValue;

                //the Lenght of X Axis all lenght from the New Minimum value to the Maximum Value.
                decimal TotalRangeofPoints = 0;

                //the Distance between the Minimum Value of the graph and the New Minimum Value.
                decimal DiffNewpointToMin = 0;

                //the Max of Y range or Score in normal case.but after new Minimum Value appear it will be reduce
                //as the last example  90.9 %
                decimal PCT_BaseValue = 100;

                #endregion

                #region → Calculation related To Better Case           .

                //in case of you choose better Than minimum
                if (CurrentNumericIssue.MinOperatorBetter)
                {
                    //Get The top Minimum value of all messages that for that numeric issue.
                    topMinimum = this.TopMinimumValue;

                    //Get the Range of all X Axis  100           - -10   =110
                    TotalRangeofPoints = CurrentNumericIssue.MaximumValue - topMinimum;

                    //Get Distance between the Minimum Value of the graph and the New Minimum Value.
                    //      10        = 0                        - -10
                    DiffNewpointToMin = CurrentNumericIssue.MinimumValue - topMinimum;

                    //Incase the New Minimum value are less than the Minimum Value of the Actual Graph
                    if (topMinimum < CurrentNumericIssue.MinimumValue)
                    {
                        //Calc the New Y Axis For Minimum X Value of the Actual Graph.
                        //90.90       =          100 * (         1.00 - (10                / 110               ))
                        PCT_BaseValue = (decimal)100 * ((decimal)1.00 - (DiffNewpointToMin / TotalRangeofPoints));

                        //Min level of PCT not more than 100 %  =>90.90
                        PCT_BaseValue = Math.Round(PCT_BaseValue, 2);
                    }
                }

                #endregion

                #region → Xvalue Between Optimum.End and Maximum       .

                //Xvalue Between Optimum.End and Maximum
                //e.g             50         and 100 
                if (Xvalue > CurrentNumericIssue.OptimumValueEnd && Xvalue <= CurrentNumericIssue.MaximumValue)
                {
                    NumericRate = PCT_BaseValue - FunY(Xvalue, CurrentNumericIssue.OptimumValueEnd, CurrentNumericIssue.MaximumValue, PCT_BaseValue);
                }

                #endregion

                #region → Xvalue Between Optimum.Start and Optimum.End .

                //(=>In Range Graph)
                //Xvalue Between Optimum.Start and Optimum.End
                //e.g             0            and 100
                else if (Xvalue >= CurrentNumericIssue.OptimumValueStart && Xvalue <= CurrentNumericIssue.OptimumValueEnd)
                {
                    NumericRate = PCT_BaseValue;
                }

                #endregion

                #region → Xvalue Is Greater than Maximum               .

                //Xvalue Is Greater than Maximum so it will Be Zero
                else if (Xvalue > CurrentNumericIssue.MaximumValue)
                {
                    NumericRate = 0;
                }

                #endregion

                #region → Xvalue Is Less than Minimum                  .

                //Xvalue Is Less than Minimum (So its Good in case of better or Equal)
                else if (Xvalue < CurrentNumericIssue.MinimumValue)
                {
                    //In case you choice Worse Option so any value in X Axis will be Zero
                    if (CurrentNumericIssue.MinOperatorWorse)
                        NumericRate = 0;

                        //In case you choice Equal Option so any value in X Axis will be 100 %
                    else if (CurrentNumericIssue.MinOperatorEqual)
                        NumericRate = 100;

                    //In case you choice Better Option.
                    else if (CurrentNumericIssue.MinOperatorBetter)
                    {
                        #region → Commnets .

                        //Distance Between Xvalue and Maximum to Get the Lenght of the Graph to Xvalue
                        //e.g.  New Minimum -10 and Min=0 And Max=100 and Xvalue=-5
                        //       105                 = 100                     - -5
                        //And Total Length is        = 100                     - -10 =110
                        #endregion

                        decimal DistanceXvalueAndMax = CurrentNumericIssue.MaximumValue - Xvalue;

                        //   95.45  =            100 * (105                  / 110               ) 
                        NumericRate = Math.Round(100 * (DistanceXvalueAndMax / TotalRangeofPoints), 2);
                    }
                }

                #endregion
            }



            #endregion

            /*================================================================================*/

            #region → Optimum = MaximumValue .

            else if (CurrentNumericIssue.OptimumValueEnd == CurrentNumericIssue.MaximumValue)
            {

                #region → Good Example (Comments)                      .

                /*
                 * Suppose We Have the Following arguments
                 * 
                 * Max=>0
                 * Max=>100
                 * 
                 * Opt.Start=50
                 * Opt.End  =100
                 * 
                 * 
                 * =>New Max Value=110
                 * 
                 *    (TotalRangeofPoints)
                 *    
                 *=> First Step we Calc the Total Lenght of the X Axis as it will be 110 ??
                 *   this calculated by this equation (Top Max) - Minimum Value
                 *                                    110       - 0=110              
                 * 
                 * =>Second Step Find the new Value in Y Axis for the Maximum Value (e.g here is (100 on X))
                 *   this calculated as the Following 
                 *   =in normal Curve it will be (Maximum/total range or New Point) 
                 *    but to be save from dividing by zero continue to see the same result by by another way.
                 *    
                 *    we get the diffrence between new top point (110) and maximum (100).110-100=10
                 *    
                 *   =so the Final Equation will be (1-(Diff New point To Max / Total Range of Points))*100
                 *   =e.g                          =(1-( (new Max. - Max.   ) / 110                  ))*100
                 *                                 =(1-( (110      - 100    ) / 110                  ))*100
                 *                                 =(1-( (10                ) / 110                  ))*100
                 *                                 =(1-( 0.9090                                      ))*100                                 
                 *                                 =(0.9090                                          ))*100
                 *                                 =90.90
                 *                                 
                 * before New Maximum Value the X=100  Y=100
                 * After New Max (110)Value     X=100  Y=90.90
                 * 
                 * 
                 * thanks.
                 */

                #endregion

                #region → Defining Some Important Variables            .

                //First Step we suppose no  values greater than the Actual Maximum.(Intial Value)
                decimal topMaximum = CurrentNumericIssue.MaximumValue;

                //the Lenght of X Axis all lenght from Minimum value to the New Maximum Value.
                decimal TotalRangeofPoints = 0;

                //the Distance between the Maximum Value of the graph and the New Maximum Value.
                decimal DiffNewpointToMax = 0;

                //the Max of Y range or Score in normal case.but after new Maximum Value appear it will be reduce
                //as the last example  90.9 %
                decimal PCT_BaseValue = 100;

                #endregion

                #region → Calculation related To Better Case           .

                //in case of you choose better Than Maximum
                if (CurrentNumericIssue.MaxOperatorBetter)
                {
                    //Get The top Maximum value of all messages that for that numeric issue.
                    topMaximum = this.TopMaximumValue;

                    //Get the Range of  X Axis  110 - 0   =110
                    TotalRangeofPoints = topMaximum - CurrentNumericIssue.MinimumValue;

                    //Get Distance between New Maximum Value And the actual Maximum Value of the graph.
                    //      10        = 110                        - 100
                    DiffNewpointToMax = topMaximum - CurrentNumericIssue.MaximumValue;

                    //Incase the New Maximum value are greater than the Maximum Value of the Actual Graph
                    if (topMaximum > CurrentNumericIssue.MaximumValue)
                    {

                        //Calc the New Y Axis For Maximum X Value of the Actual Graph.
                        //90.90       =          100   * (         1.00 - (10                / 110               ))
                        PCT_BaseValue = (decimal)(100) * ((decimal)1.00 - (DiffNewpointToMax / TotalRangeofPoints));

                        //Max level of PCT not more than 100 %=>90.90%
                        PCT_BaseValue = Math.Round(PCT_BaseValue, 2);
                    }

                }

                #endregion

                #region → Xvalue Between Minimum and Optimum.Start     .

                //Xvalue Between Minimum and Optimum.Start
                // //e.g             0         and 50 
                if (Xvalue >= CurrentNumericIssue.MinimumValue && Xvalue < CurrentNumericIssue.OptimumValueStart)
                {
                    NumericRate = FunY(Xvalue, CurrentNumericIssue.MinimumValue, CurrentNumericIssue.OptimumValueStart, PCT_BaseValue);
                }

                #endregion

                #region → Xvalue Between Optimum.Start and Optimum.End .

                //Xvalue Between Optimum.Start and Optimum.End (Range)
                //e.g             50         and 100 
                else if (Xvalue >= CurrentNumericIssue.OptimumValueStart && Xvalue <= CurrentNumericIssue.OptimumValueEnd)
                {
                    NumericRate = PCT_BaseValue;
                }

                #endregion

                #region → Xvalue Is less than Minimum                  .

                //Xvalue Is less than Minimum e.g. less than zero
                else if (Xvalue < CurrentNumericIssue.MinimumValue)
                {
                    NumericRate = 0;
                }

                #endregion

                #region → Xvalue Is Greater than Maximum               .

                //Xvalue Is Greater than Maximum (So its Good in case of better or Equal)
                else if (Xvalue > CurrentNumericIssue.MaximumValue)
                {
                    //In case you choice Worse Option so any value in X Axis will be Zero
                    if (CurrentNumericIssue.MaxOperatorWorse)
                        NumericRate = 0;


                        //In case you choice Equal Option so any value in X Axis will be 100 %
                    else if (CurrentNumericIssue.MaxOperatorEqual)
                        NumericRate = 100;

                   //In case you choice Better Option.
                    else if (CurrentNumericIssue.MaxOperatorBetter)
                    {
                        #region → Comments .

                        /* Distance Between Xvalue and Minimum to Get the Lenght of the Graph to Xvalue
                         * e.g.  New Maximum 110 and Min=0 And Max=100 and Xvalue=105
                         * Distance                  = New max Value    -  Minimum
                         *       105                 = 105              - 0    =105
                         *       
                         * And Total Length is       = Top Max -  Minimum =110-0=110
                         */
                        #endregion

                        decimal DistanceXvalueAndMax = Xvalue - CurrentNumericIssue.MinimumValue;

                        //   95.45  =            100 * (105                  / 110               ) 
                        NumericRate = Math.Round(100 * (DistanceXvalueAndMax / TotalRangeofPoints), 2);

                    }
                }

                #endregion
            }


            #endregion

            /*================================================================================*/

            #region → Range Optimums         .

            else
            {

                //Xvalue out of all graph
                if ((Xvalue < CurrentNumericIssue.MinimumValue || Xvalue > CurrentNumericIssue.MaximumValue))
                {
                    NumericRate = 0;
                }

                //Xvalue in Optimum range
                else if (Xvalue >= CurrentNumericIssue.OptimumValueStart && Xvalue <= CurrentNumericIssue.OptimumValueEnd)
                {
                    NumericRate = 100;
                }

                //Xvalue Between Minimum and Opt.Start
                else if (Xvalue >= CurrentNumericIssue.MinimumValue && Xvalue < CurrentNumericIssue.OptimumValueStart)
                {
                    NumericRate = FunY(Xvalue, CurrentNumericIssue.MinimumValue, CurrentNumericIssue.OptimumValueStart);

                }

                //Xvalue Between Optimum.End to Maximum.
                else if (Xvalue > CurrentNumericIssue.OptimumValueEnd && Xvalue <= CurrentNumericIssue.MaximumValue)
                {
                    NumericRate = 100 - FunY(Xvalue, CurrentNumericIssue.OptimumValueEnd, CurrentNumericIssue.MaximumValue);
                }
            }

            #endregion


            //This set to be showen in the pannel of Data matching (Status Label)
            Rate = NumericRate;

            //Calculating the Final score
            return Math.Round((NumericRate * this.IssueWeight / (decimal)100.00), 2);
        }
        
        /// <summary>
        /// Funs the Y.
        /// </summary>
        /// <param name="xValue">The x value.</param>
        /// <param name="StartPiont">The start piont.</param>
        /// <param name="EndPoint">The end point.</param>
        /// <param name="BasePCTValue">The base PCT value.</param>
        /// <returns></returns>
        private decimal FunY(decimal xValue, decimal StartPiont, decimal EndPoint, decimal BasePCTValue = 100)
        {
            return Math.Round(((xValue - StartPiont) / (EndPoint - StartPiont)) * BasePCTValue, 2);
        }

        #endregion  Public


        #endregion Methods

    }
}

