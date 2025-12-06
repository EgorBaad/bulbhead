namespace Bulbhead.Objects.Extensions;
using Godot;

public static class DraggableExtensions
{
    public static void DefaultPerformDrag(this Node node, InputEventMouseMotion motionEvent)
    {
        Tween tween = node.GetTree().CreateTween();
        tween.TweenProperty(node, "global_position", motionEvent.GlobalPosition, 0.1f);
    }
}