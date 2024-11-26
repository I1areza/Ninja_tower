using Godot;
using System;


public partial class TestPoweUp : PowerUpEffect
{
    public override void ApplyEffect(Player player)
    {
		GD.Print("Hurray! Cmpnt Sys is working");
    }
}
