using R3;

public class PreparationTime
{
    private ReactiveProperty<int> time;
    public ReadOnlyReactiveProperty<int> Time => time;

    public ReadOnlyReactiveProperty<bool> IsFinished { get; private set; }

    public PreparationTime()
    {
        time = new ReactiveProperty<int>(3);
        IsFinished = time.Select(t => (t <= 0)).ToReadOnlyReactiveProperty(false);
    }

    public void Tick()
    {
        if (time.Value > 0)
        {
            time.Value--;
        }
    }

    public void Reset()
    {
        time.Value = 3;
    }
}
