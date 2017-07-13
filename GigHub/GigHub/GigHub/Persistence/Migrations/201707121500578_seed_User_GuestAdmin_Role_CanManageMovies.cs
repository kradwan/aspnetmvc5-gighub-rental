namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class seed_User_GuestAdmin_Role_CanManageMovies : DbMigration
    {
        public override void Up()
        {
            Sql(@"
/* Add users: Admin & Guest */
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'c6ca04df-30fd-4d63-97fb-b881e4a2209b', N'admin@admin.com', 0, N'APQjilEt2qZZlLFiuGv9I+1u927YSmxZgMHQTi3BDq1oOjFg9y1W7fRHIsLpoY7bgw==', N'2a1d759e-126a-408e-a521-9b4c035b139c', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com', N'admin')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'f57460de-d8c3-4627-9035-d6e04bcd0eaa', N'guest@guest.com', 0, N'ACOtDcAO41GNGz4og/KFyK45UqQJmgu8awcUOnDLT16mGAghcdJPzAKkACO0e+Sf1A==', N'18d5399f-6f38-45ba-9168-19459e44b9a5', NULL, 0, 0, NULL, 1, 0, N'guest@guest.com', N'guest')

/* Add role: CanManageMovies */
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9a6c0d91-a3a9-4ab0-bcc6-02889de433ad', N'CanManageMovies')

/* Subscribe: Admin to CanManageMovies */
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c6ca04df-30fd-4d63-97fb-b881e4a2209b', N'9a6c0d91-a3a9-4ab0-bcc6-02889de433ad')
            ");
        }

        public override void Down()
        {
        }
    }
}
