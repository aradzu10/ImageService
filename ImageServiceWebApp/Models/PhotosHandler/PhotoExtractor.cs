using ImageServiceWebApp.Models.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceWebApp.Models.PhotosHandler
{
    public class PhotoExtractor
    {
        public static MessageRecievedEventArgs GetAllPhotos()
        {
            MessageRecievedEventArgs message = new MessageRecievedEventArgs(MessageTypeEnum.P_SEND, "");
            Settings settings = Settings.Instance;
            PhotoList photos = new PhotoList();

            string[] thumbnailsPath;
            try
            {
                thumbnailsPath = Directory.GetFiles(settings.OutputPath + "\\Thumbnails", "*.*", SearchOption.AllDirectories);
            }
            catch (Exception)
            {
                return message;
            }

            foreach (var thumbPath in thumbnailsPath)
            {
                Image image;
                Image imageThumb;

                int index = thumbPath.IndexOf("\\Thumbnails");
                string imagePath = (index < 0) ? thumbPath : thumbPath.Remove(index, "\\Thumbnails".Length);

                try
                {
                    image = Image.FromFile(imagePath);
                    imageThumb = Image.FromFile(thumbPath);
                }
                catch (Exception)
                {
                    continue;
                }

                photos.Photos.Add(new PhotoPackage(imagePath, thumbPath, image, imageThumb));
            }

            message.Message = photos.Serialize();

            return message;
        }
    }
}
