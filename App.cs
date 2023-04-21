public class App
{
    public void Run()
    {
        while (true)
        {
            var input = Console.ReadLine().Trim();

            if (IsQuitCommand(input))
                return;
            try
            {
                var ration_result = Parser.Calculate(input);
                Console.WriteLine(ration_result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division by zero is undefined");
            }
            catch
            {
                Console.WriteLine("Syntax Error");
            }
        }
    }

    private bool IsQuitCommand(string input)
    {
        return input.Equals("quit", StringComparison.InvariantCultureIgnoreCase);
    }
}


