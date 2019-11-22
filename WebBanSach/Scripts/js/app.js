
var Card = {
    init: function () {
        this.handleEvents();
    },
    handleEvents: function () {
        var bdElm = $('body'), currObj = this;
        
        bdElm.on('click', '.btn-add-to-cart', function () {
            var url = $(this).data('url');
            var type = $(this).data('type');

            $.ajax({
                url: url,
                type: 'POST',
                data: {type: type},
                success: function (res) {
                    res = JSON.parse(res);
                    if (res.success) {
                        
                        showNotification('alert-success', res.message);
                    } else {
                        showNotification('alert-danger', res.message);
                    }
                },
                error: function () {
                    console.log('Request add to card error!')
                }
            })
        });

        // Click btn remove item cart
        bdElm.on('click', '.btn-remove-book', function () {
            var _this = $(this);
            var id = _this.data('id');
            var url = _this.data('url');

            $.ajax({
                url: url,
                type: 'POST',
                data: {id: id},
                success: function (res) {
                    console.log(res);
                    res = JSON.parse(res);
                    if (res.success) {
                        $('.cart-item').html(res.item_count);
                        $('.items-card').html(res.item_count);
                        _this.closest('.cart__item').remove();
                        showNotification('alert-success', res.message)
                    } else {
                        showNotification('alert-danger', res.message);
                    }
                },
                error: function () {
                    console.log('Error request remove item cart');
                }
            })


        })
    }

};

var ValidateForm = {
    init: function () {
        this.RegisterFrm();
        this.CustomerFrm();
    },
    RegisterFrm: function () {
        $('.register-form').validate({
            rules: {
                fullname: {
                    required: true,
                    minlength: 3,
                    maxlength: 50
                },
                username: {
                    required: true,
                    minlength: 3,
                    maxlength: 50
                },
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    minlength: 9,
                    maxlength: 13
                },
                password: {
                    required: true,
                    minlength: 6,
                    maxlength: 30
                },
                rpassword: {
                    required: true,
                    minlength: 6,
                    maxlength: 30,
                    equalTo: '#password'
                }
            }
        });
    },
    CustomerFrm: function () {
        $('#customer-info-frm').validate({
            rules: {
                fullname: {
                    required: true,
                    minlength: 3,
                    maxlength: 50
                },
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    minlength: 9,
                    maxlength: 13
                },
                address: {
                    required: true
                }
            }
        });
    }
};

var Book = {
    init: function () {
        this.handleEvents();
    },
    handleEvents: function () {
        $('body').on('click', '.btn-download', function () {
            console.log($(this).data('url'));
            $.ajax({
                url: $(this).data('url'),
                type: "POST",
                success: function (res) {
                    console.log(res);
                },
                error: function () {
                    console.log('Error request download book');
                }
            })
        });

        $('body').on('click', '.btn-check-show', function () {
            showNotification('alert-success', 'Pank đẹp trai quá :))))');
        });

        // Rating
        $('.box-rating i').hover(
            function () {
                var $this = $(this);
                var star_item = $('.box-rating').find('i');
                $.each(star_item, function () {
                    if ($(this).data('num') <= $this.data('num')) {
                        $(this).addClass('hover');
                    }
                })
            }, function () {
                var star_item = $('.box-rating').find('i');
                $.each(star_item, function () {
                    $(this).removeClass('hover');
                })
            }
        );

        $('body').on('click', '.box-rating i', function () {
            var $this = $(this);
            $(this).parent().find('.rate-num').val($(this).data('num'));

            var star_item = $('.box-rating').find('i');
            $.each(star_item, function () {
                $(this).removeClass('hover, selected');

                if ($(this).data('num') <= $this.data('num')) {
                    $(this).addClass('selected');
                }
            })
        });

        //$('body').on('click', '.btn-rating', function () {
        //    var $this = $(this);
        //    var rating = $this.closest('.rating');
        //    var rate_num = rating.find('.rate-num').val();
        //    var rate_content = rating.find('.rate-content').val();
        //    var error = rating.find('.error');
        //    error.html('');

        //    console.log(rate_num.length);
        //    if (rate_content.length <= 0 || rate_num == 0) {
        //        error.append('Vui lòng chọn số Sao hoặc nhập nội dung đánh giá');
        //    } else {
        //        $this.css('cursor', 'wait');
        //        $this.prop('disabled', true);
        //        $.ajax({
        //            url: $(this).data('url'),
        //            type: 'post',
        //            data: { rate_content: rate_content, rate_num: rate_num},
        //            success: function (res) {
        //                res = jQuery.parseJSON(res);

        //                console.log(res);
        //                if (res.success) {
        //                    showNotification('alert-success', res.message);
        //                    location.reload();
        //                } else {
        //                    error.append(res.message);
        //                }
        //            },
        //            error: function () {
        //                console.log('error rating book!')
        //            }
        //        })
        //    }
        //});

        $('body').on('click', '.radio-disable', function () {
            showNotification('alert-success', 'Phương thức thanh toán này hiện chưa có!')
        });

        $( ".img-user-small" ).hover(
            function() {
                $(this).find('label').css('display', 'block');
            }, function() {
                $(this).find('label').css('display', 'none');
            }
        );

        $('body').on('click', '.view-book', function () {
            $.ajax({
                url: $(this).data('url'),
                type: 'POST',
                success: function (res) {
                    console.log('view book success');
                },
                error: function () {
                    console.log('view book error');
                }
            })
        })
    }
};

function showNotification(colorName, text, placementFrom, placementAlign, animateEnter, animateExit) {
    if (colorName === null || colorName === '') { colorName = 'bg-black'; }
    if (text === null || text === '') { text = 'Turning standard Bootstrap alerts'; }
    if (animateEnter === null || animateEnter === '') { animateEnter = 'animated fadeInDown'; }
    if (animateExit === null || animateExit === '') { animateExit = 'animated fadeOutUp'; }
    var allowDismiss = true;

    $.notify({
            message: text
        },
        {
            type: colorName,
            allow_dismiss: allowDismiss,
            newest_on_top: true,
            timer: 700,
            placement: {
                from: placementFrom,
                align: placementAlign
            },
            animate: {
                enter: animateEnter,
                exit: animateExit
            },
            template: '<div data-notify="container" class="bootstrap-notify-container alert alert-dismissible {0} ' + (allowDismiss ? "p-r-35" : "") + '" role="alert">' +
            '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
            '<span data-notify="icon"></span> ' +
            '<span data-notify="title">{1}</span> ' +
            '<span data-notify="message">{2}</span>' +
            '<div class="progress" data-notify="progressbar">' +
            '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
            '</div>' +
            '<a href="{3}" target="{4}" data-notify="url"></a>' +
            '</div>'
        });
}

$(document).ready(function () {

    $('#book-author_id').select2();

    $('#datetimepicker1').datetimepicker();

    $('#frm-add-contact').submit(function () {
        var data = $(this).serialize();
        var url = $(this).attr('action');

        $.post(url, data, function (resp) {
            if (resp.status) {
                $('.give-email__form').html('<p class="margin-0 font-italic">'+resp.message+'</p>');
            } else {
                showNotification('alert-success', resp.message);
            }
        }, 'json');

        return false;
    });

    // Init functions
    Card.init();
    ValidateForm.init();
    Book.init();
});

$( document ).ready(function() {
    $('body').on('click', '.type-book', function() { 
        var type = $("input[name='Book[type]']:checked").val();        
        if(type == 0){
            $('.type-ebook').addClass('hidden');
        }else if(type == 1){
            $('.type-ebook').removeClass('hidden');
        }
    });
});