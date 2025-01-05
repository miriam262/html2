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
        public static Selector piruk(string quary) { 
            var arrs = quary.Split(' ');
            Selector root = new Selector(), currentIterate = root;
            foreach (var item in arrs)
            {
                List<string> list = new List<string>();
                var j = 0;
                for (var i = 1; i < item.Length && j < item.Length; i++)
                {
                    if (item[i] == '.' || item[i] == '#')
                    {
                        list.Add(item.Substring(j, i-j));
                        j = i;
                    }
                }
                list.Add(item.Substring(j, item.Length-j));
                if (!(list[0].StartsWith('.') || list[0].StartsWith('#')))
                    foreach (var tag in HtmlHelper.Instance.htmlTags)
                        if (list[0].StartsWith(tag))
                        {
                            currentIterate.TagName = tag;
                        }
                foreach (var item1 in list)
                {
                   if(item1.StartsWith('#'))
                        currentIterate.Id = item1.Substring(1);
                   else if(item1.StartsWith('.'))
                        currentIterate.Classes.Add(item1.Substring(1));
                }
                var child = new Selector();
                child.parent = currentIterate;
                currentIterate.child = child;
                currentIterate = child;
            }
            return root;
        }
    }
}
