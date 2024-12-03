using AspNetIdentity.Enums;

namespace AspNetIdentity.Extensions
{
    public static class RolesExtension
    {
        public static string GetRole(this Roles role)
        {
            string res = role switch
            {
                Roles.User => nameof(Roles.User),
                Roles.Admin => nameof(Roles.Admin),
                Roles.Moderator => nameof(Roles.Moderator),
                Roles.SMM => nameof(Roles.SMM),
                Roles.Cashier => nameof(Roles.Cashier)
            };

            return res;
        }
    }
}
