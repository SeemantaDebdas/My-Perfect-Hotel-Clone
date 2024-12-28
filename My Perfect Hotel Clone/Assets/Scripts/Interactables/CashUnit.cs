using System;
using UnityEngine;
using MPH.Data; // Ensure the namespace matches your `Purse` class

public class CashUnit : MonoBehaviour
{
    private Vector3 startPoint;
    private Vector3 endPoint;
    private float travelDuration;
    private float elapsedTime;
    private float height;

    private bool isMoving = false;
    public int CashValue { get; private set; } = 10; 

    /// <summary>
    /// Initializes the cash unit with a destination and travel parameters.
    /// </summary>
    /// <param name="destination">The target position to move to.</param>
    /// <param name="duration">The time it will take to reach the destination.</param>
    /// <param name="arcHeight">The height of the parabolic arc.</param>
    /// <param name="purse">The purse to credit on arrival, optional.</param>
    public void Initialize(Vector3 destination, float duration, float arcHeight)
    {
        startPoint = transform.position;
        endPoint = destination;
        travelDuration = duration;
        height = arcHeight;
        elapsedTime = 0f;
        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving) return;

        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate normalized progress
        float t = Mathf.Clamp01(elapsedTime / travelDuration);

        // Interpolate position based on parabolic trajectory
        Vector3 currentPosition = CalculateParabolicPosition(startPoint, endPoint, t, height);
        transform.position = currentPosition;

        // End movement when finished
        if (t >= 1f)
        {
            //OnReachDestination();
        }
    }

    /// <summary>
    /// Calculates a parabolic position given start, end, and progress.
    /// </summary>
    private Vector3 CalculateParabolicPosition(Vector3 start, Vector3 end, float t, float arcHeight)
    {
        // Linear interpolation between start and end
        Vector3 linearPosition = Vector3.Lerp(start, end, t);

        // Add parabolic offset
        float arc = arcHeight * (1f - Mathf.Pow(2f * t - 1f, 2f)); // Parabolic equation
        return new Vector3(linearPosition.x, linearPosition.y + arc, linearPosition.z);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICashCollector cashCollector))
        {
            print("Reached");
            cashCollector.CollectCash(CashValue);
            Destroy(gameObject);
        }
    }
}
