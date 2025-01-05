using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace html
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector parent { get; set; }
        public Selector child { get; set; }
        
        public Selector() {
            TagName = string.Empty;
            Id = string.Empty;
            Classes = new List<string>();
        }
        public static Selector piruk(string query)
        {
            var parts = query.Split(' ');
            Selector root = new Selector();
            Selector currentSelector = root;

            foreach (var part in parts)
            {
                var selector = new Selector();
                if (part.StartsWith('#'))
                {
                    selector.Id = part.Substring(1);
                }
                else if (part.StartsWith('.'))
                {
                    selector.Classes.Add(part.Substring(1));
                }
                else
                {
                    selector.TagName = part;
                }

                currentSelector.child = selector;
                selector.parent = currentSelector;
                currentSelector = selector;
            }

            return root;
        }

    }
}
