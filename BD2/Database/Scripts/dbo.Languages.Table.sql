USE [DB]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 2015-03-28 20:38:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[English] [tinyint] NOT NULL,
	[German] [tinyint] NOT NULL,
	[French] [tinyint] NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
