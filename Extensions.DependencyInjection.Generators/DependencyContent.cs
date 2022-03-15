namespace Extensions.DependencyInjection.Generators
{
    public class DependencyContent
    {
        public DependencyContent(string UsingContent, string RegisterContent)
        {
            this.UsingContent = UsingContent;
            this.RegisterContent = RegisterContent;
        }

        public string UsingContent { get; }
        public string RegisterContent { get; }
    }
}