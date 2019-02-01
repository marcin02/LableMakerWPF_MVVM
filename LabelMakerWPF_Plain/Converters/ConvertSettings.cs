using LabelMakerWPF_Plain.Properties;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace LabelMakerWPF_Plain.Converters
{
    public class ConvertSettings
    {
        private string _convertString = Settings.Default.printerSettings;

        public string SettingToString(System.Drawing.Printing.PrinterSettings settings)
        {
            if (settings == null)
                return null;

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, settings);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public System.Drawing.Printing.PrinterSettings SettingFromString()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (var ms = new MemoryStream(Convert.FromBase64String(_convertString)))
                {
                    return (System.Drawing.Printing.PrinterSettings)bf.Deserialize(ms);
                }
            }
            catch (Exception)
            {
                return new System.Drawing.Printing.PrinterSettings();
            }
        }
    }
}
