using Godot;

public partial class ArealEnemyPath : PathFollow2D
{
	[Export] private float _speed;
	[Export] private bool _cyclicPath;
	private bool _isMovingBack = false;
	private Area2D _area;

	

	public override void _Ready()
	{
		_area = GetNode<Area2D>("Area2D");
		_area.BodyEntered += OnPlayerBodyEntered;
		Loop = _cyclicPath;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_cyclicPath) 
		{
			Progress += (float)delta * _speed; 
		}
		else 
		{
			if (ProgressRatio <= 1 && !_isMovingBack)
			{
				Progress += (float)delta * _speed;
			}
			else if(ProgressRatio <= 1 && _isMovingBack) 
			{
                Progress -= (float)delta * _speed;
            }
			
			if(ProgressRatio == 0 && _isMovingBack)
			{
                _isMovingBack = false;
            }
            else if (ProgressRatio == 1 && !_isMovingBack)
            {
                _isMovingBack = true;

            }


        }
	}

	private void OnPlayerBodyEntered(Node2D node) 
	{
        if (node is Player player)
        {
            player.Die();
        }
    }
}
