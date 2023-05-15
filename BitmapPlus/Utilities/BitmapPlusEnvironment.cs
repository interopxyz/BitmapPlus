using System;

namespace BitmapPlus
{
    public static class BitmapPlusEnvironment
    {
        static bool? _FileIoBlocked = null;

        public static bool FileIoBlocked
        {
            get
            {
                if (_FileIoBlocked == null)
                {
                    var value = Environment.GetEnvironmentVariable("BLOCK_FILEIO");
                    _FileIoBlocked = !String.IsNullOrEmpty(value);
                }
                return _FileIoBlocked == true;
            }
        }
      

    }
}
