using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;

namespace Church.Domain.Models
{
    public class GenericMailMessage(string title, string content)
    {
        public string Title
        {
            get
            {
                return title;
            }
        }

        public IHtmlContent Content
        {
            get
            {
                IHtmlContent result;

                if (string.IsNullOrWhiteSpace(content))
                {
                    return new HtmlString(string.Empty);
                }

                var doc = new HtmlDocument();
                doc.LoadHtml(content);

                using (var writer = new StringWriter())
                {
                    doc.Save(writer);
                    result = new HtmlString(writer.ToString());
                }

                return result;
            }
        }
    }
}
