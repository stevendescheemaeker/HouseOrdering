using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media.Imaging;

namespace HouseOrdering.Utilities
{
    public static class Utilities
    {
        public static bool GetObjectFromFile<T>(String path, out T o)
        {
            bool result = true;

            using (Stream s = File.Open(path, FileMode.Open))
            {
                try
                {
                    BinaryFormatter b = new BinaryFormatter();
                    o = (T)b.Deserialize(s);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);

                    o = default(T);
                    result = false;
                }

                s.Close();
            }

            return (result);
        }

        public static bool SaveObjectToFile<T>(String path, T o)
        {
            try
            {
                Stream s = File.Open(path, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, o);
                s.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);

                return (false);
            }

            return (true);
        }

        public static BitmapImage GetImage(Image img)
        {
            if (img == null)
                return null;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();

            return (bi);
        }
    }
}
