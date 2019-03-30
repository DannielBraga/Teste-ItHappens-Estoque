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

        <div class="row" style="margin-top:30px; margin-left:-15px;">
            <div class="col-sm-3">
                <label>Tipo de Pedido</label>
                <select id="dropTipoPedido" class="form-control"></select>
            </div>
            <div class="col-sm-4">
                <label>Filial do Estoque</label>
                <select id="dropFilial" class="form-control"></select>
            </div>
        </div>

        <div class="row divProdutos" style="margin-top:30px; margin-left:-15px; display:none;">
            <div class="col-sm-3">
                <label>Produto</label>
                <select id="dropProduto" class="form-control">                    
                </select>
            </div>
            <div class="col-sm-3">
                <label>Quantidade</label>
                <input type="text" id="txtQuantidade" class="form-control"/>
            </div>
            <div class="col-sm-1">
                <button type="button" id="btnAddProduto" style="margin-top:24px;" class="btn btn-primary">Adicionar</button>
            </div>
        </div>

        <div class="row tbProdutos" style="margin-top:30px; margin-left:-15px; display:none;">
            <div class="col-sm-8">
                <table class="table table-striped tableProdutos">
                    <thead>
                      <tr>
                        <th>Descrição do Produto</th>
                        <th>Quantidade</th>                    
                      </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>

        <br />
        <div class="row tbProdutos" style="margin-top:30px; margin-left:-0px; display:none;">
            <button type="button" id="btnSalvarPedido" style="margin-top:24px;" class="btn btn-success">Salvar Pedido</button>
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
