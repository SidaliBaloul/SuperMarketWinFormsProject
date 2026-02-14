using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.Runtime.Serialization.Json;
using System.IO;

namespace SMBusinessLayer
{
    public class ClsUtile
    {
        public static void CreateAnEventSourceForEventLogs(string SourceName)
        {
            

            if(!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }
        }

        public static void LogAnEventToEventLogs(string SourceName,string message, EventLogEntryType type)
        {
            EventLog.WriteEntry(SourceName, message, type);
        }

        public static void JSONSerialization(string username)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));

            using(MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, username);
                string jsonnstring = System.Text.Encoding.UTF8.GetString(stream.ToArray());

                File.WriteAllText("LastUserName.json", jsonnstring);
            }

            
        }

        public static string JSONDeserilization()
        {
            string username;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));

            if (!File.Exists("LastUserName.json"))
                return null;

            using (FileStream stream = new FileStream("LastUserName.json",FileMode.Open))
            {
                username = (string)serializer.ReadObject(stream);
            }

            return username;
        }

        

    }
}
