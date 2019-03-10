
$(document).ready(function () {
    $("#Button1").attr("disabled", true);

    $("#TextBox1").keyup(function () {
        if ($("#TextBox1").val().length > 3) {
            $("#Button1").attr("disabled", false);
        }


    }
    )
})