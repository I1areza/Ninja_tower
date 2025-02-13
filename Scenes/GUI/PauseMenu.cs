using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private Button _continueButton;
	[Export] private Button _selectLevelButton;
	[Export] private Button exitButton;
	
	event Action ContinueButtonPressed;
	event Action SelectLevelButtonPressed;
	event Action ExitButtonPressed;
	
	public override void _Ready()
	{
		_continueButton.Pressed += ContinueButtonPressed;
		_selectLevelButton.Pressed += SelectLevelButtonPressed;
		exitButton.Pressed += ExitButtonPressed;
		
	}


}
