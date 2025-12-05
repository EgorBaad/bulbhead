using Godot;
using System;

public partial class Door : InteractibleObject
{
	[Export]
	public bool IsOpen = false;
	[Export]
	public bool NeedsKey = false;
	[Export]
	public string KeyId;
	[Export]
	public Texture2D OpenDoorTexture;
	[Export]
	public Texture2D ClosedDoorTexture;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		ObjectSprite.Texture = IsOpen ? OpenDoorTexture : ClosedDoorTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void Interact()
	{
		if (IsOpen)
		{
			return; //Pass for now
		}

		if (NeedsKey)
		{
			//check key
		}
		else
		{
			IsOpen = true;
			ObjectSprite.Texture = OpenDoorTexture;
		}
	}
}
