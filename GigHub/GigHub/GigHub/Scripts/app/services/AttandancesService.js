var AttandancesService = function () {
    var addAttendance = function (dto, done, fail) {
        $.ajax({
            url: "api/Attendances/Attend",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: dto,
            success: function (msg) { done(msg) },
            error: function (msg) { fail(msg) }
        });
    }

    var deleteAttendance = function (dto, done, fail) {
        $.ajax({
            url: "api/Attendances/DeleteAttendance",
            type: "DELETE",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: dto,
            success: function (msg) { done(msg) },
            error: function (msg) { fail(msg) }
        });
    }
    return {
        addAttendance: addAttendance,
        deleteAttendance: deleteAttendance
    }
}();