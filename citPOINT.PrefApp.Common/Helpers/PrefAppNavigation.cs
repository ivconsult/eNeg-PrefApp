#region → Usings   .
using System;
using System.Windows.Controls;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 22.02.11     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion

namespace citPOINT.PrefApp.Common
{
    /// <summary>
    /// for navigating to Any url in case of web platform or addon and OOB Add-on
    /// </summary>
    public class PrefAppNavigation
    {

        private class Navigation : HyperlinkButton
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="Navigation"/> class.
            /// </summary>
            /// <param name="NavigateUri">The navigate URI.</param>
            /// <param name="IsBlank">if set to <c>true</c> [is blank].</param>
            internal Navigation(string NavigateUri, bool IsBlank = false)
            {
                //if (AppConfigurations.IsRunningOutOfBrowser)
                //{
                    base.NavigateUri = new Uri(NavigateUri);
                    if (IsBlank)
                        TargetName = "_blank";
                    else
                        TargetName = "_self";
                    base.OnClick();
                //}
                //else
                //{
                //    if (IsBlank)
                //        HtmlPage.Window.Navigate(new Uri(NavigateUri, UriKind.Absolute), "_blank");
                //    else
                //        HtmlPage.Window.Navigate(new Uri(NavigateUri, UriKind.Absolute));
                //}
            }
        }

        /// <summary>
        /// Navigates to URL.
        /// </summary>
        /// <param name="NavigateUri">The navigate URI.</param>
        /// <param name="IsBlank">if set to <c>true</c> [is blank].</param>
        public static void NavigateToUrl(string NavigateUri, bool IsBlank = false)
        {
            Navigation PrefAppNavigation = new Navigation(NavigateUri, IsBlank);
        }
    }
}
