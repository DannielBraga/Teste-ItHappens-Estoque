var _dropTipoPedido, _dropEstoque, _dropProduto, _txtQuantidade, _btnAddProduto;

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

