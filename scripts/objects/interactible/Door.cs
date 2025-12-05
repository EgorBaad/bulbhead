using Godot;
using System;

public partial class Door : InteractibleObject
{
	/// <summary>
    /// Indicates if the door is open
    /// </summary>
	[Export]
	public bool IsOpen = false;
	/// <summary>
    /// Indicates if the door needs a key to open
    /// </summary>
	[Export]
	public bool NeedsKey = false;
	/// <summary>
    /// ID of the key needed to open the door
    /// </summary>
	[Export]
	public string KeyId;
	/// <summary>
    /// Texture to use when the door is open
    /// </summary>
	[Export]
	public Texture2D OpenDoorTexture;
	/// <summary>
    /// Texture to use when the door is closed
    /// </summary>
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
