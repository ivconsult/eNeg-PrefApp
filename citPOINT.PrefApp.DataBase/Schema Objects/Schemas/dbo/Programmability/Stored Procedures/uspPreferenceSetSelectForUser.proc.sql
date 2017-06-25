CREATE PROC [DBO].[uspPreferenceSetSelectForUser]
	@UserID uniqueidentifier,
	@ListOfOrganizations Nvarchar(2000)
 AS
	
	/*----------------------------------------------------
	- Getting My Sets for the Current User
	----------------------------------------------------*/
	SELECT	[PreferenceSetID],
			[PreferenceSetName],
			[Checkvariation],
			[VariationValue],
			[MaxPercentage],
			[UserID],
			[MainPreferenceSetID],
			[Deleted],
			[DeletedBy],
			[DeletedOn]
	FROM   [PreferenceSet]
	WHERE  MainPreferenceSetID='72F5566E-3BF5-46E6-9406-B13E80F83BCC' /*My sets*/
	AND    UserID =@UserID
	AND PreferenceSet.Deleted=0

	/*---------------------------------------------------
	- Getting Organization Sets for joined organizations
	----------------------------------------------------*/
	UNION

	SELECT	 PreferenceSet.[PreferenceSetID],
			 PreferenceSet.[PreferenceSetName],
			 PreferenceSet.[Checkvariation],
			 PreferenceSet.[VariationValue],
			 PreferenceSet.[MaxPercentage],
			 PreferenceSet.[UserID],
			 PreferenceSet.[MainPreferenceSetID],
			 PreferenceSet.[Deleted],
			 PreferenceSet.[DeletedBy],
			 PreferenceSet.[DeletedOn]
	FROM   PreferenceSet
			INNER JOIN PreferenceSetOrganization
			ON     PreferenceSet.PreferenceSetID = PreferenceSetOrganization.PreferenceSetID
	WHERE  MainPreferenceSetID                  ='78AC5CF7-A5AB-4377-B9F9-D105F462C26E' /*Organization Sets*/
	AND PreferenceSet.Deleted=0
	AND PreferenceSetOrganization.Deleted=0
	AND    @ListOfOrganizations IS NOT NULL
	AND    @ListOfOrganizations        LIKE '%|' + CAST( PreferenceSetOrganization.OrganizationID AS Nvarchar(50)) + '|%'

	/*---------------------------------------------------
	- Getting All set stores
	----------------------------------------------------*/
	UNION

	SELECT	[PreferenceSetID],
			[PreferenceSetName],
			[Checkvariation],
			[VariationValue],
			[MaxPercentage],
			[UserID],
			[MainPreferenceSetID],
			[Deleted],
			[DeletedBy],
			[DeletedOn]
	FROM   [PreferenceSet]
	WHERE  MainPreferenceSetID='DC0981BD-0164-4042-A313-5D79CFF5211C' /*Set Store*/
	AND PreferenceSet.Deleted=0
       