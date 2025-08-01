using WitchSaga.ServicesContract;

namespace WitchSaga.Services
{
    public class FibonacciCalculatorService : IFibonacciCalculatorService
    {
        public int GetTotalFibonacciKill(int year)
        {
            if (year == 1)
            {
                return 1;
            }

            // For year >= 2
            // Fibonacci is a sequence in which each element is the sum of the two elements that precede it.
            // We need to assign the first 2 number
            var fibonacciSquence = new List<int>();
            fibonacciSquence.Add(0);
            fibonacciSquence.Add(1);

            var totalKill = 1;

            for (int i = 2; i <= year; i++)
            {
                var nextSequence = fibonacciSquence[i - 1] + fibonacciSquence[i - 2];
                fibonacciSquence.Add(nextSequence);
                totalKill += nextSequence;
            }

            return totalKill;
        }
    }
}
