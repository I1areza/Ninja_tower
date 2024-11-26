using Godot;
using System;

public partial class PortalLink : Node
{
	[Export] private Portal _portalA;
	[Export] private Portal _portalB;
	[Export] private Color _portalColor;
	public override void _Ready()
	{
		if (_portalA.PortalType == _portalB.PortalType)
		{
			GD.PushWarning("Portal Types can not be same.");
		}
		_portalA.SetLinkedPortal(_portalB);
		_portalB.SetLinkedPortal(_portalA);
		_portalA.SetPortalColor(_portalColor);
		_portalB.SetPortalColor(_portalColor);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
