namespace DevTools.TestsX
{
    public class DebugListenerTests
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void TimeSpanTest()
        {
            TimeSpan timeSpan = TimeSpan.FromMicroseconds(10123456);
            int seconds = timeSpan.Seconds;
            int milliseconds = timeSpan.Milliseconds;
            int microseconds = timeSpan.Microseconds;
            double totalSeconds = timeSpan.TotalSeconds;
            string totalSecondsString = $"{totalSeconds:#,##0.000\\'000}";
        }
    }
}
