using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
