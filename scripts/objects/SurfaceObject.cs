using Godot;
using Bulbhead.Objects.Extensions;
using System;

/// <summary>
/// A close-up interactive surface object
/// </summary>
public partial class SurfaceObject : Node2D, IDraggable
{
	/// <summary>
	/// Sprite of the surface object.
	/// </summary>
	[Export]
	public Sprite2D ObjectSprite;
	[Export]
	public bool IsDraggable = true;

	public bool _isDragging { get; set; }

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
		Button interactionArea = GetNode<Button>("InteractionArea");

		AddToGroup("SurfaceObjects");
		interactionArea.MouseEntered += OnMouseEntered;
		interactionArea.MouseExited += OnMouseExited;

		if (IsDraggable)
		{
			interactionArea.GuiInput += OnInteractionAreaGuiInput;
			interactionArea.ButtonDown += () => { _isDragging = true; };
			interactionArea.ButtonUp += () => { _isDragging = false; };
		}
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
		if (!_isDragging)
		{
			ObjectSprite.Material = null;
		}
	}

	public void OnInteractionAreaGuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseMotion motionEvent && _isDragging)
		{
			PerformDrag(motionEvent);
		}
	}

	/// <summary>
	/// Perform drag operation
	/// </summary>
	/// <param name="motionEvent">Mouse motion event</param>
	public void PerformDrag(InputEventMouseMotion motionEvent)
	{
		this.DefaultPerformDrag(motionEvent);
	}

}
