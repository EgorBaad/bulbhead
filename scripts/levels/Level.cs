using Godot;
using System;

/// <summary>
/// Generic level class for common logic
/// </summary>
public partial class Level : Node2D
{
	private InteractibleObject _selectedInteractible = null;
	private Node2D _viewport;
	private Player _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (InteractibleObject interactible in GetTree().GetNodesInGroup("InteractibleObjects"))
		{
			interactible.CanSelect += OnCanSelectInteractible;
			interactible.ObjectDeselected += OnInteractibleDeselected;
		}

		foreach (SurfaceView surfaceView in GetTree().GetNodesInGroup("SurfaceViews"))
		{
			surfaceView.ViewClosed += Enable;
		}

		_viewport = GetNode<Node2D>("Viewport");
		_player = GetNode<Player>("Viewport/Player");
		_player.Interaction += OnPlayerInteraction;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	protected void Disable()
	{
		_player.Disable();
		_viewport.Modulate = new Color(0.5f, 0.5f, 0.5f);
	}

	protected void Enable()
	{
		_player.Enable();
		_viewport.Modulate = Colors.White;
	}
	
	public void OnCanSelectInteractible(InteractibleObject obj)
	{
		//Deselect current interactible first
		if (_selectedInteractible is not null)
		{
			_selectedInteractible.DeselectInteractible();
		}
		
		//Then set the new intereactible as selected
		_selectedInteractible = obj;
		_selectedInteractible.SelectInteractible();
	}

	public void OnInteractibleDeselected(InteractibleObject obj)
	{
		if (_selectedInteractible == obj)
		{
			_selectedInteractible = null;
		}
	}

	public void OnPlayerInteraction()
	{
		if (_selectedInteractible is not null)
		{
			_selectedInteractible.Interact();
		}
	}
}
