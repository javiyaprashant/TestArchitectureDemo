var App = (function () {
    var data = [];
    var grid;
    var bodyContainer = $('.body-content');
    var getData = function () {
        showMask();
        $.ajax({
            type: 'get',
            dataType: 'json',
            url: "/Api/Properties",
            success: OnDataRecieved,
            error: OnError
        });
    };
    var OnError = function (jqXHR, textStatus, errorThrown) {
        hideMask();
        if (jqXHR.responseJSON && jqXHR.responseJSON.Message) {
            $(".fail").html('<b>Error:</b>' + jqXHR.responseJSON.Message);
        }
        else {
            $(".fail").html('<b>Error:</b>' + errorThrown);
        }
        
        $(".fail").show();
        $(".fail").fadeOut(10000);
    };
    var OnDataRecieved = function (json) {
        hideMask();
        if (json.Errors)
        {
            OnError(undefined, undefined, json.Errors);
            return;
        }
        for (var i = 0; i < json.properties.length; i++) {
            var item = json.properties[i];
            data.push({
                id: item.PropertyId,
                address: {
                    address1: json.properties[i].Address1,
                    address2: json.properties[i].Address2,
                    city: json.properties[i].City,
                    country: json.properties[i].Country,
                    county: json.properties[i].County,
                    district: json.properties[i].District,
                    state: json.properties[i].State,
                    zip: json.properties[i].ZipCode,
                    zipPlus4: json.properties[i].ZipPlus4
                },
                ListPrice: json.properties[i].ListPrice,
                YearBuilt: json.properties[i].YearBuilt,
                MonthlyRent : json.properties[i].MonthlyRent,
                GrossYield : json.properties[i].GrossYield
            });
        }
        generateGrid();
    };
    var OnSaveSuccess = function (json) {
        hideMask();
        if (json.Errors) {
            OnError(undefined, undefined, json.Errors);
            return;
        }
        $(".success").html('Data Saved Successfully');
        $(".success").show();
        $(".success").fadeOut(10000);
    };
    var saveData = function (record) {
        
        var rec = {
            PropertyId: record.id,
            Address1: record.address.address1,
            Address2: record.address.address2,
            City: record.address.city,
            Country: record.address.country,
            County: record.address.county,
            District: record.address.district,            
            State: record.address.state,
            ZipCode: record.address.zip,
            ZipPlus4: record.address.zipPlus4,
            YearBuilt: record.YearBuilt,
            ListPrice: record.ListPrice,
            MonthlyRent: record.MonthlyRent,
            GrossYield: record.GrossYield
        }
        showMask();
        $.ajax({
            type: "POST",
            url: "/Api/Properties",
            data: JSON.stringify(rec),
            success: OnSaveSuccess,
            error: OnError,
            dataType: 'json',
            contentType: "application/json"
        });
    };
    var generateGrid = function () {
        var addressField = function (config) {
            jsGrid.Field.call(this, config);
        };

        addressField.prototype = new jsGrid.Field({
            itemTemplate: function (value) {
                //console.log(value);
                var r =
                    "<span>" + value.address1 + " " + (value.address2 || "" ) + "</span><br/>" +
                    "<span>" + ( value.county || "" ) + " " + (value.district || "") + "</span><br/>" +
                    "<span>" + value.city + " " + value.state + " " + value.country + "</span><br/>" +                    
                    "<span>" + value.zip + " " + value.zipPlus4 + "</span><br/>"
                ;
                return r;
            }
        });

        jsGrid.fields.addressField = addressField;

        // currency field
        var currencyField = function (config) {
            jsGrid.Field.call(this, config);
        };

        currencyField.prototype = new jsGrid.Field({
            itemTemplate: function (value) {
                if(value)
                    return "$" + value.toFixed(2);
                return value;

            }
        });

        jsGrid.fields.currencyField = currencyField;

        // currency field
        var numberField = function (config) {
            jsGrid.Field.call(this, config);
        };

        numberField.prototype = new jsGrid.Field({
            itemTemplate: function (value) {
                if (value)
                    return value.toFixed(2);
                return value;

            }
        });

        jsGrid.fields.numberField = numberField;

        $("#jsGrid").jsGrid({
            width: "100%",
            height: "700px",

            inserting: false,
            editing: false,
            sorting: false,
            paging: false,

            data: data,
            fields: [
                {
                    name: "address",
                    title: "Address",
                    type: "addressField",
                    width: 150
                },
                {
                    name: "YearBuilt",
                    title: "Year Built",
                    type: "text",                    
                    width: 50
                },
                {
                    name: "ListPrice",
                    title: "List Price $",
                    type: "currencyField",
                    width: 50
                },                
                {
                    name: "MonthlyRent",
                    title: "Monthly Rent $",
                    type: "currencyField",
                    width: 70
                },                
                {
                    name: "GrossYield",
                    title: "Gross Yield %",
                    type: "numberField",                                       
                    width: 150
                },
                {
                    type: "control",
                    modeSwitchButton: false,
                    editButton: false,
                    deleteButton: false,
                    itemTemplate: function (value, item) {
                        return $("<button>").attr("type", "button").text("Save")
                            .on("click", function () {
                                saveData(item);
                            });
                    },
                }
            ]
        });
        grid = $("#jsGrid").data("JSGrid");
    };
    var showMask = function () {

        $("<div id='overlay'>Loading</div>").css({
            "position": "fixed",
            "top": 0,
            "left": 0,
            "width": "100%",
            "height": "100%",
            "background-color": "rgba(0,0,0,.5)",
            "z-index": 10000,
            "vertical-align": "middle",
            "text-align": "center",
            "color": "#fff",
            "font-size": "30px",
            "font-weight": "bold"
        }).appendTo("body");

    };
    var hideMask = function () {
        $('#overlay').fadeOut(400, function () {
            $('#overlay').remove();
        });
    };

    return {
        GetData: getData,
        ShowMask: showMask,
        HideMask: hideMask
    }

})();