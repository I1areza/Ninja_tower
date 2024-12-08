using System;
using Godot;
using projectIgonnafinish.Scripts.Utils;

public partial class Heatbar : Control
{
	[Export] private TextureRect _progressBar;
	private float _timeToWearOff;
	private float _progress;
	ShaderMaterial _progressBarShader;
	private bool _heatbarActivated;
	private float _time;

	public bool ModifierActive { get; private set; }


	public override void _Ready()
	{
		_progressBarShader = (ShaderMaterial)_progressBar.Material;
		Visible = false;
		
	}

	public void Init(int timetoWearOff)
	{
		_timeToWearOff = timetoWearOff;
	}
	
	public override void _Process(double delta)
	{
		if (_progress > 0f)
		{
			//_time += (float)delta;
			_progress = Mathf.Clamp((_progress - (float)delta/_timeToWearOff),0f,1f);
			_progressBarShader.SetShaderParameter("clip_x", _progress);
		}

		if (_progress == 0)
		{
			if (ModifierActive)
			{
				ModifierActive = false;
				_time = 0;
			}
			Visible = false;
		}
		
	}
	
	public void UpdateHeatbar(OnScoreUpdatedEventArgs args)
	{
		_time = 0;
		
		if (_progress + args.HeatbarProgress >= 1 && !ModifierActive)
		{
			ModifierActive = true;
		}
		Visible = true;
		_progress = Mathf.Clamp(_progress + args.HeatbarProgress, 0, 1);
		GD.Print($"{(args.Sender as IScorable)}");
		//_progressBarShader.SetShaderParameter("clip_x", _progress);
	}
}
