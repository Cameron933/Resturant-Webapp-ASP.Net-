// testing this .js file
// console.log("google simple map works");

const { intl } = require("modernizr");

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

                    infoWindow.setPosition(pos);
                    infoWindow.setContent("Location found.");
                    infoWindow.open(map);
                    map.setCenter(pos);
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
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
        transRestAddress(map,restaurants[i]);
    }

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
    const info = "<h4>" + restaurants.Name + "</h4><p>" + restaurants.ContactNumb + "</p>";

    // Information window
    // Reference:https://developers.google.com/maps/documentation/javascript/infowindows
    const infowindow = new google.maps.InfoWindow({
        content: info,
    });

    // Google geocoder(match the google map, less error) trans the Restaurant address
    // Reference:https://stackoverflow.com/questions/16274508/how-to-call-google-geocoding-service-from-c-sharp-code
    // Add markers
    var geocoder = new google.maps.geocoder();
    geocoder.geocode({ address: restaurants.Address }), function (result, status)
    {
        if (status === "OK") {
            var marker = new google.maps.Marker({
                map: map,
                position: result[0].geometry.location
            });
        }

        marker.addListener("click", function() {
            infowindow.open(map,marker)
            });
    }
}