using Godot;
using System;
using projectIgonnafinish.Scripts.Utils;

public partial class EnemyPresenter : HBoxContainer
{
	private Label _label;
	private int _maximumEnemies;
    private int _currentEnemies;
    [Export] private Score _score;

	public override void _Ready()
	{ 
       _label = GetNode<Label>("Label");
    }
    
    private void Initialize(int maximumEnemies)
    {
        _maximumEnemies = maximumEnemies;
        _currentEnemies = maximumEnemies;
        UpdateEnemyCounter(_maximumEnemies);
    }

    private void UpdateEnemyCounter(int enemiesLeft)
    {
        _currentEnemies = enemiesLeft;
        _label.Text = $"{enemiesLeft}/{_maximumEnemies}";
    }

    private void OnEnemyKilled(OnScoreUpdatedEventArgs args) 
    {
        UpdateEnemyCounter(--_currentEnemies);
    }

    public void Init(Enemy[] enemies)
    {
        Initialize(enemies.Length);
        foreach (var enemy in enemies)
        {
            
            enemy.ScoreChanged += OnEnemyKilled;
        }
    }
}
