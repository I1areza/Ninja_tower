namespace projectIgonnafinish.Scripts.Utils;

public partial class EnemyDeletedEventArgs:Godot.GodotObject
{
    public int Score { get; set; }
    public Enemy Enemy { get; set; }

    public EnemyDeletedEventArgs(int score, Enemy enemy = null)
    {
        Score = score;
        Enemy = enemy;
    }
}