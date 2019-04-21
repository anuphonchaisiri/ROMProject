
/****** Object:  Table [dbo].[COM_MASTER_EMPLOYEE_MAPPING]    Script Date: 2019-03-16 9:27:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[COM_MASTER_EMPLOYEE_MAPPING](
	[sid] [varchar](10) NOT NULL,
	[userid] [varchar](100) NOT NULL,
	[created_on] [varchar](14) NULL,
	[created_by] [varchar](20) NULL,
	[updated_on] [varchar](14) NULL,
	[updated_by] [varchar](20) NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_COM_MASTER_EMPLOYEE_MAPPING] PRIMARY KEY CLUSTERED 
(
	[sid] ASC,
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


