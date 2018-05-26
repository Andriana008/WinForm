using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace WindowsFormsApp
{
    public class LineBL
    {
        public static void SerializeList(List<Line> lines, string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Line>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, lines);
            }
        }

        public static List<Line> DeserializeList(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Line>));
            List<Line> lin = null;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                lin = (List<Line>)formatter.Deserialize(fs);
            }

            if (lin == null)
            {
                throw new ApplicationException(string.Format("cannot deserialize file {0}", path));
            }

            return lin;
        }
    }
}
