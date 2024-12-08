using System;
using Godot;
using projectIgonnafinish.Scripts.Utils;

public partial class Enemy : CharacterBody2D, IScorable
{
	[Export] protected float _speed;
	[Export] protected int _score;
	[Export] protected float _heatbarProgress;
	private Area2D _area;
	protected RayCast2D _raycast;
	private float _gravity;
	private Sprite2D _sprite2d;
	protected Vector2 _direction = new Vector2(1, 0);
	private Vector2 _velocity;
	private bool isCollided;
	public event Action<OnScoreUpdatedEventArgs> ScoreChanged;
	
	///[Signal]
	//public delegate void EnemyDiedEventHandler(OnScoreUpdatedEventArgs eventArgs);

	public override void _Ready()
	{
		_area = GetNode<Area2D>("Area2D");
		_area.BodyEntered += OnAreaEnteredByPlayer;
        _raycast = GetNode<RayCast2D>("RayCast2D");
		_gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
		_sprite2d = GetNode<Sprite2D>("EnemySprite");
		

    }
	
	public override void _Process(double delta)
	{
		
		if (!_raycast.IsColliding())
		{
			GD.Print(_raycast.IsColliding());
			Flip();
			_direction *= -1;
		}
		
	}

    public override void _PhysicsProcess(double delta)
    {
		_velocity = _direction * _speed;
        _velocity.Y = _gravity * (float)delta;
		MoveAndCollide(_velocity * (float)delta);
    }

	protected virtual void Flip() 
	{
		_sprite2d.FlipH = !_sprite2d.FlipH;
        _raycast.Position *= -1;
	}


	private void OnAreaEnteredByPlayer(Node2D body) 
	{
		_area.SetDisableMode(DisableModeEnum.Remove);
		var player = body  as Player;
		
		if (player != null && !isCollided) 
		{
			RemoveEnemy();
			isCollided = true;
		}
	} 
	
	private void RemoveEnemy() 
	{
        QueueFree();
        ScoreChanged(new OnScoreUpdatedEventArgs(_score, _heatbarProgress, this));

	}

	
}
