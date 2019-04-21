/****** Object:  Table [dbo].[COM_MASTER_COMPANY]    Script Date: 2019-03-23 3:31:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[COM_MASTER_COMPANY](
	[sid] [varchar](10) NOT NULL,
	[company_code] [varchar](10) NULL,
	[name] [varchar](50) NULL,
	[description] [varchar](200) NULL,
	[building_name] [varchar](200) NULL,
	[building_no] [varchar](20) NULL,
	[village_name] [varchar](200) NULL,
	[village_no] [varchar](10) NULL,
	[soi_name] [varchar](200) NULL,
	[road_name] [varchar](200) NULL,
	[district] [varchar](200) NULL,
	[amphoe] [varchar](200) NULL,
	[province] [varchar](200) NULL,
	[postal_code] [varchar](5) NULL,
	[phone_no] [varchar](50) NULL,
	[latitude] [varchar](25) NULL,
	[longitude] [varchar](25) NULL,
	[remark] [varchar](500) NULL,
	[created_on] [varchar](14) NULL,
	[created_by] [varchar](20) NULL,
	[updated_on] [varchar](14) NULL,
	[updated_by] [varchar](20) NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_COM_MASTER_COMPANY] PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


