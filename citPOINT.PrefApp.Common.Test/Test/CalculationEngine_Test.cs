
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
    /// Calculation Engine Test
    /// </summary>
    [TestClass]
    public class CalculationEngine_Test
    {

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Calculate for preference set_ success get expected values.
        /// </summary>
        [TestMethod]
        public void Calculate_ForPreferenceSet_SuccessGetExpectedValues()
        {
            #region → Arrange .

            CalculationEngine<CalculationSamlpesMock> calculationEngine = new CalculationEngine<CalculationSamlpesMock>(null);

            
            CalculationSamlpesMock calcMock = calculationEngine.Provider;


            #endregion

            #region → Act     .

            if (!calculationEngine.Calculate(calcMock.PreferenceSetSource.FirstOrDefault()))
            {
                Assert.Fail("Faild To calculate for Preference Set");
            }

            #endregion

            #region → Assert  .
 
            foreach (var messageItem in calcMock.ConversationMessageSource)
            {

                #region → Messages      .

                switch (messageItem.ConversationMessageID.ToString().ToLower())
                {
                    case "2e8a1f66-b77a-4bc4-86d6-049205013a67":
                        Assert.IsTrue(messageItem.Percentage.HasValue && messageItem.Percentage == 15M, "Faild To Calculate PreferenceSet");
                        break;

                    case "d300213a-fe56-4f4c-a870-1bd6037733a7": //Null
                        Assert.IsTrue(!messageItem.Percentage.HasValue, "Faild To Calculate PreferenceSet");
                        break;

                    case "d6a7372a-3ba3-4295-b117-266e6592412d":
                        Assert.IsTrue(messageItem.Percentage.HasValue && messageItem.Percentage == 84.67M, "Faild To Calculate PreferenceSet");
                        break;

                    case "af2d0840-da0b-45e0-8f26-586eea1a1b14":
                        Assert.IsTrue(messageItem.Percentage.HasValue && messageItem.Percentage == 83.33M, "Faild To Calculate PreferenceSet");
                        break;

                    case "f95101d5-716b-42f1-9240-a10710b9dc56":
                        Assert.IsTrue(messageItem.Percentage.HasValue && messageItem.Percentage == 80M, "Faild To Calculate PreferenceSet");
                        break;

                    case "98da6bc8-6dea-460f-9fca-b8df9e8c52ef":
                        Assert.IsTrue(messageItem.Percentage.HasValue && messageItem.Percentage == 61.67M, "Faild To Calculate PreferenceSet");
                        break;
                    default:
                        Assert.IsTrue(false, "Faild To Calculate PreferenceSet");
                        break;
                }

                #endregion

                #region → Conversation  .

                NegConversation conversation = messageItem.NegConversation;

                switch (conversation.NegConversationID.ToString().ToLower())
                {
                    case "7a9eb6fa-20fd-4a26-9ba0-45d007979802":
                        Assert.IsTrue(conversation.Percentage == 15M, "Faild To Calculate PreferenceSet");
                        break;



                    case "ef3c9f1c-3315-4cff-af2c-7f8c05fd8395":
                        Assert.IsTrue(conversation.Percentage == 61.67M, "Faild To Calculate PreferenceSet");
                        break;

                    default:
                        Assert.IsTrue(false, "Faild To Calculate PreferenceSet");
                        break;
                }

                #endregion

                #region → Preference Set.

                Assert.IsTrue(conversation.PreferenceSetNeg.Percentage == 84.67M, "Faild To Calculate PreferenceSet");

                #endregion

            }



            #endregion
        }

        /// <summary>
        /// Calculate for message success get expected values.
        /// </summary>
        [TestMethod]
        public void Calculate_ForMessage_SuccessGetExpectedValues()
        {
            #region → Arrange .

            CalculationEngine<CalculationSamlpesMock> calculationEngine = new CalculationEngine<CalculationSamlpesMock>(null);

            CalculationSamlpesMock calcMock = calculationEngine.Provider;

            ConversationMessage convMessage = calcMock.ConversationMessageSource.Where(ss => ss.ConversationMessageID == Guid.Parse("98da6bc8-6dea-460f-9fca-b8df9e8c52ef")).FirstOrDefault();

            NegConversation conversation = convMessage.NegConversation;

            #endregion

            #region → Act     .
                       

            if (!calculationEngine.Calculate(convMessage))
            {
                Assert.Fail("Faild To calculate for Preference Set");
            }

            #endregion

            #region → Assert  .
                        
            decimal? percentage = convMessage.Percentage;

            Assert.IsTrue(percentage.HasValue && percentage == 61.67M, "Faild To Calculate Message");
            
            Assert.IsTrue(conversation.Percentage != null && conversation.Percentage == 61.67M, "Faild To Calculate Message");
            
            Assert.IsTrue(conversation.PreferenceSetNeg.Percentage != null && conversation.PreferenceSetNeg.Percentage == 84.67M, "Faild To Calculate Message");
            
            #endregion

        }

        #endregion
        
        #endregion

    }
}

