using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace GetPet.Common
{
    public class ImageHelper
    {
        public Stream PrepareImage(Stream stream)
        {
            var thumbnail = GetThumbnailImage(new Bitmap(stream), new Size(360, 240));
            var outputStream = VaryQualityLevel(new Bitmap(thumbnail));
            return outputStream;
        }

        private Stream VaryQualityLevel(Bitmap bmp1)
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 80L);
            myEncoderParameters.Param[0] = myEncoderParameter;



            MemoryStream memoryStream = new MemoryStream();

            bmp1.Save(memoryStream, jpgEncoder, myEncoderParameters);

            return memoryStream;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static Image GetThumbnailImage(Image OriginalImage, Size ThumbSize)
        {
            Int32 thWidth = OriginalImage.Width; //  (int)(OriginalImage.Width * 0.8); // ThumbSize.Width;
            Int32 thHeight = OriginalImage.Height; //  (int)(OriginalImage.Height * 0.8); // ThumbSize.Height;
            Image i = OriginalImage;
            Int32 w = i.Width;
            Int32 h = i.Height;
            Int32 th = thWidth;
            Int32 tw = thWidth;
            if (h > w)
            {
                Double ratio = (Double)w / (Double)h;
                th = thHeight < h ? thHeight : h;
                tw = thWidth < w ? (Int32)(ratio * thWidth) : w;
            }
            else
            {
                Double ratio = (Double)h / (Double)w;
                th = thHeight < h ? (Int32)(ratio * thHeight) : h;
                tw = thWidth < w ? thWidth : w;
            }
            Bitmap target = new Bitmap(tw, th);
            Graphics g = Graphics.FromImage(target);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            Rectangle rect = new Rectangle(0, 0, tw, th);
            g.DrawImage(i, rect, 0, 0, w, h, GraphicsUnit.Pixel);
            return (Image)target;
        }

    }
}
