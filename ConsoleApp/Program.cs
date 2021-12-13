// See https://aka.ms/new-console-template for more information

using System.Xml.Serialization;
using ConsoleApp;
using XmlLab_v15;

// var oldCard= new OldCard()
// {
//     Country = new Country()
//     {
//         Name = "Ukraine"
//     }
// };
//
// var xs = new XmlSerializer(typeof(OldCard));
// var tw = new StreamWriter(@"oldCard.xml");
// xs.Serialize(tw, oldCard);
//
// Console.WriteLine("Done");

var cards = XmlHelper.ProcessXml();
foreach (var card in cards)
{
    Console.WriteLine(card.Id);
}

Console.WriteLine("\nXML validation result:");
XmlHelper.ValidateXml();

Console.WriteLine("\nXML -> JSON:");
XmlHelper.XmlToJson();