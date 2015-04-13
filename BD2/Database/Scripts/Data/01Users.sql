USE [DB]
GO

INSERT INTO [dbo].[Users]
           ([Login]
           ,[Password]
           ,[Role]
           ,[PersonId])
     VALUES
           ('root'
           ,'root'
           ,0
           ,1)
GO

INSERT INTO [dbo].[Users]
           ([Login]
           ,[Password]
           ,[Role]
           ,[PersonId])
     VALUES
           ('oceniajacy'
           ,'oceniajcy'
           ,1
           ,2)
GO

INSERT INTO [dbo].[Users]
           ([Login]
           ,[Password]
           ,[Role]
           ,[PersonId])
     VALUES
           ('readonly'
           ,'readonly'
           ,2
           ,3)
GO

INSERT INTO [dbo].[Users]
           ([Login]
           ,[Password]
           ,[Role]
           ,[PersonId])
     VALUES
           ('candidate'
           ,'candidate'
           ,3
           ,4)
GO