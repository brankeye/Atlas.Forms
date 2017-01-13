using System.Collections.Generic;

namespace atlas.core.Library.Pages
{
    public static class PageKeyParser
    {
        public static readonly char[] _defaultSeparator = { '/' };

        public static Queue<string> GetPageKeysFromSequence(string sequence)
        {
            var queue = new Queue<string>();
            var blocks = sequence.Split(_defaultSeparator);
            foreach (var block in blocks)
            {
                queue.Enqueue(block);
            }
            return queue;
        }

        public static bool IsSequence(string key)
        {
            return key.Contains(_defaultSeparator[0].ToString());
        }
    }
}
