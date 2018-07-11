using Eve_Calender.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eve_Calender.Services
{
    public class ESICalenderService
    {
        public async Task<GetCharactersCharacterIdCalendarEventIdOk[]> GetMails(AccessTokenModel accessToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.AccessToken}");
            client.DefaultRequestHeaders.Add("User-Agent", "eve-calendar-ical");

            HttpResponseMessage responseMessage = await client.GetAsync($"http://esi.evetech.net/latest/characters/{accessToken.CharacterId}/calendar?token=" + accessToken.AccessToken);

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<List<GetCharactersCharacterIdCalendar200Ok>>(await responseMessage.Content.ReadAsStringAsync());

                var mails = response.Select(async x =>
                {
                    HttpResponseMessage _responseMessage = await client.GetAsync($"http://esi.evetech.net/latest/characters/{accessToken.CharacterId}/calendar/{x.EventId}/");
                    return JsonConvert.DeserializeObject<GetCharactersCharacterIdCalendarEventIdOk>(await _responseMessage.Content.ReadAsStringAsync());
                    
                });
                return await Task.WhenAll(mails);
            } else
            {
                Console.WriteLine(await responseMessage.Content.ReadAsStringAsync());
                return new GetCharactersCharacterIdCalendarEventIdOk[0];
            }
        }
    }
}
