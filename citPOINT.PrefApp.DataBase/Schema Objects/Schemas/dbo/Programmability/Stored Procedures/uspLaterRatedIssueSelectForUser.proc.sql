CREATE PROC [DBO].[uspLaterRatedIssueSelectForUser]
	@UserID uniqueidentifier,
	@ListOfOrganizations Nvarchar(2000) 
AS
	/*----------------------------------------------------
	- Getting My Sets for the Current User
	----------------------------------------------------*/
	SELECT LaterRatedIssue.LaterRatedIssueID    ,
		   LaterRatedIssue.LaterRatedIssueValue ,
		   LaterRatedIssue.IssueID          ,
		   LaterRatedIssue.LaterRatedIssueWeight,
		   LaterRatedIssue.Deleted          ,
		   LaterRatedIssue.DeletedBy        ,
		   LaterRatedIssue.DeletedOn
	FROM   Issue
		   INNER JOIN PreferenceSet
		   ON     Issue.PreferenceSetID = PreferenceSet.PreferenceSetID
		   INNER JOIN LaterRatedIssue
		   ON     Issue.IssueID = LaterRatedIssue.IssueID
	WHERE  MainPreferenceSetID  ='72F5566E-3BF5-46E6-9406-B13E80F83BCC'
		   /*My sets*/
	AND    PreferenceSet.Deleted =0
	AND    Issue.Deleted         =0
	AND    LaterRatedIssue.Deleted   =0
	AND    LaterRatedIssue.DeletedBy =@UserID
	/*---------------------------------------------------
	- Getting Organization Sets for joined organizations
	----------------------------------------------------*/
	UNION

	SELECT LaterRatedIssue.LaterRatedIssueID    ,
		   LaterRatedIssue.LaterRatedIssueValue ,
		   LaterRatedIssue.IssueID          ,
		   LaterRatedIssue.LaterRatedIssueWeight,
		   LaterRatedIssue.Deleted          ,
		   LaterRatedIssue.DeletedBy        ,
		   LaterRatedIssue.DeletedOn
	FROM   Issue
		   INNER JOIN PreferenceSet
		   ON     Issue.PreferenceSetID = PreferenceSet.PreferenceSetID
		   INNER JOIN PreferenceSetOrganization
		   ON     PreferenceSet.PreferenceSetID = PreferenceSetOrganization.PreferenceSetID
		   INNER JOIN LaterRatedIssue
		   ON     Issue.IssueID = LaterRatedIssue.IssueID
	WHERE  MainPreferenceSetID  ='78AC5CF7-A5AB-4377-B9F9-D105F462C26E'
		   /*Organization Sets*/
	AND    PreferenceSet.Deleted          =0
	AND    Issue.Deleted                  =0
	AND    LaterRatedIssue.Deleted            =0
	AND    @ListOfOrganizations IS NOT NULL
	AND    @ListOfOrganizations        LIKE '%|' + CAST( PreferenceSetOrganization.OrganizationID AS Nvarchar(50)) + '|%'

	UNION

	/*---------------------------------------------------
	- Getting All set stores
	----------------------------------------------------*/
	SELECT LaterRatedIssue.LaterRatedIssueID    ,
		   LaterRatedIssue.LaterRatedIssueValue ,
		   LaterRatedIssue.IssueID          ,
		   LaterRatedIssue.LaterRatedIssueWeight,
		   LaterRatedIssue.Deleted          ,
		   LaterRatedIssue.DeletedBy        ,
		   LaterRatedIssue.DeletedOn
	FROM   Issue
		   INNER JOIN PreferenceSet
		   ON     Issue.PreferenceSetID = PreferenceSet.PreferenceSetID
		   INNER JOIN LaterRatedIssue
		   ON     Issue.IssueID = LaterRatedIssue.IssueID
	WHERE  MainPreferenceSetID  ='DC0981BD-0164-4042-A313-5D79CFF5211C'
		   /*Set Store*/
	AND    PreferenceSet.Deleted=0
	AND    Issue.Deleted        =0
	AND    LaterRatedIssue.Deleted  =0