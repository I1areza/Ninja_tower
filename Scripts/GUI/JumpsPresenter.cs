using Godot;
using System;

public partial class JumpsPresenter : HBoxContainer
{
	private int _maximumJumps;
    private int _currentJumps;
    private Label _label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		
    }

	private void Init(int maximumJumps) 
	{
		_maximumJumps = maximumJumps;
		_currentJumps = maximumJumps;
        UpdateJumps(maximumJumps);
    }

	private void UpdateJumps(int jumpsLeft) 
	{
		_label.Text = $"{jumpsLeft}/{_maximumJumps}";
    }

	private void OnSwipceCompleted(Vector2 vector) 
	{
		_currentJumps -= 1;
        UpdateJumps(_currentJumps);
    }

	public void Init(TouchController touchController)
	{
		Init(touchController.MaximumSwipeCount);
		touchController.SwipeCompleted += OnSwipceCompleted;
	}
}
