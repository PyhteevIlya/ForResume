internal class Program
{
    private static async Task Main(string[] args)
    {
        var task1 = FirstTaskAsync();
        var task2 = SecondTaskAsync();

        await Task.WhenAll(task1, task2);

        Console.WriteLine($"first task result: {task1.Result}; second task result: {task2.Result}");

        async Task<string> FirstTaskAsync()
        {
            int i = 0;
            string s = "";
            while (i != 10)
            {
                i++;
                Console.WriteLine(i);
                await Task.Delay(new Random().Next(500, 1000));
                s += i.ToString()+" ";
            }
            return s;
        }

        async Task<string> SecondTaskAsync()
        {
            int i = 0;
            string s = "";
            while (i != 100)
            {
                i += 10;
                Console.WriteLine(i);
                await Task.Delay(new Random().Next(500, 1000));
                s += i.ToString()+" ";
            }
            return s;
        }
    }
}