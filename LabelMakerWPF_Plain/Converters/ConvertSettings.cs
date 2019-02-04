﻿using LabelMakerWPF_Plain.Properties;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace LabelMakerWPF_Plain.Converters
{
    public class ConvertSettings
    {
        public string SettingToString(object settings)
        {
            var input = settings.GetType().GetProperties();
            if (settings == null)
                return null;

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, settings);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public object SettingFromString(string settings)
        {           
                BinaryFormatter bf = new BinaryFormatter();
                using (var ms = new MemoryStream(Convert.FromBase64String(settings)))
                {
                    var output = bf.Deserialize(ms);
                    return output;
                }
        }
    }
}
