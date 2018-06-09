using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models.PhotosHandler
{
    public class PhotoDB
    {
        public PhotoList photoList { get; private set; }

        private static PhotoDB instance;
        public static PhotoDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhotoDB();
                }
                return instance;
            }
            private set { }
        }

        private PhotoDB() { photoList = new PhotoList(); }

        public static void AddPhotos(PhotoList photoList_)
        {
            PhotoList photoList = Instance.photoList;
            photoList.Photos.AddRange(photoList_.Photos);
        }

        public static void AddPhoto(PhotoPackage photo)
        {
            PhotoList photoList = Instance.photoList;
            photoList.Photos.Add(photo);
        }

        public static int GetIndex(PhotoPackage photo)
        {
            PhotoList photoList = Instance.photoList;
            return photoList.Photos.IndexOf(photo);
        }

        public static PhotoPackage GetPhoto(int index)
        {
            PhotoList photoList = Instance.photoList;
            return photoList.Photos[index];
        }

        public static void DeletePhoto(PhotoPackage photo)
        {
            PhotoList photoList = Instance.photoList;
            photoList.Photos.RemoveAll(a => a.PhotoPath == photo.PhotoPath);
        }
    }
}