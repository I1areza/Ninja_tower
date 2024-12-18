using Godot;

public partial class Vignette : ColorRect
{
	
	private ShaderMaterial _shader;
	private AnimationPlayer _animationPlayer;

	

	public void FadeIn(Vector2 point)
	{
		
		
		
		var uvPoint = new Vector2(point.X/GetViewport().GetVisibleRect().Size.X,  point.Y/GetViewport().GetVisibleRect().Size.Y);
		
		_shader.SetShaderParameter("player_position", uvPoint);
		_animationPlayer.Play("FadeIn");
	}
	
	public void FadeOut(Vector2 point)
	{
		var uvPoint = new Vector2(point.X/GetViewport().GetVisibleRect().Size.X,  point.Y/GetViewport().GetVisibleRect().Size.Y);
		_shader.SetShaderParameter("player_position", uvPoint);
		_animationPlayer.Play("FadeOut");
	}
	
	
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_shader = (ShaderMaterial)Material;
	}

	
	
	
}
