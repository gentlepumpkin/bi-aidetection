namespace AITool
{
    public static class ExtensionsForInt32
    {
        public static bool Between(this int num, int min, int max, bool inclusive = true)
        {
            if (min < 0)
            {
                min = 0;
            }

            return inclusive
                ? min <= num && num <= max
                : min < num && num < max;
        }
    }
}
