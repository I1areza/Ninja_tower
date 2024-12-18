using Godot;
using System;

public partial class Level : Node
{
	[Export] private Player _player;
	[Export] private ExitLevelDoor _exitLevelDoor;

	public Player Player => _player;
	public ExitLevelDoor ExitLevelDoor => _exitLevelDoor;


}
