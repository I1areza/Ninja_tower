using Godot;
using System;

public partial class Portal : Area2D
{
	private bool _isActive = true;
	private Portal _linkedPortal;
	[Export] private bool _isOneWay;
	[Export] private PortalType _portalType;
	[Export] private Marker2D _marker2D;

	public Vector2 Direction => _marker2D != null ?  (_marker2D.GlobalPosition - GlobalPosition).Normalized(): Vector2.Zero;

	public PortalType PortalType => _portalType;

	public void SetLinkedPortal(Portal portal)
	{
		_linkedPortal = portal;
	}

	public void SetPortalColor(Color color)
	{
		Modulate = color;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player && _portalType == PortalType.In && _isActive)
		{
			_isActive = false;
			player.Teleport(_linkedPortal.GlobalPosition, _linkedPortal.Direction);
		}
	}


	private void OnBodyExtited(Node2D body)
	{
		if (body is Player)
		{
			_isActive = true;
		}
	}

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExtited;
	}
}

public enum PortalType
{
	In=0,
	Out=1
}