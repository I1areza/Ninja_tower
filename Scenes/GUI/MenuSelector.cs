using Godot;

public partial class MenuSelector : Control
{
	[Export] private ColorRect _menuColorRectBackGround;
	[Export] private PauseMenu PauseMenu;
	[Export] private Control LevelSelectionMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void ShowPauseMenu()
	{
		_menuColorRectBackGround.Show();
		PauseMenu.Show();
		LevelSelectionMenu.Hide();
	}
	
	public void ShowLevelSelectionMenu()
	{
		_menuColorRectBackGround.Show();
		LevelSelectionMenu.Show();
		PauseMenu.Hide();
	}
	
	
}
