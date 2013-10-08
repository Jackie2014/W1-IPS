using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System;

namespace IPDetectServer.Lib.Common
{
    public class JsonConverter
    {
        /// <summary>
        /// Json Serialize
        /// </summary>
        public static string ToJson(object item)
        {
            if (item == null || (item is String && String.IsNullOrEmpty(item as String)))
            {
                return String.Empty;
            }

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                string retu = Encoding.UTF8.GetString(ms.ToArray(), 0, int.Parse(ms.Length.ToString()));
                
                return retu;
            }  
        }

        /// <summary>
        /// Json Deserialize
        /// </summary>
        public static T FromJsonTo<T>(string jsonString)
        {
            try
            {
                T jsonObject = default(T);
                if (String.IsNullOrEmpty(jsonString))
                {
                    return jsonObject;
                }

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    jsonObject = (T)ser.ReadObject(ms);
                    ms.Close();
                    return jsonObject;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clone object with json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CloneObject<T>(T obj)
        {
            string content = ToJson(obj);
            return FromJsonTo<T>(content);
        }
    }
}
