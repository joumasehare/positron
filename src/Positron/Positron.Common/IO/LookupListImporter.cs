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
