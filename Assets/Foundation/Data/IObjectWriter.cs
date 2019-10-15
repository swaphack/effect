using System;
using System.Collections;

namespace Assets.Foundation.Data
{
    public interface IObjectWriter
    {
        bool Write(bool value);
        bool Write(byte value);
        bool Write(sbyte value);
        bool Write(char value);
        bool Write(short value);
        bool Write(ushort value);
        bool Write(int value);
        bool Write(uint value);
        bool Write(long value);
        bool Write(ulong value);
        bool Write(float value);
        bool Write(double value);
        bool Write(string value);
        bool Write(IList value);
        bool Write(IDictionary value);
        bool Write(Object value);
    }
}
