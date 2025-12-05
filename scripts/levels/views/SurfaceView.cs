using Godot;
using System;

/// <summary>
/// View for close-up surface interactions
/// Holds one or more SurfaceObject subnodes
/// </summary>
public partial class SurfaceView : Node2D
{
	[Export]
	public Sprite2D SurfaceBackground;

	[Signal]
	public delegate void ViewClosedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("SurfaceViews");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Close()
	{
		Visible = false;
		EmitSignal(SignalName.ViewClosed);
	}
}
