using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using projectIgonnafinish.Scripts.Utils;


public partial class GameManager : Node
{
	private int _score;
	[Export] private int _heatBarWearOffTime;
	[Export] private UIManager _uiManager;
	[Export] private int _levelSwipesCount;
	[Export] private Player _player;
	[Export] private int _scoreModifier;
	[Export(PropertyHint.File)] private String _nextScenePath;
	[Export] TouchController _touchController;
	public event Action<int> ScoreUpdated;
	public event Action<OnScoreUpdatedEventArgs> HeatscoreUpdated;
	
	
	public override void _Ready()
	{
		_touchController.Init(_levelSwipesCount);
		var iscorables = FindChildren<IScorable>();
		foreach (var scorable in iscorables)
		{
			scorable.ScoreChanged += OnScoreChanged;
			
		}
		HeatscoreUpdated += _uiManager.GetHeatbar().UpdateHeatbar;
		ScoreUpdated += _uiManager.GetScore().UpdateScore;
		var enemies = iscorables.GetObjectsOfType<Enemy>();
		_uiManager.InitializeUIManager(enemies,_touchController, _heatBarWearOffTime, _player);
		_player.PlayerDied += ResetLevel;
    }

	private void OnScoreChanged(OnScoreUpdatedEventArgs args)
	{
		if (_uiManager.GetHeatbar().ModifierActive)
		{
			_score += args.Score * _scoreModifier;
		}
		else
		{
			_score += args.Score;
		}
		ScoreUpdated?.Invoke(_score);
		HeatscoreUpdated?.Invoke(args);
	}

	private void ResetLevel()
	{
		ResetLevel(new Vector2());
	}
	private async void ResetLevel(Vector2 point) 
	{
        await ToSignal(GetTree().CreateTimer(3), SceneTreeTimer.SignalName.Timeout);
		GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
	}

	private T[] FindChildren<T>()
	{
		List<T> Tchildren = new List<T>();
		var children = FindChildren("*").ToArray();
		foreach (var child in children)
		{
			if (child is T tchild)
			{
				Tchildren.Add(tchild);
			}
		}
		return Tchildren.ToArray<T>();
	}
	
	
	
}
