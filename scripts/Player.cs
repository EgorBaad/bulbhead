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
	/// Signal emitted on interaction input, while the animation plays.
	/// </summary>
	[Signal]
	public delegate void InteractionEventHandler();

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
		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("PlayerAnimation");
		if (Velocity.X != 0)
		{
			animatedSprite.Animation = "walk_side";
			if (Velocity.X > 0)
			{
				animatedSprite.FlipH = true;
			}
			else
			{
				animatedSprite.FlipH = false;
			}
		}
		else if (Velocity.Y != 0)
		{
			if (Velocity.Y > 0)
			{
				animatedSprite.Animation = "walk_down";
			}
			else
			{
				animatedSprite.Animation = "walk_up";
			}
		}
		else
		{
			if (animatedSprite.IsPlaying() && animatedSprite.Animation.Equals("interact"))
			{
				return;
			}
			animatedSprite.Animation = "default";
		}
		
		animatedSprite.Play();
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
}
