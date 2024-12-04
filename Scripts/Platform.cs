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
	
	

	public override void _Ready()
	{
		
		
		((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.TEXTURE_ALPHA, _textureAlpha);
		
		// The code relies on the correct order of animation tracks.
		// 0 should be shader texture alpha.
		// 1 should be outline alpha.
		//Without that the code won't work as expected.
		_animationPlayer.GetAnimation("FadeIn").TrackSetKeyValue(0,0, _textureAlpha);
		
		_shape.Disabled = true;
	}
	
	public void ConfigurePlatform(Color color, bool isFlickering, float flickeringSpeed )
	{
		((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.OUTLINE_COLOR, color);
		((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.FLICKERING_SPEED, flickeringSpeed);
		((ShaderMaterial)_sprite.Material).SetShaderParameter(OutlineShaderParams.ENABLE_FLICKERING, isFlickering);
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
