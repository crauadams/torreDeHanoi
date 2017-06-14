var discos = [
    "<div class='disco azul {posicao}' data-id=1>&nbsp;</div>",
    "<div class='disco branco {posicao}' data-id=2>&nbsp;</div>",
    "<div class='disco vermelho {posicao}' data-id=3>&nbsp;</div>",
    "<div class='disco verde {posicao}' data-id=4>&nbsp;</div>",
    "<div class='disco laranja {posicao}' data-id=5>&nbsp;</div>",
    "<div class='disco roxo {posicao}' data-id=6>&nbsp;</div>",
    "<div class='disco bordo {posicao}' data-id=7>&nbsp;</div>"
];

var countIntervals = [];
var elapsedTimeIntervals = {};

$(function () {
    var game = $.connection.gameHub;

    game.client.moverDisco = moverDisco;

    $.connection.hub.start().done(function () {
        console.log("Conectado...");
    });

    $('#btn-iniciar').click(function (e) {
        $.post('/api/game/start-game/' + document.getElementById('qtd-disco').value, function (game) {
            buildGameTemplate(game);
        });
    });

    $.get('/app/user-control/log-movimento.html', function (template) {
        $("#report").empty();
        $("#report").append(template);
    });


    $(document).on('click', '.unordered.list.icon', function (e) {
        getLogMovimento($(this).attr("data-id"));
    });
});


function moverDisco(movimento, qtdMovimentosExecutados) {
    var disco = $(".game[data-id=" + movimento.GameId + "]" + " .disco[data-id=" + movimento.DiscoId + "]");
    var classes = (disco !== undefined ? disco.attr("class").split(' ') : []);
    var torreDestino = $(".game[data-id=" + movimento.GameId + "] .torres[data-id=" + movimento.DestinoId + "] .vertical");
    var posicao = torreDestino.parent().find(".disco").length + 1;

    for (var i = 0; i < classes.length; i++) {
        if (classes[i].indexOf('torre_') !== -1 || classes[i].indexOf('posicao_') !== -1) {
            disco.removeClass(classes[i]);
        }
    }

    disco.addClass("torre_" + movimento.DestinoId);
    disco.addClass("posicao_" + posicao);
    torreDestino.after(disco.detach());

    $(".game[data-id='" + movimento.GameId + "'] .qtd-mov-realizado").text(qtdMovimentosExecutados);

    var movimentosNecessarios = parseInt($(".game[data-id='" + movimento.GameId + "'] .qtd-mov-necessario").text());

    if (qtdMovimentosExecutados >= movimentosNecessarios) {
        var intervalId = getIntervalForGameId(movimento.GameId);

        if (intervalId !== undefined)
            clearInterval(intervalId);

        $(".game[data-id='" + movimento.GameId + "'] .contador").append("<i class='unordered list icon hand' data-id=" + movimento.GameId + ">&nbsp;</i>")
    }
}

function buildGameTemplate(game) {
    $.get('/app/user-control/game.html', function (template) {
        var html = template.replace('{0}', game.Id);
        $("#games").append(html);

        $(".game[data-id='" + game.Id + "'] .game-title").text(game.Nome);
        $(".game[data-id='" + game.Id + "'] .contador").text('3');
        $(".game[data-id='" + game.Id + "'] .jogo-id").text(game.Id);
        $(".game[data-id='" + game.Id + "'] .qtd-mov-necessario").text(game.QuantidadeDeMovimentosNecessarios);

        var torre = $(".game[data-id='" + game.Id + "'] .torres .vertical").first();

        for (var i = game.Torres[0].Discos.length; i > 0 ; i--) {
            var disco = discos[i - 1];
            torre.after(disco.replace("{posicao}", "posicao_" + ((game.Torres[0].Discos.length - i) + 1)));
        }

        var intervalId = setInterval(function () {
            var span = $("#games .game[data-id=" + game.Id + "] .contador");
            var count = parseInt(span.text());
            count = count - 1;
            span.text(count);

            if (count <= 0) {
                clearInterval(countIntervals[0]);
                countIntervals.splice(0, 1);

                $.ajax({
                    url: '/api/game/mover-disco/game-id/' + game.Id,
                    type: 'PUT',
                    success: function (result) {
                        span.text('');
                        var startTime = Date.now();

                        var interval = setInterval(function () {
                            var elapsedTime = Date.now() - startTime;
                            $(".game[data-id='" + game.Id + "'] .tempo-decorrido").text((elapsedTime / 1000).toFixed(3));
                        }, 100);

                        elapsedTimeIntervals[game.Id] = interval;
                    }
                });
            }
        }, 1000);

        countIntervals.push(intervalId);
    });
}


function getIntervalForGameId(id) {
    var intervalId;

    for (var key in elapsedTimeIntervals) {
        if (key == id) {
            intervalId = elapsedTimeIntervals[key];
            break;
        }
    }

    return intervalId;
}

function getCorDisco(id) {
    switch (id) {
        case 1:
            return "azul";
        case 2:
            return "branco";
        case 3:
            return "vermelho";
        case 4:
            return "verde";
        case 5:
            return "laranja";
        case 6:
            return "roxo";
        case 7:
            return "bordo";
    }
}

function getLogMovimento(gameId) {
    $.get('/api/log/movimento/' + gameId, function (result) {
        $.each(result, function (i, e) {
            $("table tbody").append("<tr><td><div class='log-disco " + getCorDisco(e.Movimento.DiscoId) + "'>&nbsp;</div></td><td>" + e.Movimento.OrigemId + "</td><td>" + e.Movimento.DestinoId + "</td></tr>");
        });
        $('.ui.modal').modal('show');
    });
}