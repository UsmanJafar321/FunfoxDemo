using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Manager
{
    public class ImageHandler
    {
        //adil 2:34 PM 2/27/2019
        public static string UploadPhoto(string path, HttpPostedFileBase image,double scale = 0.5)
        {
            string result = string.Empty;
            if (path != string.Empty && image != null)
            {
                var fileName = FunctionManager.GetRandomNumber(10) + image.FileName; // genrate new file name
                string serverPath = System.Web.HttpContext.Current.Server.MapPath(path);//path for upload
                Stream strm = image.InputStream; //get image stream
                ReduceImageSize(scale, strm, serverPath, fileName); // function to reduce image size
                result = fileName; // return file name to your action
            }
            return result;
        }
        public static void ReduceImageSize(double scaleFactor, Stream sourcePath, string targetPath,string imageName)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                // change width and height
                var newWidth = (int)(image.Width * scaleFactor); 
                var newHeight = (int)(image.Height * scaleFactor);
                if (newWidth>953 && newHeight>1153)
                {
                    newWidth = 953;
                    newHeight=1153;
                }
                var thumbnailImg = new Bitmap(newWidth, newHeight);
               // var thumbnailImg = new Bitmap(953, 1153);//manual diemensions
                //thumbnail

                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                //Manual Dimensions
                // var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                //draw image again from width and height
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath+ imageName, image.RawFormat); // save image to path with reduce in size
            }
        }
        
    }
}
