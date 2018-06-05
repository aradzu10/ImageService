using ImageServiceWebApp.Models.Communication;
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
        public event EventHandler NotifyEvent;

        [Required]
        [Display(Name = "Is Connected")]
        public bool IsConnected { get; set; }

        [Required]
        [Display(Name = "Number of pictures backed up")]
        public int NumOfPics { get; set; }

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

            //handler.ConnectToServer();

            ConfigurationModel settingsModel = ConfigurationModel.Instance;
            handler.SetSettings += settingsModel.SetSettings;
            handler.OnHandelRemove += settingsModel.RemoveHandler;

            settingsModel.NotifyHandlerChange += handler.SendMessage;

            // TODO - sign to events in client (log and photos)

            //IsConnected = handler.ConnectToServer();
            //new Task(() =>
            //{
            //    handler.Communication();
            //}).Start();

            Students = new List<Student>();
            Students.Add(new Student() { FirstName = "Matan", LastName = "Dombelski", ID = "318439981" });
            Students.Add(new Student() { FirstName = "Arad", LastName = "Zulti", ID = "315240564" });

            NotifyEvent?.Invoke(this, null);
        }

        public void SetNumOfPics(object sender, Settings settings)
        {
            NumOfPics = settings.PicturesCounter;
            NotifyEvent?.Invoke(this, null);
        }
    }
}