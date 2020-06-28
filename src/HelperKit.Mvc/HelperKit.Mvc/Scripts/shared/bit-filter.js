function BitFilterRebind($element) {
    $element.find('[data-bit-filter="select-operador"]').each(function (i, e) {
        let $this = $(this);
        let $inputOperador = $this.find('[name^="bit-filter-ope-"]');
        let $optionOperador = $this.find('[data-bit-filter-operador="' + $inputOperador.val() + '"]');
        if (!$optionOperador || $optionOperador.length === 0) {
            $optionOperador = $this.find('[data-bit-filter-operador]').first();
        }
        $optionOperador.closest('ul').prev().html($optionOperador.html());
    });

    function mostarValorDateRangePicker(start, end) {
        if (!start && !end) //todo
        {
            this.element.find('span').html('');
            this.element.closest('.bf-daterange').find('input[type="hidden"]').first().val('');
            this.element.closest('.bf-daterange').find('input[type="hidden"]').last().val('');
        } else if (!end) {
            this.element.find('span').html(start.format('DD/MM/YYYY'));
            this.element.closest('.bf-daterange').find('input[type="hidden"]').first().val(start.format('DD/MM/YYYY'));
            this.element.closest('.bf-daterange').find('input[type="hidden"]').last().val(start.format('DD/MM/YYYY'));
        } else {
            this.element.find('span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
            this.element.closest('.bf-daterange').find('input[type="hidden"]').first().val(start.format('DD/MM/YYYY'));
            this.element.closest('.bf-daterange').find('input[type="hidden"]').last().val(end.format('DD/MM/YYYY'));
        }
    }

    $element.find('.bf-daterangepicker').daterangepicker({
        "opens": "center",
        locale: { cancelLabel: 'Borrar', applyLabel: 'Aceptar', customRangeLabel: 'Customizable' },
        ranges: {
            'Todo': [null, null],
            'Hoy': [moment(), null],
            'Ayer': [moment().subtract(1, 'days'), null],
            'Últimos 7 Días': [moment().subtract(6, 'days'), moment()],
            'Últimos 30 Días': [moment().subtract(29, 'days'), moment()],
            'Este Mes': [moment().startOf('month'), moment().endOf('month')],
            'Último Mes': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, mostarValorDateRangePicker);

    $('.bf-daterangepicker').on('cancel.daterangepicker', function (ev, picker) {
        $(this).closest('.bf-daterange').find('span').html('');
        $(this).closest('.bf-daterange').find('.form-control').val('');
    });
}

function BitFilterInit($element) {
    $($element).on('click', '[data-bit-filter-operador]', function (e) {
        e.preventDefault();
        let $this = $(this);
        let $parentUl = $this.closest('ul');
        $parentUl.prev().html($this.html()); //button
        if ($parentUl.next().attr('name').indexOf('bit-filter-ope-') !== 0) {
            console.log("Error en configuración de filtros: Hidden de operador mal ubicado");
        } else {
            $parentUl.next().val($this.attr('data-bit-filter-operador'));
        }
    });

    $($element).on('keyup', '[name^="bit-filter-value-"]', function (e) {
        if (e.keyCode === 13) {
            $(this).closest('[data-type="bit-filter"]').find('[data-bit-filter="submit"]').click();
        }
    });

    let paginationData = [];

    $($element).on('click', '[data-bit-filter="submit"]', function (e) {
        e.preventDefault();
        let $this = $(this);
        let $bitFilter = $this.closest('[data-type="bit-filter"]');

        let sourceUrl = $bitFilter.data('bf-source-url');
        let method = $bitFilter.data('bf-method') || 'GET';
        let blockId = $bitFilter.data('bf-block-id');
        let formData = $bitFilter.find('[name]').serializeArray();

        let data = formData.concat(paginationData);
        paginationData = [];

        $this.closest('table').find('tbody').addClass('bf-cargando');

        $.ajax({
            method: method,
            url: sourceUrl,
            data: data
        }).done(function (resultHtml) {
            let $html;

            if (blockId) {
                $html = $(resultHtml).find('[data-type="bit-filter"][data-bf-block-id="' + blockId + '"]');
            } else {
                $html = $(resultHtml).find('[data-type="bit-filter"]').first();
            }

            $bitFilter.replaceWith($html);
            RebindJquery($html);
        }).fail(function () {
            alert("Ha ocurrido un error al cargar los datos.");
            $this.closest('table').find('tbody').removesClass('bf-cargando');
        });
    });
    $($element).on('click', '[data-bit-filter="reset"]', function (e) {
        e.preventDefault();
        let $this = $(this);
        $this.parent().parent().parent().find('[name^=bit-filter-value]').val('');
        $this.parent().parent().parent().find('.bf-daterangepicker span').text('');
    });

    $($element).on('click', '.pagination a', function (e) {
        let $this = $(this);
        let $bitFilter = $this.closest('[data-type="bit-filter"]');
        if ($bitFilter.length > 0) {
            e.preventDefault();
            let paginationUrl = $this.attr('href');
            paginationData = ObtenerParametrosUrl(paginationUrl);
            $bitFilter.find('[data-bit-filter="submit"]').click();
        }
    });

    function ObtenerParametrosUrl(url) {
        let query = url.split('?')[1] || '';
        let re = /([^&=]+)=?([^&]*)/g;
        let decodeRE = /\+/g;  // Regex for replacing addition symbol with a space
        let decode = function (str) { return decodeURIComponent(str.replace(decodeRE, " ")); };
        let params = [], e;
        while (e = re.exec(query)) {
            let k = decode(e[1]), v = decode(e[2]);
            /*if (k.substring(k.length - 2) === '[]') {
                k = k.substring(0, k.length - 2);
                (params[k] || (params[k] = [])).push(v); //?
            }
            else*/
            params.push({ name: k, value: v });
        }
        return params;
    }

    BitFilterRebind($element);
}

BitFilterInit($(document));