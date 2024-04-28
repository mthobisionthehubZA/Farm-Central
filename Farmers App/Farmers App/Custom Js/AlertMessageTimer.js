// alerm message timer to disappear message after some time of display
window.setTimeout(function (){
    $(".alert").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 3000);