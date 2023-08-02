using SpiderWebDownload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderWebDownload.Interfaces
{
    public interface IServices
    {
        Task<DownloadedFileModel[]> RunDownloadAsync();
    }
}
