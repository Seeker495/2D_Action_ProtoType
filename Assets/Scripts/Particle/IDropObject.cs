using UnityEngine;

public interface IDropObject
{
    public void SetSize(in eDropSize size);

    public int Get();

    public Vector2 GetPosition();

    public Vector2 GetVelocity();
}
