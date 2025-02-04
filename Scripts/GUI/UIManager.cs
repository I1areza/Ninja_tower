using Godot;

public partial class UIManager : CanvasLayer
{
	[Export] private JumpsPresenter _jumpsPresenter;
	[Export] private EnemyPresenter _enemyPresenter;
	[Export] private Heatbar _heatbar;
	[Export] private Score _score;
	[Export] private TextureButton _menuButton;
	[Export] private MenuSelector _menuSelector;
	[Export] private StartMenu _startMenu;
	private static UIManager _instance;

	public static UIManager Instance => _instance;

	
	public TextureButton MenuButton => _menuButton;
	public Heatbar GetHeatbar()=>_heatbar;
	public Score GetScore() => _score;
	

	


	public override void _Ready()
	{
		if (_instance != null)
		{
			if (_instance != this)
			{
				QueueFree();
			}
			return;
		}
		_instance = this;
		_menuButton.Pressed += _menuSelector.ShowPauseMenu;
	}

	public void InitializeUIManager(Enemy[] enemies, TouchController touchController, int heatbarDecreaseTIme, Player player)
	{
		_enemyPresenter.Init(enemies);
		_jumpsPresenter.Init(touchController);
		_heatbar.Init(heatbarDecreaseTIme);
		_score.Init(_heatbar);
	}

	
	
}
