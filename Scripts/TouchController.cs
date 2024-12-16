using System;
using Godot; 
[GlobalClass]
public partial class TouchController: Node
{
	[Export] private float _minimumSwipeDistance = 30f;
	[Export] private float _maximumSwipeDistance = 120f;
	[Export] private float _minumumSpeed = 250;
	[Export] private float _maximumSpeed = 500f;
	[Export] private int _maximumSwipeCount;
	
	
    private int _currentSwipeCount;
    private bool _inProgress;
	private bool _swipesEnabled = true;
	Vector2 to;
	Vector2 from;
	
	[Signal]
	public delegate void SwipeCompletedEventHandler(Vector2 from, Vector2 to, float speed);
	
	[Signal]
	public delegate void SwipeStartedEventHandler(Vector2 from, Vector2 to, float speed);
	
    
    public Action  NoSwipesLeft;

	public int MaximumSwipeCount => _maximumSwipeCount;

	public bool InProgress => _inProgress;

	public float MinimumSpeed => _minumumSpeed;
	public float MaximumSpeed => _maximumSpeed;
	
    public void Init(int swipes)
    {
	    _maximumSwipeCount = swipes;
	    _currentSwipeCount = swipes;
    }


    public override void _Ready()
    {
    }
    
    public  float CalculateSpeed(Vector2 swipe) 
    {
	    var swipe_distance = swipe.Length();
	    if (swipe_distance >= _maximumSwipeDistance)
	    {
		    return _maximumSpeed;
	    }
	    else if (swipe_distance > _minimumSwipeDistance && swipe_distance < _maximumSwipeDistance)
	    {
		    var swipe_proportion = (swipe_distance - _minimumSwipeDistance) / (_maximumSwipeDistance - _minimumSwipeDistance);
		    return swipe_proportion * (_maximumSpeed - _minumumSpeed) + _minumumSpeed;
	    }
	    else if (swipe_distance == _minimumSwipeDistance)
	    {
		    return _minumumSpeed;
	    }
	    else
	    {
		    return 0;
	    }
    }
    

    public override void _UnhandledInput(InputEvent @event)
    {
		if(_currentSwipeCount <= 0) 
		{

			if (_swipesEnabled)
			{
				_swipesEnabled = false;
				NoSwipesLeft?.Invoke();
			}

			return;
		}
		if (@event is InputEventScreenTouch && @event.IsPressed() && !InProgress)
		{
            var position = (@event as InputEventScreenTouch).Position;
            _inProgress = true;
            from = position;
		}
		else if(@event is InputEventScreenDrag && InProgress) 
		{
            var position = (@event as InputEventScreenDrag).Position;
            EmitSignal(nameof(SignalName.SwipeStarted), from, position, CalculateSpeed(from-position));
        }
		else if (@event is InputEventScreenTouch && @event.IsReleased() && InProgress) 
		{
			var position = (@event as InputEventScreenTouch).Position;
            to = position;
            EmitSignal(nameof(SignalName.SwipeCompleted), from, to, CalculateSpeed(from-to));
            _inProgress = false;
			_currentSwipeCount -= 1;
        }
    }
}

