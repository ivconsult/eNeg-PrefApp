﻿
#region → Usings   . 
using System;
using System.Windows;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 22.09.10     M.Wahab          Creation
 */

# endregion

#region → ToDos    .  

/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion

namespace citPOINT.PrefApp.Data.WebTest
{
    /// <summary>
    /// Class used if we want to show a MessageBox 
    /// </summary>
    public class eNegMessageBox
    {
        #region → Methods        .
        
        /// <summary>
        /// Show Message Box (In case Of Error or Success)
        /// </summary>
        /// <param name="Succcess">Succcess</param>
        /// <param name="MethodName">MethodName</param>
        /// <param name="ErrorMessage">ErrorMessage</param>
        public static void ShowMessageBox(bool Succcess, string MethodName, string ErrorMessage)
        {
            ErrorMessage=string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage : "Error:" + ErrorMessage;
            MessageBox.Show(string.Format("Method :{0}\r\nStatus:{1}\r\n{2}", MethodName, Succcess ? "Success" : "Fail", ErrorMessage), "eNeg Test", MessageBoxButton.OK);
        }

         
        /// <summary>
        /// Show Message Box (In case Of Error or Success)
        /// </summary>
        /// <param name="Succcess">Succcess</param>
        /// <param name="MethodName">MethodName</param>
        /// <param name="ErrorMessage">ErrorMessage</param>
        public static void ShowMessageBox(bool Succcess, string MethodName, Exception ErrorMessage)
        {
            string _ErrorMsg = "";

            _ErrorMsg = ErrorMessage.Message;
            _ErrorMsg += "\r\n" + ErrorMessage.StackTrace;

            if (ErrorMessage.InnerException != null)
            {

                _ErrorMsg += "\r\n" + ErrorMessage.InnerException.Message;
                _ErrorMsg += "\r\n" + ErrorMessage.InnerException.StackTrace;

                if (ErrorMessage.InnerException != null)
                {
                    _ErrorMsg += "\r\n" + ErrorMessage.InnerException.InnerException.Message;
                    _ErrorMsg += "\r\n" + ErrorMessage.InnerException.InnerException.StackTrace;

                }
            }

            ShowMessageBox(Succcess, MethodName, _ErrorMsg);
        }

        #endregion Methods
    }
}
