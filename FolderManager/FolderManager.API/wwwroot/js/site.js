$(function () {

    var toggler = document.getElementsByClassName("caret");
    var i;

    for (i = 0; i < toggler.length; i++) {
        toggler[i].addEventListener("click", function () {
            this.parentElement.querySelector(".nested").classList.toggle("active");
            this.classList.toggle("caret-down");
        });
    }

    $("#open").click(function () {
        for (i = 0; i < toggler.length; i++) {
            toggler[i].parentElement.querySelector(".nested").classList.toggle("active");
            toggler[i].classList.toggle("caret-down");
        }
     });
       

    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        console.log(actionUrl);
        var method = form.attr('http-method');
        var dataToSend = form.serialize();

        var methodName = (method === 'delete') ? 'DELETE' : 'POST';

        $.ajax({
            url: actionUrl,
            type: methodName,
            data: dataToSend,
            success: function () {
                placeholderElement.find('.modal').modal('hide');
                location.reload();
            }
            
        });
        console.log(success);
    });

})