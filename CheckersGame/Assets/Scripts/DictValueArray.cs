using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class DictValueArray : IEquatable<DictValueArray>
    {
        public int row;
        public int column;

        public DictValueArray(int[] key)
        {
            this.row = key[0];
            this.column = key[1];
        }

        public override int GetHashCode()
        {
            return (row * 10) + column;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DictValueArray);
        }

        public bool Equals(DictValueArray dva)
        {
            if (this.row == dva.row && this.column == dva.column)
                return true;
            return false;
        }
    }
}
