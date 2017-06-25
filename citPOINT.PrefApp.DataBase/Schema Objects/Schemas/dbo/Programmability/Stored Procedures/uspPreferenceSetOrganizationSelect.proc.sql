CREATE PROC [DBO].[uspPreferenceSetOrganizationSelect]
	@PreferenceSetOrganizationID UNIQUEIDENTIFIER 
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON 
	
	BEGIN TRAN
	SELECT [PreferenceSetOrganizationID],
		   [PreferenceSetID            ],
		   [OrganizationID             ],
		   [Deleted                    ],
		   [DeletedBy                  ],
		   [DeletedOn                  ]
	FROM   [DBO].[PreferenceSetOrganization]
	WHERE  (
				  [PreferenceSetOrganizationID]      = @PreferenceSetOrganizationID
		   OR     @PreferenceSetOrganizationID IS NULL
		   )
		  
	COMMIT 
	GO