using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class DictValueArray : IEquatable<DictValueArray>
    {
        public int Row;
        public int Column;

        public DictValueArray(int[] key)
        {
            this.Row = key[0];
            this.Column = key[1];
        }

        public override int GetHashCode()
        {
            return (Row * 10) + Column;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DictValueArray);
        }

        public bool Equals(DictValueArray dva)
        {
            return dva != null && (this.Row == dva.Row && Column == dva.Column);
        }
    }
}
