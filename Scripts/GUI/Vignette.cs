using Godot;

public partial class Vignette : ColorRect
{
	private Player _player;
	private ShaderMaterial _shader;
	private AnimationPlayer _animationPlayer;

	public void Init(Player player)
	{
		_player = player;
		_player.PlayerDied += OnPlayerDied;
	}

	private void OnPlayerDied(Vector2 point)
	{
		
		
		
		var uvPoint = new Vector2(point.X/GetViewport().GetVisibleRect().Size.X,  point.Y/GetViewport().GetVisibleRect().Size.Y);
		
		_shader.SetShaderParameter("player_position", uvPoint);
		_animationPlayer.Play("Vignette");
	}
	
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_shader = (ShaderMaterial)Material;
	}

	
	
	
}
