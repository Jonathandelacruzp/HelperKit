$(document).on('click', '[data-type=modal-link]', function (e) {
    e.preventDefault();
    let $this = $(this),
        url = $this.attr('data-url'),
        modalSize = $this.attr('data-modal-size'),
        $modalLoading = $('#default-modal-loading'),
        modalHtml = $(`<div class="modal fade modal-fade-in-scale-up" role="dialog"><div class="modal-dialog ${modalSize}"><div class="modal-content"></div></div></div>`),
        $modalContent = modalHtml.find('.modal-content'),
        $defaultContainer = $('#default-modal-container');
    $modalContent.html($modalLoading.html());
    $defaultContainer.append(modalHtml), modalHtml.modal('show'), $modalContent.load(url, function (j, k) {
        'error' === k && $modalContent.html($('#default-modal-loading-error').html());
    });
});
$(document).on('hide.bs.modal', function () {
    let a = document.querySelectorAll('#default-modal-container .modal');
    0 < a.length && setTimeout(() => {
        a[a.length - 1].remove();
    }, 500);
});
$(document).on('change', '[data-type="cascade-select"]', function () {
    let $this = $(this),
        value = $this.val();
    if (value) {
        let url = $this.data('source-url'),
            params = $this.data('filter-param'),
            target = $this.data('target'),
            data = {
                [params]: value
            },
            aditionamParams = $this.data('aditional-param'),
            $target = $(target);
        if (aditionamParams) {
            aditionamParams.split(',').forEach(function (x) {
                data[x] = $(`#${x}`).val();
            });
        }
        $target.attr('disabled', 'disabled').find('option[value!=""]').remove();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            async: false,
            success: function (result) {
                let valueMember = $target.data('value-member'),
                    textMember = $target.data('text-member');
                valueMember = valueMember ? valueMember : 'Value', textMember = textMember ? textMember : 'Text';
                $target.html('').append(new Option('[- Seleccionar -]', ''));
                if (0 !== result.length) {
                    result.forEach(function (m) {
                        $target.append(new Option(m[textMember], m[valueMember]));
                    });
                    $target.removeAttr('disabled').trigger('change');
                }
            }
        });
    }
});
$(function () {
    rebind();
});