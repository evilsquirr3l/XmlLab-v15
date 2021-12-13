using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json;
using XmlLab_v15;

namespace ConsoleApp;

public static class XmlHelper
{
    public static void XmlToJson()
    {
        var xDoc = new XmlDocument();
        xDoc.Load("oldCards.xml");
        var xRoot = xDoc.DocumentElement;

        var childNodes = xRoot!.SelectNodes("*")!;
        foreach (XmlNode n in childNodes)
            System.Console.WriteLine(JsonConvert.SerializeXmlNode(n, Newtonsoft.Json.Formatting.Indented, false));
    }

    public static OldCard[] ProcessXml()
    {
        var cards = new List<OldCard>();

        var xDoc = new XmlDocument();
        xDoc.Load("oldCards.xml");
        var xRoot = xDoc.DocumentElement;

        var childNodes = xRoot!.SelectNodes("*")!;
        foreach (XmlNode n in childNodes)
        {
            var card = new OldCard()
            {
                Id = Convert.ToInt32(n.SelectSingleNode("@Id")?.InnerText),
                Author = n.SelectSingleNode("Author")?.InnerText ?? "",
                CardType = Enum.Parse<CardType>(n.SelectSingleNode("CardType")?.InnerText ?? "Unknown"),
                Country = new Country()
                {
                    Id = Convert.ToInt32(n.SelectSingleNode("//Id")?.InnerText),
                    Name = n.SelectSingleNode("//Name")?.InnerText ?? "",
                },
                IsSent = Convert.ToBoolean(n.SelectSingleNode("//IsSent")?.InnerText),
                PublishingYear = Convert.ToInt32(n.SelectSingleNode("PublishingYear")?.InnerText),
                Theme = Enum.Parse<Theme>(n.SelectSingleNode("Theme")?.InnerText ?? "Unknown"),
                Valuable = Enum.Parse<Valuable>(n.SelectSingleNode("Valuable")?.InnerText ?? "Unknown")
            };

            cards.Add(card);
        }

        cards.Sort();

        return cards.ToArray();
    }

    public static void ValidateXml()
    {
        var schema = new XmlSchemaSet();
        schema.Add("", "oldCard.xsd");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load("invalid.xml");
        xmlDoc.Schemas.Add(schema);
        xmlDoc.Validate(ValidationEventHandler!);
    }

    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error)
            System.Console.WriteLine(e.Message);
    }
}