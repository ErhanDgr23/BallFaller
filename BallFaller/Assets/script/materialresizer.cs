using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialresizer : MonoBehaviour
{
    void Start()
    {
        // Örnek olarak, bu script bir objenin boyutunu aldığınızı varsayalım
        Vector3 objectSize = transform.localScale;

        // Materyal tiling değerlerini ayarlamak için
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && renderer.sharedMaterial != null)
        {
            // Materyalin tiling değerlerini objenin boyutuna göre ayarlayın
            renderer.sharedMaterial.mainTextureScale = new Vector2(objectSize.x, objectSize.y / 14f);
        }
    }
}
