using Godot;
using System;
using projectIgonnafinish.Scripts.Utils;

public partial class Coin : Area2D, IScorable
{
	[Export] private int _score;
	[Export] private float _heatbarProgress;
	[Export] private bool _isSwaying;
	[Export] private bool _otherWay;
	[Export] private Animation _animation;
	public event Action<OnScoreUpdatedEventArgs> ScoreChanged;
	
	private AnimationPlayer _animationPlayer;
	public override void _Ready()
	{
		BodyEntered += OnAreaEnteredByPlayer;
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		#region Animation Update
		//This code is required to create and assign an animation that won't be shared across multiple entities of coin node.
		//see details https://github.com/godotengine/godot/issues/82421
		var animLibrary = new AnimationLibrary();
		var newAnimation = (Animation)_animation.Duplicate();
		var animName = $"CoinAnimation {Guid.NewGuid().ToString()}";
		var animLibName = "animLibrary";
		animLibrary.AddAnimation(animName, newAnimation);
		_animationPlayer.AddAnimationLibrary(animLibName, animLibrary);
		//animation name consists of Libraryname and animation name. "exampleLib/exampleAnim"
		_animationPlayer.GetAnimation($"{animLibName}/{animName}").TrackSetEnabled(1, _isSwaying);
		_animationPlayer.Play($"{animLibName}/{animName}");
		//each coin will start randomly between 0 and 1 second frame.
		_animationPlayer.Advance(GD.RandRange(0f,1f));
		#endregion
		
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
	private void OnAreaEnteredByPlayer(Node2D body) 
	{
		var player = body as Player;
		
		if (player != null)
		{
			ScoreChanged.Invoke(new OnScoreUpdatedEventArgs(_score, _heatbarProgress));
			CallDeferred(MethodName.QueueFree);
		}
	} 

	
}
