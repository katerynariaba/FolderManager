$(function () {
    //$("#createForm").validate({
    //    rules: {
    //        name: {
    //            required: true,
    //            email: true
    //        }
    //    },
    //    messages: {
    //        name: {
    //            required: "To pole obowiazkowe",
    //            email: "email"
    //        }
    //    },
    //});

    $(".validate").validate({
        rules: {
            name: "required"
        },
        messages: {
            name: "Please specify your name"
        }
    });

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

        console.log(methodName);

        $.ajax({
            url: actionUrl,
            type: methodName,
            data: dataToSend,
            //success: function () {
            //    placeholderElement.find('.modal').modal('hide');
            //    location.reload();
            //}
            
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
    $("form[name='creating']").validate({
        rules: {
            name: "required"
        },
        messages: {
            firstname: "Please enter name"
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});


$(function () {
    DragAndDrop.enable("#myUL");

    $(document).on("dblclick", "#myUL *[class*=node]", function () {
        $(this).beginEditing();
    });

});