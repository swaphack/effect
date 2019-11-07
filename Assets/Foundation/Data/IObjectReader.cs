using System;
using System.Collections;

namespace Game.Foundation.Data
{
    public interface IObjectReader
    {
        bool Read(ref bool value);
        bool Read(ref byte value);
        bool Read(ref sbyte value);
        bool Read(ref char value);
        bool Read(ref short value);
        bool Read(ref ushort value);
        bool Read(ref int value);
        bool Read(ref uint value);
        bool Read(ref long value);
        bool Read(ref ulong value);
        bool Read(ref float value);
        bool Read(ref double value);
        bool Read(ref string value);
        bool Read(ref IList value);
        bool Read(ref IDictionary value);
        bool Read(ref Object value);
    }
}
