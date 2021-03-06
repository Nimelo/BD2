USE [DB]
GO
/****** Object:  Table [dbo].[ProgrammingLanguages]    Script Date: 2015-03-28 20:38:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgrammingLanguages](
	[Id] [bigint] NOT NULL IDENTITY(1,1),
	[C] [tinyint] NOT NULL,
	[CSharp] [tinyint] NOT NULL,
	[Java] [tinyint] NOT NULL,
 CONSTRAINT [PK_ProgrammingLanguages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
