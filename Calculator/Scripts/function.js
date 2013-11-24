

$(document).ready(function () {
    $(".js-variable-change").change(function () {
        var selectedVal = $(this).val();
        VariableRender(selectedVal);
        $(".js-result").html("");
    });

    $(".js-main-form").bind('submit', function (e) {
        SendValues(e);
        e.preventDefault();
        return false;
    });
});


function VariableRender(variableNumber) {
    $(".js-line-conatiner").html("");

    for (var i = 0; i < variableNumber ; i++){
        $(".js-line-conatiner").append("<li>");
        for (var j = 0; j < variableNumber; j++) {
            if (j > 0) {
                $(".js-line-conatiner").append(" + ");
            }
            $(".js-line-conatiner").append("<input class=\"text-input\" maxlength=\"14\" id=\"CoefficientMatrix_" + i + "__" + j + "_\" name=\"CoefficientMatrix[" + i + "][" + j + "]\" pattern=\"([+-])?\\d+(\\.)?\\d*\" placeholder=\"Digits, or dot, or -\" required=\"required\" type=\"text\" value=\"\"> x<sub>" + (j + 1) + "</sub>");  
        }
        $(".js-line-conatiner").append(" = <input class=\"text-input\" maxlength=\"14\" id=\"FreeMembers_" + i + "_\" name=\"FreeMembers[" + i + "]\" pattern=\"([+-])?\\d+(\\.)?\\d*\" placeholder=\"Digits, or dot, or -\" required=\"required\" type=\"text\" value=\"\"></li>");
    }
}

function SendValues(e) {

    var form = $(".js-main-form");

    var coefs = new Array();
    var freeCoefs = new Array();

    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        traditional: true,
        success: function (response) {
            $(".js-result").html(response);
        }
    });

    e.preventDefault();
    return false;
}