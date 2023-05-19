using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pathway for enemy moving.
/// </summary>
[ExecuteInEditMode]
public class Pathway : MonoBehaviour
{
    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        EnermyPath[] waypoints = GetComponentsInChildren<EnermyPath>();
        if (waypoints.Length > 1)
        {
            int idx;
            for (idx = 1; idx < waypoints.Length; idx++)
            {
                // Draw blue lines along pathway in edit mode
                Debug.DrawLine(waypoints[idx - 1].transform.position, waypoints[idx].transform.position, Color.red);
            }
        }
    }

    /// <summary>
    /// Gets the nearest waypoint for specified position.
    /// </summary>
    /// <returns>The nearest waypoint.</returns>
    /// <param name="position">Position.</param>
    public EnermyPath GetNearestEnermyPath(Vector3 position)
    {
        float minDistance = float.MaxValue;
        EnermyPath nearestEnermyPath = null;
        foreach (EnermyPath waypoint in GetComponentsInChildren<EnermyPath>())
        {
            if (waypoint.GetHashCode() != GetHashCode())
            {
                // Calculate distance to waypoint
                Vector3 vect = position - waypoint.transform.position;
                float distance = vect.magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnermyPath = waypoint;
                }
            }
        }
        return nearestEnermyPath;
    }

    /// <summary>
    /// Gets the next waypoint on this pathway.
    /// </summary>
    /// <returns>The next waypoint.</returns>
    /// <param name="currentEnermyPath">Current waypoint.</param>
    /// <param name="loop">If set to <c>true</c> loop.</param>
    public EnermyPath GetNextEnermyPath(EnermyPath currentEnermyPath, bool loop)
    {
        EnermyPath res = null;
        int idx = currentEnermyPath.transform.GetSiblingIndex();
        if (idx < (transform.childCount - 1))
        {
            idx += 1;
        }
        else
        {
            idx = 0;
        }
        if (!(loop == false && idx == 0))
        {
            res = transform.GetChild(idx).GetComponent<EnermyPath>();
        }
        return res;
    }

    public float GetPathDistance(EnermyPath fromEnermyPath)
    {
        EnermyPath[] waypoints = GetComponentsInChildren<EnermyPath>();
        bool hitted = false;
        float pathDistance = 0f;
        int idx;
        for (idx = 0; idx < waypoints.Length; ++idx)
        {
            if (hitted == true)
            {
                Vector2 distance = waypoints[idx].transform.position - waypoints[idx - 1].transform.position;
                pathDistance += distance.magnitude;
            }
            if (waypoints[idx] == fromEnermyPath)
            {
                hitted = true;
            }
        }
        return pathDistance;
    }
}
