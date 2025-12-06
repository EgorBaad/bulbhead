using Godot;
using System;

/// <summary>
/// A close-up interactive surface object
/// </summary>
public partial class SurfaceObject : Node2D
{
	/// <summary>
	/// Sprite of the surface object.
	/// </summary>
	[Export]
	public Sprite2D ObjectSprite;

	/// <summary>
	/// Shader material used to highlight when the interaction is available
	/// </summary>
	private ShaderMaterial _highlightMaterial = new ShaderMaterial
	{
		Shader = GD.Load<Shader>("res://assets/shaders/outline.gdshader"),
	};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Control interactionArea = GetNode<Control>("InteractionArea");


		AddToGroup("SurfaceObjects");
		interactionArea.MouseEntered += OnMouseEntered;
		interactionArea.MouseExited += OnMouseExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnMouseEntered()
	{
		ObjectSprite.Material = _highlightMaterial;
	}

	public void OnMouseExited()
	{
		ObjectSprite.Material = null;
	}
}
