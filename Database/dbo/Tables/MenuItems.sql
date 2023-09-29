
CREATE TABLE [dbo].[MenuItems](
	[MenuItemID] [int] NOT NULL,
	[ParentMenuItemID] [int] NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Url] [nvarchar](255) NULL,
	[Position] [int] NULL,
	[Level] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED 
(
	[MenuItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MenuItems] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[MenuItems] ADD  CONSTRAINT [FK_MenuItems_ParentMenuItemID] FOREIGN KEY([ParentMenuItemID])
REFERENCES [dbo].[MenuItems] ([MenuItemID])
GO

ALTER TABLE [dbo].[MenuItems] CHECK CONSTRAINT [FK_MenuItems_ParentMenuItemID]
GO
