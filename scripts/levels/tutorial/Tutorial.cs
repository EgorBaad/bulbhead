using Godot;
using System;

public partial class Tutorial : Level
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnTableInteracted()
	{
		GetNode<SurfaceView>("TableSurface").Visible = true;
		Disable();
	}
}
