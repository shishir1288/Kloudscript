namespace Kloudscript.Quiz.Url.Shortening.Utilities
{
    public class ApplicationSettings
    {
        public Connectionstrings ConnectionStrings { get; set; }
        public string ServiceUrl { get; set; }
        public int? ShortCodeLength { get; set; }
        public bool IsShortCodeAvailable { get; set; }
        public bool IsOrignialUrlSetToNull { get; set; }
    }

    public class Connectionstrings
    {
        public string DefaultConnection { get; set; }
    }
}
