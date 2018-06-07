using ImageServiceWebApp.Models.Messages;
using ImageServiceWebApp.Models.PhotosHandler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class PhotosModel
    {
        public delegate void NotifyEvent();
        public NotifyEvent notify;
        public event EventHandler<MessageRecievedEventArgs> NotifyPhotoChange;

        public PhotoList photoList { get; set; }

        private static PhotosModel instance;

        public static PhotosModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhotosModel();
                }
                return instance;
            }
            private set { }
        }

        private PhotosModel()
        {
            photoList = new PhotoList();

            Image image = Image.FromFile("C:\\test\\out\\2018\\5\\cat-pet-animal-domestic-104827.jpeg");
            Image thumb = Image.FromFile("C:\\test\\out\\Thumbnails\\2018\\5\\cat-pet-animal-domestic-104827.jpeg");

            photoList.Photos.Add(new PhotoPackage(
                "C:\\test\\out\\2018\\5\\cat-pet-animal-domestic-104827.jpeg",
                "C:\\test\\out\\Thumbnails\\2018\\5\\cat-pet-animal-domestic-104827.jpeg",
                image, thumb));
        }

        public void HandlePhoto(object sender, MessageRecievedEventArgs message)
        {
            if (message.Status == MessageTypeEnum.P_SEND)
            {
                AddPhoto(message.Message);
            }

            if (message.Status == MessageTypeEnum.P_SENDALL)
            {
                AddPhotos(message.Message);
            }

            notify?.Invoke();
        }

        private void AddPhotos(string photos_)
        {
            PhotoList photos = PhotoList.Deserialize(photos_);
            photoList.Photos.AddRange(photos.Photos);
        }

        private void AddPhoto(string photo_)
        {
            PhotoPackage photo = PhotoPackage.Deserialize(photo_);
            photoList.Photos.Add(photo);
        }

        public void NotifyDeletePhoto(string photo)
        {
            MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs(MessageTypeEnum.P_DELETE, photo);
            NotifyPhotoChange?.Invoke(this, messageRecievedEventArgs);
        }
    }
}