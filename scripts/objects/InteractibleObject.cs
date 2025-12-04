using System;
using Godot;

/// <summary>
/// Base class for interactible objects
/// </summary>
public partial class InteractibleObject : Area2D
{
	/// <summary>
	/// Area used to detect interaction availability.
	/// </summary>
	[Export]
	public CollisionShape2D InteractionArea;

	/// <summary>
	/// Sprite of the interactible object.
	/// </summary>
	[Export]
	private Sprite2D ObjectSprite;

	/// <summary>
	/// Material used to highlight the object when interaction is available.
	/// </summary>
	private ShaderMaterial _highlightMaterial = new ShaderMaterial
	{
		Shader = GD.Load<Shader>("res://assets/shaders/outline.gdshader"),
	};

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	public void OnBodyEntered(Node2D body)
	{
		//When player is in range, highlight the object
		if (body is Player)
		{
			ObjectSprite.Material = _highlightMaterial;
		}
	}

	public void OnBodyExited(Node2D body)
	{
		//Player left range, remove highlight
		if (body is Player)
		{
			ObjectSprite.Material = null;
		}
	}
}
