namespace ApiErrorsExample.Utilities
{
    public static class NumberUtilities
    {
        public static bool IsInRange(this int number, int lowerBound, int upperBound)
        {
            return number >= lowerBound && number <= upperBound;
        }
    }
}
