using Godot;
using System;

public partial class GameManager : Node
{
	[Export] private UIManager _uiManager;
	[Export] private Player _player;
	[Export] private int _levelTimeLimit;
	[Export(PropertyHint.File)] private String _nextScenePath;
	//private LevelTimer _levelTimer;

	public override void _Ready()
	{
		//_levelTimer = _uiManager.GetTimer();	
		//_levelTimer.InitializeTimer(_levelTimeLimit);
        _player.PlayerDeath += ResetLevel;
		//_levelTimer.OnLevelTimerTimeout += ResetLevel;
    }

	private async void ResetLevel() 
	{
        await ToSignal(GetTree().CreateTimer(3), SceneTreeTimer.SignalName.Timeout);
		GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
	}
}
