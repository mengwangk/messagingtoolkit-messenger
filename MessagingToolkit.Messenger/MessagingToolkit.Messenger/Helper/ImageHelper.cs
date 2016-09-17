using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MessagingToolkit.Messenger.Helper
{
    /// <summary>
    /// Image helper utility class.
    /// </summary>
    public static class ImageHelper
    {
        public static byte[] ConvertImageToByteArray(Image image, ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, format);
            return ms.ToArray();
        }

        public static Image ConvertByteArrayToImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            Image image = Image.FromStream(ms);
            return image;
        }

        public static byte[] ImageFileToByteArray(string fileName)
        {
            int fileSize = 0;
            FileStream fileHandler = null;
            try
            {
                fileHandler = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                fileSize = (int)fileHandler.Length;
                byte[] buf = new byte[fileSize];
                fileHandler.Read(buf, 0, fileSize);
                return buf;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (fileHandler != null) fileHandler.Close();                
            }
        }
    }
}
