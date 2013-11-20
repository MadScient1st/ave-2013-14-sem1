using System;
using System.Collections.Generic;
using System.Text;

public sealed class BitArray
{
    private byte[] _byteArray;
    private int _numBits;

    public BitArray(int numBits)
    {
        if (numBits < 0)
            throw new ArgumentOutOfRangeException(
              "numBits", numBits.ToString(), "numBits must be > 0");
        _numBits = numBits;
        _byteArray = new byte[(numBits + 7) / 8];
    }
    public bool this[int bitPos]
    {
        get
        {
            if (bitPos < 0 || bitPos >= _numBits)
                throw new ArgumentOutOfRangeException("bitPos");
            return ((_byteArray[bitPos / 8] & (1 << (bitPos % 8)))) != 0;
        }
        set
        {
            if (bitPos < 0 || bitPos >= _numBits)
                throw new ArgumentOutOfRangeException("bitPos");
            if (value)
                _byteArray[bitPos / 8] = (byte)((_byteArray[bitPos / 8] | (1 << (bitPos % 8))));
            else
                _byteArray[bitPos / 8] = (byte)((_byteArray[bitPos / 8] & ~(1 << (bitPos % 8))));
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BitArrayDemo();
    }
    static void BitArrayDemo()
    {
        BitArray ba = new BitArray(14);
        for (int i = 0; i < 14; i++)
        {
            ba[i] = ((i % 2) == 0);
        }
        for (int i = 0; i < 14; i++)
        {
            Console.WriteLine("Bit " + i + " is " + ba[i]);
        }
    }
}
