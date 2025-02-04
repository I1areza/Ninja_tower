using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using projectIgonnafinish.Scripts.Utils;


public partial class GameManager : Node
{
	private static GameManager _instance;
	private int _score;
	private bool _isGamePaused;
	[Export] private int _heatBarWearOffTime;
	
	[Export] private int _levelSwipesCount;
	[Export] private Player _player;
	[Export] private int _scoreModifier;
	[Export(PropertyHint.File)] private String _nextScenePath;
	[Export] TouchController _touchController;

	public int Score => _score;
	public event Action<int, int> ScoreUpdated;
	public event Action<OnScoreUpdatedEventArgs> HeatscoreUpdated;
	
	public static GameManager Instance => _instance;
	
	
	public override void _Ready()
	{
		if (_instance != null)
		{
			if (_instance != this)
			{
				QueueFree();
			}
			return;
		}
		_instance = this;
		
		_touchController.Init(_levelSwipesCount);
		var iscorables = FindChildren<IScorable>();
		foreach (var scorable in iscorables)
		{
			scorable.ScoreChanged += OnScoreChanged;
			
		}
		HeatscoreUpdated += UIManager.Instance.GetHeatbar().UpdateHeatbar;
		ScoreUpdated += UIManager.Instance.GetScore().StartUpdateScore;
		
		//_player.PlayerDied += ResetLevel;
		var enemies = iscorables.GetObjectsOfType<Enemy>();
		UIManager.Instance.InitializeUIManager(enemies,_touchController, _heatBarWearOffTime, _player);
		
	}

	private void OnScoreChanged(OnScoreUpdatedEventArgs args)
	{
		if (UIManager.Instance.GetHeatbar().ModifierActive)
		{
			_score += args.Score * _scoreModifier;
		}
		else
		{
			_score += args.Score;
		}
		ScoreUpdated?.Invoke(_score, args.Score);
		HeatscoreUpdated?.Invoke(args);
	}

	/*private void ResetLevel()
	{
		ResetLevel(new Vector2());
	}
	private async void ResetLevel(Vector2 point) 
	{
        await ToSignal(GetTree().CreateTimer(3), SceneTreeTimer.SignalName.Timeout);
		GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
	}*/

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

	private void PauseGame()
	{
		GetTree().Paused = true;
		
	}
	
}
