using Godot;
using System;
using projectIgonnafinish.Scripts.Utils;

public partial class Score : Label
{
	private int score;
	// Called when the node enters the scene tree for the first time.
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void UpdateScore(EnemyDiedEventArgs args)
	{
		score += args.Score;
		Text = score.ToString();
	}
	
	
}
