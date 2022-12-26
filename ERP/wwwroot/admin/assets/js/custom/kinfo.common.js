// "Zipcode AutoComplete"

(function ($, undefined) {
    $.fn.kinfoAutoComplete = function (settings) {
        var config = {
            'url': '../Base/GetZipcode',
            'span': '#zipAddress',
            'zipcodeid': '',
            'zipAddress': '',
            'ZIPCode': ''
        };
        if (settings) { $.extend(config, settings); }
        return this.keyup(function (e) {

            var key = e.which || e.keyCode;
            var charCode = (e.which) ? e.which : event.keyCode

        }).bind("keydown", function (event) {
            if (event.keyCode == 13)
                event.preventDefault();
            if (event.keyCode === $.ui.keyCode.TAB &&
                $(this).autocomplete("instance").menu.active) {
                event.preventDefault();
            }
        })
            .autocomplete({
                minLength: 3,
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        enctype: 'multipart/form-data',
                        ContentType: 'application/json; charset=utf-8',
                        url: config.url,
                        datatype: "jsonp",
                        data: {
                            'q': request.term
                        },
                        success: function (data) {
                            var obj = $.parseJSON(data);
                            if (obj != null) {
                                response($.map(obj, function (item) {
                                    return {
                                        label: item.CityName + ',' + item.StateShortName + ',' + item.ZIPCode,
                                        value: item.ZIPCodeID,
                                        id: item.ZIPCodeID,
                                        Zip: item.ZIPCode,
                                        city: item.CityName,
                                        state: item.StateShortName,
                                        country: item.CountryShortName
                                    };
                                }));
                            }
                        }
                    });
                },
                focus: function () {
                    // prevent value inserted on focus 
                    event.preventDefault(); // without this: keyboard movements reset the input to ''
                    return false;
                },
                change: function (event, ui) {
                    if (!ui.item) {
                        if (config.zipAddress != '') {
                            $(config.zipAddress).val("");
                        }
                        if (config.ZIPCode != '') {
                            $(config.ZIPCode).val("");
                        }
                    }
                },

                select: function (event, ui) {
                    var terms = split(this.value);
                    var zipid = ui.item.value;
                    var ziplabel = ui.item.Zip;
                    if (config.zipcodeid != '') {
                        $(config.zipcodeid).val(zipid);
                    }
                    this.value = ziplabel;
                    if (config.zipAddress != '') {
                        $(config.zipAddress).text(ui.item.city + ',' + ui.item.state + ',' + ui.item.country);
                    }
                    return false;
                }
            });
    };
    // "Zipcode AutoComplete"

})(jQuery);