using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picture_Editor
{
    public class Makernote
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public bool IsHex { get; set; }

        public short Length { get; set; }

        public short Offset { get; set; }

        public Dictionary<int, string> Values { get; set; }

    }
}
