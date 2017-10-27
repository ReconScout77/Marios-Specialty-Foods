$('.create-review').submit(function (event) {
    event.preventDefault();
    $.ajax({
        url: '@Url.Action("CreateReview", Reviews)',
        type: 'POST',
        dataType: 'json',
        data: $(this).serialize(),
        success: function (newReview) {
            $('#added-review').html(newReview);
        }
    });
});
