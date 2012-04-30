using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml.Serialization;
using System.IO;

namespace La2Launch
{
    [XmlRootAttribute(ElementName = "LineageServerList", IsNullable = false)]
    public class LaServerList
    {
        private List<LaServer> serverList = new List<LaServer>();
        private int defaultServer;
        private string ggServer;

        public List<LaServer> ServerList
        {
            get { return serverList; }
            set { serverList = value; }
        }

        public void AddServer(LaServer server)
        {
            this.serverList.Add(server);
        }

        public void DelServer(LaServer server)
        {
            this.serverList.Remove(server);
        }

        public LaServer this[int i]
        {
            get 
            {
                try
                {
                    return this.serverList[i];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }
            }
        }

        public int DefaultServer
        {
            get { return defaultServer; }
            set 
            {
                defaultServer = value;
                if (value > this.serverList.Count)
                    this.defaultServer = 0;
            }
        }

        public string GgServer
        {
            get { return ggServer; }
            set { ggServer = value; }
        }

        public void SaveServerlist(string filename)
        {
            if (this.ServerList.Count == 0)
                return;

            // Create an instance of the XmlSerializer class;
            // specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(LaServerList));
            TextWriter writer = new StreamWriter(filename);

            // Serialize the purchase order, and close the TextWriter.
            serializer.Serialize(writer, this);
            writer.Close();
        }

        public static LaServerList LoadServerList(string filename)
        {
            // retrun an emtpy object
            if (!File.Exists(filename))
                return new LaServerList();

            // Create an instance of the XmlSerializer class;
            // specify the type of object to be deserialized.
            XmlSerializer serializer = new XmlSerializer(typeof(LaServerList));

            /* If the XML document has been altered with unknown 
            nodes or attributes, handle them with the 
            UnknownNode and UnknownAttribute events.*/
            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);

            /* Use the Deserialize method to restore the object's state with
            data from the XML document. */
            LaServerList serverList = (LaServerList)serializer.Deserialize(fs);
            fs.Close();
            return serverList;
        }

        private static void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            //Program.debugStream.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        private static void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            //System.Xml.XmlAttribute attr = e.Attr;
            //Program.debugStream.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
        }
    }

    public class LaServer
    {
        #region private Members

        private string serverName;
        private string serverIp;
        private string appToStart;
        private bool l2auth;
        private bool l2testauth;
        private bool ggServer;

        #endregion

        #region Public Getter/Setter

        public string ServerIp
        {
            get { return serverIp; }
            set { serverIp = value; }
        }

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        public string AppToStart
        {
            get { return appToStart; }
            set { appToStart = value; }
        }

        public bool L2auth
        {
            get { return l2auth; }
            set { l2auth = value; }
        }

        public bool L2testauth
        {
            get { return l2testauth; }
            set { l2testauth = value; }
        }

        public bool GgServer
        {
            get { return ggServer; }
            set { ggServer = value; }
        }
#endregion

        public override string ToString()
        {
            return this.serverName;
        }
    }
}
