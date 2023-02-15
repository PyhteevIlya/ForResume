using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class Program
{
    static async Task Main(string[] args)
    {
        Channel<string> s_channelFirst = Channel.CreateUnbounded<string>();
        Channel<string> s_channelSecond = Channel.CreateUnbounded<string>();

        ChannelReader<string> reader1 = s_channelFirst.Reader;

        ChannelReader<string> reader2 = s_channelSecond.Reader;

        var task1 = TaskFirst();
        var task2 = TaskSecond();

        await Task.WhenAll(task1, task2);

        static ChannelReader<T> Merge<T>(
        ChannelReader<T> s_channelFirst, ChannelReader<T> s_channelSecond)
        {
            var output = Channel.CreateUnbounded<T>();

            Task.Run(async () =>
            {
                await foreach (var item in s_channelFirst.ReadAllAsync())
                    await output.Writer.WriteAsync(item);
            });
            Task.Run(async () =>
            {
                await foreach (var item in s_channelSecond.ReadAllAsync())
                    await output.Writer.WriteAsync(item);
            });
            return output;
        }

        var ch = Merge(reader1, reader2);

        await foreach (var item in ch.ReadAllAsync())
            Console.WriteLine(item);

        async Task TaskFirst()
        {
            ChannelWriter<string> writer1 = s_channelFirst.Writer;
            for (int i = 1; i <= 10; i++)
            {
                await s_channelFirst.Writer.WriteAsync(i.ToString());
                await Task.Delay(new Random().Next(500, 1000));
            }
        }

        async Task TaskSecond()
        {
            ChannelWriter<string> writer2 = s_channelSecond.Writer;
            for (int i = 10; i <= 100; i += 10)
            {
                await s_channelSecond.Writer.WriteAsync(i.ToString());
                await Task.Delay(new Random().Next(500, 1000));
            }
        }
    }
}