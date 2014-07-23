using QuakeQueryDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UrTQuery
{
    public static class ServerListSettings
    {
        public static void SaveServerList(List<Server> ServerList)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamReader sr = new StreamReader(ms))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, ServerList);
                    ms.Position = 0;
                    byte[] buffer = new byte[(int)ms.Length];
                    ms.Read(buffer, 0, buffer.Length);
                    Properties.Settings.Default.ServerList = Convert.ToBase64String(buffer);
                }
            }
        }

        public static List<Server> LoadServerList()
        {

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Properties.Settings.Default.ServerList)))
            {
                if (ms.Length == 0)
                    return null;
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Server>)bf.Deserialize(ms);
            }
        }
    }

}
