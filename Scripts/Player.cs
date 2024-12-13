using Godot;
using System.Diagnostics;


[GlobalClass]
public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void PlayerDeathEventHandler();
    [Export] private TouchController _touchController;
    [Export] private TrajectoryController _trajectoryController;
    [Export(PropertyHint.Layers2DPhysics)] private int _deathLayer;
    [Export] private AnimationPlayer _animationPlayer;
    

    private GpuParticles2D _onDeathMeatParticles;
    private GpuParticles2D _onDeathBoneParticles;
    private float _gravity;
    private bool _isMoving;
    private Vector2 _to;
    private Vector2 _from;
    private Vector2 _velocity;
    
    public bool IsMoving => _isMoving;

    public override void _Ready()
	{
        _onDeathMeatParticles = GetNode<GpuParticles2D>("MeatParticles");
        _onDeathBoneParticles = GetNode<GpuParticles2D>("BonesParticles");
        _gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
        _touchController.SwipeCompleted += OnSwipeCompleted;
    }

    public override void _PhysicsProcess(double delta)
	{
        if (_isMoving) {
            _velocity.Y += (float)delta * _gravity;
            var collision = MoveAndCollide(_velocity * (float)delta);
            if (collision != null)
            {
               
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

    private void OnSwipeCompleted(Vector2 from, Vector2 to, float speed)
    {
        if (!_isMoving)
        {
            _velocity = (from-to).Normalized() * speed;
            _isMoving = true;
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
        _velocity = direction.Normalized() * _touchController.MaximumSpeed;
        _isMoving = true;
    }
    
}
