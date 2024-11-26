using Godot;

public partial class Weapon : Area2D
{
    private Sprite2D _weaponSprite;
    private CollisionShape2D _collisionShape;
    private AnimatedSprite2D _slashAnimatedSprite;

    public override void _Ready()
    {
        _weaponSprite = GetNode<Sprite2D>("Sprite2D");
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        _slashAnimatedSprite = GetNode<AnimatedSprite2D>("Slash");
        BodyEntered += OnPlayerBodyEntered;
    }


    private void OnPlayerBodyEntered(Node2D body) 
    {
        if (body is Player player) 
        {
            _slashAnimatedSprite.Play();
            player.Die();
        }
    }
}
