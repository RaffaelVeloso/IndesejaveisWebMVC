USE [base_indesejaveis]
GO
/****** Object:  StoredProcedure [dbo].[de_tb_noticia]    Script Date: 27/05/2018 21:59:44 ******/
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
