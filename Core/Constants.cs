namespace University.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Admin";
            public const string Manager = "Manager";
            public const string Student = "Lecturer";
            public const string Lecturer = "Student";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireManager = "RequireManager";
        }
    }
}
