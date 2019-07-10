using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload
{
    public class start
    {
        public string session_id { get; set; }
    }

    public class upload
    {
        public string tag { get; set; }
    }


    public class complete
    {
        public string session_id { get; set; }
        public string[] tags { get; set; }
    }


    public class completed
    {
        public string file_id { get; set; }
    }

}
