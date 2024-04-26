USE [MyCompanyDB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompany]    Script Date: 4/26/2024 11:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Michelle Galangera>
-- Create date: <April 26, 2024>
-- Description:	<This stored procedure is responsible 
--				for deleting a company from the database 
--				based on the provided company ID.>

-- Returns:
-- @Result (int): Indicates the result of the operation. 
--                 0 - Success
--                 1 - Failed (e.g., company not found)

-- Example Usage:
-- EXEC DeleteCompany @CompanyId = 1
-- =============================================
ALTER PROCEDURE [dbo].[DeleteCompany]
	@CompanyId BIGINT
AS
BEGIN

	BEGIN TRY
       
        BEGIN TRANSACTION;
			IF EXISTS(SELECT * FROM [dbo].[tblCompany] WHERE Id = @CompanyId)
			BEGIN
				--=============================================================
				-- DELETE RECORD
				--=============================================================
				DELETE FROM [dbo].[tblCompany] WHERE Id = @CompanyId

				SELECT 0 [StatusCode], 'Success' [StatusMessage]
			END 
			ELSE
			BEGIN
				SELECT 1 [StatusCode], 'Company not found' [StatusMessage]
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
		 ('DeleteCompany' ,
		  CAST(@CompanyId AS VARCHAR(10)) ,
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
