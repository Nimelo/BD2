USE [DB]
GO
SET IDENTITY_INSERT [DB].[dbo].[Candidates] ON

INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           1
           ,1)
GO

INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           2
           ,2)

INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           3
           ,3)


INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           4
           ,4)

INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           5
           ,5)


INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           6
           ,6)

INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           7
           ,7)


INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           8
           ,8)

INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           9
           ,9)

DECLARE @intFlag INT
SET @intFlag = 5
WHILE (@intFlag <=20)
BEGIN
	INSERT INTO [dbo].[Candidates]
           ([DecisionId]
           ,[EvaluationId]
           ,[PersonId])
     VALUES
           (NULL,
           1
           ,@intFlag)

    SET @intFlag = @intFlag + 1
END
GO