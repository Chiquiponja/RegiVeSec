using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RegiVeSec.Models
{
  public class FileDescriptionShort
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public ICollection<IFormFile> File { get; set; }
  }
}
