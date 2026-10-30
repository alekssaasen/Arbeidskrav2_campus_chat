using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Data;

public static class SeedData
{
    /// <summary>
    /// Creates a list of seed user profiles.
    /// </summary>
    /// <returns>A list of profiles.</returns>
    public static List<Profile> CreateProfiles()
    {
        return new List<Profile>
        {
            Profile.Create(
                1, "Alex Morgan", "Backend development student",
                false, "#4A90E2", new DateOnly(2025, 8, 15)),

            Profile.Create(
                2, "Sofia Hansen", "Interested in web development",
                false, "#E57373", new DateOnly(2025, 8, 15)),

            Profile.Create(
                3, "Oliver Berg", "Learning C# and databases",
                false, "#81C784", new DateOnly(2024, 8, 15)),

            Profile.Create(
                4, "Emma Johansen", "Enjoys collaborating on projects",
                false, "#BA68C8", new DateOnly(2024, 8, 15)),

            Profile.Create(
                5, "Noah Andersen", "Interested in software architecture",
                false, "#FFB74D", new DateOnly(2023, 8, 15)),

            Profile.Create(
                6, "Audit User", "Read-only audit account",
                true, "#78909C", new DateOnly(2023, 8, 15))
        };
    }

    /// <summary>
    /// Creates a list of seed discussion threads.
    /// </summary>
    /// <returns>A list of discussion threads.</returns>
    public static List<DiscussionThread> CreateThreads()
    {
        return new List<DiscussionThread>
        {
            DiscussionThread.Create(
                1,
                "C# Help",
                "Programming",
                "Ask questions and help each other learn C#.",
                false,
                ""
            ),

            DiscussionThread.Create(
                2,
                "Backend Projects",
                "Software Development",
                "Discuss backend projects, APIs, and architecture.",
                false,
                ""
            ),

            DiscussionThread.Create(
                3,
                "Database Discussions",
                "Databases",
                "Share SQL tips and discuss database design.",
                false,
                ""
            ),

            DiscussionThread.Create(
                4,
                "Database Study Group",
                "Study Groups",
                "Private study group for database assignments.",
                true,
                "sql123"
            ),

            DiscussionThread.Create(
                5,
                "Exam Preparation",
                "Exams",
                "Private discussion about upcoming exams.",
                true,
                "exam2026"
            ),

            DiscussionThread.Create(
                6,
                "General Chat",
                "Community",
                "Talk about school, projects, and student life.",
                false,
                ""
            )
        };
    }
}