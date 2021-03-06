USE [FreeWheel]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 12/27/2021 4:28:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movie]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Movie](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nchar](100) NOT NULL,
	[ReleaseDate] [datetime] NOT NULL,
	[Description] [nchar](1000) NULL,
	[Genre] [nchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[AverageRating] [float] NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MovieRating]    Script Date: 12/27/2021 4:28:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieRating]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MovieRating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nchar](1000) NULL,
	[ModifiedAt] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_MovieRating] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/27/2021 4:28:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NULL,
	[LastName] [nchar](50) NOT NULL,
	[EmailID] [nchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__Movie__IsActive__21B6055D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Movie] ADD  DEFAULT ((1)) FOR [IsActive]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__MovieRati__IsAct__36B12243]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MovieRating] ADD  DEFAULT ((1)) FOR [IsActive]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__User__IsActive__15502E78]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [IsActive]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovieRating_Movie]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovieRating]'))
ALTER TABLE [dbo].[MovieRating]  WITH CHECK ADD  CONSTRAINT [FK_MovieRating_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovieRating_Movie]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovieRating]'))
ALTER TABLE [dbo].[MovieRating] CHECK CONSTRAINT [FK_MovieRating_Movie]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovieRating_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovieRating]'))
ALTER TABLE [dbo].[MovieRating]  WITH CHECK ADD  CONSTRAINT [FK_MovieRating_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovieRating_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovieRating]'))
ALTER TABLE [dbo].[MovieRating] CHECK CONSTRAINT [FK_MovieRating_User]
GO
