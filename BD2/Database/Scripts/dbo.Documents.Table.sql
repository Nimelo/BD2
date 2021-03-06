USE [DB]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 2015-03-28 20:38:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documents](
	[Id] [bigint] NOT NULL IDENTITY(1,1),
	[GUID] [uniqueidentifier] NOT NULL,
	[File] [varbinary](max) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Extension] [varchar](max) NOT NULL,
	[OwnerId] [bigint] NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Candidates] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Candidates] ([Id])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Candidates]
GO
