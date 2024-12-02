using Godot;
using System;

public partial class RemoteFollow : PathFollow2D
{
	[Export] private Node2D _remoteNode;
	
	private RemoteTransform2D _remoteTransform;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_remoteTransform = GetNode<RemoteTransform2D>("RemoteTransform2D");
		if (_remoteNode != null)
		{
			_remoteTransform.RemotePath = _remoteNode._ImportPath;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
