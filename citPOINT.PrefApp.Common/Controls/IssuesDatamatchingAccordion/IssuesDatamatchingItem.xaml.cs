#region → Usings   .
using citPOINT.PrefApp.Data.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 19.01.11     M.Wahab         ○→ Creation
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
    ///  Issues Data matching Item
    /// </summary>
    public partial class IssuesDatamatchingItem : UserControl
    {
        #region → Fields         .

        private Issue mIssueValue;
        private ConversationMessage mMessageValue;

        #endregion

        #region → Properties     .

        #region → Colors Properties                 .

        /// <summary>
        /// Gets the color of the numeric back ground.
        /// </summary>
        /// <value>The color of the numeric back ground.</value>
        private LinearGradientBrush NumericBackGroundColor
        {
            get
            {
                foreach (var item in Resources)
                {
                    if (((DictionaryEntry)item).Key.ToString() == "NumericBackGroundColor")
                        return (((DictionaryEntry)(item)).Value as LinearGradientBrush);
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the color of the option back ground.
        /// </summary>
        /// <value>The color of the option back ground.</value>
        private LinearGradientBrush OptionBackGroundColor
        {
            get
            {
                foreach (var item in Resources)
                {
                    if (((DictionaryEntry)item).Key.ToString() == "OptionBackGroundColor")
                        return (((DictionaryEntry)(item)).Value as LinearGradientBrush);
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the not rated back ground.
        /// </summary>
        /// <value>The not rated back ground.</value>
        private LinearGradientBrush NotRatedBackGround
        {
            get
            {
                foreach (var item in Resources)
                {
                    if (((DictionaryEntry)item).Key.ToString() == "NotRatedBackGround")
                        return (((DictionaryEntry)(item)).Value as LinearGradientBrush);
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the later rated back ground.
        /// </summary>
        /// <value>The later rated back ground.</value>
        private LinearGradientBrush LaterRatedBackGround
        {
            get
            {
                foreach (var item in Resources)
                {
                    if (((DictionaryEntry)item).Key.ToString() == "LaterRatedBackGround")
                        return (((DictionaryEntry)(item)).Value as LinearGradientBrush);
                }
                return null;
            }
        }

        #endregion

        #region → Existing Values for Data matching .

        /// <summary>
        /// Gets the numeric value.
        /// </summary>
        /// <value>The numeric value.</value>
        private double? NumericValue
        {
            get
            {
                if (this.MessageValue != null)
                {
                    MessageIssue messageIssue = this.MessageValue.MessageIssues.FirstOrDefault(s => s.IssueID == this.mIssueValue.IssueID);
                    if (messageIssue != null)
                    {
                        double x;
                        if (double.TryParse(messageIssue.Value, out x))
                            return x;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the option values.
        /// </summary>
        /// <value>The option values.</value>
        private IEnumerable<MessageOptionIssue> OptionValues
        {
            get
            {
                if (this.MessageValue != null)
                {
                    MessageIssue messageIssue = this.MessageValue.MessageIssues.FirstOrDefault(s => s.IssueID == this.mIssueValue.IssueID);
                    if (messageIssue != null)
                    {
                        return messageIssue.MessageOptionIssues.AsEnumerable();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the later rated values.
        /// </summary>
        /// <value>The later rated values.</value>
        private IEnumerable<MessageLaterRatedIssue> LaterRatedValues
        {
            get
            {
                if (this.MessageValue != null)
                {
                    MessageIssue messageIssue = this.MessageValue.MessageIssues.FirstOrDefault(s => s.IssueID == this.mIssueValue.IssueID);
                    if (messageIssue != null)
                    {
                        return messageIssue.MessageLaterRatedIssues.AsEnumerable();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the not rated value.
        /// </summary>
        /// <value>The not rated value.</value>
        private string NotRatedValue
        {
            get
            {
                if (this.MessageValue != null)
                {
                    MessageIssue messageIssue = this.MessageValue.MessageIssues.FirstOrDefault(s => s.IssueID == this.mIssueValue.IssueID);
                    if (messageIssue != null)
                    {
                        return messageIssue.Value == null ? string.Empty : messageIssue.Value;
                    }
                }

                return string.Empty;
            }
        }

        #endregion

        #region → Get The Actual Bind Control       .

        /// <summary>
        /// Gets the uxtxt numeric text box.
        /// </summary>
        /// <value>The uxtxt numeric text box.</value>
        private RadNumericUpDown uxtxtNumericTextBox
        {
            get
            {
                foreach (var xControl in this.uxpnlConetnt.Children)
                {
                    if (xControl.GetType().Equals(typeof(RadNumericUpDown)))
                    {
                        return (xControl as RadNumericUpDown);
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the uxtxt box.
        /// </summary>
        /// <value>The uxtxt box.</value>
        private TextBox uxtxtBox
        {
            get
            {
                foreach (var xControl in this.uxpnlConetnt.Children)
                {
                    if (xControl.GetType().Equals(typeof(TextBox)))
                    {
                        return (xControl as TextBox);
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Uxes the check box.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        private CheckBox uxCheckBox(string Value)
        {
            foreach (var xControl in this.uxpnlConetnt.Children)
            {
                if (xControl.GetType().Equals(typeof(CheckBox)) &&
                    (GetContentString(xControl, true) == RemoveSpaces(Value).ToLower()))
                {
                    return (xControl as CheckBox);
                }
            }

            return uxCheckBoxLevel2(Value);
        }

        /// <summary>
        /// Gets the list of choices words.
        /// </summary>
        /// <value>The list of choices words.</value>
        private List<string> ListOfChoicesWords
        {
            get
            {
                List<string> lst = new List<string>();

                foreach (var xControl in this.uxpnlConetnt.Children)
                {
                    if (xControl.GetType().Equals(typeof(CheckBox)))
                    {
                        lst.Add(GetContentString(xControl, false));
                    }
                }

                return lst;
            }
        }

        /// <summary>
        /// Uxes the check box level2.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        private CheckBox uxCheckBoxLevel2(string Value)
        {
            SpellChecker sp = new SpellChecker(ListOfChoicesWords);

            Value = sp.Correct(Value);

            if (Value != null)
            {
                foreach (var xControl in this.uxpnlConetnt.Children)
                {
                    if (xControl.GetType().Equals(typeof(CheckBox)) &&
                        (GetContentString(xControl, true) == RemoveSpaces(Value).ToLower()))
                    {
                        return (xControl as CheckBox);
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// Gets the content string.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="removeSpaces">if set to <c>true</c> [remove spaces].</param>
        /// <returns></returns>
        private string GetContentString(UIElement element, bool removeSpaces)
        {
            string content = string.Empty;

            if (element.GetType().Equals(typeof(CheckBox)))
            {
                CheckBox chk = (element as CheckBox);

                if (chk.Content.GetType().Equals(typeof(string)))
                {
                    content = chk.Content.ToString();
                }
                else if (chk.Content.GetType().Equals(typeof(TextBlock)))
                {
                    content = (chk.Content as TextBlock).Text.ToString();
                }
            }

            if (removeSpaces)
            {
                return RemoveSpaces(content.ToLower());
            }
            else
            {
                return content;
            }
        }

        #endregion


        /// <summary>
        /// Gets or sets the message value.
        /// </summary>
        /// <value>The message value.</value>
        public ConversationMessage MessageValue
        {
            get
            {
                return mMessageValue;
            }
            set
            {
                mMessageValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the issue value.
        /// </summary>
        /// <value>The issue value.</value>
        public Issue IssueValue
        {
            get { return mIssueValue; }
            set
            {
                mIssueValue = value;

                uxtxtIssueName.Text = mIssueValue.IssueName;

                UpdateStatusText();

                mIssueValue.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mIssueValue_PropertyChanged);


                #region → Drag Drop Operation   .

                if (!MessageValue.IsClosed && PrefAppConfigurations.MainDragDropManager != null)
                {
                    //Addin the Accordion Item to Drag targets.
                    PrefAppConfigurations.MainDragDropManager.DestinationTargets.Add(this);

                    PrefAppConfigurations.MainDragDropManager.DragDropCompleted += new EventHandler<DragEventArgs>(MainDragDropManager_DragDropCompleted);
                }

                #endregion

                #region → Numeric Handling      .

                if (mIssueValue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric)
                {
                    this.uxbrdDetails.Background = this.NumericBackGroundColor;

                    Color color = NumericBackGroundColor.GradientStops[0].Color;

                    this.uxbrdDetails.BorderBrush = new SolidColorBrush(Color.FromArgb(255, (byte)(color.R - 30), (byte)(color.G - 30), (byte)(color.B - 30)));
                                        
                      //this.uxtxtIssueName.Text += " (Number):";

                      AdjsutNumericType();
                }

                #endregion

                #region → Options Handling      .

                else if (mIssueValue.IssueTypeID == PrefAppConstant.IssueTypes.Options)
                {
                    this.uxbrdDetails.Background = this.OptionBackGroundColor;

                    Color color = OptionBackGroundColor.GradientStops[0].Color;

                    this.uxbrdDetails.BorderBrush = new SolidColorBrush(Color.FromArgb(255, (byte)(color.R - 10), (byte)(color.G - 10), (byte)(color.B - 10)));
                                        
                    //uxtxtIssueName.Text += " (Option):";
                    AdjsutOptionType();
                }

                #endregion

                #region → Later Rated Handling  .

                else if (mIssueValue.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated)
                {
                    this.uxbrdDetails.Background = this.LaterRatedBackGround;

                    Color color = LaterRatedBackGround.GradientStops[0].Color;

                    this.uxbrdDetails.BorderBrush = new SolidColorBrush(Color.FromArgb(255, (byte)(color.R - 30), (byte)(color.G - 30), (byte)(color.B - 30)));
                    

                    //uxtxtIssueName.Text += " (Later Rated):";
                    AdjsutLaterRatedType();
                }
                #endregion

                #region → Not Rated Handling    .

                else if (mIssueValue.IssueTypeID == PrefAppConstant.IssueTypes.NotRated)
                {
                    this.uxbrdDetails.Background = this.NotRatedBackGround;

                    Color color = NotRatedBackGround.GradientStops[0].Color;

                    this.uxbrdDetails.BorderBrush = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B));
                    

                    //uxtxtIssueName.Text += " (Not Rated):";
                    AdjsutNotRatedType();
                }
                #endregion

                #region → Other Types Handling  .

                else
                {
                    this.uxbrdDetails.Background = new SolidColorBrush(Colors.Black);
                }

                #endregion

            }
        }

        /// <summary>
        /// Sets a value indicating whether [show header].
        /// </summary>
        /// <value><c>true</c> if [show header]; otherwise, <c>false</c>.</value>
        public bool ShowHeader
        {
            set
            {
                if (!value)
                {
                    this.uxpnlMain.RowDefinitions[0].MaxHeight = 0;
                    this.uxpnlMain.RowDefinitions[0].Height = new GridLength(0);
                    this.uxpnlMain.RowDefinitions[0].MinHeight = 0;
                }
            }
        }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="IssuesDatamatchingItem"/> class.
        /// </summary>
        public IssuesDatamatchingItem()
        {
            InitializeComponent();
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the mIssueValue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        void mIssueValue_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Status")
            {
                UpdateStatusText();
            }
        }

        /// <summary>
        /// Handles the Click event of the uxhlbAdjsutLaterRatedValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void uxhlbAdjsutLaterRatedValues_Click(object sender, RoutedEventArgs e)
        {
            PrefAppMessanger.NewPopUp.Send(this.IssueValue, "New LaterRated", PrefAppMessanger.PopUpType.NewLaterRated);
        }


        #region → Event Handling For Numeric     .

        /// <summary>
        /// Handles the ValueChanged event of the txtNumeric control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs"/> instance containing the event data.</param>
        void txtNumeric_ValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            //create new Message
            DataMatchingMessage DataMatchmsg = new DataMatchingMessage();

            //Adding the Current Issue to the Mesage
            DataMatchmsg.CurrentIssue = this.IssueValue;

            //Setting the current value of the Numeric Text box in the message
            DataMatchmsg.Value = e.NewValue == null ? null : Math.Round(e.NewValue.Value, 2).ToString("0.00");

            //Set the Current message (PrefApp Message Table) to the Datamatching Message.
            DataMatchmsg.CurrentMessage = MessageValue;

            //Send the Message by Datamatching Message.
            PrefAppMessanger.DataMatchMessage.Send(DataMatchmsg);

        }

        /// <summary>
        /// Handles the Click event of the uxhlbAdjsutNumericValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void uxhlbAdjsutNumericValues_Click(object sender, RoutedEventArgs e)
        {
            PrefAppMessanger.ChangeScreenMessage.Send(PrefAppViewTypes.AppSettingsView);

            //Sending Message with the Current Isssue that give the System notification to
            //Open the Current Isssue in values View.
            PrefAppMessanger.FlippMessage.Send(mIssueValue);
        }

        #endregion


        #region → Event Handling For Options     .

        /// <summary>
        /// Handles the StateChanged event of the uxchkOption control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void uxchkOption_StateChanged(object sender, RoutedEventArgs e)
        {

            //create new Message
            DataMatchingMessage DataMatchmsg = new DataMatchingMessage();

            //Adding the Current Issue to the Mesage
            DataMatchmsg.CurrentIssue = this.IssueValue;

            //Set the Current Option On or Off.
            DataMatchmsg.IsChecked = (sender as CheckBox).IsChecked.Value;

            //Set the Current message (PrefApp Message Table) to the Datamatching Message.
            DataMatchmsg.CurrentMessage = MessageValue;

            //Setting the Optin Value to the Data matching Mesasge.e.g (Blue.)
            DataMatchmsg.Value = GetContentString((sender as CheckBox), false);

            //Send the Message by Datamatching Message.
            PrefAppMessanger.DataMatchMessage.Send(DataMatchmsg);

        }

        #endregion


        #region → Event Handling For Later Rated .

        /// <summary>
        /// Handles the StateChaned event of the uxchkLaterRated control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void uxchkLaterRated_StateChaned(object sender, RoutedEventArgs e)
        {
            //Until now we suppose that it run like Options
            uxchkOption_StateChanged(sender, e);
        }

        #endregion


        #region → Event Handling For Not Rated   .

        /// <summary>
        /// Handles the LostFocus event of the txtNotRated control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void txtNotRated_LostFocus(object sender, RoutedEventArgs e)
        {
            //create new Message
            DataMatchingMessage DataMatchmsg = new DataMatchingMessage();

            //Adding the Current Issue to the Mesage
            DataMatchmsg.CurrentIssue = this.IssueValue;

            //Set the Current message (PrefApp Message Table) to the Datamatching Message.
            DataMatchmsg.CurrentMessage = MessageValue;

            //Setting the Value =Textbox Value.
            DataMatchmsg.Value = (sender as TextBox).Text;

            //Send the Message by Datamatching Message.
            PrefAppMessanger.DataMatchMessage.Send(DataMatchmsg);
        }

        #endregion


        /// <summary>
        /// Handles the DragDropCompleted event of the MainDragDropManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.PrefApp.Common.DragEventArgs"/> instance containing the event data.</param>
        void MainDragDropManager_DragDropCompleted(object sender, DragEventArgs e)
        {
            IssuesDatamatchingItem issuesDatamatchingItem = null;

            //Check if drag Target is the same current Control
            if (e.TargetControl.GetType().Equals(typeof(IssuesDatamatchingItem)))
            {
                issuesDatamatchingItem = (e.TargetControl as IssuesDatamatchingItem);
            }

            //if Ok this type is the same and also the target is the Current this.
            if (issuesDatamatchingItem != null && this == issuesDatamatchingItem)
            {
                #region → Numeric Handling      .

                if (this.IssueValue.IssueTypeID == PrefAppConstant.IssueTypes.Numeric && e.IsNumeric)
                {
                    if (this.uxtxtNumericTextBox != null)
                        this.uxtxtNumericTextBox.Value = e.NumericValue;
                }

                #endregion

                #region → Options Handling      .

                else if (this.IssueValue.IssueTypeID == PrefAppConstant.IssueTypes.Options)
                {
                    CheckBox checkBox = this.uxCheckBox(e.Value.TrimEnd().TrimStart());

                    if (checkBox != null)
                    {
                        checkBox.IsChecked = true;
                    }
                    else
                    {
                        //Send Message to appear Pop Up Window for add new option for Option Issue
                        PrefAppMessanger.NewPopUp.Send(IssueValue, e.Value.TrimEnd().TrimStart(), PrefAppMessanger.PopUpType.NewOption);
                    }
                }

                #endregion

                #region → Later Rated Handling  .

                else if (this.IssueValue.IssueTypeID == PrefAppConstant.IssueTypes.LaterRated)
                {
                    CheckBox checkBox = this.uxCheckBox(e.Value.TrimEnd().TrimStart());

                    if (checkBox != null)
                    {
                        checkBox.IsChecked = true;
                    }
                    else
                    {
                        //Send Message to appear Pop Up Window for add new option forLater Rated Issue
                        PrefAppMessanger.NewPopUp.Send(IssueValue, e.Value.TrimEnd().TrimStart(), PrefAppMessanger.PopUpType.NewLaterRated);
                    }
                }

                #endregion

                #region → Not Rated Handling    .

                else if (this.IssueValue.IssueTypeID == PrefAppConstant.IssueTypes.NotRated)
                {
                    if (this.uxtxtBox != null)
                    {
                        //Check if the Message more than 300 Charatcer.
                        if (e.Value != null && e.Value.Length > uxtxtBox.MaxLength)
                        {
                            this.uxtxtBox.Text = e.Value.Substring(0, uxtxtBox.MaxLength);
                        }
                        else
                        {
                            this.uxtxtBox.Text = e.Value;
                        }

                        //For updating the View Model
                        this.txtNotRated_LostFocus(this.uxtxtBox, null);

                    }

                }
                #endregion
            }
        }

        #endregion

        #region → Events         .
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Removes the spaces.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        string RemoveSpaces(string Value)
        {

            while (Value != null && Value.IndexOf(" ") != -1)
            {
                Value = Value.Replace(" ", "");

            }

            //e.g Word,  ==Word
            Value = Value.Replace(",", "");
            //e.g Word.  ==Word
            Value = Value.Replace(".", "");
            //e.g Word;  ==Word
            Value = Value.Replace(";", "");

            return Value;

        }

        /// <summary>
        /// Updates the status text.
        /// </summary>
        void UpdateStatusText()
        {
            this.uxtxtIssueStatus.Text = this.mIssueValue.GetStatus(this.MessageValue.NegConversationID);

            MessageIssue messageIssue = this.MessageValue.MessageIssues.Where(s => s.IssueID == this.IssueValue.IssueID).FirstOrDefault();

            uxrdPrgRating.Value = (double)(messageIssue != null ? messageIssue.Rate : 0M);
        }

        /// <summary>
        /// Adds the read only lable.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <param name="textAlignment">The text alignment.</param>
        /// <returns></returns>
        private UIElement AddReadOnlyLable(string Value, TextAlignment textAlignment)
        {
            #region → Border  .


            Border uxbrContainer = new Border();
            uxbrContainer.BorderBrush = new SolidColorBrush(Colors.Black);
            uxbrContainer.BorderThickness = new Thickness(0.5);
            uxbrContainer.Margin = new Thickness(2, 2, 2, 5);
            uxbrContainer.MinHeight = 25;
            uxbrContainer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            uxbrContainer.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

            #endregion

            #region → label   .

            TextBlock uxlblActualText = new TextBlock();
            uxlblActualText.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            uxlblActualText.Margin = new Thickness(5, 0, 5, 0);
            uxlblActualText.TextAlignment = textAlignment;
            uxlblActualText.Text = Value;
            uxlblActualText.TextWrapping = TextWrapping.Wrap;

            uxbrContainer.Child = uxlblActualText;

            #endregion

            return uxbrContainer;

        }

        /// <summary>
        /// Adjsuts the type of the numeric.
        /// </summary>
        void AdjsutNumericType()
        {

            #region → Make a Link to Adjust Values   .

            //Craete New Hyper link button
            HyperlinkButton uxhlbAdjsutNumericValues = new HyperlinkButton();

            //Handling the Click event.
            uxhlbAdjsutNumericValues.Click += new RoutedEventHandler(uxhlbAdjsutNumericValues_Click);

            //Create a margin with 2 in all sides.
            uxhlbAdjsutNumericValues.Margin = new Thickness(2);

            //link text
            uxhlbAdjsutNumericValues.Content = "Adjust Values";

            uxhlbAdjsutNumericValues.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            //adding the hyperlink to the stack panel.
            this.uxpnlAdjustValuesLink.Children.Add(uxhlbAdjsutNumericValues);


            #endregion

            #region → Add Numeric Text Box          .

            //create New Numeric text box.
            RadNumericUpDown txtNumeric = new RadNumericUpDown();

            #region → Adjust Length

            //Setting its value
            txtNumeric.Value = NumericValue;

            //Setting the decimal Digists to 2. e.g. 25.36    no more like 25.3625  =>25.36 only.
            txtNumeric.NumberDecimalDigits = 2;

            //Prevent Numeric Lenght
            txtNumeric.Maximum = Math.Pow(10, 17);

            //Prevent Numeric Lenght
            txtNumeric.Minimum = -Math.Pow(10, 17);

            //Set Unit
            if (mIssueValue.NumericIssues.FirstOrDefault() != null)
            {
                txtNumeric.CustomUnit = mIssueValue.NumericIssues.FirstOrDefault().Unit;
            }

            #endregion

            //Create a margin with 5 in all sides.
            txtNumeric.Margin = new Thickness(0, 0, 0, 0);

            //Check if this message Come from closed Negotiation
            if (!this.MessageValue.IsClosed)
            {
                //handling the value changes.
                txtNumeric.ValueChanged += new EventHandler<Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs>(txtNumeric_ValueChanged);

                //Adding to current stack panel.
                this.uxpnlConetnt.Children.Add(txtNumeric);
            }
            else
            {
                //Adding to current stack panel.
                this.uxpnlConetnt.Children.Add(AddReadOnlyLable(NumericValue.HasValue ? NumericValue.Value.ToString("0.00") : "", TextAlignment.Right));
            }

            #endregion

        }

        /// <summary>
        /// Adjsuts the type of the option.
        /// </summary>
        void AdjsutOptionType()
        {

            #region → Make a Link to Adjust Values   .

            //Craete New Hyper link button
            HyperlinkButton uxhlbAdjsutNumericValues = new HyperlinkButton();

            //Handling the Click event.
            uxhlbAdjsutNumericValues.Click += new RoutedEventHandler(uxhlbAdjsutNumericValues_Click);

            //Create a margin with 2 in all sides.
            uxhlbAdjsutNumericValues.Margin = new Thickness(2);

            //link text
            uxhlbAdjsutNumericValues.Content = "Adjust Values";

            uxhlbAdjsutNumericValues.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            //adding the hyperlink to the stack panel.
            this.uxpnlAdjustValuesLink.Children.Add(uxhlbAdjsutNumericValues);


            #endregion

            #region → Adding Check Boxes            .

            //looping for all options
            foreach (var option in mIssueValue.OptionIssues.OrderBy(s => s.OptionIssueValue))
            {
                //Create new check box.
                CheckBox uxchkOption = new CheckBox();

                //create margin with 2 in all sides.
                uxchkOption.Margin = new Thickness(0);

                //Setting if the Check box will be checked or unchecked
                if (OptionValues != null)
                {
                    uxchkOption.IsChecked = OptionValues.FirstOrDefault(s => s.OptionIssueID == option.OptionIssueID) != null;
                }


                //Setting the checkbox with its text.
                uxchkOption.Content = new TextBlock() { Text = option.OptionIssueValue, TextWrapping = TextWrapping.Wrap };


                //Check if this message Come from closed Negotiation
                if (!this.MessageValue.IsClosed)
                {
                    //handling the check state and uncheck state.
                    uxchkOption.Checked += new RoutedEventHandler(uxchkOption_StateChanged);
                    uxchkOption.Unchecked += new RoutedEventHandler(uxchkOption_StateChanged);
                }
                else
                {
                    uxchkOption.IsTabStop = false;
                    uxchkOption.IsHitTestVisible = false;

                }

                //When I add an option during data matching, the new option should be selected by default
                if (PrefAppConfigurations.PendingItems != null && !uxchkOption.IsChecked.Value)
                {
                    uxchkOption.IsChecked = PrefAppConfigurations.PendingItems.Where(s => s.PendingID == option.OptionIssueID).Count() > 0;
                }

                //Adding to current stack panel.
                this.uxpnlConetnt.Children.Add(uxchkOption);

            }

            #endregion
        }

        /// <summary>
        /// Adjsuts the type of the later rated.
        /// </summary>
        void AdjsutLaterRatedType()
        {

            #region → Make a Link to Adjust Values   .

            //Craete New Hyper link button
            HyperlinkButton uxhlbAdjsutLaterRatedValues = new HyperlinkButton();

            //Handling the Click event.
            uxhlbAdjsutLaterRatedValues.Click += new RoutedEventHandler(uxhlbAdjsutLaterRatedValues_Click);

            //Create a margin with 2 in all sides.
            uxhlbAdjsutLaterRatedValues.Margin = new Thickness(2);

            //link text
            uxhlbAdjsutLaterRatedValues.Content = "Add Option";

            uxhlbAdjsutLaterRatedValues.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            //adding the hyperlink to the stack panel.
            this.uxpnlAdjustValuesLink.Children.Add(uxhlbAdjsutLaterRatedValues);

            #endregion

            #region → Add List Of CheckBoxes        .

            //Loping on all later Rated choices
            foreach (var laterRated in mIssueValue.LaterRatedIssues.OrderBy(s => s.LaterRatedIssueValue))
            {
                //create New check box.
                CheckBox uxchkLaterRated = new CheckBox();

                //Create a margin with 2 in all sides.
                uxchkLaterRated.Margin = new Thickness(0);

                //Setting the check box text.
                uxchkLaterRated.Content = laterRated.LaterRatedIssueValue;


                //setting the value to true or false according to last choices.
                if (LaterRatedValues != null)
                {
                    uxchkLaterRated.IsChecked = LaterRatedValues.FirstOrDefault(s => s.LaterRatedIssueID == laterRated.LaterRatedIssueID) != null;
                }

                //Check if this message Come from closed Negotiation
                if (!this.MessageValue.IsClosed)
                {
                    //Handling Check or Un checked.
                    uxchkLaterRated.Checked += new RoutedEventHandler(uxchkLaterRated_StateChaned);
                    uxchkLaterRated.Unchecked += new RoutedEventHandler(uxchkLaterRated_StateChaned);
                }
                else
                {
                    uxchkLaterRated.IsTabStop = false;
                    uxchkLaterRated.IsHitTestVisible = false;
                }

                //When I add a later rated target during data matching, the new option should be selected by default
                if (PrefAppConfigurations.PendingItems != null && !uxchkLaterRated.IsChecked.Value)
                {
                    uxchkLaterRated.IsChecked = PrefAppConfigurations.PendingItems.Where(s => s.PendingID == laterRated.LaterRatedIssueID).Count() > 0;
                }

                //Adding to the Current stack Panel.
                this.uxpnlConetnt.Children.Add(uxchkLaterRated);
            }

            #endregion
        }

        /// <summary>
        /// Adjsuts the type of the not rated.
        /// </summary>
        void AdjsutNotRatedType()
        {
            //create new Text box.
            TextBox txtNotRated = new TextBox();

            //Setting the Text box value.
            txtNotRated.Text = NotRatedValue;

            //Create new margin with 5 in all side.
            txtNotRated.Margin = new Thickness(0);

            //Centeralize the text box.
            txtNotRated.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            //Wrapping Content to load larg text.
            txtNotRated.TextWrapping = TextWrapping.Wrap;

            //set Max lenght to 300 Character.
            txtNotRated.MaxLength = 300;


            //Check if this message Come from closed Negotiation
            if (!this.MessageValue.IsClosed)
            {

                //handling the lost focus of the Text box.
                txtNotRated.LostFocus += new RoutedEventHandler(txtNotRated_LostFocus);


                //Adding the current stack Panel
                this.uxpnlConetnt.Children.Add(txtNotRated);
            }
            else
            {
                //Adding to current stack panel.
                this.uxpnlConetnt.Children.Add(AddReadOnlyLable(NotRatedValue, TextAlignment.Left));

            }

            //No progress in case of not rated.
            this.uxrdPrgRating.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region → Public         .


        #endregion

        #endregion

    }
}
