namespace SeaBrief.Compression;

public static class Delta2
{
    public static long[] Encode(long[] input)
    {
        if (input.Length == 1) return input;
        
        long[] compressed = new long[input.Length];

        compressed[0] = input[0];
        long last = input[1] - input[0];
        compressed[1] = last;

        for (int i = 2; i < input.Length; i++)
        {
            long delta = input[i] - input[i - 1];
            long delta2 = delta - last;
            compressed[i] = delta2;
            last = delta;
        }

        return compressed;
    }

    public static long[] Decode(long[] input)
    {
        if (input.Length == 1) return input;

        long[] decompressed = new long[input.Length];
        decompressed[0] = input[0];
        decompressed[1] = input[0] + input[1];

        for (int i = 2; i < input.Length; i++)
        {
            long delta2 = input[i];
            long prev = decompressed[i - 1];
            long prev2 = decompressed[i - 2];
            long delta = delta2 + prev2;
            decompressed[i] = delta;
        }

        return decompressed;
    }
}