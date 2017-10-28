USE [PropertyManagement]
GO

/****** Object:  Table [dbo].[Properties]    Script Date: 10/28/2017 2:19:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Properties](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PropertyId] [bigint] NULL,
	[Address1] [nvarchar](80) NULL,
	[Address2] [nvarchar](80) NULL,
	[City] [nvarchar](60) NULL,
	[Country] [nvarchar](60) NULL,
	[County] [nvarchar](40) NULL,
	[District] [nvarchar](40) NULL,
	[State] [nvarchar](40) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[ZipPlus4] [nvarchar](10) NULL,
	[YearBuilt] [int] NULL,
	[ListPrice] [decimal](18, 2) NULL,
	[MonthlyRent] [decimal](18, 2) NULL,
	[GrossYield] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](40) NULL,
 CONSTRAINT [PK_dbo.Properties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


