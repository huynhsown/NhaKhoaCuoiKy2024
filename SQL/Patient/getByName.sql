USE NHAKHOA
GO
CREATE PROCEDURE [dbo].[getByName]
	@HoVaTen VARCHAR(255)
AS
BEGIN
	SELECT * FROM BENHNHAN
	WHERE HoVaTen LIKE '%' + @HoVaTen + '%';
	RETURN 0;
END;