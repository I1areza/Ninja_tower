using Godot;
using System;

public partial class UIManager : CanvasLayer
{
	[Export] private TouchController _touchController;
	[Export] private EnemyContainer _enemyContainer;
	private JumpsPresenter _jumpsPresenter;
	private EnemyPresenter _enemyPresenter;
	private Heatbar _heatbar;
	//private LevelTimer _levelTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		//_levelTimer = GetNode<LevelTimer>("GUI/Timer");
		_jumpsPresenter = GetNode<JumpsPresenter>("GUI/HBoxContainer/JumpsLeft");
		_enemyPresenter = GetNode<EnemyPresenter>("GUI/HBoxContainer/EnemiesLeft");
		_jumpsPresenter.SetTouchController(_touchController);
		_enemyPresenter.SetEnemyContainer(_enemyContainer);
	}
	
	/*public LevelTimer GetTimer()
	{
		return _levelTimer;
	}*/
	
	
}
