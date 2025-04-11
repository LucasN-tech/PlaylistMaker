using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistMaker.Domain.Entities
{
    public class AzureImageAnalysisResult
    {
        public string DenseCaption { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Objects { get; set; }
    }
}