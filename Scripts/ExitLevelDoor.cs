using Godot;
using System;

public partial class ExitLevelDoor : Area2D
{
	public event Action OnExit;

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			OnExit?.Invoke();
		}
	}
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}
}
