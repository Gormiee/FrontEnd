using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Vinter22_Eksamen_boardgames.Models;

namespace Vinter22_Eksamen_boardgames.Data
{
    internal class FileTranslator
    {
        internal static ObservableCollection<Game> ReadFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Game>));
            TextReader reader = new StreamReader(fileName);
            var games = (ObservableCollection<Game>)serializer.Deserialize(reader);
            reader.Close();
            return games;
        }

        internal static void SaveFile(string fileName, ObservableCollection<Game> games)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Game>));
            TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, games);
            writer.Close();
        }
    }
}
