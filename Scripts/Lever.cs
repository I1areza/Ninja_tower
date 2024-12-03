using Godot;
using System;

public partial class Lever : Area2D
{
	[ExportGroup("Lever References")]
	[Export]  Platform _platform = null;
	[Export] private AnimatedSprite2D _animatedSprite;
	[Export] private AnimationPlayer _animationPlayer;
	
	[ExportGroup("Outline Shader properties")]
	[Export] private bool _isFlickeringEnabled;
	[Export] private float _flickeringSpeed;
	[Export] private Color _outlineColor;
	
	
	[Signal] public delegate void OnLeverTriggeredEventHandler();
	
	private bool isActive = true;
	public override void _Ready()
	{
		((ShaderMaterial)_animatedSprite.Material).SetShaderParameter(OutlineShaderParams.OUTLINE_COLOR, _outlineColor);
		_animationPlayer.GetAnimation("FadeIn").TrackSetKeyValue(0,0, _outlineColor);
		var endColor = _outlineColor;
		endColor.A = 0;
		_animationPlayer.GetAnimation("FadeIn").TrackSetKeyValue(0,1, endColor);
		
		_platform.ConfigurePlatform(_outlineColor, _isFlickeringEnabled, _flickeringSpeed);
		BodyEntered+=OnBodyEntered;
	}
	private void OnBodyEntered(Node2D body)
	{
		if (body is Player && isActive)
		{
			_animatedSprite.Play();
			_animationPlayer.Play("FadeIn");
			_platform.FadeIn();
			isActive = false;
		}

		
	}
}
