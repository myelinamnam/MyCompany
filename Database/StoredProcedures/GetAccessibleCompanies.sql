-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Michelle Galangera>
-- Create date: <April 26, 2024>
-- Description:	<This stored procedure retrieves 
--				a list of companies accessible 
--				to a specific user or role.>

-- Parameters:
-- @UserId (int): The unique identifier of the user. 
--				  This parameter is required. 
--				  It retrieves companies accessible 
--				  to the specified user.

-- @Result (int): Indicates the result of the operation. 
--                 0 - Success
--                 1 - Failed (e.g., user not found)

-- Example Usage:
-- EXEC GetAccessibleCompanies @UserId = 1
-- =============================================
ALTER PROCEDURE [GetAccessibleCompanies]
	@UserId BIGINT
AS
BEGIN

--DECLARE @UserId BIGINT = 1

	BEGIN TRY
       
        BEGIN TRANSACTION;
			IF NOT EXISTS(SELECT * FROM [dbo].[tblUserCompanyAccess] A WITH(NOLOCK)
						  LEFT JOIN [dbo].[tblCompany] B ON A.CompanyId = B.Id
						  WHERE A.UserId = @UserId)
			BEGIN
				SELECT 1 [StatusCode], 'User not found' [StatusMessage]
			END 
			ELSE
			BEGIN
				SELECT 0 [StatusCode], 
					  'Success' [StatusMessage],
					  A.UserId [UserID] ,
					  A.CompanyId [CompanyID] ,
					  company.Name [Name] ,
					  company.[Description] [Description] ,
					  company.FoundedYear [FoundedYear] ,
					  company.Industry [Industry] ,
					  company.Headquarters [Headquarters] ,
					  company.Website [Website] ,
					  company.Email [Email] ,
					  company.Phone [Phone] ,
					  company.CEO [CEO]
				FROM [dbo].[tblUserCompanyAccess] A WITH(NOLOCK)
				LEFT JOIN [dbo].[tblCompany] company ON A.CompanyId = company.Id
				WHERE A.UserId = @UserId
			END 
        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
		--=============================================================
		-- SAVE ERROR LOGS
		--=============================================================
		INSERT INTO [dbo].[tblLogs]
		([Endpoint] ,
		 [Request] ,
		 [Response] ,
		 [DateCreated])
		 VALUES
		 ('GetAccessibleCompanies' ,
		  CAST(@UserId AS VARCHAR(10)) ,
		  ERROR_MESSAGE() ,
		  GETDATE()
		 )
		
		SELECT 1 [StatusCode], ERROR_MESSAGE() [StatusMessage]
        
		--=============================================================
        -- ROLL BACK THE TRANSACTION
		--=============================================================
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
    END CATCH;

END
GO
