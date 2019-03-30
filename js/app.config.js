var _objWs = {

    baseWsUrl: defaults.SiteRootUrl,
    service: "",
    metodo: "",
    data: "",
    async: true,

    ajaxWs: function (callback) {
        $.ajax({
            type: "POST",
            url: this.baseWsUrl + "ws/" + this.service + "/" + this.metodo,
            data: this.data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: this.async,
            success: function (response, status) {
                callback.call(this, response, status);
            },
            error: function (response, status, e) {
                callback.call(this, response, status);
            }
        });
    },

    getService: function () { return this.service; },
    getMetodo: function () { return this.metodo; },
    getData: function () { return this.data; }

};

function fnWsError(xhr) {
    if (xhr.responseText == "" && xhr.statusText == "error") {
        alert("Você esta sem acesso a internet.");
    } else if (xhr.status == 500) {
        var err = JSON.parse(xhr.responseText);
        if (err === undefined)
            alert("Ocorreu um erro interno de servidor.");
        else
            alert(err.Message);
    } else {
        var err = JSON.parse(xhr.responseText);
        alert(err.Message);
    }
}