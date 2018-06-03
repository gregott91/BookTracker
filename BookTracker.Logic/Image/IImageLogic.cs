using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Image
{
    public interface IImageLogic
    {
        Task<byte[]> GetImageFromUrlAsync(string url);
    }
}
