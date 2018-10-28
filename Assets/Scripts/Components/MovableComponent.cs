using UnityEngine;

public class MovableComponent : MonoBehaviour {
    public float     Speed = 5.0f;
    public Transform Transform;

    private void Awake() {
        Transform = transform;
    }
}