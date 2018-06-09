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
        public event EventHandler<MessageRecievedEventArgs> NotifyPhotoChange;

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

        private PhotosModel() { }

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
        }

        private void AddPhotos(string photos_)
        {
            PhotoList photos = PhotoList.Deserialize(photos_);
            Settings.Instance.PicturesCounter = photos.Photos.Count;
            PhotoDB.AddPhotos(photos);
        }

        private void AddPhoto(string photo_)
        {
            PhotoPackage photo = PhotoPackage.Deserialize(photo_);
            Settings.Instance.PicturesCounter++;
            PhotoDB.AddPhoto(photo);
        }

        public void NotifyDeletePhoto(int index)
        {
            string photo = PhotoDB.GetPhoto(index).Serialize();
            MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs(MessageTypeEnum.P_DELETE, photo);
            NotifyPhotoChange?.Invoke(this, messageRecievedEventArgs);
        }

        public void DeletePhoto(object sender, PhotoPackage photo)
        {
            PhotoDB.DeletePhoto(photo);
        }
    }
}