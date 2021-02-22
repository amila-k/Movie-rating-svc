using System.IO;
using Service.Helpers.Interfaces;

namespace Service.Helpers
{
    public class ImageHelper : IImageHelper
    {
        private readonly string _path;

        public ImageHelper(string path)
        {
            _path = path;
        }

        public byte[] GetImageByName(string name)
        {
            var path = Path.Combine(_path, "show-images", $"{name}.jpg");

            if (!File.Exists(path))
            {
                return null;
            }

            byte[] data;

            FileInfo fInfo = new FileInfo(path);
            long numBytes = fInfo.Length;

            FileStream fStram = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStram);

            data = br.ReadBytes((int)numBytes);

            return data;
        }
    }
}
