using Godot;
using System;
using projectIgonnafinish.Scripts.Utils;

public partial class Score : Control
{
	private Heatbar _heatbar;
	private int _score;
	private Label _label;
	private int _bufferScore;
	private bool _scoreChangeInitiated;
	[Export(PropertyHint.File)] private String _singleScoreScene;
	

	public void Init(Heatbar heatbar)
	{
		_heatbar = heatbar;
	}
	
	
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
	}

	private void UpdateScore(int score)
	{
		
		_label.Text = score.ToString("D4");
	}
	
	//TODO
	//method to accumulate points for a second before updating the score. Needed in order to minimize the number of calls of that update score methid.
	public void StartUpdateScore(int finalScore, int points)
	{
		_score = finalScore;
		_bufferScore += points;
		if (!_scoreChangeInitiated)
		{
			_scoreChangeInitiated = true;
			var timer = GetTree().CreateTimer(0.5);
			timer.Timeout += () => InstantiateScore(_bufferScore);
		}
	}

	private void InstantiateScore(int bufferScore)
	{
		var scene = GD.Load<PackedScene>(_singleScoreScene);
		var scoreScene = scene.Instantiate();
		_label.AddChild(scoreScene);
		((Label)scoreScene).Text = $"+{bufferScore}";
		if (_heatbar.ModifierActive)
		{
			((Label)scoreScene).Text += "x2";
		}
		var animationPlayer = scoreScene.GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.AnimationFinished += OnAnimationFinished;
		animationPlayer.Play("FadeInOut");
		_scoreChangeInitiated = false;
		_bufferScore = 0;
	}

	private void OnAnimationFinished(StringName animationName)
	{
		if (animationName != "")
		{
			UpdateScore(_score);
		}
	}
}
