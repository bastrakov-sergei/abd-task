using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Views
{
    public static class ViewHelpers
    {
        public static string Print(this IHtmlHelper html, byte[] bytes)
        {
            var result = new StringBuilder();
            foreach (var b in bytes)
            {
                result.Append(b.ToString("X2"));
            }
            return result.ToString();
        }
    }
}
