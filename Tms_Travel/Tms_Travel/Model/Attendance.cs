using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tms_Travel.Helpers;

namespace Tms_Travel.Model
{
    public class Attendance
    {
        public User Attendee { get; set; }
        public Post Post { get; set; }

        [Key]
        [Column(Order = 1)]
        public string PostId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }

        public async static Task<bool> AttendAsync(Attendance att)
        {
            var url = Constants.ApiAttend;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(att);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync(url, httpContent);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }

        public async static Task<bool> DeleteAsync(Attendance att)
        {
            var url = Constants.ApiAttend  + att.PostId + "/" + att.AttendeeId;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.DeleteAsync(url);
                    return true;
                }
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }
    }
}
