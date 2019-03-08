function _getAll() {
    $.ajax({
        url: "/Home/GetAll",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<div class="product-box col-md border border-warning rounded mr-2 mb-1">';
                html += '<div class="product-info-down w-100 rounded-top p-4">';
                html += '<p class="h5 text-light">Tướng:' + item.tuong + '</p>';
                html += '<p class="h5 text-light">Trang phục:' + item.skin + '</p>';
                html += '<p class="h5 text-light">Bảng ngọc: ' + item.ngoc + '</p>';
                html += '<p class="h5 text-light">Hạng cao nhất:' + item.hang + '</p>';
                html += '</div>';
                html += '<div class="product mt-2">';
                html += '<a href="#" title=""><img class="img-fluid" src="/Content/Images/' + item.img + '"' + 'alt=""></a>';
                html += '<p class="h3 mt-4 text-center font-weight-bold text-danger">' + NumberFormat(item.gia) + '<sup>đ</sup></p>';
                html += '<div class="row my-2">';
                html += '<div class="col">';
                html += '<a href="#" class="btn btn-outline-success w-100" onclick="getDetail(\''+ item.id +'\')">Xem</a>';
                html += '</div>';
                html += '<div class="col">';
                html += '<a href="#" class="btn btn-outline-primary w-100">Mua ngay</a>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
            });
            $('#home-body').html(html);
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


function NumberFormat(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}

//function getDetails(str) {
//    alert("xin chao" + str);

//    //$.ajax({6
//    //    url: "/Home/GetDetails/" + id,
//    //    type: "GET",
//    //    contentType: "application/json; charset=utf-8",
//    //    dataType: "json",
//    //    success: function (result) {
//    //        var html = "";
//    //        html += '<p class="h4 d-inline text-warning">Mã: </p><p class="h4 d-inline font-weight-bold text-danger">' + result.id + '</p><br>';
//    //        html += '<p class="h6"> Tướng:' + result.tuong + '</p>';
//    //        html += '<p class="h6"> Trang phục:' + result.skin + '</p>';
//    //        html += '<p class="h6"> Bảng ngọc:' + result.ngoc + '</p>';
//    //        html += '<p class="h6"> Bậc cao nhất:' + result.hang + '</p>';
//    //        html += '<p class="h6"> Rank hiện tại:' + result.hang1 + '</p>';
//    //        html += '<p class="h6"> Clan:' + result.clan + '</p>';
//    //        $('#home-body').html(html);
//    //    },
//    //    error: function (errormessage) {
//    //        alert(errormessage.responseText);
//    //    }
//    //});
//}


function getLogin() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('modal-body').innerHTML = this.responseText;
        }
    };
    xhttp.open("GET", "/Home/getLogin", true);
    xhttp.send();
}

$(document).ready(function () {
    $('#btn-login').click(function () {
        var val = $('.form-control').val();
        if (val == "") $('.text-danger').html('<p>Chưa nhập tên đăng nhập</p>');
    });
})