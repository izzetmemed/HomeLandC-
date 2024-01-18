using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Business.Concrete
{
    public class MediaOperation
    {
        public MediaAccess media = new MediaAccess();
        public async Task<IResult> Add( Medium Model)
        {
           media.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public async void MakeContact(RentHome data)
        {
           var Data= await media.GetAllNormal(data);
            for (int i = 0; i < Data.Count; i++)
            {
                const string accountSid = "SK2da7845fe160556b686ae8ee76116a95"; 
                const string authToken = "KkxJkzYvAKXyxzwn4GOTWrUYiQ1VWCqG";   

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                   body: $"Sizin axtarışınıza uyğun yeni ev yüklənildi. Baxmaq üçün http://localhost:3000/Kind/{data.Id}",
                    from: new Twilio.Types.PhoneNumber("0705715610"),
                    to: new Twilio.Types.PhoneNumber(Data[i].Number) 
                );

                Console.WriteLine(message.Sid);
            }

        }

    }
}
