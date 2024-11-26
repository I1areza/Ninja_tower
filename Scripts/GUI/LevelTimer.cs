using Godot;
using System;

public partial class LevelTimer : Label
{
    [Signal]
    public delegate void OnLevelTimerTimeoutEventHandler();
    private Timer _timer;
    
    public override void _Ready()
	{
        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += OnTimerTimeout;
    }

	public override void _Process(double delta)
	{
        Text = Mathf.FloorToInt(_timer.TimeLeft).ToString();
    }

	public void InitializeTimer(float time) 
	{
        _timer.WaitTime = time;
        _timer.Start();
    }
    

    private void OnTimerTimeout() 
    {
        EmitSignal(SignalName.OnLevelTimerTimeout);
    }
}
