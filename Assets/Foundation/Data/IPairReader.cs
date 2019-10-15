using System;
using System.Collections;

namespace Assets.Foundation.Data
{
    public interface IPairReader
    {
        bool Read(string name, ref bool value);
        bool Read(string name, ref byte value);
        bool Read(string name, ref sbyte value);
        bool Read(string name, ref char value);
        bool Read(string name, ref short value);
        bool Read(string name, ref ushort value);
        bool Read(string name, ref int value);
        bool Read(string name, ref uint value);
        bool Read(string name, ref long value);
        bool Read(string name, ref ulong value);
        bool Read(string name, ref float value);
        bool Read(string name, ref double value);
        bool Read(string name, ref string value);
        bool Read(string name, ref IList value);
        bool Read(string name, ref IDictionary value);
        bool Read(string name, ref Object value);
    }
}
