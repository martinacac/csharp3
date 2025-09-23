public class FizzBuzz
{
    public void CountTo(int lastNumber)
    {
        for (int aktualnicislo = 1; aktualnicislo < lastNumber; aktualnicislo++)
        {

            if ((aktualnicislo % 3 == 0) && (aktualnicislo % 5 == 0))
            {
                Console.WriteLine("FizzBuzz");
            }
            else if (aktualnicislo % 3 == 0)
            {
                Console.WriteLine("Fizz");
            }
            else if (aktualnicislo % 5 == 0)
            {
                Console.WriteLine("Buzz");
            }
            else
            {
                Console.WriteLine(aktualnicislo);
            }

        }
    }
}
