namespace WebApi.Middlewares
{
    public class StatsService
    {
        private int _requestCounter;

        public void IncrementStatsCounter()
        {
            // Thread safety atomic operation, without interruption from other threads, to avoid race conditions.
            Interlocked.Increment(ref _requestCounter);
        }

        public int GetStatsCounter() => _requestCounter;
    }
}
