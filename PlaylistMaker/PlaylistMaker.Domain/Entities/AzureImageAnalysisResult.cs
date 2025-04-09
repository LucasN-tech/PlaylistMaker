using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistMaker.Domain.Entities
{
    public class AzureImageAnalysisResult
    {
        public string Caption { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
