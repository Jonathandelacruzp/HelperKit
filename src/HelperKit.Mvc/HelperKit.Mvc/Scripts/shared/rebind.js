const rebind = () => {
    let $d = $(document);
    $d.find('form').each(function () { $.data($(this)[0], 'validator', !1); });
    $.validator.unobtrusive.parse('form');
    document.querySelectorAll('[data-val-required]').forEach(a => { a.setAttribute('required', 'required'); });
    document.querySelectorAll('input[type="checkbox"]').forEach(a => { 'False' === a.getAttribute('checked') && a.removeAttribute('checked'); });
    document.querySelectorAll('[data-val-length-max]').forEach(a => { let b = a.getAttribute('data-val-length-max'); a.setAttribute('maxlength', b); });

    /* Custom rebind Files */
    if ($('[data-toggle="tooltip"]')[0]) {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        });
    }

    if ($.fn.select2) {
        $d.find('.select2, [data-plugin="select2"]').select2({
            placeholder: "[ - Seleccione - ]",
            allowClear: true,
            language: 'es'
        });
    }
};

if ($.extend && $.validator) {
    $.extend(jQuery.validator.messages, {
        required: "Este campo es obligatorio.",
        remote: "Por favor, rellena este campo.",
        email: "Ingrese un e-mail válido.",
        url: "Por favor, escribe una URL válida.",
        date: "Por favor, escribe una fecha válida.",
        dateISO: "Por favor, escribe una fecha (ISO) válida.",
        number: "Por favor, escribe un número entero válido.",
        digits: "Por favor, escribe sólo dígitos.",
        creditcard: "Por favor, escribe un número de tarjeta válido.",
        equalTo: "Por favor, escribe el mismo valor de nuevo.",
        accept: "Por favor, escribe un valor con una extensión aceptada.",
        maxlength: jQuery.validator.format("Por favor, no escribas más de {0} caracteres."),
        minlength: jQuery.validator.format("Por favor, no escribas menos de {0} caracteres."),
        rangelength: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1} caracteres."),
        range: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1}."),
        max: jQuery.validator.format("Por favor, escribe un valor menor o igual a {0}."),
        min: jQuery.validator.format("Por favor, escribe un valor mayor o igual a {0}.")
    });
    $.validator.unobtrusive.adapters.add('filetype', ['validtypes'], function (options) {
        options.rules['filetype'] = { validtypes: options.params.validtypes.split(',') };
        options.messages['filetype'] = options.message;
    });
    $.validator.addMethod("filetype", function (value, element, param) {
        for (var i = 0; i < element.files.length; i++) {
            var extension = getFileExtension(element.files[i].name);
            if ($.inArray(extension, param.validtypes) === -1)
                return false;
        }
        return true;
    });
    $.validator.addMethod('date', function (value, element) { return true; });
}