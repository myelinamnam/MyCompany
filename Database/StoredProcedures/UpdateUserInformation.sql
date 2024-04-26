USE [MyCompanyDB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompany]    Script Date: 4/26/2024 11:29:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Michelle Galangera>
-- Create date: <April 26, 2024>
-- Description:	<This stored procedure is responsible 
--				for updating user information 
--				in the database.>

-- Returns:
-- @Result (int): Indicates the result of the operation. 
--                 0 - Success
--                 1 - Failed (e.g., duplicate record)
-- =============================================
ALTER PROCEDURE [dbo].[UpdateUserInformation]
	@UserId BIGINT ,
	@EmailAddress VARCHAR(100) ,
	@MobileNumber VARCHAR(20) ,
	@Firstname VARCHAR(50) ,
	@Lastname VARCHAR(50) ,
	@Birthday DATETIME ,
	@Address VARCHAR(300)
AS
BEGIN

 BEGIN TRY
       
        BEGIN TRANSACTION;
			IF NOT EXISTS(SELECT * FROM [dbo].[tblUserInformation] WHERE Id = @UserId)
			BEGIN				
				SELECT 1 [StatusCode], 
					   'The user you are attempting to update does not exists in the database. Please verify the user ID and ensure it is unique and existing before attempting to update it again.' [StatusMessage]
			END 
			ELSE IF EXISTS(SELECT * FROM [dbo].[tblUserInformation] WHERE EmailAddress = @EmailAddress) 
			BEGIN
				SELECT 1 [StatusCode], 
					   'The user name you are attempting to update already exists in the database and cannot be duplicated. Please verify the user email and ensure it is unique before attempting to update it again. If necessary, consider modifying the user email address to distinguish it from existing entries.' [StatusMessage]
			END
			ELSE
			BEGIN
				--=============================================================
				-- UPDATE RECORD
				--=============================================================
				UPDATE [dbo].[tblUserInformation]
				SET [EmailAddress] = @EmailAddress ,
					[MobileNumber] = @MobileNumber ,
					[Firstname] = @Firstname ,
					[Lastname] = @Lastname ,
					[Birthday] = @Birthday ,
					[Address] = @Address ,
					[DateUpdated] = GETDATE() ,
					[UserUpdatedBy] = 'admin' 
				WHERE Id = @UserId

				SELECT 0 [StatusCode], 'Success' [StatusMessage]
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
		 ('UpdateUserInformation' ,
		  @EmailAddress,
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
