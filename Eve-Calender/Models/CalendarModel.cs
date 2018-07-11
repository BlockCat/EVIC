using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eve_Calender.Models
{
    /// <summary>
    /// event
    /// </summary>    
    public class GetCharactersCharacterIdCalendar200Ok
    {

        /// <summary>
        /// event_date string
        /// </summary>
        /// <value>event_date string</value>        
        [JsonProperty(PropertyName = "event_date")]
        public DateTime? EventDate { get; set; }

        /// <summary>
        /// event_id integer
        /// </summary>
        /// <value>event_id integer</value>        
        [JsonProperty(PropertyName = "event_id")]
        public int? EventId { get; set; }

        /// <summary>
        /// event_response string
        /// </summary>
        /// <value>event_response string</value>        
        [JsonProperty(PropertyName = "event_response")]
        public string EventResponse { get; set; }

        /// <summary>
        /// importance integer
        /// </summary>
        /// <value>importance integer</value>        
        [JsonProperty(PropertyName = "importance")]
        public int? Importance { get; set; }

        /// <summary>
        /// title string
        /// </summary>
        /// <value>title string</value>        
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }

    public partial class GetCharactersCharacterIdCalendarEventIdOk
    {

        /// <summary>
        /// date string
        /// </summary>
        /// <value>date string</value>       
        [JsonProperty(PropertyName = "date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Length in minutes
        /// </summary>
        /// <value>Length in minutes</value>      
        [JsonProperty(PropertyName = "duration")]
        public int? Duration { get; set; }      

        /// <summary>
        /// event_id integer
        /// </summary>
        /// <value>event_id integer</value>
        [JsonProperty(PropertyName = "event_id")]
        public int? EventId { get; set; }

        /// <summary>
        /// importance integer
        /// </summary>
        /// <value>importance integer</value>        
        [JsonProperty(PropertyName = "importance")]
        public int? Importance { get; set; }

        /// <summary>
        /// owner_id integer
        /// </summary>
        /// <value>owner_id integer</value>        
        [JsonProperty(PropertyName = "owner_id")]
        public int? OwnerId { get; set; }

        /// <summary>
        /// owner_name string
        /// </summary>
        /// <value>owner_name string</value>        
        [JsonProperty(PropertyName = "owner_name")]
        public string OwnerName { get; set; }

        /// <summary>
        /// owner_type string
        /// </summary>
        /// <value>owner_type string</value>
        [JsonProperty(PropertyName = "owner_type")]
        public string OwnerType { get; set; }

        /// <summary>
        /// response string
        /// </summary>
        /// <value>response string</value>        
        [JsonProperty(PropertyName = "response")]
        public string Response { get; set; }

        /// <summary>
        /// text string
        /// </summary>
        /// <value>text string</value>        
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// title string
        /// </summary>
        /// <value>title string</value>        
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}

