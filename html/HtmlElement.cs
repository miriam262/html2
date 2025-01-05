using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace html
{
    internal class HtmlElement
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public string[] Attributes { get; set; }
        public string[] Classes { get; set; }
        public string InnerHtml {  get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }
        public HtmlElement() {
            Name = "";
            InnerHtml = "";
            Children = new List<HtmlElement>();
        }
        public IEnumerable<HtmlElement> Descendants ()//כאן עשיתי רק בנים ישירים ועלי לבדוק אם צריך גם נכדים ואם כן אז איך עושים את זה?S
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current != null)
                {
                    yield return current;
                    foreach (var item in current.Children)
                    {
                        queue.Enqueue(item);
                    }
                   
                }
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            var curr = Parent;
            while(curr != null)
            {
                yield return curr;
                curr = curr.Parent;
            }
        }
        public IEnumerable<HtmlElement> FindElements(Selector selector)
        {
            // רשימה שתשמור את כל האלמנטים שמצאנו
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();

            // הפעל את הפונקציה הריקורסיבית שתעבור על הצאצאים
            FindElementsRecursive(this, selector, result);

            return result;
        }

        private void FindElementsRecursive(HtmlElement currentElement, Selector selector, HashSet<HtmlElement> result)
        {
            // 1. אם האלמנט הנוכחי לא עונה לקריטריונים של הסלקטור, נמשיך הלאה
            if (!MatchesSelector(currentElement, selector))
                return;

            // 2. אם הסלקטור הנוכחי הוא האחרון, נוסיף את האלמנט לתוצאה
            if (selector.child == null)
            {
                result.Add(currentElement);
            }
            else
            {
                // 3. אחרת, נמשיך לרוץ על כל הצאצאים עם הסלקטור הבא (כולל נכדים ונינים)
                foreach (var descendant in currentElement.Descendants())
                {
                    // אנו מבצעים את הקריאה הרקורסיבית גם עבור כל צאצא, ולא רק לילד הראשון.
                    FindElementsRecursive(descendant, selector.child, result);
                }
            }

        }
        private bool MatchesSelector(HtmlElement element, Selector selector)
        {
            // 1. אם יש לסלקטור שם תג, נוודא שהאלמנט תואם
            if (!string.IsNullOrEmpty(selector.TagName) && element.Name != selector.TagName)
                return false;

            // 2. אם יש לסלקטור -ID, נוודא שהאלמנט תואם
            if (!string.IsNullOrEmpty(selector.Id) && element.Id != selector.Id)
                return false;

            // 3. אם יש לסלקטור מחלקות, נוודא שהאלמנט מכיל את כל המחלקות
            if (selector.Classes.Any() && !selector.Classes.All(c => element.Classes.Contains(c)))
                return false;

            return true;
        }


    }
}
