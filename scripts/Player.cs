using Godot;
using System;

public partial class Player : CharacterBody2D
{
	/// <summary>
	/// The speed at which the player moves. Sets to velocity.
	/// </summary>
	[Export]
	public float Speed = 100.0f;
	/// <summary>
	/// Timer to use for idle animation triggering.
	/// </summary>
	[Export]
	public Timer IdleTimer;

	/// <summary>
	/// Signal emitted on interaction input, while the animation plays.
	/// </summary>
	[Signal]
	public delegate void InteractionEventHandler();

	private AnimatedSprite2D _animation;

	public override void _Ready()
	{
		_animation = GetNode<AnimatedSprite2D>("PlayerAnimation");
		IdleTimer.Timeout += HandleIdleTimeout;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("interact"))
		{
			HandleInteraction();
			return;
		}

		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;

		}
		else
		{
			velocity = Vector2.Zero;
		}

		Velocity = velocity;
		AnimateMovement();
		CheckIdle();
		MoveAndSlide();
	}

	/// <summary>
	/// Animates the player based on movement direction.
	/// </summary>
	/// <remarks>
	/// Takes the current velocity and sets the appropriate animation.
	/// </remarks>
	private void AnimateMovement()
	{
		if (Velocity.X != 0)
		{
			_animation.Animation = "walk_side";
			if (Velocity.X > 0)
			{
				_animation.FlipH = true;
			}
			else
			{
				_animation.FlipH = false;
			}
		}
		else if (Velocity.Y != 0)
		{
			if (Velocity.Y > 0)
			{
				_animation.Animation = "walk_down";
			}
			else
			{
				_animation.Animation = "walk_up";
			}
		}
		else
		{
			if (!_animation.IsPlaying() || _animation.Animation.ToString().StartsWith("walk_"))
			{
				_animation.Animation = "default";
			}
		}
		
		_animation.Play();
	}

	/// <summary>
	/// Checks if the player is idle and starts/stops the idle timer accordingly.
	/// </summary>
	private void CheckIdle()
	{
		if (_animation.Animation == "default" && IdleTimer.IsStopped())
		{
			IdleTimer.Start();
		}
		else if (_animation.Animation != "default" && !IdleTimer.IsStopped())
		{
			IdleTimer.Stop();
		}
	}

	/// <summary>
	/// Handles player interaction input. Emits the Interaction signal.
	/// </summary>
	private void HandleInteraction()
	{
		EmitSignal(SignalName.Interaction);

		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("PlayerAnimation");
		animatedSprite.Animation = "interact";
	}

	/// <summary>
	/// Actions to perform on idle state change.
	/// </summary>
	private void HandleIdleTimeout()
	{
		_animation.Animation = "idle";
		_animation.Play();
	}
}
