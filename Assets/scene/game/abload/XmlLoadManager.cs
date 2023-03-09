using cambrian.common;
using System.Xml;

namespace scene.game
{
    public class XmlLoadManager
    {
        public static void loadXml()
        {
            string[] xmlsUpdate = SpotRedRoot.roots.regionModule.region.samplesurl;
            if (xmlsUpdate == null) xmlsUpdate = new string[0];

            string path = CacheLocalPath.AB_RESROUCE_PATH;
            for (int i = 0; i < xmlsUpdate.Length; i++)
            {
                byte[] bytes = FileKit.read(path + xmlsUpdate[i]);
                XmlNode node = LoadKit.loadXmlText(System.Text.Encoding.UTF8.GetString(bytes));
                Sample.factory.init(node);
            }
        }
    }
}
