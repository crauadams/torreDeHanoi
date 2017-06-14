$(function () {

    $(document).on("click", "table tbody a", function () {
        var url = $(this).attr("data-url");
        var methodType = $(this).attr("data-method");

        if (methodType === "GET") {
            $.get(url, handleRetorno);
        } else if (methodType === "POST") {
            $.post(url, handleRetorno);
        } else if (methodType === "PUT") {
            $.ajax({
                url: url,
                type: 'PUT',
                success: handleRetorno
            });
        }
    });
});

function handleRetorno(data) {
    $('textarea').text(JSON.stringify(data, null, "\t"));
}