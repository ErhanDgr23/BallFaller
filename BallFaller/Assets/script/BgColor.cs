using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgColor : MonoBehaviour
{
    public Camera mainCamera; 
    public Color[] colors;   
    private int currentColorIndex = 0;  
    private float transitionTime = 4.0f;
    private float transitionProgress = 2.0f; 

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (colors.Length > 0)
        {
            currentColorIndex = Random.Range(0, colors.Length);
            mainCamera.backgroundColor = colors[currentColorIndex];
        }
    }

    void Update()
    {
        if (colors.Length > 1)
        {
            transitionProgress += Time.deltaTime / transitionTime;

            Color currentColor = colors[currentColorIndex];
            Color nextColor = colors[(currentColorIndex + 1) % colors.Length];

            mainCamera.backgroundColor = Color.Lerp(currentColor, nextColor, transitionProgress);

            if (transitionProgress >= 1.0f)
            {
                transitionProgress = 0.0f;
                currentColorIndex = (currentColorIndex + 1) % colors.Length;
            }
        }
    }
}
