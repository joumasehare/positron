using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Common.IO
{
    public static class LookupListImporter
    {
        public static List<SpatialQuery> LoadLookupList(string path)
        {
            return File.ReadAllLines(path).Select(SpatialQuery.Parse).ToList();
        }
    }
}
