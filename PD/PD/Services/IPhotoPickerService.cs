using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services
{
    public  interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}