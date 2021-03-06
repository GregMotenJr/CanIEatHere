USE [CanIEatHere]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 6/14/2017 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AppUser](
	[AppUserID] [int] IDENTITY(1,1) NOT NULL,
	[ASPUserID] [nvarchar](128) NOT NULL,
	[AppUserName] [nvarchar](256) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[City] [varchar](87) NULL,
	[State] [varchar](50) NULL,
	[ZIP] [varchar](15) NULL,
	[DietaryRestrictionID1] [int] NULL,
	[DietaryRestrictionID2] [int] NULL,
	[DietaryRestrictionID3] [int] NULL,
 CONSTRAINT [PK_AppUser] PRIMARY KEY CLUSTERED 
(
	[AppUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_AspNetUsers] FOREIGN KEY([ASPUserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_AspNetUsers]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_DietaryRestriction] FOREIGN KEY([DietaryRestrictionID1])
REFERENCES [dbo].[DietaryRestriction] ([DietaryRestrictionID])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_DietaryRestriction]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_DietaryRestriction1] FOREIGN KEY([DietaryRestrictionID2])
REFERENCES [dbo].[DietaryRestriction] ([DietaryRestrictionID])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_DietaryRestriction1]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_DietaryRestriction2] FOREIGN KEY([DietaryRestrictionID3])
REFERENCES [dbo].[DietaryRestriction] ([DietaryRestrictionID])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_DietaryRestriction2]
GO
