/****** Object:  Table [dbo].[COM_MASTER_ACCOUNT]    Script Date: 2019-03-16 5:16:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[COM_MASTER_ACCOUNT](
	[sid] [varchar](10) NOT NULL,
	[account_code] [varchar](50) NOT NULL,
	[account_name] [varchar](250) NULL,
	[created_by] [varchar](50) NULL,
	[created_on] [varchar](20) NULL,
	[updated_by] [varchar](50) NULL,
	[updated_on] [varchar](20) NULL,
 CONSTRAINT [PK_COM_MASTER_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[sid] ASC,
	[account_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


