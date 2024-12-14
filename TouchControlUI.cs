using Godot;

public partial class TouchControlUI : Node2D
{
	[Export] private TouchController _touchController;
	[Export] private Player _player;

	private Sprite2D _innerCircle;
	private Sprite2D _outerCircle;
	public override void _Ready()
	{
		_innerCircle = GetNode<Sprite2D>("Inner Circle");
		_outerCircle = GetNode<Sprite2D>("Outer Circle");
		_innerCircle.Visible = false;
		_outerCircle.Visible = false;
		_touchController.SwipeStarted += OnSwipeStarted;
		_touchController.SwipeCompleted += SwipeCompleted;
	}
	private void OnSwipeStarted(Vector2 from, Vector2 to, float speed)
	{
		_innerCircle.Visible = true;
		_outerCircle.Visible = true;
		_innerCircle.GlobalPosition = to;
		_outerCircle.GlobalPosition = from;
		
	}

	private void SwipeCompleted(Vector2 from, Vector2 to, float speed)
	{
		_innerCircle.Visible = false;
		_outerCircle.Visible = false;
	}
}
