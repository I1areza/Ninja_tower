using Godot;
using System;

public partial class Platform : AnimatableBody2D
{	
	[ExportGroup("Script References")]
	[Export] private CollisionShape2D _shape;
	[Export] private Sprite2D _sprite;
	[Export] private AnimationPlayer _animationPlayer;
	
	[ExportGroup("Outline Shader properties")]
	[Export] private float _textureAlpha;
	[Export] private bool _isFlickeringEnabled;
	//[Export] private float _outlineAlpha;
	[Export] private int _outlineWidth;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		
		((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.TEXTURE_ALPHA, _textureAlpha);
		((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.ENABLE_FLICKERING, _isFlickeringEnabled);
		//((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.MAX_OUTLINE_ALPHA, _outlineAlpha);
		((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.OUTLINE_WIDTH, _outlineWidth);
		// The code relies on the correct order of animation tracks.
		// 0 should be shader texture alpha.
		// 1 should be outline alpha.
		//Without that the code won't work as expected.
		_animationPlayer.GetAnimation("FadeIn").TrackSetKeyValue(0,0, _textureAlpha);
		
		_shape.Disabled = true;
	}


	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.D))
		{
			FadeIn();
			SetPlatformOutlineColor(new Color(.5f, .5f, .5f, 1f));
		}
	}


	public void SetPlatformOutlineColor(Color color)
	{
		((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.OUTLINE_COLOR, color);
		_animationPlayer.GetAnimation("FadeIn").TrackSetKeyValue(1,0, color);
		var endColor = color;
		endColor.A = 0;
		_animationPlayer.GetAnimation("FadeIn").TrackSetKeyValue(1,1, endColor);
	}

	public void FadeIn()
	{
		_animationPlayer.Play("FadeIn");
		_shape.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
	}

	
}
