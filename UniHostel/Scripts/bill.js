$(document).ready(function () {
    $("select #RoomList").change(function () {
        getPreviousNumber();
    });
    getPreviousNumber();

});

getPreviousNumber = function () {
    var RoomID = $("#RoomList option:selected").val();
    var list = $("input[data-prenum]");
    for (var i = 0; i < list.length; i++) {
        var serviceID = $(list[i]).attr("data-prenum");
        var resource = 'http://localhost:61368/Bills/GetPreviousNumber';
        var result = callAjax(resource, RoomID, serviceID, list[i]);
    }
};

callAjax = function (resource, roomID, serviceID, component) {
    $.ajax({
        url: resource + "?CompServiceId=" + serviceID + "&roomID=" + roomID,
        success: function (result) {
            $(component).val(result);
        },
        error: function (error) {
            console.log(error);
        }
    });
};


function calculateCompulServiceMoney(obj) {
    var curNumList = $(".CurNum");
    var i = $(curNumList).index(obj);
    var curNum = +$(curNumList).eq(i).val();
    var preNum = +$(".PreNum").eq(i).val();
    var price = +$(".unit-price").eq(i).text();
    var amount = (curNum - preNum) * price;
    $(".amount").eq(i).text(amount);
    calculateTotal(null);
}   

function calculateAdvancedServiceMoney(obj) {
    var quantity = +$(obj).val();
    console.log(obj);
    var price = +$(obj).parents('tr').find(".unit-price").text();
    var amount = quantity * price;
    $(obj).parent('td').siblings(".amount").text(amount);
    calculateTotal(null);
}

var calculateTotal = function (obj) {
    var otherFee = 0;
    if (obj != null) {
        otherFee = parseInt(obj.value);
    }
    var amountList = $(".amount");
    var total = otherFee;
    for (var i = 0; i < amountList.length; i++) {
        total += +$(amountList).eq(i).text();
    }
    $(".total").val(total);
}
