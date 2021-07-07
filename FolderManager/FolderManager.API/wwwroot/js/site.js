$(function () {
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
        var method = form.attr('http-method');
        var dataToSend = form.serialize();

        var validator = $("form[name='creating']").validate({
            rules: {
                name: {
                    required: true,
                    maxlength: 30
                }
            },
            submitHandler: function (form) {
                form.submit();
            }
        });

        validator?.form();
        var errors = validator?.numberOfInvalids();

        if (errors > 0) {
            return
        }

        var methodName;
        switch (method) {
            case 'delete':
                methodName = 'DELETE';
                break;
            case 'post':
                methodName = 'POST';
                break;
            case 'patch':
                methodName = 'PATCH';
                break;
            case 'put':
                methodName = 'PUT';
                break;
            default:
                break;
        }

        $.ajax({
            url: actionUrl,
            type: methodName,
            data: dataToSend,
            success: function () {
                placeholderElement.find('.modal').modal('hide');
                location.reload();
            }
            
        });
    });

})

function sortClick(id, order) {
    var el = $("#" + id);
    console.log(id);
    var items = el.children("li");
    if (items.length <= 1) return;

    items.sort(sortPredicate).appendTo(el);

    function sortPredicate(a, b) {
        if (order == 'desc') {
            return (b.children[0].innerText).localeCompare(a.children[0].innerText);
        }
        else {
            return (a.children[0].innerText).localeCompare(b.children[0].innerText);
        }
    }
}

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
})
