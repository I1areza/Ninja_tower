namespace projectIgonnafinish.Scripts.Utils;

public partial class EnemyDiedEventArgs:Godot.GodotObject
{
    public int Score { get; set; }
    public float HeatbarProgress { get; set; }

    public EnemyDiedEventArgs(int score, float heatbarProgress = 0f)
    {
        Score = score;
        HeatbarProgress = heatbarProgress;
    }
}