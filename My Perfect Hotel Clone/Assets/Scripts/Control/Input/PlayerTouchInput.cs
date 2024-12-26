using UnityEngine;

namespace MPH.Control
{
    public class PlayerTouchInput : MonoBehaviour
    {
        [field: SerializeField] public FloatingJoystick Joystick { get; private set; }
        
        public Vector2 GetDirection() => Joystick.Direction;
        
        public Vector3 GetMoveInput() => new(Joystick.Horizontal, 0, Joystick.Vertical);
    }
}