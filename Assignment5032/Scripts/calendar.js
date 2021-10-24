// full calendar 
// Reference:https://fullcalendar.io/docs/v3/month-view
function calendarMaker(events) {
    $('#calendar').fullCalendar({
        defaultView: 'month',
        contentHeight: 500,
        // calendar interactive header
        // Reference:https://fullcalendar.io/docs/v3/toolbar
        header: {
            left: "agendaWeek,month",
            center: "title",
            right: "today, prev,next",

        },
        businessHours: true,
        // display events
        events: events,
        timeFormat: "hh:mm a",
        // When user hovering over event, display it descriptions
        // Reference:https://fullcalendar.io/docs/v3/eventClick
        eventMouseover: function (event) {
            $('#eventName').text("      " + event.title);
            $('#restName').text(" " + event.store);
            $('#startTime').text(event.start);
            $('#endTime').text(event.end);
            $('#descriptions').css("display", "block");
            if (moment(event.start) > $.now()) {
                var urls = "/Books/BookEvent/" + event.eventId;
                console.log(urls)
                $('#bookingLink').css("display", "block");
                $('#bookingLink').attr("href", urls);
            }
            else {
                $('#bookingLink').css("display", "none");
            }
        }

    });
}