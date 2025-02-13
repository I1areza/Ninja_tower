using Godot;
using System;

public partial class Torch : Node2D
{
	private Area2D _area2D;
	private ParticleProcessMaterial _particleProcessMaterial;
	private GpuParticles2D _gpuParticles2D;
	
	
	public override void _Ready()
	{
		_area2D = GetNode<Area2D>("Area2D");
		_area2D.BodyEntered += OnBodyEntered;
		_gpuParticles2D = GetNode<GpuParticles2D>("GPUParticles2D");
		_particleProcessMaterial = (ParticleProcessMaterial)GetNode<GpuParticles2D>("GPUParticles2D").ProcessMaterial;
		
	}

	private void OnBodyEntered(Node2D body)
	{
		
		if (body is Player player)
		{
			
			var tweener = GetTree().CreateTween();
			
			if (player.NormilizedVelocity.X > 0f)
			{
				tweener.TweenMethod(Callable.From<float>(SetAmountRatio), 0.5f, 1f, 0.1);
				tweener.TweenMethod(Callable.From<float>(SetInitialVelocity), 0f, 10f, 0.1f);
				tweener.TweenMethod(Callable.From<float>(SetInitialVelocity), 10f, 0f, 0.5f);
				tweener.TweenMethod(Callable.From<float>(SetAmountRatio), 1f, .5f, 1);

			}else if(player.NormilizedVelocity.X < 0f)
			{
				tweener.TweenMethod(Callable.From<float>(SetAmountRatio), .5f, 1.0f, 0.1);
				tweener.TweenMethod(Callable.From<float>(SetInitialVelocity), 0f, -10f, 0.1f);
				tweener.TweenMethod(Callable.From<float>(SetInitialVelocity), -10f, 0f, 0.5f);
				tweener.TweenMethod(Callable.From<float>(SetAmountRatio), 1.0f ,.5f, 1);
			}
		}
	}

	private void SetInitialVelocity(float initialVelocity)
	{
		_particleProcessMaterial.InitialVelocityMin = initialVelocity;
		_particleProcessMaterial.InitialVelocityMax = initialVelocity;
	}

	private void SetAmountRatio(float ratio)
	{
		_gpuParticles2D.AmountRatio = ratio;
	}
}
