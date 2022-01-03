using System;
using System.Threading.Tasks;

namespace AsynchronousFactoryMethod
{
    class Program
    {
        public class Foo
        {
            private Foo()
            {
                //
            }

            private async Task<Foo> InitAsync()
            {
                await Task.Delay(1000);
                return this;
            }

            public static Task<Foo> CreateAsyn()
            {
                var result = new Foo();
                return result.InitAsync();
            }
        }
        
        public class Demo
        {
            public static async Task Main (string[] args)
            {
                Foo x = await Foo.CreateAsyn();
            }
        }
    }
}
