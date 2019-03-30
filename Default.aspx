<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Easy Estoque - Sistema de Gestão de Estoque</title>

    <!-- Bootstrap -->
    <link href="App_Themes/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Layout da tela -->
    <link href="App_Themes/estoque/estoque.css" rel="stylesheet" />
</head>
<body>

    <script type="text/javascript">
        var defaults = {
            SiteRootUrl: '<%=clsFuncoes.SiteRootUrl() %>',
             SiteNome: '<%=clsFuncoes.SiteNome() %>',
             SiteDesc: '<%=clsFuncoes.SiteDesc() %>'
         };
    </script>


    <div class="row" style="margin-left:30px;">

        <h2>Bem-vindo ao Easy Estoque!</h2>
        <br />

        <h3>Realize seu Pedido de Estoque abaixo.</h3>

        <div class="row" style="margin-top:30px; margin-left:0px;">
            <div class="col-sm-4">
                <label>Tipo de Pedido</label>
                <select id="dropTipoPedido">                    
                </select>
            </div>
            <div class="col-sm-4">
                <label>Filial do Estoque</label>
                <select id="dropEstoque">                    
                </select>
            </div>
        </div>

    </div>

    <!-- jQuery -->
    <script src="js/jquery/jquery-2.1.3.min.js"></script>
    <!-- Bootstrap -->
    <script src="js/bootstrap/bootstrap.min.js"></script>
    
    <script src="js/app.config.js"></script>
    <script src="js/estoque/estoque.js"></script>

</body>
</html>
