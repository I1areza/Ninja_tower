using Godot;
using System;

public partial class StartMenu : Control
{
	[Export] private Button _startButton;
	[Export] private Button _selectLevelButton;
	[Export] private Button exitButton;
	
	
	public event Action ContinueButtonPressed;
	public event Action SelectLevelButtonPressed;
	public event Action ExitButtonPressed;
	
	public override void _Ready()
	{
		ContinueButtonPressed += () =>
		{
			GD.Print(("Button clicked"));
		};
		_startButton.Pressed += ContinueButtonPressed;
		//_selectLevelButton.Pressed += SelectLevelButtonPressed;
		//exitButton.Pressed += ExitButtonPressed;
	}

	
}
