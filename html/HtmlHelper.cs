using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace html
{
internal class HtmlHelper
    {
        private static readonly HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] htmlTags;
        public string[] notNeedCloseTags;

        private HtmlHelper()
        {
            // קרא את קובץ ה-JSON ל- htmlTags
            htmlTags = LoadJsonArrayFromFile("HtmlTags.json");

            // קרא את קובץ ה-JSON ל- notNeedCloseTags
            notNeedCloseTags = LoadJsonArrayFromFile("HtmlVoidTags.json");
        }

        // פונקציה לקרוא את קובץ ה-JSON ולפרוס אותו למערך סטרינגים
        private string[] LoadJsonArrayFromFile(string fileName)
        {
            try
            {
                // קרא את תוכן הקובץ כטקסט
                string jsonContent = File.ReadAllText(fileName);

                // המר את התוכן שהתקבל למערך של סטרינגים
                return JsonConvert.DeserializeObject<string[]>(jsonContent);
            }
            catch (Exception ex)
            {
                // אם יש שגיאה, הצג את השגיאה ותחזיר מערך ריק
                Console.WriteLine($"Error reading or deserializing file {fileName}: {ex.Message}");
                return new string[] { }; // במקרה של שגיאה נחזיר מערך ריק
            }
        }
    }

}
