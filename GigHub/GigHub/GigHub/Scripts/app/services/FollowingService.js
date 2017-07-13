var FollowingService;
(function (FollowingService_1) {
    var FollowingService = (function () {
        function FollowingService() {
        }
        FollowingService.prototype.AddFollowing = function (dto, done, fail) {
            $.ajax({
                url: "../../api/Following/Follow",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: dto,
                success: function (msg) {
                    done(msg);
                },
                error: function (msg) {
                    fail(msg);
                }
            });
        };
        FollowingService.prototype.RemoveFollowing = function (dto, done, fail) {
            $.ajax({
                url: "../../api/Following/UnFollow",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: dto,
                success: function (msg) {
                    done(msg);
                },
                error: function (msg) {
                    fail(msg);
                }
            });
        };
        return FollowingService;
    }());
    FollowingService_1.FollowingService = FollowingService;
})(FollowingService || (FollowingService = {}));
//# sourceMappingURL=FollowingService.js.map