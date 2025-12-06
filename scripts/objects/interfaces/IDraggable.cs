namespace Bulbhead.Objects.Extensions;
using Godot;
using System;

public interface IDraggable
{
    bool _isDragging { get; set; }
    void PerformDrag(InputEventMouseMotion motionEvent);
}