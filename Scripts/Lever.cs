using Godot;
using System;

public partial class Lever : Area2D
{
	[Export]  Platform _platform = null;
	[Export] private Color _color;
	[Export] private AnimatedSprite2D _animatedSprite;
	[Signal] public delegate void OnLeverTriggeredEventHandler();
	
	private bool isActive = true;
	public override void _Ready()
	{
		((ShaderMaterial)_animatedSprite.Material).SetShaderParameter("outline_color", _color);
		_platform.SetPlatformOutlineColor(_color);
		BodyEntered+=OnBodyEntered;
	}
	private void OnBodyEntered(Node2D body)
	{
		if (body is Player && isActive)
		{
			_animatedSprite.Play();
			_platform.FadeIn();
			isActive = false;
		}

		
	}
}
