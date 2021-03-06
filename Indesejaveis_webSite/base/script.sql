USE [base_indesejaveis]
GO
/****** Object:  Table [dbo].[tb_imagem]    Script Date: 27/05/2018 22:04:10 ******/
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
/****** Object:  Table [dbo].[tb_noticia]    Script Date: 27/05/2018 22:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_noticia](
	[cod_noticia] [int] IDENTITY(1,1) NOT NULL,
	[nom_titulo_noticia] [varchar](100) NULL,
	[ds_tipo_noticia] [varchar](30) NULL,
	[ds_noticia] [varchar](1000) NULL,
	[dat_noticia] [datetime] NULL,
	[cod_imagem] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[cod_noticia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_noticia]  WITH CHECK ADD  CONSTRAINT [fk_ImagemNoticia] FOREIGN KEY([cod_imagem])
REFERENCES [dbo].[tb_imagem] ([cod_imagem])
GO
ALTER TABLE [dbo].[tb_noticia] CHECK CONSTRAINT [fk_ImagemNoticia]
GO
/****** Object:  StoredProcedure [dbo].[de_tb_noticia]    Script Date: 27/05/2018 22:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Helder Felipe>        
-- Create date: <21/04/2018>        
-- Description: <Deletar Noticia>  
-- Action: Hfvas - 20/05/2018 - Definir exclusao da tabela tb_imagem quando excluir noticia.         
-- =============================================        
CREATE PROCEDURE [dbo].[de_tb_noticia]         
        
@cod_noticia int    
        
AS        
        
BEGIN        
   
 DECLARE @cod_imagem INT = (SELECT cod_imagem FROM tb_noticia WHERE cod_noticia = @cod_noticia)       
 
 DELETE FROM tb_noticia WHERE cod_noticia = @cod_noticia  
 DELETE FROM tb_imagem WHERE cod_imagem = @cod_imagem  
    
        
END 
GO
/****** Object:  StoredProcedure [dbo].[in_tb_imagem]    Script Date: 27/05/2018 22:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Helder Felipe>
-- Create date: <17/03/2018>
-- Description:	<Inserir Imagem>
-- =============================================
CREATE PROCEDURE [dbo].[in_tb_imagem] 

@nom_imagem VARCHAR(100),
@ds_imagem VARCHAR(200),
@ds_origem_imagem VARCHAR(100)

AS

BEGIN


	INSERT INTO tb_imagem (nom_imagem, ds_imagem, ds_origem_imagem) VALUES (@nom_imagem, @ds_imagem, @ds_origem_imagem)


END
GO
/****** Object:  StoredProcedure [dbo].[in_tb_noticia]    Script Date: 27/05/2018 22:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Helder Felipe>      
-- Create date: <24/02/2018>      
-- Description: <Inserir Noticia>     
-- Action: Hfvas - 15/04/2018 - Adicao do Campo ds_tipo_noticia e im_vitrine.  
-- Action: Hfvas - 21/04/2018 - Salvando na tb_imagem para ter o cod_imagem e salvar na tb_imagem. 
-- Action: Hfvas - 20/05/2018 - Aumento do parametro im_vitrine de 25 para 100.
-- =============================================      
CREATE PROCEDURE [dbo].[in_tb_noticia]       
  
@nom_titulo_noticia VARCHAR(100),      
@ds_noticia VARCHAR(1000),      
@ds_tipo_noticia VARCHAR(30),    
@im_vitrine VARCHAR(100)  
      
AS      
      
BEGIN      
  
 exec in_tb_imagem @im_vitrine,@ds_tipo_noticia,'tb_noticia'  
  
 DECLARE @cod_imagem INT = (SELECT TOP (1) cod_imagem FROM tb_imagem ORDER BY cod_imagem DESC)  
  
 INSERT INTO     
 tb_noticia (nom_titulo_noticia, ds_noticia, dat_noticia, ds_tipo_noticia, cod_imagem)     
 VALUES     
 (@nom_titulo_noticia, @ds_noticia, GETDATE(), @ds_tipo_noticia, @cod_imagem)      
      
END 
GO
/****** Object:  StoredProcedure [dbo].[se_all_tb_noticia]    Script Date: 27/05/2018 22:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Helder Felipe>      
-- Create date: <21/04/2018>      
-- Description: <Selecionar Todas as Noticia>       
-- =============================================      
CREATE PROCEDURE [dbo].[se_all_tb_noticia]       
  
  AS    
      
BEGIN      
      
 SELECT n.*,i.nom_imagem FROM tb_noticia AS n
 INNER JOIN tb_imagem AS i ON n.cod_imagem = i.cod_imagem 
  
END 
GO
/****** Object:  StoredProcedure [dbo].[se_tb_noticia]    Script Date: 27/05/2018 22:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Helder Felipe>      
-- Create date: <21/04/2018>      
-- Description: <Selecionar com parametros para as Noticias>   
-- Action: Hfvas - 21/04/2018 - Acrescentar INNER JOIN com tb_imagem.        
-- =============================================      
CREATE PROCEDURE [dbo].[se_tb_noticia]       
@nom_titulo_noticia VARCHAR(100),      
@ds_noticia VARCHAR(1000),      
@ds_tipo_noticia VARCHAR(30)  
  AS    
      
BEGIN      
      
 SELECT   
 n.*, i.nom_imagem  
 FROM   
 tb_noticia AS n  
 INNER JOIN tb_imagem AS i ON n.cod_imagem = i.cod_imagem  
 WHERE  
 nom_titulo_noticia like '%' + @nom_titulo_noticia + '%' or  
 ds_noticia like '%' + @ds_noticia + '%' or  
 ds_tipo_noticia like @ds_tipo_noticia  
  
  
END 
GO
/****** Object:  StoredProcedure [dbo].[up_tb_imagem]    Script Date: 27/05/2018 22:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Helder Felipe>  
-- Create date: <21/04/2018>  
-- Description: <Atualizar Imagem>  
-- =============================================  
CREATE PROCEDURE [dbo].[up_tb_imagem]   
  
@cod_imagem INT,
@nom_imagem VARCHAR(100),  
@ds_imagem VARCHAR(200),  
@ds_origem_imagem VARCHAR(100)  
  
AS  
  
BEGIN  
  
  UPDATE 
  tb_imagem 
  SET 
  nom_imagem = @nom_imagem, ds_imagem = @ds_imagem, ds_origem_imagem = @ds_origem_imagem 
  WHERE 
  cod_imagem = @cod_imagem
 
END  
GO
/****** Object:  StoredProcedure [dbo].[up_tb_noticia]    Script Date: 27/05/2018 22:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Helder Felipe>      
-- Create date: <21/04/2018>      
-- Description: <Atualizar Noticia>   
-- Action: Hfvas - 21/04/2018 - Criando um EXEC para alterar na tb_imagem tambem.    
-- Action: Hfvas - 27/05/2018 - Aumento do parametro im_vitrine de 25 para 100.  
-- =============================================      
CREATE PROCEDURE [dbo].[up_tb_noticia]       
  
@cod_noticia INT,      
@nom_titulo_noticia VARCHAR(100),      
@ds_noticia VARCHAR(1000),      
@ds_tipo_noticia VARCHAR(30),    
@im_vitrine VARCHAR(100)   
      
AS      
      
BEGIN      
      
 DECLARE @cod_imagem INT = (SELECT cod_imagem FROM tb_noticia WHERE cod_noticia = @cod_noticia)   
    
 EXEC up_tb_imagem @cod_imagem, @im_vitrine, @ds_tipo_noticia, 'tb_noticia'  
  
 UPDATE   
 tb_noticia   
 SET   
 nom_titulo_noticia = @nom_titulo_noticia,   
 ds_noticia = @ds_noticia,   
 ds_tipo_noticia = @ds_tipo_noticia  
 WHERE  
 cod_noticia = @cod_noticia  
  
END   
  
GO
