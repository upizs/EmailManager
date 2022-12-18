using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmailManager
{
    public class WebReader
    {
        //ReadTheLink
        public async Task<bool> ReadLink(string url)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                try
                {
                    // We'll use the GetAsync method to send 
                    // a GET request to the specified URL
                    var response = await client.GetAsync(url);

                    var postResponse = await client.PostAsync(url, null);


                    // If the response is successful, we'll
                    // interpret the response as XML
                    if (response.IsSuccessStatusCode)
                    {
                        var xml = await response.Content.ReadAsStringAsync();

                    }
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.Message);
                }
                
            }
            return result;
        }
    }
}
