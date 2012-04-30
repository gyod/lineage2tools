using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace SpoilStatus
{
    // Todo: zur Singelton machen damit von überall zugreifbar ist
    [XmlRootAttribute(ElementName = "LineageServerList", IsNullable = false)]
    public class ServerList
    {
        private List<ServerInfo> serverInfoList = new List<ServerInfo>();

        public ServerList()
        {
            /*
            // Fill some dummys
            ServerInfo info = new ServerInfo();
            info.AdenaRate = 1;
            info.DefaultServer = false;
            info.DropRate = 1;
            info.ExpRate = 1;
            info.GameHost = "123.123.123.123";
            info.ServerName = "DummyServer";
            info.ServerWebAdress = "DummyAdress";
            info.SpoilRate = 1;
            info.SpRate = 1;

            serverInfoList.Add(info);*/
        }

        public void SaveServerlist(string filename)
        {
            if (this.serverInfoList.Count == 0)
                return;

            // Create an instance of the XmlSerializer class;
            // specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ServerList));
            TextWriter writer = new StreamWriter(filename);
           
            // Serialize the purchase order, and close the TextWriter.
            serializer.Serialize(writer, this);
            writer.Close();
        }

        public static ServerList LoadServerList(string filename)
        {
            // retrun an emtpy object
            if (!File.Exists(filename))
                return new ServerList();

            // Create an instance of the XmlSerializer class;
            // specify the type of object to be deserialized.
            XmlSerializer serializer = new XmlSerializer(typeof(ServerList));

            /* If the XML document has been altered with unknown 
            nodes or attributes, handle them with the 
            UnknownNode and UnknownAttribute events.*/
            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);
            // Declare an object variable of the type to be deserialized.
            ServerList serverList;
            /* Use the Deserialize method to restore the object's state with
            data from the XML document. */
            serverList = (ServerList)serializer.Deserialize(fs);
            fs.Close();
            return serverList;
        }

        private static void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
#if DEBUG
            Program.debugStream.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
#endif
        }

        private static void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
#if DEBUG
            System.Xml.XmlAttribute attr = e.Attr;
            Program.debugStream.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
#endif
        }


        [XmlArrayAttribute("Servers")]
        public List<ServerInfo> ServerInfoList
        {
            get { return serverInfoList; }
            set { serverInfoList = value; }
        }

        [XmlIgnore]
        public ServerInfo DefaultServer
        {
            get { return this.serverInfoList[0]; }
            set 
            {
                int idx = this.serverInfoList.IndexOf(value);
                ServerInfo info = this.serverInfoList[idx];
                this.serverInfoList.Remove(info);
                this.serverInfoList.Insert(0, info);
            }
        }

        public bool HasElements()
        {
            return this.serverInfoList.Count > 0;
        }
    }

    public class ServerInfo
    {
        #region private members

        private string serverName;
        private string serverWebAdress;
        private string gameHost;
        private int expRate = 1;
        private int spRate = 1;
        private int dropRate = 1;
        private int spoilRate = 1;
        private int adenaRate = 1;
        
        #endregion

        #region public getter/setter

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        public string ServerWebAdress
        {
            get { return serverWebAdress; }
            set { serverWebAdress = value; }
        }

        public string GameHost
        {
            get { return gameHost; }
            set { gameHost = value; }
        }

        public int ExpRate
        {
            get { return expRate; }
            set { expRate = value; }
        }

        public int SpRate
        {
            get { return spRate; }
            set { spRate = value; }
        }

        public int DropRate
        {
            get { return dropRate; }
            set { dropRate = value; }
        }

        public int SpoilRate
        {
            get { return spoilRate; }
            set { spoilRate = value; }
        }

        public int AdenaRate
        {
            get { return adenaRate; }
            set { adenaRate = value; }
        }

        #endregion

        public override string ToString()
        {
            return this.serverName;
        }
    }
}
