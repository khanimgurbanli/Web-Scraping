namespace Test.Helpers
{
    public static class GetCategoryName
    {
        public static string Get(this string url)
        {
            string _category=string.Empty;
            string[] parts = url.Split('?');
            if (parts.Length > 0)
            {
                string[] categoryParts = parts[0].Split('/');
                _category = categoryParts.LastOrDefault();
            }

            return _category;
        }
    }
}
