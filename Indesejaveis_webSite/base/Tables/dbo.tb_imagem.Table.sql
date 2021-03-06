USE [base_indesejaveis]
GO
/****** Object:  Table [dbo].[tb_imagem]    Script Date: 27/05/2018 21:59:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_imagem](
	[cod_imagem] [int] IDENTITY(1,1) NOT NULL,
	[nom_imagem] [varchar](100) NULL,
	[ds_imagem] [varchar](200) NULL,
	[ds_origem_imagem] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[cod_imagem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
