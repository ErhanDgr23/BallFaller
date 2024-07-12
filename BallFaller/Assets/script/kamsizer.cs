using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamsizer : MonoBehaviour
{
    public Camera mainCamera;
    public Transform targetObject1;
    public Transform targetObject2;
    public float minSize = 5f;
    public float maxSize = 60f;
    public float sizeChangeSpeed = 2f;
    public float padding = 1.8f;

    void Update()
    {
        if (targetObject1 == null || targetObject2 == null || mainCamera == null) return;

        Vector3 screenPoint1 = mainCamera.WorldToViewportPoint(targetObject1.position);
        Vector3 screenPoint2 = mainCamera.WorldToViewportPoint(targetObject2.position);

        bool isObject1OutOfView = screenPoint1.x < 0 || screenPoint1.x > 1 || screenPoint1.y < 0 || screenPoint1.y > 1;
        bool isObject2OutOfView = screenPoint2.x < 0 || screenPoint2.x > 1 || screenPoint2.y < 0 || screenPoint2.y > 1;

        if (isObject1OutOfView || isObject2OutOfView)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, maxSize, Time.deltaTime * sizeChangeSpeed);
        }
        else
        {
            Bounds bounds = new Bounds(targetObject1.position, Vector3.zero);
            bounds.Encapsulate(targetObject2.position);
            bounds.Encapsulate(targetObject1.position + targetObject1.localScale);
            bounds.Encapsulate(targetObject2.position + targetObject2.localScale);

            float targetSize = Mathf.Max(bounds.size.x / (2 * mainCamera.aspect), bounds.size.y / 2) * padding;
            targetSize = Mathf.Clamp(targetSize, minSize, maxSize);

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * sizeChangeSpeed);
        }
    }
}
