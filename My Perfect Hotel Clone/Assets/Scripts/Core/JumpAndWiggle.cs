using UnityEngine;
using DG.Tweening;

public class JumpAndWiggle : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpHeight = 2f;       // Height of the jump
    public float jumpDuration = 1f;    // Duration of the jump
    public int jumpCount = 2;          // Number of jumps
    
    [Header("Wiggle Settings")]
    public float wiggleStrength = 15f; // Strength of the wiggle (degrees)
    public int wiggleVibrato = 10;     // How much the object vibrates
    public float wiggleDuration = 0.5f; // Duration of the wiggle
    
    [Header("Timing")]
    public float repeatDelay = 2f;     // Time before repeating the animation

    private Vector3 originalPosition;  // Original position of the GameObject

    private void Start()
    {
        originalPosition = transform.position;
        StartJumpAndWiggle();
    }

    private void StartJumpAndWiggle()
    {
        Sequence sequence = DOTween.Sequence();

        // Jump animation
        sequence.Append(transform.DOJump(originalPosition, jumpHeight, jumpCount, jumpDuration)
            .SetEase(Ease.OutQuad));

        // Wiggle animation (rotate slightly during jump)
        sequence.Join(transform.DORotate(new Vector3(0, 0, wiggleStrength), wiggleDuration, RotateMode.LocalAxisAdd)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine));

        // Return to original position
        sequence.Append(transform.DOMove(originalPosition, 0.2f));

        // Repeat after delay
        sequence.AppendInterval(repeatDelay);
        sequence.SetLoops(-1); // Infinite looping
    }
}