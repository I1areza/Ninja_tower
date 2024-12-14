using Godot;

public partial class TouchControlUI : Node2D
{
	[Export] private TouchController _touchController;
	[Export] private Player _player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
