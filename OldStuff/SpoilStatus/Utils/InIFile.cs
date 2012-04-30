using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SpoilStatus.Utils
{
    public class InIFile
    {
        private FileStream file;
        private string filePath;
        private Dictionary<string, object> properties = new Dictionary<string, object>();

        public InIFile(string fileName)
        {
            this.filePath = fileName;
            if (File.Exists(fileName))
            {
                this.file = new FileStream(fileName, FileMode.Open);
                this.readFile();
            } 
        }

        private void readFile()
        {
            StreamReader reader = new StreamReader(file);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.StartsWith("#")) // Kommentar
                    continue;

                string[] splittedLine = line.Split('=');
                if (splittedLine.Length != 2)
                {
                    reader.Close();
                    throw new MalformedInIFileException(line);
                }

                this.properties.Add(splittedLine[0], splittedLine[1]);
            }
            reader.Close();
            file.Close();
        }

        public object getValue(string property, object defaultValue)
        {
            if (!this.properties.ContainsKey(property))
                return defaultValue;

            return this.properties[property];
        }

        #region public getters

        public int GetAsInt(string property, int defaultValue)
        {
            return Convert.ToInt32(this.getValue(property, defaultValue));
        }

        public string GetAsString(string property, string defaultValue)
        {
            return Convert.ToString(this.getValue(property, defaultValue));
        }

        public bool GetAsBool(string property, bool defaultValue)
        {
            return Convert.ToBoolean(this.getValue(property, defaultValue));
        }

        public double GetAsDouble(string property, double defaultValue)
        {
            return Convert.ToDouble(this.getValue(property, defaultValue));
        }

        #endregion

        #region Exceptions

        public class MalformedInIFileException : Exception
        {
            public MalformedInIFileException(string iniLine)
                : base("Line '" + iniLine + "' malformed")
            {
            }
        }

        public class PropertyNotFoundException : Exception
        {
            private string property;
            public PropertyNotFoundException(string missingProperty) 
                : base("Property '" + missingProperty + "' not found")
            {
                this.property = missingProperty;
            }

            public string MissingProperty
            {
                get { return property; }
            }
        }

        #endregion

        /// <summary>
        /// Saves all Properties marked with [InIAttr]
        /// </summary>
        /// <param name="o">The Object with the Properties to save</param>
        public void SaveProperties(object o)
        {
            string fileName = this.filePath;
            // Close the Old File and create a new one
            StreamWriter writer = new StreamWriter(fileName, false);
            writer.WriteLine("# generated by IniFileReader/Writer");

            Type t = o.GetType();
            foreach (PropertyInfo pInfo in t.GetProperties())
            {
                // Iterate through all the Attributes for each method.
                foreach (Attribute attr in Attribute.GetCustomAttributes(pInfo))
                {
                    // Check for the InIAttr attribute.
                    if (attr.GetType() == typeof(InIAttrAttribute))
                    {
                        if (pInfo.PropertyType.IsEnum && ((InIAttrAttribute)attr).SaveEmumAsInt)
                            writer.WriteLine(pInfo.Name + "=" + (int)pInfo.GetValue(o, null));
                        else
                            writer.WriteLine(pInfo.Name + "=" + pInfo.GetValue(o, null));
                    }
                }

            }

            writer.Flush();
            writer.Close();
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class InIAttrAttribute : Attribute
    {
        private bool saveEmumAsInt = false;

        public InIAttrAttribute()
        {

        }
        /// <summary>
        /// If True, A Enum will be saved as an Integer
        /// </summary>
        /// <param name="saveEmumAsInt"></param>
        public InIAttrAttribute(bool saveEmumAsInt)
        {
            this.saveEmumAsInt = saveEmumAsInt;
        }

        public bool SaveEmumAsInt
        {
            get { return saveEmumAsInt; }
        }

    }
}