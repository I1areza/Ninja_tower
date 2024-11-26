using Godot;
using System;

public partial class PlayerSpriteController : Sprite2D
{
	[Export] private Texture2D _idleTexture; 
	[Export] private Texture2D _jumpTexture;
	[Export] private Texture2D _fallTexture;
	[Export] private Texture2D _TeleportTexture;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SetJumpTexture()
	{
		Texture = _TeleportTexture;
	}
	private void SetIdleTexture()
	{
		Texture = _idleTexture;
	}
}
