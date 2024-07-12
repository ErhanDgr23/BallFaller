using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rowcountainer : MonoBehaviour
{
    public List<GameObject> pyramidBalls = new List<GameObject>(); // Oluşturulan küplerin listesi
    public List<GameObject> spawnedObjects = new List<GameObject>();

    int rastgeleIndeks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject RandomBalls()
    {
        rastgeleIndeks = Random.Range(0, pyramidBalls.Count);
        GameObject secilenObje = pyramidBalls[rastgeleIndeks];

        return secilenObje;
    }

    public int getindexrandomballs()
    {
        return rastgeleIndeks;
    }
}
