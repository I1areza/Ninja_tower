using Godot;

public partial class Heatbar : Control
{
	
	[Export] private TextureRect _progressBar;
	ShaderMaterial _progressBarShader;
	public override void _Ready()
	{
		_progressBarShader = (ShaderMaterial)_progressBar.Material;
	}
	public override void _Process(double delta)
	{
	}


	public void UpdateHeatbar(float progress)
	{
		if (progress > 1 || progress < 0)
				GD.PushError("The value must be between 0 and 1");
		_progressBarShader.SetShaderParameter("clip_x", progress);
	}
}
