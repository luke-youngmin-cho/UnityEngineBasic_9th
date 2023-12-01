namespace MultiThread_RaceCondition
{
	// 동기화
	// Mutual exclusion (상호 배제)
	// 어떤 쓰레드가 자원을 사용하고있으면 해당 자원은 다른 쓰레드가 접근못함
	// 
	// Dead lock (교착 상태) , Starvation (기아 상태)
	// 각각의 쓰레드가 서로 필요한 자원들을 공유하고있을때 리소스해제를 못하는현상

	// Critical Section
	// 상호배제가 필요한 구역

	// Semaphore (세마포어) 
	// 정해놓은 수의 스레드만 자원을 공유할 수 있도록 제한하는 방법.

	// Mutex (뮤텍스)
	// 하나의 쓰레드만 자원을 점유하도록 제한하는 방법.
	public class A
	{
		private static object _lockObject = new object();
		private static Mutex _mutex = new Mutex();
		public static A instance
		{
			get
			{
				// Mutual Exclusion
                lock (_lockObject)
                {
					if (_instance == null)
					{
						_instance = new A();
					}
				}

				_mutex.WaitOne();
				if (_instance == null)
				{
					_instance = new A();
				}
				_mutex.ReleaseMutex();

				return _instance;
			}
		}
		private static A _instance;
	}

	internal class Program
	{
		static Semaphore semaphore = new Semaphore(2, 2);

		static void Main(string[] args)
		{
			for (int i = 0; i < 10; i++)
			{
				Thread thread = new Thread(DoSomething);
				thread.Start(i);
			}
		}

		static void DoSomething(object i)
		{
            Console.WriteLine($"Thread {i} is waiting...");
			semaphore.WaitOne();

            Console.WriteLine($"Thread {i} is starting something");
			Thread.Sleep(5000);
			Console.WriteLine($"Thread {i} is finishing something");

			semaphore.Release();
		}

		static IntPtr pProgress;

		unsafe static bool IsDone(float percent)
		{
			return (*(float*)pProgress.ToPointer()) > percent;
		}
	}
}