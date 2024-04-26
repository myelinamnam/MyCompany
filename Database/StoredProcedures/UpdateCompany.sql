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
--				for updating company information 
--				in the database.>

-- Returns:
-- @Result (int): Indicates the result of the operation. 
--                 0 - Success
--                 1 - Failed (e.g., duplicate record)
-- =============================================
ALTER PROCEDURE [dbo].[UpdateCompany]
	@CompanyId BIGINT ,
	@Name VARCHAR(300),
	@Description VARCHAR(300),
	@FoundedYear INT,
	@Industry VARCHAR(200),
	@Headquarters VARCHAR(200),
	@Website VARCHAR(200),
	@Email VARCHAR(100),
	@Phone VARCHAR(20),
	@CEO VARCHAR(200)
AS
BEGIN

 BEGIN TRY
       
        BEGIN TRANSACTION;
			IF NOT EXISTS(SELECT * FROM [dbo].[tblCompany] WHERE Id = @CompanyId)
			BEGIN				
				SELECT 1 [StatusCode], 
					   'The company you are attempting to update does not exists in the database. Please verify the company ID and ensure it is unique and existing before attempting to update it again.' [StatusMessage]
			END 
			ELSE IF EXISTS(SELECT * FROM [dbo].[tblCompany] WHERE Name = @Name) 
			BEGIN
				SELECT 1 [StatusCode], 
					   'The company name you are attempting to update already exists in the database and cannot be duplicated. Please verify the company name and ensure it is unique before attempting to update it again. If necessary, consider modifying the company name to distinguish it from existing entries.' [StatusMessage]
			END
			ELSE
			BEGIN
				--=============================================================
				-- UPDATE RECORD
				--=============================================================
				UPDATE [dbo].[tblCompany]
				SET [Name] = @Name ,
					[Description] = @Description ,
					[FoundedYear] = @FoundedYear ,
					[Industry] = @Industry ,
					[Headquarters] = @Headquarters,
					[Website] = @Website ,
					[Email] = @Email ,
					[Phone] = @Phone ,
					[CEO] = @CEO ,
					[DateUpdated] = GETDATE(),
					[UserUpdatedBy] = 'admin'
				WHERE Id = @CompanyId

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
		 ('UpdateCompany' ,
		  @Name,
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
