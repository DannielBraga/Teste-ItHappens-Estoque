var _dropTipoPedido, _dropEstoque;

$(function () {

    fnConfigurarTelaInicial();
    fnVerificarEvento();

    fnCarregarDropsIniciais();
});

function fnConfigurarTelaInicial() {

    _dropTipoPedido = $("#dropTipoPedido");
    _dropEstoque = $("#dropEstoque");
}

function fnVerificarEvento() {

}

function fnCarregarDropsIniciais() {
    fnCarregarDropTipoPedido();
    fnCarregarDropEstoque();
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

function fnCarregarDropEstoque() {
    _objWs.service = "EstoqueWs.asmx";
    _objWs.metodo = "carregarDropEstoqueFilial";
    _objWs.data = "{}";
    _objWs.ajaxWs(function (response, status) {
        if (status == "success") {
            _dropEstoque.html(response.d);
        } else
            fnWsError(response);
    }); 
}

