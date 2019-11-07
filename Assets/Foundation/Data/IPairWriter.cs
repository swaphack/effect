using System;
using System.Collections;

namespace Game.Foundation.Data
{
    public interface IPairWriter
    {
        bool Write(string name, bool value);
        bool Write(string name, byte value);
        bool Write(string name, sbyte value);
        bool Write(string name, char value);
        bool Write(string name, short value);
        bool Write(string name, ushort value);
        bool Write(string name, int value);
        bool Write(string name, uint value);
        bool Write(string name, long value);
        bool Write(string name, ulong value);
        bool Write(string name, float value);
        bool Write(string name, double value);
        bool Write(string name, string value);
        bool Write(string name, IList value);
        bool Write(string name, IDictionary value);
        bool Write(string name, Object value);
    }
}
