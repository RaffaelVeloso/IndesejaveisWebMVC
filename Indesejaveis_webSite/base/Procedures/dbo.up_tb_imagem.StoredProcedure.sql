USE [base_indesejaveis]
GO
/****** Object:  StoredProcedure [dbo].[up_tb_imagem]    Script Date: 27/05/2018 21:59:44 ******/
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
