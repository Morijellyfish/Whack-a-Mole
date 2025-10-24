using R3;

public class ScoreData
{
    private ReactiveProperty<int> score = new ReactiveProperty<int>(0);
    public ReadOnlyReactiveProperty<int> Score => score;

    public void AddScore()
    {
        score.Value += 1;
    }
}
