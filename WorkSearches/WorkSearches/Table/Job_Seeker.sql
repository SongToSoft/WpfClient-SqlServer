CREATE TABLE [dbo].[Job_Seeker]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(20) NULL, 
    [Vacancy] NCHAR(20) NULL, 
    [Estimated Salary] MONEY NULL, 
    [Activity] BIT NULL
)
