namespace bosxixi.ProgressBar
{
    public interface IProgressBar
    {
        void Tick(long currentTick, string message = "");
        void Tick(string message = "");
        decimal Percentage { get; }
        long CurrentTick { get; }
        long MaxTicks { get; }
    }
}
