$(document).ready(function() {
    $('.create-review').submit(function (event) {
        event.preventDefault();
        $.ajax({
            url: '~/Products/CreateReview',
            type: 'POST',
            dataType: 'json',
            data: $(this).serialize(),
            success: function (newReview) {
                $('#added-review').html(newReview);
            }
        });
    });
})