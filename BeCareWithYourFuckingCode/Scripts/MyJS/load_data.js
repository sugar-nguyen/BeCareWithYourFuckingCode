function _getAll(num) {
    $.ajax({
        url: "/Home/GetAll/" + num,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            var tmp;
            result = check_data_enough(result);
            var htmlstring = htmlString(result, html);
            $('#home-body').html(htmlstring);
            $('.product-box').hover(function (e) {
                e.preventDefault();
                $(this).find('.product-info-down').first().stop(true, true).slideDown(150);
            }, function () {
                $(this).find('.product-info-down').first().stop(true, true).slideUp(105)
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function check_data_enough(result) {
    if (result.length % 4 == 0)
        return result;
    else {
        var length = result.length % 4;
        result.length -= length;
    }
    return result;
}

function NumberFormat(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}


function htmlString(result, html) {
    $.each(result, function (key, item) {
        if (key == 0 || key % 4 == 0) {
            html += '<div class = "row mb-2">';
            tmp = 1;
        }
        html += '<div class="product-box col-md col-lg col-sm border border-warning rounded mr-2 mb-1">';
        html += '<div class="product-info-down w-100 rounded-top p-4">';
        html += '<p class="h5 text-warning">Tướng:' + item.tuong + '</p>';
        html += '<p class="h5 text-warning">Trang phục:' + item.skin + '</p>';
        html += '<p class="h5 text-warning">Bảng ngọc: ' + item.ngoc + '</p>';
        html += '<p class="h5 text-warning">Hạng cao nhất:' + item.hang + '</p>';
        html += '</div>';
        html += '<div class="product mt-2">';
        html += '<a href="#" title=""><img class="img-fluid" src="/Content/Images/' + item.img + '"' + 'alt=""></a>';
        html += '<p class="h3 mt-4 text-center font-weight-bold text-danger">' + NumberFormat(item.gia) + '<sup>đ</sup></p>';
        html += '<div class="row my-2">';
        html += '<div class="col">';
        html += '<a href="/GameAccount/getDetail/' + item.id + '" class="btn btn-outline-success w-100" >Xem</a>';
        html += '</div>';
        html += '<div class="col">';
        html += '<a href="/Deal/BuyNow/'+item.id+'" class="btn btn-outline-primary w-100">Mua</a>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        tmp++;
        if (tmp == 5) html += '</div>';
    });

    return html;
}
