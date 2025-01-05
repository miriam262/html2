// See https://aka.ms/new-console-template for more information
using html;
using System.Linq;
using System.Text.RegularExpressions;

var html = await Load("https://hebrewbooks.org/bais");

var clenHtml = new Regex("\\s").Replace(html, "");

var htmlByTags = new Regex("<(.*?)>").Split(clenHtml).Where(s => s.Length > 0);

var root = new HtmlElement();
var currentElement = root;
currentElement.Parent = null;
currentElement.Name = "html";
foreach (var line in htmlByTags)
{
    bool flag = false;
    if (line == "/html") break;
    else if (line.StartsWith('/'))
        currentElement = currentElement.Parent;
    else
        foreach (var tag in HtmlHelper.Instance.htmlTags)
            if (line.StartsWith(tag))
            {
                flag = true;
                var child = new HtmlElement();
                if (currentElement != null)
                    currentElement.Children.Add(child);
                currentElement = child;
                child.Parent = currentElement;
                currentElement.Attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line).Cast<Match>().Select(m => m.Value).ToArray();
                currentElement.Name = tag;
                foreach (var attrib in currentElement.Attributes)
                {
                    if (attrib.StartsWith("class"))
                        currentElement.Classes = attrib.Split(' ').ToArray();
                    else if (attrib.Substring(tag.Length).StartsWith("id"))
                        currentElement.Id = attrib.Substring(tag.Length * 2 + 1);
                }
            }
    if (!flag)
        currentElement.InnerHtml = line;
    if (line.EndsWith('/'))
        currentElement = currentElement.Parent;
    else
        foreach (var item in HtmlHelper.Instance.notNeedCloseTags)
        {
            if (line.StartsWith(item))
                currentElement = currentElement.Parent;
        }
}
Console.WriteLine(Selector.piruk("#mydiv.dd.ss div.cc"));
currentElement = new HtmlElement();
currentElement.Name = "div";
var cdiv = new HtmlElement();var div2 = new HtmlElement();var h2 = new HtmlElement();var ch2 = new HtmlElement();var cdiv2 = new HtmlElement();
cdiv.Name = "div";h2.Id = "uuuu"; ;div2.Name = "div";cdiv2.Name = "div";h2.Name = "h2";
currentElement.Id = "1";cdiv.Id = "2";div2.Id = "3";
currentElement.Children.Add(cdiv); cdiv.Children.Add(div2) ;div2.Children.Add(h2);cdiv.Parent = currentElement;div2.Parent = cdiv ;h2.Parent = div2;
div2.Parent = cdiv; cdiv.Parent = currentElement;
currentElement.Children.Add(cdiv); cdiv.Children.Add(div2);
foreach (var descendant in currentElement.Descendants())
{
    Console.WriteLine(descendant.Name);
}
//foreach (var a in cm.Ancestors())
//{
//    Console.WriteLine(a.Name);
//}
var selector = Selector.piruk("div h2");
var matchingElements = currentElement.FindElements(selector);

foreach (var element in matchingElements)
{
    Console.WriteLine($"Found element: {element.Name} with ID: {element.Id}");
}

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
