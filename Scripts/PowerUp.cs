using Godot;
using System;
using System.Diagnostics;
[GlobalClass]
public partial class PowerUp : Area2D
{
	[Export] private PowerUpEffect _effect;
	[Export] private Texture2D _texture;
	private Sprite2D _sprite2d;  
	public override void _Ready()
	{
		_sprite2d = GetNode<Sprite2D>("PowerUpSprite");
        _sprite2d.Texture = _texture;
        Debug.Assert(_effect != null);
		BodyEntered += OnBodyEntered;
		

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void OnBodyEntered(Node2D body) 
	{
		if(body is Player player) 
		{
            _effect.ApplyEffect(player);
			QueueFree();

        }
	}
}
