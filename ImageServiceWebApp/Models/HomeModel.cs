﻿using ImageServiceWebApp.Models.Communication;
using ImageServiceWebApp.Models.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class HomeModel
    {
        private bool isConnected;
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Server Status")]
        public string IsConnected
        {
            get
            {
                if (isConnected)
                {
                    return "Connected";
                }
                return "Not Connected";
            }
            private set { }
        }

        [Required]
        [Display(Name = "Number of pictures backed up")]
        public int NumOfPics
        {
            get
            {
                return Settings.Instance.PicturesCounter;
            }
            private set { }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Students")]
        public List<Student> Students { get; set; }

        public class Student
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "ID")]
            public string ID { get; set; }
        }

        public HomeModel()
        {
            HandleCommunication handler = new HandleCommunication();

            isConnected = handler.ConnectToServer();
            if (!isConnected) return;

            ConfigurationModel settingsModel = ConfigurationModel.Instance;
            handler.SetSettings += settingsModel.SetSettings;
            handler.OnHandelRemove += settingsModel.RemoveHandler;

            settingsModel.NotifyHandlerChange += handler.SendMessage;

            LogModel logModel = LogModel.Instance;
            handler.OnLogMessage += logModel.AddMassage;

            PhotosModel photosModel = PhotosModel.Instance;
            handler.GetPhotos += photosModel.HandlePhoto;
            handler.OnDeletePhoto += photosModel.DeletePhoto;

            photosModel.NotifyPhotoChange += handler.SendMessage;

            new Task(() =>
            {
                handler.Communication();
            }).Start();

            handler.SendMessage(this, new MessageRecievedEventArgs(MessageTypeEnum.P_SENDALL, ""));

            Students = new List<Student>();
            Students.Add(new Student() { FirstName = "Matan", LastName = "Dombelski", ID = "318439981" });
            Students.Add(new Student() { FirstName = "Arad", LastName = "Zulti", ID = "315240564" });
        }

        public void SetNumOfPics(object sender, Settings settings)
        {
            NumOfPics = settings.PicturesCounter;
        }
    }
}