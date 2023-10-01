CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[KeyPerson] [nvarchar](255) NULL,
	[InvolvingIndustry] [nvarchar](255) NULL,
	[PhoneNo] [nvarchar](255) NULL,
	[FaxNo] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[PFNo] [nvarchar](255) NULL,
	[ESINo] [nvarchar](255) NULL,
	[HeadOffice] [nvarchar](255) NULL,
	[PanNo] [nvarchar](255) NULL,
	[RegNo] [nvarchar](255) NULL,
	[KeyPersonAddress] [nvarchar](255) NULL,
	[KeyPersonPhNo] [nvarchar](255) NULL,
	[KeyPersonDOB] [nvarchar](255) NULL,
	[KeyDesignation] [nvarchar](255) NULL,
	[RegistrationDate] [nvarchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]