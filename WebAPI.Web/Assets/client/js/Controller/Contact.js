//js cho bản đồ

var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        contact.initMap();
    },

    initMap: function () {
        const uluru = { lat: parseFloat($('#hidLat').val()), lng: parseFloat($('#hidlng').val()) };
        const map = new google.maps.Map(document.getElementById("map"), {
            zoom: 17,
            center: uluru,
        });
        const contentString = $('#hidAddress').val();
        const infowindow = new google.maps.InfoWindow({
            content: contentString,
        });
        const marker = new google.maps.Marker({
            position: uluru,
            map,
            title: $('#hidName').val(),
        });
        infowindow.open(map, marker);
    }
}
contact.init();