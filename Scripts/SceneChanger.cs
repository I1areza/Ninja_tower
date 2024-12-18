using Godot;
using System;
using System.Diagnostics;

public partial class SceneChanger : Node
{
	private Level _currentLevel;
	[Export(PropertyHint.ResourceType,"LevelsList")] private LevelsList _levelsList;
	private Vignette _vignette;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
			_vignette = GetNode<Vignette>("Vignette");
			ReloadScene();
			_vignette.FadeOut(_currentLevel.Player.GetGlobalTransform().Origin);
	}

	private async void OnPlayerDied(Vector2 point)
	{
		_vignette.FadeIn(point);
		await ToSignal(GetTree().CreateTimer(3), SceneTreeTimer.SignalName.Timeout);
		ReloadScene();
		_vignette.FadeOut(_currentLevel.Player.GetGlobalTransform().Origin);
	}

	private async void OnExit()
	{
		_vignette.FadeIn(_currentLevel.ExitLevelDoor.GetGlobalTransform().Origin);
		await ToSignal(GetTree().CreateTimer(3), SceneTreeTimer.SignalName.Timeout);
		LoadNextScene();
		_vignette.FadeOut(_currentLevel.Player.GetGlobalTransform().Origin);
	}

	private void LoadNextScene()
	{
		_currentLevel.QueueFree();
		if (_levelsList.TryGetNextLevel(out var nextPackedScene))
		{
			_currentLevel = (Level)nextPackedScene.Instantiate();
			AddChild(_currentLevel);
			_currentLevel.Player.PlayerDied += OnPlayerDied;
			_currentLevel.ExitLevelDoor.OnExit += LoadNextScene;
		}
		GD.PrintErr("No more levels to load");
		
		
	}
	
	private void ReloadScene()
	{
		_currentLevel?.QueueFree();
		_currentLevel = (Level)_levelsList.GetCurrentLevel().Instantiate();
		AddChild(_currentLevel);
		_currentLevel.Player.PlayerDied += OnPlayerDied;
		_currentLevel.ExitLevelDoor.OnExit += OnExit;

	}
}
