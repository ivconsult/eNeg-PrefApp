CREATE PROC [dbo].[uspIssueSelectForUser]
	@UserID uniqueidentifier,
	@ListOfOrganizations Nvarchar(2000) 
AS

	/*----------------------------------------------------
	- Getting My Sets for the Current User
	----------------------------------------------------*/
	SELECT Issue.IssueID        ,
		   Issue.IssueName      ,
		   Issue.PreferenceSetID,
		   Issue.IssueTypeID    ,
		   Issue.IssueWeight    ,
		   Issue.Deleted        ,
		   Issue.DeletedBy      ,
		   Issue.DeletedOn
	FROM   Issue
		   INNER JOIN PreferenceSet
		   ON     Issue.PreferenceSetID = PreferenceSet.PreferenceSetID
	WHERE  MainPreferenceSetID          ='72F5566E-3BF5-46E6-9406-B13E80F83BCC'  /*My sets*/
	AND    PreferenceSet.Deleted=0
	AND    Issue.DeletedBy      =@UserID
	AND    Issue.Deleted        =0
	/*---------------------------------------------------
	- Getting Organization Sets for joined organizations
	----------------------------------------------------*/
	UNION

	SELECT Issue.IssueID        ,
		   Issue.IssueName      ,
		   Issue.PreferenceSetID,
		   Issue.IssueTypeID    ,
		   Issue.IssueWeight    ,
		   Issue.Deleted        ,
		   Issue.DeletedBy      ,
		   Issue.DeletedOn
	FROM   Issue
		   INNER JOIN PreferenceSet
		   ON     Issue.PreferenceSetID = PreferenceSet.PreferenceSetID
		   INNER JOIN PreferenceSetOrganization
		   ON     PreferenceSet.PreferenceSetID = PreferenceSetOrganization.PreferenceSetID
	WHERE  MainPreferenceSetID                  ='78AC5CF7-A5AB-4377-B9F9-D105F462C26E'   /*Organization Sets*/
	AND    PreferenceSet.Deleted            =0
	AND    PreferenceSetOrganization.Deleted=0
	AND    Issue.Deleted                    =0
	AND    @ListOfOrganizations   IS NOT NULL
	AND    @ListOfOrganizations          LIKE '%|' + CAST( PreferenceSetOrganization.OrganizationID AS Nvarchar(50)) + '|%'

	UNION

	/*---------------------------------------------------
	- Getting All set stores
	----------------------------------------------------*/
	SELECT Issue.IssueID        ,
		   Issue.IssueName      ,
		   Issue.PreferenceSetID,
		   Issue.IssueTypeID    ,
		   Issue.IssueWeight    ,
		   Issue.Deleted        ,
		   Issue.DeletedBy      ,
		   Issue.DeletedOn
	FROM   Issue
		   INNER JOIN PreferenceSet
		   ON     Issue.PreferenceSetID = PreferenceSet.PreferenceSetID
	WHERE  MainPreferenceSetID          ='DC0981BD-0164-4042-A313-5D79CFF5211C'   /*Set Store*/
	AND    PreferenceSet.Deleted=0
	AND    Issue.Deleted        =0