using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KvantShared.Utils
{
    public class JsonHelper
    {
        private static readonly JsonSerializerSettings SerializerSettings;
        private const Formatting Indented = Formatting.Indented;

        static JsonHelper()
        {
            SerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        /// <summary>
        /// Convert object to JSON string representation
        /// </summary>
        /// <param name="o">Object to convert</param>
        /// <returns></returns>
        public static string Serialize(object o)
        {
            var serialized = JsonConvert.SerializeObject(o, Indented, SerializerSettings);
            return serialized;
        }

        /// <summary>
        /// Convert JSON string back to object with type T
        /// </summary>
        /// <typeparam name="T">Target type (exact, not abstract)</typeparam>
        /// <param name="str">JSON string</param>
        /// <returns></returns>
        public static T Deserialize<T>(string str)
        {
            var o = JsonConvert.DeserializeObject<T>(str, SerializerSettings);
            return o;
        }

        /// <summary>
        /// Convert JSON string back to object of specified type
        /// </summary>
        /// <param name="str">JSON string</param>
        /// <param name="type">Target object type</param>
        /// <returns></returns>
        public static object Deserialize(string str, Type type)
        {
            var o = JsonConvert.DeserializeObject(str, type, SerializerSettings);
            return o;
        }

        /// <summary>
        /// Populate existing object with values from JSON string.
        /// DANGER!!! Existing properties just added, not overrided. So internal collections will retain old values.
        /// Clear collections by hand before calling this method!
        /// </summary>
        /// <param name="str">JSON string</param>
        /// <param name="dest">Object to populate with new values</param>
        public static void Populate(string str, object dest)
        {
            JsonConvert.PopulateObject(str, dest, SerializerSettings);
        }
    }
}
