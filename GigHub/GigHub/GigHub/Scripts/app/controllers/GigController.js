var GigController = function (attandancesService) {
    var btnToggleAttendance;

    var init = function (container) {
        $(container)
            .on("click", ".js-toggle-attendace", toggleAttendance);

        $("#searchTerm")
            .blur(function () {
                $.get("/Home/Index",
                    { "query": $("#searchTerm").val() },
                    function (response) {

                    });
            });
    }

    var toggleAttendance = function (e) {
        btnToggleAttendance = $(e.target);

        var dto = JSON.stringify({ GigId: btnToggleAttendance.attr("data-gig-id") });

        if (btnToggleAttendance.hasClass("btn-default")) {
            attandancesService.addAttendance(dto, done, fail);
        } else {
            attandancesService.deleteAttendance(dto, done, fail);
        }

    }

    var done = function (msg) {
        var text = (btnToggleAttendance.text().trim() === "Going") ? "Going ?" : "Going";

        btnToggleAttendance.toggleClass("btn-default").toggleClass("btn-info").text(text);
    }

    var fail = function (msg) {
        if (msg != null) {
            if (msg.Message != null)
                alert(msg.Message);
            else if (msg.responseJSON != null) {
                alert(msg.responseJSON.Message);
            }
        }
    };
    return {
        init: init
    }
}(AttandancesService);
