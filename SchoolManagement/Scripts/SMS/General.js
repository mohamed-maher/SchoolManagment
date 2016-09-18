$(document).ready(function () {
    SelectIDs();
    $("#txtNumber").kendoNumericTextBox({
        value: 10
    });
});

function SelectIDs() {
    $.ajax({
        url: "/SelectIDs",
        type: "Get",
        data: { },
        success: function (data) {
            alert("Success")
        }, error: function (ex) { }
    });
}
