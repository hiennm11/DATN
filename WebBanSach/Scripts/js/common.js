;(function( $, $w, $d, w, d, $b ){
	$(document).ready(function() {
		'use strict';

		// set header absolute width page heading box
		if( $('body').find('.heading-box').length > 0 ) {
			$('body').addClass('page-heading');
		} else {
			$('body').removeClass('page-heading');
		}
		// Mobile
	    $('.header-mobile__left_btn').on('click', function(){
	        $('body').addClass('panel_show_left');
	        $('.wrap-main').on('click', function(){
	            $('body').removeClass('panel_show_left');
	        });
	    });
	    $('.header-mobile__right_btn').on('click', function(){
	        $('body').addClass('panel_show_right');
	        $('.wrap-main').on('click', function(){
	            $('body').removeClass('panel_show_right');
	        });
	    });
	    // Menu mobile
	    $('.main-menu__menu-top').each(function(){
	    	$(this).multiMenuTP({ type: 'multitoggle' });
	    });

	    // Sticky menu
		if( $('.sticky-main').length > 0 ) {
			$(window).scroll(function(event){
				var scrollTop = $(this).scrollTop();
				if( scrollTop >= 85 && scrollTop < 250 ) {
					$('.sticky-main').addClass('header--step-sticky');
				} else {
					$('.sticky-main').removeClass('header--step-sticky');
				}

				if( scrollTop >= 250 ) {
					$('.sticky-main').addClass('header--sticky');
				} else {
					$('.sticky-main').removeClass('header--sticky');
				}
			});
		}
		
		//Slider
		if( $('.slider').length > 0 ) {
			$('.slider').each( function() {
				var $this = $(this);
				var defaults = '';
				var params;
				if( typeof( $this.data('params')) !== 'undefined' || typeof($this.data('params')) !== '' ) {
					params = $.extend({}, defaults, $this.data('params'));
				}
				if( params !== '' || params !== 'undefined' ) {
					var slider = new Swiper($this, params);
				} else {
					var slider = new Swiper($this);
				}
			});
		}
		
		// Tab mobile
		$('.tab-mobile__title a').on('click', function() {
			$('.tab-mobile__title').removeClass('active');
			$(this).parent('.tab-mobile__title').addClass('active');
		});

    	// Rating event
		if( $('.star-rate').length > 0 ) {
			$('.star-rate').each(function(){
				var $this = $(this);
				var default_option = {starWidth:'15px',rating:0};

				var params;
				if( typeof( $this.data('options')) !== 'undefined' || typeof($this.data('options')) !== '' ) {
					params = $.extend({}, default_option, $this.data('options'));
				}
				if( params !== '' || params !== 'undefined' ) {
					$this.rateYo(params);
				} else {
					$this.rateYo();
				}

			});
		}

		// hide panel 
		if( $('.account__page').length > 0 ) {
			$('.panel-mb').hide();
			$('#page').css({
				'padding-top': '0'
			});
		}

		// Qty up down
		$('button.up').on('click', function() {
			var numQtyUp = parseInt($(this).parent().find('.qty').val());
				numQtyUp += 1;
			$(this).parent().find('.qty').val(numQtyUp);
		});
		$('button.down').on('click', function() {
			var numQtyDown = parseInt($(this).parent().find('.qty').val());
			if( numQtyDown > 1 ) {
				numQtyDown -= 1;
				$(this).parent().find('.qty').val(numQtyDown);
			}
		});
		// Checkout page check active step
		$('.checkout .nav li').on('click', function() {
			var index = $(this).index();
			if( index === 1 ) {
				$('.checkout .nav li:first-child').addClass('loaded');
				$('.checkout .nav li:eq(2)').removeClass('loaded');
			} else if ( index === 2 ) {
				$('.checkout .nav li').addClass('loaded');
			} else {
				$('.checkout .nav li').removeClass('loaded');
			}
		});
		// Click edit focus form
		$('.edit-name').on('click', function(e) {
			e.preventDefault();
			$(this).parent().find('input').focus().setCursorToTextEnd();
		});
		// Table mobile
		$('.tab-mobile__title a').on('click', function(e) {
			e.preventDefault();
			$('.tab-mobile__title a').removeClass('active');
			$(this).addClass('active');
		});
		// coupon input show
		$('.add-coupon-code').on('click', function(e) {
			e.preventDefault();
			$('.input-coupon').removeClass('hidden');
			$(this).addClass('hidden');
		});
	});
} (jQuery, jQuery(window), jQuery(document), window, document, jQuery('body')));
// plugin set focus input
(function($){
    $.fn.setCursorToTextEnd = function() {
        $initialVal = this.val();
        this.val($initialVal + ' ');
        this.val($initialVal);
    };
})(jQuery);