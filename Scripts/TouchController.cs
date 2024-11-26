using Godot;
[GlobalClass]
public partial class TouchController: Node
{
	[Export] private int _maximumSwipeCount;
    private int _currentSwipeCount;
    private bool _inProgress = false;
	private bool _swipesEnabled = true;
	Vector2 to;
	Vector2 from;
	[Signal]
	public delegate void SwipeCompletedEventHandler(Vector2 direction);
	[Signal]
	public delegate void SwipeStartedEventHandler(Vector2 direction);
	[Signal]
	public delegate void SwipeEndedEventHandler();
    [Signal]
    public delegate void NoSwipesLeftEventHandler();

	public int MaximumSwipeCount 
	{
		get { return _maximumSwipeCount; }
	}

    public bool InProgress 
	{
		get { return _inProgress; }
    }


    public override void _Ready()
    {
		_currentSwipeCount = _maximumSwipeCount;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
		if(_currentSwipeCount <= 0) 
		{
			
			if (_swipesEnabled) 
			{
				_swipesEnabled = false;
				EmitSignal(nameof(SignalName.NoSwipesLeft));
            }
            return;
		}
		if (@event is InputEventScreenTouch && @event.IsPressed() && !InProgress)
		{
            var position = (@event as InputEventScreenTouch).Position;
            _inProgress = true;
            to = position;
		}
		else if(@event is InputEventScreenDrag && InProgress) 
		{
            var position = (@event as InputEventScreenDrag).Position;
            EmitSignal(nameof(SignalName.SwipeStarted), to - position);
        }
		else if (@event is InputEventScreenTouch && @event.IsReleased() && InProgress) 
		{
			var position = (@event as InputEventScreenTouch).Position;
            from = position;
            EmitSignal(nameof(SignalName.SwipeCompleted), to - from);
            _inProgress = false;
			_currentSwipeCount -= 1;
        }
    }
}

