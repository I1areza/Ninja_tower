using Godot;
using System;

[GlobalClass]
public partial class TrajectoryController : Line2D
{
	 
	CharacterBody2D _collisionTest;
	[Export] TouchController _touchController;
	[Export] Player _player;
	[Export] private float _pointQuantity;

	
	private float _gravity;

	private Vector2 _velocity;

	private bool isTrajectoryShown;

    public override void _Ready()
	{
        _touchController.SwipeStarted += OnSwipeStarted;
		_touchController.SwipeCompleted += OnSwipeCompleted;
        _gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
		_collisionTest = GetNode<CharacterBody2D>("CollisionTest");
        
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		var velocity = _velocity;
		if (isTrajectoryShown & !_player.IsMoving)
		{
			var position = Vector2.Zero;
            _collisionTest.Position = position;
            ClearPoints();
			for (int i = 0; i < _pointQuantity; i++)
			{
				AddPoint(position);
				velocity.Y += _gravity * (float)delta;
				position += velocity * (float)delta;
                var collision = _collisionTest.MoveAndCollide(velocity * (float)delta, true);
				if (collision != null) 
				{
					break;
				}
                _collisionTest.Position = position;
            }
		}
		else
		{
			ClearPoints();
		}
	}

	private void OnSwipeStarted(Vector2 from, Vector2 to, float speed) 
	{
        isTrajectoryShown = true;
        _velocity = (from-to).Normalized() * speed;
    }

	private void OnSwipeCompleted(Vector2 from, Vector2 to, float speed) 
	{
        isTrajectoryShown = false;
    }
}
