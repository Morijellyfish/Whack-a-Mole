using R3;

public class GameTime
{
    private ReactiveProperty<int> time;
    public ReadOnlyReactiveProperty<int> Time => time;

    public ReadOnlyReactiveProperty<bool> IsTimeUp { get; private set; }

    public GameTime()
    {
        time = new ReactiveProperty<int>(10);
        IsTimeUp = time.Select(t => (t <= 0)).ToReadOnlyReactiveProperty(false);
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
        time.Value = 10;
    }
}
