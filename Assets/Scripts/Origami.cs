using System;

public class Origami : DroppingObject
{
    public static event Action onFallOnGround;
    public int score = 5;

    protected override void FallOnGround()
    {
        onFallOnGround?.Invoke();
        base.FallOnGround();
    }
}
