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
--				for saving or updating company information 
--				in the database.>

-- Returns:
-- @Result (int): Indicates the result of the operation. 
--                 0 - Success
--                 1 - Failed (e.g., duplicate record)
-- =============================================
ALTER PROCEDURE [dbo].[SaveCompany]
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
			IF EXISTS(SELECT * FROM [dbo].[tblCompany] WHERE Name = @Name)
			BEGIN
				--=============================================================
				-- DELETE RECORD
				--=============================================================
				SELECT 1 [StatusCode], 
					   'The company name you are attempting to add already exists in the database and cannot be duplicated. Please verify the company name and ensure it is unique before attempting to add it again. If necessary, consider modifying the company name to distinguish it from existing entries.' [StatusMessage]
			END 
			ELSE
			BEGIN
				INSERT INTO [dbo].[tblCompany]
				([Name] ,
				[Description] ,
				[FoundedYear] ,
				[Industry] ,
				[Headquarters] ,
				[Website] ,
				[Email] ,
				[Phone] ,
				[CEO] ,
				[DateCreated] ,
				[UserCreatedBy] )
				VALUES
				(@Name ,
				@Description ,
				@FoundedYear ,
				@Industry ,
				@Headquarters ,
				@Website ,
				@Email ,
				@Phone ,
				@CEO ,
				GETDATE() ,
				'admin')

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
		 ('SaveCompany' ,
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
