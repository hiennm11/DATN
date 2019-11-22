$(function () {
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#user_img').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#profile-img").change(function () {
        readURL(this);
    });

    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-otf-target"));
            $target.replaceWith(data);
        });

        return false;
    };

    
    var getPage = function () {
        var $a = $(this);
        var $sk = $('#search_key').serialize;
        var options = {
            url: $a.attr("href"),
            data: $sk,
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-otf-target");
            $(target).replaceWith(data);
        });
        return false;

    };
    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);
    $(".body-content").on("click", ".pagedList a", getPage);


    $("body").on("click", ".thongtinDH td", function () {
        var customerId = $(this).closest("tr").find("td:first").html();
        window.location = "/User/ChiTietDonHang/" + customerId;
    });

    $('.txtSoLuong').change(function (e) {
        var $form = $("#CapNhatGH");        
        e.preventDefault();
        $form.submit();        
    });

});