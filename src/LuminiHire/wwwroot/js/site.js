// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function editUni(id = 0) {
    $.ajax({
        type: 'GET',
        url: '/UniData/JsonRedirectToAddEdit',
        data: "id=" + id,
        success: function (result) {
            window.location = result.redirectUrl;
        }
    });
}

function deleteUni(id = 0) {
    $.ajax({
        type: 'GET',
        url: '/UniData/DeleteUniversity',
        data: "id=" + id,
        success: function (result) {
            window.location = result.redirectUrl;
        }
    });
}

function displayUniInfo(parent, child) {
    $(parent).toggleClass('canHover');
    $(child).toggle(500);
}

function handlePcts(input) {

    //Regex for doubles with comma
    var regex = new RegExp( /^\d{0,3}(\,\d{0,3}){0,2}$/);

    var toTest = String(input.value);

    if (regex.test(toTest)) {

        if (input.value < 0) input.value = 0;
        if (input.value > 100) input.value = 100;    
    }
    else {
        input.value = 0;
    }
}