using Godot;
using System;

[GlobalClass]
public abstract partial class PowerUpEffect : Node
{
	abstract public void ApplyEffect(Player player);
}
