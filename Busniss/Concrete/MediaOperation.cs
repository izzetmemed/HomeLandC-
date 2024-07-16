using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDb.Concrete;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MediaOperation:IMediaService
    {
        public MediaAccess media = new MediaAccess();
        public Access RentHome=  new Access();
        public SellAccess sellAccess = new SellAccess();
        public LandDA landDA= new LandDA();
        public OfficeDA officeDA= new OfficeDA();
        public ObyektAccess obyektAccess = new ObyektAccess();
        public async Task<IResult> Add( MediaType Model)
        {
            if (Regex.IsMatch(Model.Number, @"^\S+@\S+\.\S+$"))
            {
                media.Add(Model);
            }
            return new SuccessResult(MyMessage.Success);
        }
        public async Task<List<MediaType>> GetAll()
        {
            return await media.GetAll();
        }
        public async void MakeContact(RentHome data)
        {
            async Task<int> GetLastId()
            {
                var items = await RentHome.GetAll();

                if (items.Any())
                {
                    var lastItem = items.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<RentHome>(lastItem);




                    return RentHome.Id;
                }
                else
                {
                    return 0;
                }
            }
            var LastId = await GetLastId();
            var Data= await media.GetAllRent(data);
            for (int i = 0; i < Data.Count; i++)
            {
               
                SendEmail(int.Parse(Data[i].Counter), Data[i].Number, LastId, "https://HomeLand.az/Kart/");
                this.Update(Data[i]);
                await Task.Delay(1000);
                if(i == 2)
                {
                    break;
                }
            }
           
        }
        public async void MakeContactObyekt(Obyekt data)
        {
            async Task<int> GetLastId()
            {
                var items = await obyektAccess.GetAll();

                if (items.Any())
                {
                    var lastItem = items.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<Obyekt>(lastItem);




                    return RentHome.Id;
                }
                else
                {
                    return 0;
                }
            }
            var LastId = await GetLastId();
            var Data = await media.GetAllObyekt(data);
            for (int i = 0; i < Data.Count; i++)
            {

                SendEmail(int.Parse(Data[i].Counter), Data[i].Number, LastId, "https://HomeLand.az/Obyekt/Kart/");
                this.Update(Data[i]);
                await Task.Delay(1000);
                if (i == 2)
                {
                    break;
                }
            }

        }
        public async void MakeContactSell(Sell data)
        {
            async Task<int> GetLastId()
            {
                var items = await sellAccess.GetAll();

                if (items.Any())
                {
                    var lastItem = items.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<Sell>(lastItem);




                    return RentHome.Id;
                }
                else
                {
                    return 0;
                }
            }
            var LastId = await GetLastId();
            var Data = await media.GetAllSell(data);
            for (int i = 0; i < Data.Count; i++)
            {

                SendEmail(int.Parse(Data[i].Counter), Data[i].Number, LastId, "https://HomeLand.az/Sell/Kart/");
                this.Update(Data[i]);
                await Task.Delay(1000);
                if (i == 2)
                {
                    break;
                }
            }

        }
        public async void MakeContactLand(Land data)
        {
            async Task<int> GetLastId()
            {
                var items = await landDA.GetAll();

                if (items.Any())
                {
                    var lastItem = items.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<Land>(lastItem);




                    return RentHome.Id;
                }
                else
                {
                    return 0;
                }
            }
            var LastId = await GetLastId();
            var Data = await media.GetAllLand(data);
            for (int i = 0; i < Data.Count; i++)
            {

                SendEmail(int.Parse(Data[i].Counter), Data[i].Number, LastId, "https://HomeLand.az/Land/Kart/");
                this.Update(Data[i]);
                await Task.Delay(1000);
                if (i == 2)
                {
                    break;
                }
            }

        }
        public async void MakeContactOffice(Office data)
        {
            async Task<int> GetLastId()
            {
                var items = await sellAccess.GetAll();

                if (items.Any())
                {
                    var lastItem = items.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<Office>(lastItem);




                    return RentHome.Id;
                }
                else
                {
                    return 0;
                }
            }
            var LastId = await GetLastId();
            var Data = await media.GetAllOffice(data);
            for (int i = 0; i < Data.Count; i++)
            {

                SendEmail(int.Parse(Data[i].Counter), Data[i].Number, LastId, "https://HomeLand.az/Office/Kart/");
                this.Update(Data[i]);
                await Task.Delay(1000);
                if (i == 2)
                {
                    break;
                }
            }

        }
        public IResult Update(MediaType Model)
        {
            Model.Counter=(int.Parse(Model.Counter)-1).ToString();
            media.Update(Model);
            return new SuccessResult("Update Media");
        }
        private void SendEmail(int count, string Number, int id,string Link)
        {

            string fromEmail = "homeland.az.service@gmail.com";
            string toEmail = Number; 
            string subject = "HomeLand.az";
            string body = $"Sizə uyğun yeni elan yükləndi." + Environment.NewLine + 
                $"Tarix: {DateTime.Now.ToString("M/d/yyyy h:mm")}" + Environment.NewLine+
                $"Elanı görmək üçün keçid edin. {Link}{id}  "+ Environment.NewLine +
                $"Sizə göndərəcəyimiz elan sayı: {count}." + Environment.NewLine + 
                $"Hörmətlə : Homeland Company";

            try
            {
                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("homeland.az.service@gmail.com", "dvmq vowb frps tsye");
                    smtp.EnableSsl = true;

                    using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(fromEmail, toEmail, subject, body))
                    {
                        smtp.Send(mailMessage);
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Gmail is wrong");
            }
           
        }

    }
}
