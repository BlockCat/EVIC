using Eve_Calender.Models;
using Eve_Calender.Services;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Eve_Calender.Controllers
{    
    public class HomeController : Controller
    {
        private AuthorizationService authorizationService;
        private ESICalenderService calendarService;

        public HomeController(AuthorizationService authorizationService, ESICalenderService calendarService)
        {
            this.authorizationService = authorizationService;
            this.calendarService = calendarService;
        }

        public IActionResult Index()
        {
            ViewData["authorization_url"] = authorizationService.getAuthenticationUrl();
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("/result")]
        public IActionResult Result(ResultModel model)
        {
            string baseUrl = Program.BASE_URL;            
            ViewData["icalendar"] = $"{baseUrl}/calendar/{model.CharacterId}/{model.UniqueId.ToString()}/";
            return View();
        }

        [HttpGet]
        [Route("/calendar/{character}/{uniqueId}")]
        public async Task<IActionResult> Calendar(Guid uniqueId, int character)
        {
            var filter = new BsonDocument("_id", character);
            var accessToken = new MongoContext().Database.GetCollection<AccessTokenModel>("AccessToken").FindSync<AccessTokenModel>(filter).FirstOrDefault();            
            if (accessToken == null || !accessToken.UniqueId.Equals(uniqueId))
                return new JsonResult(new
                {
                    Error = "incorrect accesstoken"
                });

            if (DateTime.Now > accessToken.ExpiryDate)
            {
                accessToken = await authorizationService.RefreshToken(accessToken);
            }
            var events = await calendarService.GetMails(accessToken);

            Calendar calendar = new Calendar();
            calendar.ProductId = "-//Running With Dogs//Calender esi 1.0//EN";
            calendar.Version = "1.0";
            calendar.AddTimeZone(TimeZoneInfo.Utc);
            
            calendar.Events.AddRange(events.Select(x => {
                CalendarEvent cevent = new CalendarEvent();
                cevent.Start = new CalDateTime(x.Date.Value);
                cevent.End = new CalDateTime(x.Date.Value.AddMinutes(x.Duration.Value));
                cevent.Priority = x.Importance.Value;                
                cevent.Summary = x.Title;
                cevent.Description = x.Text;
                return cevent;
            }));

            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);
            return Content(serializedCalendar, "text/calendar;charset=UTF-8");
        }
    }
}
