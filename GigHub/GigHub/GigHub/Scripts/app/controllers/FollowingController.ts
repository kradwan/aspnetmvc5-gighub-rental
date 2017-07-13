module FollowingController {

    export class FollowingController {

        private followingService: FollowingService.FollowingService;
        private btnToggleFollowing: JQuery;

        constructor() {
            this.followingService = new FollowingService.FollowingService();
        }

        public Init(container: string): void {
            $(container)
                .on("click", ".js-toggle-following", (e) => {
                    this.btnToggleFollowing = $(e.target);
                    let dto = JSON.stringify({ FolloweeId: this.btnToggleFollowing.attr("data-follow-id") });

                    if (this.btnToggleFollowing.hasClass("btn-default")) {
                        this.followingService.AddFollowing(
                            dto,
                            (msg) => {
                                this.done(msg);
                            },
                            (msg) => {
                                this.fail(msg);
                            });
                    } else {
                        this.followingService.RemoveFollowing(
                            dto,
                            (msg) => {
                            this.done(msg);
                            },
                            (msg) => {
                            this.fail(msg);
                        }
                        );
                    }

                });
        }
        private done(msg: string) {
            let text = this.btnToggleFollowing.text().trim() === "Follow" ? "Following" : "Follow";
            this.btnToggleFollowing.toggleClass("btn-default").toggleClass("btn-info").text(text);
        }

        private fail(msg: any) {
            if (msg != null) {
                if (msg.Message != null)
                    alert(msg.Message);
                else if (msg.responseJSON != null) {
                    alert(msg.responseJSON.Message);
                }
            }
        }
    }
}