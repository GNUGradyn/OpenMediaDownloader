using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMediaDownloader.Models
{
    public class OutputFormatModel
    {
        public bool UseVideo { get; set; }
        public bool UseAudio { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Container { get; set; }
        public string AudioCodec { get; set; }
        public string VideoCodec { get; set; }
        public string FPS { get; set; }
    }
}
