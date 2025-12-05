using Godot;
using System;

/// <summary>
/// Generic level class for common logic
/// </summary>
public partial class Level : Node2D
{
	private InteractibleObject _selectedInteractible = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
}
