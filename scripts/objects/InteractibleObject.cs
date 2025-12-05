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
	public Sprite2D ObjectSprite;

	/// <summary>
	/// Text to show for interaction prompt.
	/// </summary>
	[Export]
	public string InteractionText = "Interact";

	/// <summary>
	/// Object can be selected as primary interaction
	/// </summary>
	/// <param name="obj">Object that emitted the signal</param>
	[Signal]
	public delegate void CanSelectEventHandler(InteractibleObject obj);

	/// <summary>
	/// Object is deselected. Emitted by interactible upon deselection by moving out of range or by calling DeselectInteractible() method.
	/// </summary>
	/// <param name="obj">Object that emitted the signal</param>
	[Signal]
	public delegate void ObjectDeselectedEventHandler(InteractibleObject obj);

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

		AddToGroup("InteractibleObjects");

		GetNode<Label>("InteractionText").Text = InteractionText;
	}

	public void OnBodyEntered(Node2D body)
	{
		//When player is in range, let wrappers know it can be selected
		if (body is Player)
		{
			EmitSignal(SignalName.CanSelect, this);
		}
	}

	public void OnBodyExited(Node2D body)
	{
		//Player left range, deselect
		if (body is Player)
		{
			DeselectInteractible();
		}
	}

	/// <summary>
	/// Mark interactible as selected
	/// </summary>
	public void SelectInteractible()
	{
		ObjectSprite.Material = _highlightMaterial;
		GetNode<Label>("InteractionText").Visible = true;
	}

	/// <summary>
	/// Remove selection
	/// </summary>
	public void DeselectInteractible()
	{
		ObjectSprite.Material = null;
		GetNode<Label>("InteractionText").Visible = false;
		EmitSignal(SignalName.ObjectDeselected, this);
	}

	public virtual void Interact()
	{
		GD.Print("No interaction logic defined! Check object " + this);
	}
}
