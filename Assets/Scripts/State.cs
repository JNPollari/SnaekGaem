using UnityEngine;

public class State 
{
    private Vector2 position;
    private Quaternion rotation;

    public State(Vector2 _position, Quaternion _rotation) {
        position = _position;
        rotation = _rotation;
    }

    public Vector2 GetPosition() {
        return position;
    }

    public Quaternion GetRotation() {
        return rotation;
    }
}
