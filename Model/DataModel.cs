using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditableTextBox_Demo.Model
{
    public static class DataModel
    {
        public static string GetData()
        {
            return "<Window x:Class=\"TextBoxStylingExample.MainWindow\"\r\n" +
                "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n" +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n" +
                "Title=\"TextBox Styling\" Height=\"200\" Width=\"400\">\r\n" +
                "<Window.Resources>\r\n" +
                "\t<!-- Define a style for the TextBox -->\r\n" +
                "\t<Style x:Key=\"StyledTextBox\" TargetType=\"TextBox\">\r\n" +
                "\t<Setter Property=\"Width\" Value=\"%width%\"/>\r\n" +
                "\t<Setter Property=\"Height\" Value=\"%height%\"/>\r\n" +
                "\t<Setter Property=\"FontSize\" Value=\"%fontsize%\"/>\r\n" +
                "\t<Setter Property=\"Padding\" Value=\"%padding%\"/>\r\n" +
                "\t<Setter Property=\"BorderBrush\" Value=\"%borderbrush%\"/>\r\n" +
                "\t<Setter Property=\"BorderThickness\" Value=\"%borderthickness%\"/>\r\n" +
                "\t<Setter Property=\"Background\" Value=\"%background%\"/>\r\n" +
                "\t<Setter Property=\"Foreground\" Value=\"%foreground%\"/>\r\n" +
                "\t<Setter Property=\"VerticalContentAlignment\" Value=\"%verticalcontentalignment%\"/>\r\n" +
                "</Style>\r\n" +
                "</Window.Resources>\r\n\r\n" +
                "<Grid>\r\n" +
                "\t<!-- Apply the style to the TextBox -->\r\n" +
                "\t<TextBox Style=\"{StaticResource StyledTextBox}\" HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\"/>\r\n" +
                "</Grid>\r\n</Window>";
        }

        public static Dictionary<string, string> GetDataReplacements()
        {
            return new Dictionary<string, string>
            {
                { "width", "32" },
                { "height", "130" },
                { "foreground", "Black" },
                { "background", "LightBlue" },
                { "fontsize", "16" },
                { "borderbrush", "DarkBlue" },
                { "borderthickness", "2" },
                { "padding", "10" },
                { "verticalcontentalignment", "Center" }
            };
        }
    }
}
