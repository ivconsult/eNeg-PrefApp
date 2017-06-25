CREATE PROC [DBO].[uspNumericIssueSelectForUser]
	@UserID uniqueidentifier,
	@ListOfOrganizations Nvarchar(2000) 
	
AS
	/*----------------------------------------------------
	- Getting My Sets for the Current User
	----------------------------------------------------*/
	SELECT NumericIssue.NumericIssueID   ,
		   NumericIssue.IssueID          ,
		   NumericIssue.MinimumValue     ,
		   NumericIssue.MaximumValue     ,
		   NumericIssue.OptimumValueStart,
		   NumericIssue.OptimumValueEnd  ,
		   NumericIssue.MinimumOperator  ,
		   NumericIssue.MaximumOperator  ,
		   NumericIssue.Unit             ,
		   NumericIssue.Deleted          ,
		   NumericIssue.DeletedBy        ,
		   NumericIssue.DeletedOn
	FROM   Issue
		   INNER JOIN PreferenceSet
		   ON     Issue.PreferenceSetID = PreferenceSet.PreferenceSetID
		   INNER JOIN NumericIssue
		   ON     Issue.IssueID = NumericIssue.IssueID
	WHERE  MainPreferenceSetID  ='72F5566E-3BF5-46E6-9406-B13E80F83BCC'  /*My sets*/
	AND    PreferenceSet.Deleted =0
	AND    Issue.Deleted         =0
	AND    NumericIssue.Deleted  =0
	AND    NumericIssue.DeletedBy=@UserID
	/*---------------------------------------------------
	- Getting Organization Sets for joined organizations
	----------------------------------------------------*/
	UNION

	SELECT dbo.NumericIssue.NumericIssueID   ,
		   dbo.NumericIssue.IssueID          ,
		   dbo.NumericIssue.MinimumValue     ,
		   dbo.NumericIssue.MaximumValue     ,
		   dbo.NumericIssue.OptimumValueStart,
		   dbo.NumericIssue.OptimumValueEnd  ,
		   dbo.NumericIssue.MinimumOperator  ,
		   dbo.NumericIssue.MaximumOperator  ,
		   dbo.NumericIssue.Unit             ,
		   dbo.NumericIssue.Deleted          ,
		   dbo.NumericIssue.DeletedBy        ,
		   dbo.NumericIssue.DeletedOn
	FROM   dbo.Issue
		   INNER JOIN dbo.PreferenceSet
		   ON     dbo.Issue.PreferenceSetID = dbo.PreferenceSet.PreferenceSetID
		   INNER JOIN dbo.PreferenceSetOrganization
		   ON     dbo.PreferenceSet.PreferenceSetID = dbo.PreferenceSetOrganization.PreferenceSetID
		   INNER JOIN dbo.NumericIssue
		   ON     dbo.Issue.IssueID = dbo.NumericIssue.IssueID
	WHERE  MainPreferenceSetID      ='78AC5CF7-A5AB-4377-B9F9-D105F462C26E'  /*Organization Sets*/
	AND    PreferenceSet.Deleted          =0
	AND    Issue.Deleted                  =0
	AND    NumericIssue.Deleted           =0
	AND    @ListOfOrganizations IS NOT NULL
	AND    @ListOfOrganizations        LIKE '%|' + CAST( PreferenceSetOrganization.OrganizationID AS Nvarchar(50)) + '|%'

	UNION

	/*---------------------------------------------------
	- Getting All set stores
	----------------------------------------------------*/
	SELECT NumericIssue.NumericIssueID   ,
		   NumericIssue.IssueID          ,
		   NumericIssue.MinimumValue     ,
		   NumericIssue.MaximumValue     ,
		   NumericIssue.OptimumValueStart,
		   NumericIssue.OptimumValueEnd  ,
		   NumericIssue.MinimumOperator  ,
		   NumericIssue.MaximumOperator  ,
		   NumericIssue.Unit             ,
		   NumericIssue.Deleted          ,
		   NumericIssue.DeletedBy        ,
		   NumericIssue.DeletedOn
	FROM   Issue
		   INNER JOIN PreferenceSet
		   ON     Issue.PreferenceSetID = PreferenceSet.PreferenceSetID
		   INNER JOIN NumericIssue
		   ON     Issue.IssueID = NumericIssue.IssueID
	WHERE  MainPreferenceSetID  ='DC0981BD-0164-4042-A313-5D79CFF5211C'  /*Set Store*/
	AND    PreferenceSet.Deleted=0
	AND    Issue.Deleted        =0
	AND    NumericIssue.Deleted =0