// testing this .js file
// console.log("google simple map works");
// const { intl } = require("modernizr");

// let variable, changeable
let map;
// variable for the http request
let xmlHttp = new XMLHttpRequest();
// call the function defined in controller & get data from db
// we use false here for sync.  not true for async
xmlHttp.open("GET", "Restaurants/GetRestaurantsList", false);
xmlHttp.send(null);
// new variable for the restaurant array
let restaurants;
// convert string response into JSON array
restaurants = JSON.parse(xmlHttp.responseText);
// console.log(restaurants);


// Map function
// Reference: https://developers.google.com/maps/documentation/javascript/examples/map-simple#maps_map_simple-javascript
// The default lat & lng set to monash clayton campus
function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -37.915990, lng: 145.138820 },
        zoom: 14,
    });

    // show current location of user
    // Reference:https://developers.google.com/maps/documentation/javascript/geolocation
    infoWindow = new google.maps.InfoWindow();

    const locationButton = document.createElement("button");

    locationButton.textContent = "Pan to Current Location";
    locationButton.classList.add("custom-map-control-button");
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);
    locationButton.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation)
        {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    //  const variable, not changeable. like finnal?
                    const pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };

                    map.setCenter(pos);
                },
                () => {
                    handleLocationError(false, infoWindow, map.getCenter());
                }
            );
        }
        else
        {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());
        }
    });

    // at the end
    // use for loop to mark each restaurant on the map by calling the function defined below
    for (var i = 0; i < restaurants.length; i++)
    {
        // console.log(restaurants[i]);
        transRestAddress(map, restaurants[i]);
    }

    // routing function
    // Reference:https://developers.google.com/maps/documentation/javascript/examples/directions-panel#maps_directions_panel-javascript
    const directionsRenderer = new google.maps.DirectionsRenderer();
    const directionsService = new google.maps.DirectionsService();

    directionsRenderer.setMap(map);
    directionsRenderer.setPanel(document.getElementById("sidebar"));

    var getWay = document.getElementById("getway");

    getWay.addEventListener("click", function () {
        directionsService.route({
            origin: { query: document.getElementById("start_p").value },
            destination: { query: document.getElementById("destination").value },
            travelMode: google.maps.TravelMode[document.getElementById("mode").value]
        },
            (response, status) => {
                if (status == "OK") {
                    directionsRenderer.setDirections(response);
                }else {
                    window.alert("Unable to directing you, sorry!");
                }
        });
    });

    // routing function auto complete the input address feature
    // Reference:https://developers.google.com/maps/documentation/javascript/examples/places-autocomplete
    var start_p = document.getElementById("start_p");
    const autoAddress = new google.maps.places.Autocomplete(start_p);
    autoAddress.bindTo("bounds", map);

}


// show current location error handler
// Reference:https://developers.google.com/maps/documentation/javascript/geolocation
function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}


// function for markers of all restaurant location
function transRestAddress(map, restaurants)
{
    // The name of restaurant as information
    var info = "<h4>" + restaurants.Name + "</h4><p>" + restaurants.ContactNumb + "</p>";
    
    // Information window
    // Reference:https://developers.google.com/maps/documentation/javascript/infowindows
    const infowindow = new google.maps.InfoWindow({
        content: info,
    });

    // Google geocoder(match the google map, less error) trans the Restaurant address
    // Reference:https://stackoverflow.com/questions/16274508/how-to-call-google-geocoding-service-from-c-sharp-code
    // Add markers
    var geocoder = new google.maps.Geocoder();

    // map marker icon
    // Reference:https://developers.google.com/maps/documentation/javascript/examples/icon-complex
    const image = {
        url: "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png",
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(20, 32),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(0, 32),
    };
    
    geocoder.geocode({ address: restaurants.Address }, function (result, status) {
        if (status === "OK") {
            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                position: result[0].geometry.location
            });
        }

        marker.addListener("click", function () {
            infowindow.open(map, marker)
        });
    });
}