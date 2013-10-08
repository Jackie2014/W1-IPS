// Check or uncheck to select/unselect the related record.
function ResetCheckBoxItem() {
    var $hidGridSe = $("#hidGridSelectedIds");
    var selIds = $hidGridSe.val();
    if (selIds != null && selIds != "") {
        var selIds_array = selIds.split(",")
        var newString = "";
        for (var i = 0; i < selIds_array.length; i++) {
            var selval = selIds_array[i];
            $selObj = $("[fieldvalue=" + selval + "]");
            if ($selObj.length > 0) {
                if (newString == "") {
                    newString = selval;
                }
                else {
                    newString += "," + selval;
                }

                $selObj.attr("checked", true);
            }
        }

        $hidGridSe.val(newString);
    }
}

function actionSelectItem() {
    var newSelected = "";
    $("[selectcheckbox=true]").each(function () {
        var $chkItem = $(this);
        if ($chkItem.attr("checked")) {
            if (newSelected == '') {
                newSelected = $chkItem.attr("fieldvalue");
            }
            else {
                newSelected = newSelected + "," + $chkItem.attr("fieldvalue");
            }
        }
    });

    $("#hidGridSelectedIds").val(newSelected);
}

$(function () {
    ResetCheckBoxItem();
    $("#chkAllSelected").click(function () {
        $("[selectcheckbox=true]").attr("checked", $(this).attr("checked"));
        actionSelectItem();
    });

    $("[selectcheckbox=true]").click(function () {
        actionSelectItem();
    });
});

// Change order rule.
function changeOrder(id) {
    var $obj = $(id);
    var orderField = $obj.attr("fieldname");
    var orderBy = "";
    //var currFieldOrderFlag = $obj.next().html();    
    if ($obj.attr("sort") == 'up') {
        orderBy = 'DESC';
        $obj.attr("sort", "down");
        $obj.attr("class", "hrf-sort-down");
    }
    else {
        orderBy = 'ASC';
        $obj.attr("sort", "up");
        $obj.attr("class", "hrf-sort-up");
    }
    $("#hidSortFieldName").val(orderField);
    $("#hidSortFieldOrderBy").val(orderBy);

    // Reset other sort field.
    $("[sortable=true]").each(function () {
        var $hrf_tem = $(this);
        if ($hrf_tem.attr("fieldname") != orderField) {
            $hrf_tem.next().html('');
        }
    });

    $('#btnSortInGrid').click();
}

// When page loaded, set sort column order flag.
$(function () {
    $("[sortable=true]").each(function () {
        var $hef_tem = $(this);
        $hef_tem.click(function () {
            changeOrder(this);
        });
        var fieldName = $("#hidSortFieldName").val();
        var orderBy = $("#hidSortFieldOrderBy").val();

        if (fieldName == $hef_tem.attr("fieldname")) {
            if (orderBy == "ASC") {
                $hef_tem.attr("sort", "up");
                $hef_tem.attr("class", "hrf-sort-up");
            }
            else {
                $hef_tem.attr("sort", "down");
                $hef_tem.attr("class", "hrf-sort-down");
            }
        }
    });
});

// When page loaded, set 'current page no' text box disabled.
$(function () {
    $('.currentpage').attr("disabled","disabled");
});

// Disable navigate function when page count equals to zero and one.
$(function () {
    var totalPage = $('#lblTotalPage').html();
    var currentPage = $('.mobile-grid-tb-paging-txt-currentpage').val();

    // No data or total page number equal to one.
    if (totalPage == '0' || totalPage == '1') {
        $(".mobile-grid-tb-paging a").each(function () {
            disableNavigate(this);
        });
    }

    // Last page.
    if (totalPage > '1' && totalPage == currentPage) {
        $(".mobile-grid-tb-paging a").each(function (i) {
            if (i == 2 || i == 3) {
                disableNavigate(this);
            }
        });
    }

    // First page.
    if (totalPage > '1' && currentPage == '1') {
        $(".mobile-grid-tb-paging a").each(function (i) {
            if (i == 0 || i == 1) {
                disableNavigate(this);
            }
        });
    }
});

function disableNavigate(linkId) {
    $(linkId).attr("onclick", "");
    $(linkId).attr("style", "color:gray");
    $(linkId).css({ "text-decoration": "none", "cursor": "default" });
}

// Disable sort function when record count equals to zero.
$(function () {
    if ($('#lblTotalPage').html() == '0') {
        $("#btnsort").bind({ click: function () { return false; }, dblclick: function () { return false; } });
    }
    else {
        $("#btnsort").bind({ click: changeOrder });
    }
});
