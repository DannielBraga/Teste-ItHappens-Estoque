var _dropTipoPedido, _dropEstoque, _dropProduto, _txtQuantidade, _btnAddProduto, _btnSalvarPedido;
var v_lista_produtos = new Array();

$(function () {

    fnConfigurarTelaInicial();
    fnVerificarEvento();

    fnCarregarDropsIniciais();
});

function fnConfigurarTelaInicial() {

    _dropTipoPedido = $("#dropTipoPedido");
    _dropFilial = $("#dropFilial");
    _dropProduto = $("#dropProduto");
    _txtQuantidade = $("#txtQuantidade");
    _btnAddProduto = $("#btnAddProduto");
    _btnSalvarPedido = $("#btnSalvarPedido");
}

function fnVerificarEvento() {

    _dropTipoPedido.change(function () {
        if (_dropFilial.val() != 0 && _dropTipoPedido.val() != 0) {
            fnCarregarDropProdutoFilial(_dropFilial.val(), _dropTipoPedido.val());
        }
    });

    _dropFilial.change(function () {
        if (_dropFilial.val() != 0 && _dropTipoPedido.val() != 0) {
            fnCarregarDropProdutoFilial(_dropFilial.val(), _dropTipoPedido.val());
        }
    });

    _btnAddProduto.click(function () {
        if (!fnVerificarCampos()) return false; 
        else fnAdicionarProdutoItem();
        
    });

    _btnSalvarPedido.click(function () {
        fnSalvarPedido();
    });

}

function fnCarregarDropsIniciais() {
    fnCarregarDropTipoPedido();
    fnCarregarDropFilial();
}

function fnCarregarDropTipoPedido() {
    _objWs.service = "TipoPedidoEstoqueWs.asmx";
    _objWs.metodo = "carregarDropTipoPedido";
    _objWs.data = "{}";
    _objWs.ajaxWs(function (response, status) {
        if (status == "success") {
            _dropTipoPedido.html(response.d);
        } else
            fnWsError(response);
    });   

}

function fnCarregarDropFilial() {
    _objWs.service = "EstoqueWs.asmx";
    _objWs.metodo = "carregarDropEstoqueFilial";
    _objWs.data = "{}";
    _objWs.ajaxWs(function (response, status) {
        if (status == "success") {
            _dropFilial.html(response.d);
        } else
            fnWsError(response);
    }); 
}

function fnCarregarDropProdutoFilial(p_codfilial, p_tipopedido) {
    _objWs.service = "ProdutoWs.asmx";
    _objWs.metodo = "carregarDropProdutoFilial";
    _objWs.data = "{'p_codfilial': " + p_codfilial + ", 'p_tipopedido': " + p_tipopedido+"}";
    _objWs.ajaxWs(function (response, status) {
        if (status == "success") {
            _dropProduto.html(response.d);
            $(".divProdutos").fadeIn("fast");
        } else
            fnWsError(response);
    });
}

function fnAdicionarProdutoItem() {

    var v_itempedidoestoque = new Object();
    v_itempedidoestoque.CodProduto = _dropProduto.val();
    v_itempedidoestoque.Quantidade = _txtQuantidade.val();
    v_lista_produtos.push(v_itempedidoestoque);
    
    var v_linha = "";

    v_linha += "<tr>";
    v_linha += "    <td>" + $("#dropProduto option:selected").text() + "</td>";
    v_linha += "    <td>" + _txtQuantidade.val() + "</td>";
    v_linha += "</tr>";

    $(".tableProdutos").append(v_linha);
    $(".tbProdutos").fadeIn("fast");

}

function fnVerificarCampos() {
    if (_dropProduto.val() == 0) {
        alert("Selecione um Produto.");
        return false;
    }

    if (_txtQuantidade.val() == "" || _txtQuantidade.val() == 0) {
        alert("Informe a quantidade.");
        return false;
    }

    for (var i = 0; i < v_lista_produtos.length; i++) {
        if (v_lista_produtos[i].CodProduto == _dropProduto.val()) {
            alert("Este Produto já foi adicionado. Selecione outro.");
            return false;
        }
    }

    return true;
}

function fnSalvarPedido() {
    if (v_lista_produtos.length == 0) {
        alert("Não há nada para salvar!");
        return false;
    }

    if (!confirm("Tem certeza que deseja salvar os dados?")) return false;

    var dto = { "v_lista_itempedidoestoque": v_lista_produtos };

    _objWs.service = "ItemPedidoEstoqueWs.asmx";
    _objWs.metodo = "salvarListaItemPedidoEstoque";
    _objWs.data = JSON.stringify(dto);
    _objWs.ajaxWs(function (response, status) {
        if (status == "success") {
            alert("O Pedido foi realizado com sucesso!");
            window.location.reload();
        } else
            fnWsError(response);
    });
}
