using System.Text;
using Positron.Common.Models;

namespace Positron.Common.IO
{
    public class DataFileImporter
    {
        public static List<VehiclePosition> ImportVehiclePositions(string dataFilePath)
        {
            byte[] data = File.ReadAllBytes(dataFilePath);

            List<VehiclePosition> vehiclePositionList = [];
            int offset = 0;
            while (offset < data.Length)
                vehiclePositionList.Add(GetVehiclePosition(data, ref offset));
            return vehiclePositionList;
        }

        public static VehiclePosition GetVehiclePosition(byte[] buffer, ref int offset)
        {
            VehiclePosition vehiclePosition = new VehiclePosition();
            vehiclePosition.Id = BitConverter.ToInt32(buffer, offset);
            offset += 4;
            StringBuilder stringBuilder = new StringBuilder();
            while (buffer[offset] != 0)
            {
                stringBuilder.Append((char)buffer[offset]);
                ++offset;
            }
            vehiclePosition.Registration = stringBuilder.ToString();
            ++offset;
            vehiclePosition.Latitude = BitConverter.ToSingle(buffer, offset);
            offset += 4;
            vehiclePosition.Longitude = BitConverter.ToSingle(buffer, offset);
            offset += 4;
            ulong uint64 = BitConverter.ToUInt64(buffer, offset);

            vehiclePosition.RecordedDateTimeUtc = Epoch.AddSeconds(uint64);
            offset += 8;
            return vehiclePosition;
        }

        internal static DateTime Epoch => new DateTime(1970, 1, 1, 0, 0, 0, 0);
    }
}
