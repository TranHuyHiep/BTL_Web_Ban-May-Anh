


function loadData() {
    $.ajax({
        url: 'https://localhost:44368/shoppingcart/getall',
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            console.log(res)
            var str = '';
            var count = 0;
            res.forEach(function (item) {
                count += 1;
                var money = item.product.giaLonNhat * item.quantity;
                str +=
                    `<tr>
                            <td>${count}</td>
                            <td><img src="../img/ImageCamera/${item.product.anhDaiDien}" alt="" style="width: 50px;"></td>
                            <td>${item.product.tenSp}</td>
                            <td class="align-middle">${item.product.giaLonNhat}</td>
                            <td class="align-middle">
                                <div class="input-group quantity mx-auto" style="width: 100px;">
                                    <div class="input-group-btn">
                                        <button class="btn btn-sm btn-primary btn-minus">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                    <input type="text" class="quantity form-control form-control-sm bg-secondary border-0 text-center" value="${item.quantity}
                                       onclick="changeMoney()">
                                    <div class="input-group-btn">
                                        <button class="btn btn-sm btn-primary btn-plus" onclick="changeMoney()">
                                            <i class="fa fa-plus"></i>
                                        </button>
                                    </div>
                                </div>
                            </td>
                            <td class="align-middle" id="amount_${count}">${money}</td>
                            <td class="align-middle"><button class="btn btn-sm btn-danger"><i class="fa fa-times"></i></button></td>
                        </tr>`
            })

            if (count == 0) {
                str = "<tr><td><p>Giỏ hàng trống</p></td></tr>"
            }

            $('#showCart').html(str);
        }
    })
}

loadData();

function btnDeleteAll() {
    $.ajax({
        url: 'https://localhost:44368/shoppingcart/DeleteAll',
        type: 'POST',
        dataType: 'json',
        success: function (res) {
            if (res.status) {
                alert('Đã xóa giỏ hàng!');
            }
            loadData();
        }
    })
}

function btnContinue() {
    window.location.href = "";
}

function btnCheckout() {
    var hoaDonBan = {
        TongTienHD: 111,
        PhuongThucThanhToan: 0,
        GhiChu: 'cc',
        TrangThai: 0,
        MaKhachHang: 'N01'
    }

    $.ajax({
        url: 'https://localhost:44368/shoppingcart/CreateOrder',
        type: 'POST',
        dataType: 'json',
        data: {
            orderViewModel: JSON.stringify(hoaDonBan)
        },
        success: function (res) {
            if (res.status) {
                alert("Mua hàng thành thành công!")
            }
            btnDeleteAll();
        }
    })

}