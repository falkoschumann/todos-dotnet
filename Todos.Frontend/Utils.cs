namespace Todos.Frontend
{
    internal class Utils
    {
        internal static string Pluralize(int count, string word)
        {
            return count > 1 ? $"{word}s" : word;
        }
    }
}
