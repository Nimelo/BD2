USE [DB]
GO
SET IDENTITY_INSERT [DB].[dbo].[Persons] ON

INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (1,
		   'Jan'
           ,'Kowalski'
           ,'JanKowalski@mail.com'
           ,'123456789'
           ,'Katowice, Rynek 1a'
           ,'93111713678')
GO

INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (2,
		   'Adam'
           ,'Kowalski'
           ,'AdamKowalski@mail.com'
           ,'7653166'
           ,'Chorzów, Plac Hutników 2'
           ,'56111713678')



		   INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (3,
		   'Bartosz'
           ,'Wielki'
           ,'BartoszWielki@mail.com'
           ,'7658945'
           ,'Katowice, Rynek 5'
           ,'54611713678')

		   INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (4,
		   'Barbara'
           ,'Kost'
           ,'BarbaraKost@mail.com'
           ,'7658945'
           ,'Siemianowice, JP2'
           ,'971713678')


INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (5,
		   'Anna'
           ,'Kruk'
           ,'annakruk@mail.com'
           ,'777235465'
           ,'Katowice, Chorzowska 10/3'
           ,'80033034524')

INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (6,
		   'Mariusz'
           ,'Kociol'
           ,'mariuszkociol@mail.com'
           ,'879675465'
           ,'Katowice, Bytomska 4'
           ,'76062345656')

INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (7,
		   'Grzegorz'
           ,'Zawist'
           ,'g.zawist@mail.com'
           ,'667567932'
           ,'Bytom, Radosna 35b'
           ,'90112902347')

INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (8,
		   'Adam'
           ,'Kopacz'
           ,'kopaczadam@mail.com'
           ,'789876567'
           ,'Katowice, Stawowa 4/2'
           ,'91081823457')

INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (9,
		   'Mateusz'
           ,'Miszowski'
           ,'mateuszmiszowski@mail.com'
           ,'567456765'
           ,'Sosnowiec, Ogrodowa 12'
           ,'87021654656')


DECLARE @intFlag INT
SET @intFlag = 5
WHILE (@intFlag <=20)
BEGIN
	INSERT INTO [dbo].[Persons]
           ([Id] ,
		   [Name]
           ,[SurName]
           ,[Mail]
           ,[Phone]
           ,[Address]
           ,[Pesel])
     VALUES
           (@intFlag,
		   'User' + CAST(@intFlag as Varchar(max))
           ,'User' + CAST(@intFlag as Varchar(max))
           ,'User' + + CAST(@intFlag as Varchar(max)) + '@mail.com'
           ,'7658945'
           ,' '
           ,'971713678')
    SET @intFlag = @intFlag + 1
END
GO



