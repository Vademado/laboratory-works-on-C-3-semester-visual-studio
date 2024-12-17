namespace laboratory_work_15
{
    internal class SingleRandomizer
    {
        private static SingleRandomizer instance;
        private Random random;
        private static object locker = new object();

        private SingleRandomizer()
        {
            random = new Random();
            Console.WriteLine("The constructor is called");
        }

        public static SingleRandomizer getInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = new SingleRandomizer();
                }
            }
            return instance;
        }

        public int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
