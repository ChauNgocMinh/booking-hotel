/*
    !function ($) {
        "use strict";
        var CalendarApp = function () {
            this.$body = $("body")
            this.$calendar = $('#calendar');
            this.$modal = $('#my_event');
            this.$calendarObj = null;
        };

        CalendarApp.prototype.onSelect = function (start, end, allDay) {
            var $this = this;
            $this.$modal.modal({ backdrop: 'static' });

            var form = $("<form></form>");
            form.append("<div class='event-inputs'></div>");
            form.find(".event-inputs")
                .append("<div class='form-group col-lg-6'><label> Customer ID <span class= 'text-danger' >*</span ></label><input class='form-control' type='text'></div>")
                .append("<div class='form-group col-lg-6'><label> Room ID <span class= 'text-danger' >*</span ></label><input class='form-control' type='text'></div>")
                .append("<div class='form-group'><label class='control-label col-lg-6'>Check-In Date</label><input class='form-control' type='date' name='CheckInDate'/></div>")
                .append("<div class='form-group'><label class='control-label col-lg-6'>Check-Out Date</label><input class='form-control' type='date' name='TenDangNhap'/></div>")
                .append("<div class='form-group'><label class='control-label'>TrangThai</label><select class='form-control' name='TrangThai'><option value='Pending'>Pending</option><option value='Confirm'>Confirm</option><option value='Cancel'>Cancel</option></select></div>")
                .append("<div class='form-group'><label class='control-label'>Total Gia</label><input class='form-control' type='number' name='TotalGia'/></div>");

            $this.$modal.find('.delete-event').hide().end().find('.save-event').show().end().find('.modal-body').empty().prepend(form).end().find('.save-event').unbind('click').click(function () {
                form.submit();
            });

            CalendarApp.prototype.onEventClick = function (calEvent, jsEvent, view) {
                var $this = this;
                var form = $("<form></form>");
                form.append("<label>Booking Details</label>");
                form.append("<div class='form-group'><label class='control-label'>Check-In Date</label><input class='form-control' type='text' value='" + calEvent.start.format('YYYY-MM-DD') + "' readonly /></div>")
                    .append("<div class='form-group'><label class='control-label'>Check-Out Date</label><input class='form-control' type='text' value='" + calEvent.end.format('YYYY-MM-DD') + "' readonly /></div>")
                    .append("<div class='form-group'><label class='control-label'>Total Gia</label><input class='form-control' type='text' value='$" + calEvent.TotalGia + "' readonly /></div>")
                    .append("<div class='form-group'><label class='control-label'>TrangThai</label><input class='form-control' type='text' value='" + calEvent.TrangThai + "' readonly /></div>");

                $this.$modal.modal({ backdrop: 'static' });
                $this.$modal.find('.delete-event').hide().end().find('.save-event').hide().end().find('.modal-body').empty().prepend(form);

                $this.$modal.find('form').on('submit', function () {
                    $this.$modal.modal('hide');
                    return false;
                });
            };

            $this.$modal.find('form').on('submit', function () {
                var checkInDate = form.find("input[name='CheckInDate']").val();
                var TenDangNhap = form.find("input[name='TenDangNhap']").val();
                var totalGia = form.find("input[name='TotalGia']").val();
                var TrangThai = form.find("select[name='TrangThai']").val();

                if (checkInDate !== '' && TenDangNhap !== '' && totalGia !== '' && TrangThai !== null) {
                    // Convert dates to ISO format if needed
                    checkInDate = new Date(checkInDate).toISOString();
                    TenDangNhap = new Date(TenDangNhap).toISOString();

                    var newBooking = {
                        title: 'Booking Title',
                        start: checkInDate,
                        end: TenDangNhap,
                        TotalGia: totalGia,
                        TrangThai: TrangThai
                    };

                    $this.$calendarObj.fullCalendar('renderEvent', newBooking, true);
                    $this.$modal.modal('hide');
                } else {
                    alert('Please fill in all fields.');
                }
                return false;
            });

            $this.$calendarObj.fullCalendar('unselect');
        };

        CalendarApp.prototype.init = function () {
            var $this = this;
            $this.$calendarObj = $this.$calendar.fullCalendar({
                slotDuration: '00:15:00',
                minTime: '08:00:00',
                SoLuongToiDaTime: '19:00:00',
                defaultView: 'month',
                handleWindowResize: true,
                header: { left: 'prev,next today', center: 'title', right: 'month,agendaWeek,agendaDay' },
                events: [], // You can load events from your database here
                editable: true,
                eventLimit: true,
                selectable: true,
                eventClick: function (calEvent, jsEvent, view) {
                    $this.onEventClick(calEvent, jsEvent, view);
                },
                select: function (start, end, allDay) {
                    $this.onSelect(start, end, allDay);
                }
            });
            $this.fetchAndUpdateEvents();
        };

        CalendarApp.prototype.fetchAndUpdateEvents = function () {
            var $this = this;

            fetch('/Bookings/GetListBookingJson')
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    $this.$calendarObj.fullCalendar('removeEvents');
                    var transformedData = data.map(function (item) {
                        return {
                            id: item.BookingID,
                            title: "Booking " + item.BookingID,
                            start: item.CheckInDate,
                            end: item.TenDangNhap,
                            allDay: false,
                            roomId: item.RoomID,
                            customerId: item.CustomerId,
                            TotalGia: item.TotalGia,
                            TrangThai: item.TrangThai
                        };
                    });
                    $this.$calendarObj.fullCalendar('addEventSource', transformedData);
                })
                .catch(function (error) {
                    console.error('Error fetching events:', error);
                });
        };

        $.CalendarApp = new CalendarApp;
        $.CalendarApp.Constructor = CalendarApp;
    }(window.jQuery);

(function ($) {
    "use strict";
    $.CalendarApp.init();
})(window.jQuery);
*/
!(function ($) {
    "use strict";

    var CalendarApp = function () {
        this.$body = $("body")
        this.$calendar = $('#calendar');
        this.$modal = $('#my_event');
        this.$modal2 = $('#booking_detail');
        this.$calendarObj = null;
    };

    CalendarApp.prototype.onSelect = function (start, end, allDay) {
        var $this = this;
        $this.$modal.modal({ backdrop: 'static' });

        var form = $("<form></form>");
        form.append("<div class='event-inputs row'></div>");
        form.find(".event-inputs")
            .append("<div class='form-group col-lg-12'><label> Customer ID <span class= 'text-danger' >*</span ></label><input class='form-control' type='text'></div>")
            .append("<div class='form-group col-lg-12'><label> Room ID <span class= 'text-danger' >*</span ></label><input class='form-control' type='text'></div>")
            .append("<div class='form-group col-lg-6'><label class='control-label'>Check-In Date</label><input class='form-control' type='date' name='CheckInDate'/></div>")
            .append("<div class='form-group col-lg-6'><label class='control-label'>Check-Out Date</label><input class='form-control' type='date' name='TenDangNhap'/></div>")
            .append("<div class='form-group col-lg-12'><label class='control-label'>TrangThai</label><select class='form-control' name='TrangThai'><option value='Pending'>Pending</option><option value='Confirm'>Confirm</option><option value='Cancel'>Cancel</option></select></div>")
            .append("<div class='form-group col-12'><label class='control-label'>Total Gia</label><input class='form-control' type='number' name='TotalGia'/></div>");

        $this.$modal.find('.delete-event').hide().end().find('.save-event').show().end().find('.modal-body').empty().prepend(form).end().find('.save-event').unbind('click').click(function () {
            form.submit();
        });

        $this.$modal.find('form').on('submit', function () {
            var checkInDate = form.find("input[name='CheckInDate']").val();
            var TenDangNhap = form.find("input[name='TenDangNhap']").val();
            var totalGia = form.find("input[name='TotalGia']").val();
            var TrangThai = form.find("select[name='TrangThai']").val();

            if (checkInDate !== '' && TenDangNhap !== '' && totalGia !== '' && TrangThai !== null) {
                // Convert dates to ISO format if needed
                checkInDate = new Date(checkInDate).toISOString();
                TenDangNhap = new Date(TenDangNhap).toISOString();

                var newBooking = {
                    title: 'Booking Title',
                    start: checkInDate,
                    end: TenDangNhap,
                    TotalGia: totalGia,
                    TrangThai: TrangThai
                };

                $this.$calendarObj.fullCalendar('renderEvent', newBooking, true);
                $this.$modal.modal('hide');
            } else {
                alert('Please fill in all fields.');
            }
            return false;
        });

        $this.$calendarObj.fullCalendar('unselect');
    };

    CalendarApp.prototype.onEventClick = function (calEvent, jsEvent, view) {
        var $this = this;
        var form = $("<form class='row'></form>");
        form.append("<label class='col-12'>" + calEvent.title +"</label>");
        form.append("<div class='form-group col-12'><label class='control-label'>Customer ID</label><input class='form-control' type='text' value='" + calEvent.CustomerId + "'  /></div>")
            .append("<div class='form-group col-6'><label class='control-label'>Check-In Date</label><input class='form-control' type='text' value='" + calEvent.start.format('YYYY-MM-DD') + "'  /></div>")
            .append("<div class='form-group col-6'><label class='control-label'>Check-Out Date</label><input class='form-control' type='text' value='" + calEvent.end.format('YYYY-MM-DD') + "'  /></div>")
            .append("<div class='form-group col-6'><label class='control-label'>Room Number</label><input class='form-control' type='text' value='" + calEvent.roomId + "'  /></div>")
            .append("<div class='form-group col-6'><label class='control-label'>Room Type</label><input class='form-control' type='text' value='" + calEvent.LoaiPhong + "'  /></div>")
            .append("<div class='form-group col-12'><label class='control-label'>TrangThai</label><input class='form-control' type='text' value='" + calEvent.TrangThai + "'  /></div>")
            .append("<div class='form-group col-12'><label class='control-label'> Total Gia </label><input class='form-control' type='text' value='$" + calEvent.TotalGia + "' readonly /></div>");

        $this.$modal.modal({ backdrop: 'static' });
        $this.$modal.find('.delete-event').show().end().find('.save-event').show().end().find('.modal-body').empty().prepend(form);

        $this.$modal.find('form').on('submit', function () {
            $this.$modal.modal('hide');
            return false;
        });
    };

    CalendarApp.prototype.init = function () {
        var $this = this;
        $this.$calendarObj = $this.$calendar.fullCalendar({
            slotDuration: '00:15:00',
            minTime: '08:00:00',
            SoLuongToiDaTime: '19:00:00',
            defaultView: 'month',
            handleWindowResize: true,
            header: { left: 'prev,next today', center: 'title', right: 'month,agendaWeek,agendaDay' },
            events: [], // You can load events from your database here
            editable: true,
            eventLimit: true,
            selectable: true,
            eventClick: function (calEvent, jsEvent, view) {
                $this.onEventClick(calEvent, jsEvent, view);
            },
            select: function (start, end, allDay) {
                $this.onSelect(start, end, allDay);
            }
        });
        $this.fetchAndUpdateEvents();
    };

    CalendarApp.prototype.fetchAndUpdateEvents = function () {
        var $this = this;

        fetch('/Bookings/GetListBookingJson')
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                $this.$calendarObj.fullCalendar('removeEvents');
                var transformedData = data.map(function (item) {
                    return {
                        BookingID: item.BookingID,
                        title: "Booking " + item.BookingID,
                        start: item.CheckInDate,
                        end: item.TenDangNhap,
                        allDay: false,
                        roomId: item.RoomID,
                        CustomerId: item.CustomerId,
                        TotalGia: item.TotalGia,
                        TrangThai: item.TrangThai,
                        LoaiPhong: item.LoaiPhong
                    };
                });
                $this.$calendarObj.fullCalendar('addEventSource', transformedData);
            })
            .catch(function (error) {
                console.error('Error fetching events:', error);
            });
    };

    $.CalendarApp = new CalendarApp;
    $.CalendarApp.Constructor = CalendarApp;
}(window.jQuery));

(function ($) {
    "use strict";
    $.CalendarApp.init();
})(window.jQuery);
