using Godot;
using System.Diagnostics;


[GlobalClass]
public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void PlayerDeathEventHandler();
    [Export] private TouchController _controller;
    [Export] private TrajectoryController _trajectoryController;
    [Export] private float _minumumSpeed = 10f;
    [Export] private float _maximumSpeed = 100f;
    [Export] private float _maximumSwipeDistance = 120f;
    [Export] private float _minimumSwipeDistance = 30f;
    [Export(PropertyHint.Layers2DPhysics)] private int _deathLayer;
    [Export] private AnimationPlayer _animationPlayer;
    

    private GpuParticles2D _onDeathMeatParticles;
    private GpuParticles2D _onDeathBoneParticles;
    private float _gravity;
    private bool _isMoving;
    private Vector2 _to;
    private Vector2 _from;
    private Vector2 _velocity;
    private float _speedRange;
    private float _swipeRange;
    
    public bool IsMoving => _isMoving;

    public override void _Ready()
	{
        _onDeathMeatParticles = GetNode<GpuParticles2D>("MeatParticles");
        _onDeathBoneParticles = GetNode<GpuParticles2D>("BonesParticles");
        _gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
        _controller.SwipeCompleted += OnSwipeCompleted;
        _speedRange = _maximumSpeed - _minumumSpeed;
        if (_speedRange <= 0)
        {
            Debug.Assert(false, "Maximum speed should be more than the minimum speed");
        }
        _swipeRange = _maximumSwipeDistance - _minimumSwipeDistance;
        if (_swipeRange <= 0)
        {
            Debug.Assert(false, "Maximum Swipe Range should be more than the minimum swipe range");
        }
    }

    public override void _PhysicsProcess(double delta)
	{
        if (_isMoving) {
            _velocity.Y += (float)delta * _gravity;
            var collision = MoveAndCollide(_velocity * (float)delta);
            if (collision != null)
            {
                GD.Print(collision.GetCollider().ToString());
                if (collision.GetCollider() is TileMapLayer  || collision.GetCollider() is Platform) 
                {
                    
                    if (collision.GetCollider() is TileMapLayer tileMapLayer) 
                    {
                        if (tileMapLayer.TileSet.GetPhysicsLayerCollisionLayer(0) == _deathLayer)
                        {
                            Die(); 
                        }
                    }
                    _isMoving = false;
                    _velocity = Vector2.Zero;
                }
            }
        }
    }

    private void OnSwipeCompleted(Vector2 direction)
    {
        var speed = CalculateSpeed(direction);

        if (!_isMoving)
        {
            _velocity = direction.Normalized() * speed;
            _isMoving = true;
        }
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
            var swipe_proportion = (swipe_distance - _minimumSwipeDistance) / _swipeRange;
            return swipe_proportion * _speedRange + _minumumSpeed;
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

    public void Die() 
    {
        _onDeathMeatParticles.Emitting = true;
        _onDeathBoneParticles.Emitting = true;
        GetNode<Sprite2D>("Sprite2D").Visible = false;
        CollisionLayer = 0;
        _onDeathMeatParticles.Finished += QueueFree;
        EmitSignal(SignalName.PlayerDeath);
        
    }

    public async void Teleport(Vector2 globalPosition, Vector2 direction)
    {
        _velocity = Vector2.Zero;
        _isMoving = false;
        _animationPlayer.Play("teleport_in");
        //_sprite.RegionRect = new Rect2(103f, 9f, 15f, 16f);
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        GlobalPosition = globalPosition;
        _animationPlayer.Play("teleport_out");
        _velocity = direction.Normalized() * _maximumSpeed;
        _isMoving = true;
    }
    
}
