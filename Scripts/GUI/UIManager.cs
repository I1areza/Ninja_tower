using Godot;
using System;

public partial class UIManager : CanvasLayer
{
	private JumpsPresenter _jumpsPresenter;
	private EnemyPresenter _enemyPresenter;
	private Heatbar _heatbar;
	private Score _score;
	//private LevelTimer _levelTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		//_levelTimer = GetNode<LevelTimer>("GUI/Timer");
		_jumpsPresenter = GetNode<JumpsPresenter>("GUI/HBoxContainer/JumpsLeft");
		_enemyPresenter = GetNode<EnemyPresenter>("GUI/HBoxContainer/EnemiesLeft");
		_heatbar = GetNode<Heatbar>("GUI/HeatBar");
		_score = GetNode<Score>("GUI/Score");


	}
	
	public void InitializeUIManager(Enemy[] enemies, TouchController touchController, int heatbarDecreaseTIme)
	{
		_enemyPresenter.Init(enemies);
		_jumpsPresenter.Init(touchController);
		_heatbar.Init(heatbarDecreaseTIme);
	}
	
	public Heatbar GetHeatbar()=>_heatbar;
	public Score GetScore() => _score;
	
}
