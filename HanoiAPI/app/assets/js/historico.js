$(function () {
    $.get('/api/log/historico', function (data) {
        $.each(data, function (i, e) {
            $("table tbody").append("<tr><td>" + e.NomeJogo + "</td><td>" + e.QuantidadeDeDiscos + "</td><td>" + e.DataInicio + "</td><td>" + e.DataFim + "</td></tr>");
        });

        if (data != null && data.length > 0) {
            $(".btn-slack").show();
        } else {
            $(".btn-slack").hide();
        }
    });

    $(".btn-slack").click(function () {
        $.post("/api/integration/slack/historico", function () {
            $(".btn-slack").text("Enviado");
            $(".btn-slack").prop("disabled", true);
        });
    });
});