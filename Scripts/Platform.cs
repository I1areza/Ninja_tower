using Godot;
using System;

public partial class Platform : AnimatableBody2D
{
	[Export] private CollisionShape2D _shape;
	[Export] private Sprite2D _sprite;
	[Export] private AnimationPlayer _animationPlayer;
	[Export] private float _alpha;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		/*GD.Print("Alpha " + _alpha);
		GD.Print("Shader Alpha Value before set " + ((ShaderMaterial)_sprite.Material).GetShaderParameter("texture_alpha"));*/
		((ShaderMaterial)_sprite.Material).SetShaderParameter("texture_alpha", _alpha);
		/*GD.Print("Shader Alpha Value after set " + ((ShaderMaterial)_sprite.Material).GetShaderParameter("texture_alpha"));
		GD.Print("Received animation value "+_animationPlayer.GetAnimation("FadeIn").TrackGetKeyValue(0, 0));*/
		_animationPlayer.GetAnimation("FadeIn").TrackSetKeyValue(0,0, _alpha);
		/*GD.Print("Property alpha: "+_alpha);
		GD.Print("Aniation key Alpha " + _animationPlayer.GetAnimation("FadeIn").TrackGetKeyValue(0,0));*/
		_shape.Disabled = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		/*if (Input.IsKeyPressed(Key.D))
		{
			_animationPlayer.Play("FadeIn");
		}*/
	}


	public void SetPlatformOutlineColor(Color color)
	{
		((ShaderMaterial)_sprite.Material).SetShaderParameter("outline_color", color);
	}

	public void FadeIn()
	{
		_animationPlayer.Play("FadeIn");
		_shape.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
	}

	/*public void GetShaderAlpha()
	{
		GD.Print( (float)((ShaderMaterial)_sprite.Material).GetShaderParameter("texture_alpha"));
	}*/
}
