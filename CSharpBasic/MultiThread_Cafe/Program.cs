namespace MultiThread_Cafe
{
	internal class Program
	{
		static void RoastCoffeeBeans(object speed)
		{
			//Thread.Sleep(1000 / (int)speed);
            Console.WriteLine($"{Thread.CurrentThread.Name} : Roasted Coffee beans !");
			GrindCoffeeBeans(speed);
        }

		static void GrindCoffeeBeans(object speed)
		{
			//Thread.Sleep(1000 / (int)speed);
			Console.WriteLine($"{Thread.CurrentThread.Name} : Grinded Coffee beans !");
			BrewCoffee(speed);
		}

		static void BrewCoffee(object speed)
		{
			//Thread.Sleep(1000 / (int)speed);
            Console.WriteLine($"{Thread.CurrentThread.Name} : Brewed coffee.!");
			for (int i = 0; i < 100000; i++)
			{
				coffeeTotal++;
			}
		}

		static int PP(ref int x)
		{
			int tmp = x;
			x = x + 1;
			return tmp;
		}

		static void ThreadPoolCallBack(object threadContext)
		{
            Console.WriteLine($"Thread {(int)threadContext} started");
        }

		static int coffeeTotal;

		static async Task Main(string[] args)
		{
			ThreadPool.SetMinThreads(1, 0);
			ThreadPool.SetMaxThreads(3, 0);

			for (int i = 0; i < 4; i++)
			{
				ThreadPool.QueueUserWorkItem(ThreadPoolCallBack, i);
			}

			int stackSize = 2 * 1024 * 1024; // 1MB = 1024 * 1024
			Thread barista1 = new Thread(RoastCoffeeBeans, stackSize);
			barista1.Name = "Barista Kim";
			barista1.IsBackground = true; // Main Thread 끝나면 같이 종료해라
			barista1.Start(2);

			Thread barista2 = new Thread(RoastCoffeeBeans);
			barista2.Name = "Barista Lee";
			barista2.IsBackground = true; // Main Thread 끝나면 같이 종료해라
			barista2.Start(3);

			Thread barista3 = new Thread(RoastCoffeeBeans);
			barista3.Name = "Barista Choi";
			barista3.IsBackground = true; // Main Thread 끝나면 같이 종료해라
			barista3.Start(4);

			Thread barista4 = new Thread(RoastCoffeeBeans);
			barista4.Name = "Barista Park";
			barista4.IsBackground = true; // Main Thread 끝나면 같이 종료해라
			barista4.Start(2);


			Console.WriteLine("I'm waiting for Barista Kim.");
            barista1.Join();
			barista2.Join();
			barista3.Join();
			barista4.Join();

			await ThreadTest();
            //int count = 0;
            //while (count < 100)
            //{
            //	Console.WriteLine("Saying something...");
            //	Thread.Sleep(100);
            //}
        }
		
		static async Task ThreadTest()
		{
			Task[] tasks = new Task[1000];
			for (int i = 0; i < 1000; i++)
			{
				tasks[i] = Task.Run(() => RoastCoffeeBeans(i));
			}

			await Task.WhenAll(tasks);
			Console.WriteLine($"Brewed total {coffeeTotal} shots");
		}

	}
}