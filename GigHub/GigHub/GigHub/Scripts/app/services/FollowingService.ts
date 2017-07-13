module FollowingService {
    export class FollowingService {
        constructor() {
            
        }

        public  AddFollowing(dto, done, fail) {
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
        }

        public RemoveFollowing(dto, done, fail) {
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

        }
    }
}