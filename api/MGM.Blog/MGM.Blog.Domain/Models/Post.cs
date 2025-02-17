namespace MGM.Blog.Domain.Models
{
    public sealed class Post : Entity
    {
        public string Text { get; private set; } = null!;

        public string Title { get; private set; } = null!;

        public User User { get; set; } = null!;

        public Guid UserId { get; set; }

        public Post()
        {}

        public Post(string text, string title, Guid userId)
        {
            Text = text;
            Title = title;
            UserId = userId;
        }

        public static Post Create(string text, string title, Guid userId)
        {
            ValidateInputs(text, title, userId);
            return new Post(text, title, userId);
        }

        public void Update(string text, string title, Guid userId)
        {
            ValidateInputs(text, title, userId);

            Text = text;
            Title = title;
            UserId = userId;

            UpdateLastModified();
        }

        private static void ValidateInputs(string text, string title, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be null or empty.", nameof(text));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Text cannot be null or empty.", nameof(title));

            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
        }
    }
}
