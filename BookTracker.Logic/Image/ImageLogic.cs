using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Image
{
    public class ImageLogic : IImageLogic
    {
       public async Task<byte[]> GetImageFromUrlAsync(string url)
        {
            var client = new HttpClient();
            return await client.GetByteArrayAsync(url);
        }
    }
}
