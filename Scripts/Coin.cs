using Godot;
using System;
using projectIgonnafinish.Scripts.Utils;

public partial class Coin : Area2D, IScorable
{
	[Export] private int _score;
	[Export] private float _heatbarProgress;
	[Export] private bool _isSwaying = false;
	public event Action<OnScoreUpdatedEventArgs> ScoreChanged;
	
	private AnimationPlayer _animationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		BodyEntered += OnAreaEnteredByPlayer;
		_animationPlayer.Play("Coin Movement");
		_animationPlayer.Advance(GD.RandRange(0f,1f));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
	private void OnAreaEnteredByPlayer(Node2D body) 
	{
		var player = body as Player;
		
		if (player != null)
		{
			ScoreChanged(new OnScoreUpdatedEventArgs(_score, _heatbarProgress));
			CallDeferred(MethodName.QueueFree);
		}
	} 

	
}
