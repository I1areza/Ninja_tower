using Godot;
using System;
using static Godot.TextServer;

public partial class WeaponizedEnemy: Enemy
{
    private Weapon _weapon;
    public override void _Ready()
	{
        _weapon = GetNode<Weapon>("Weapon");
        base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (!_raycast.IsColliding())
        {
            Flip();
            _direction *= -1;
        }
    }

	protected override void Flip()
	{
		base.Flip();
		_weapon.Scale = new Vector2(_weapon.Scale.X * -1, _weapon.Scale.Y);

	}
}
