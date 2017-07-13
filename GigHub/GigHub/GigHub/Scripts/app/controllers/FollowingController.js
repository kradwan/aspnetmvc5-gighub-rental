var FollowingController;
(function (FollowingController_1) {
    var FollowingController = (function () {
        function FollowingController() {
            this.followingService = new FollowingService.FollowingService();
        }
        FollowingController.prototype.Init = function (container) {
            var _this = this;
            $(container)
                .on("click", ".js-toggle-following", function (e) {
                _this.btnToggleFollowing = $(e.target);
                var dto = JSON.stringify({ FolloweeId: _this.btnToggleFollowing.attr("data-follow-id") });
                if (_this.btnToggleFollowing.hasClass("btn-default")) {
                    _this.followingService.AddFollowing(dto, function (msg) {
                        _this.done(msg);
                    }, function (msg) {
                        _this.fail(msg);
                    });
                }
                else {
                    _this.followingService.RemoveFollowing(dto, function (msg) {
                        _this.done(msg);
                    }, function (msg) {
                        _this.fail(msg);
                    });
                }
            });
        };
        FollowingController.prototype.done = function (msg) {
            var text = this.btnToggleFollowing.text().trim() === "Follow" ? "Following" : "Follow";
            this.btnToggleFollowing.toggleClass("btn-default").toggleClass("btn-info").text(text);
        };
        FollowingController.prototype.fail = function (msg) {
            if (msg != null) {
                if (msg.Message != null)
                    alert(msg.Message);
                else if (msg.responseJSON != null) {
                    alert(msg.responseJSON.Message);
                }
            }
        };
        return FollowingController;
    }());
    FollowingController_1.FollowingController = FollowingController;
})(FollowingController || (FollowingController = {}));
//# sourceMappingURL=FollowingController.js.map