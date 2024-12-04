using Godot;
using System;
using projectIgonnafinish.Scripts.Utils;

public partial class GameManager : Node
{
	
	
	[Export] private UIManager _uiManager;
	[Export] private Player _player;
	[Export] private int _levelTimeLimit;
	[Export] private EnemyContainer _enemyContainer;
	[Export(PropertyHint.File)] private String _nextScenePath;
	
	
	public override void _Ready()
	{
		
        _player.PlayerDeath += ResetLevel;
    }

	private async void ResetLevel() 
	{
        await ToSignal(GetTree().CreateTimer(3), SceneTreeTimer.SignalName.Timeout);
		GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
	}

	private void OnEnemyDeath(EnemyDiedEventArgs args)
	{
		
	}
	
}
