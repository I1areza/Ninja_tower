using Godot;
using System;
using projectIgonnafinish.Scripts.Utils;

public partial class Score : Control
{
	private Label _label; 
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
	}

	public void UpdateScore(int score)
	{
		
		_label.Text = score.ToString();
	}
	
	
}
