﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class pyramidgenerator : MonoBehaviour {

    public int pyramidHeight = 5;

    [SerializeField] Material kupmat;
    [SerializeField] float offsetX = 0.1f;
    [SerializeField] float offsetY = 0.1f;
    [SerializeField] float spacingFactor = 0.5f;
    [SerializeField] float spacingFactor2 = 0.2f;
    [SerializeField] float testerecount, coincount;
    [SerializeField] Transform sawcoinparent, colliderparent, kapparent, kap, player, parentTransform;
    [SerializeField] GameObject kappre, coinpre, sawpre, cubePrefab, spike;
    [SerializeField] List<GameObject> pyramidCubes = new List<GameObject>();
    [SerializeField] List<Transform> rowParents = new List<Transform>();
    [SerializeField] List<GameObject> coins = new List<GameObject>();
    [SerializeField] List<GameObject> saws = new List<GameObject>();
    [SerializeField] List<GameObject> kaplar = new List<GameObject>();

    kamsizer kamsizersc;
    gamemanager manager;

    void Start()
    {
        manager = gamemanager.mangersc;
        kamsizersc = Camera.main.GetComponent<kamsizer>();


        testerecount = pyramidHeight - Random.Range(4, 8);
        coincount = pyramidHeight - Random.Range(4, 8);

        BuildPyramid();
    }

    void BuildPyramid()
    {
        ClearPyramid();

        #region //pyramid olusturucu
        float cubeSizeX = cubePrefab.transform.localScale.x + offsetX;
        float cubeSizeY = cubePrefab.transform.localScale.y + offsetY;
        float startX;
        float startY = 0;

        for (int i = pyramidHeight - 1; i >= 0; i--)
        {
            int cubesInRow = i + 3; // En üstte 3 küp ile başlıyor, her satırda 1 artırıyor
            float rowWidth = cubesInRow * cubeSizeX;

            startX = -rowWidth / 2 + cubeSizeX / 2;
            startY = (pyramidHeight - 1 - i) * cubeSizeY;

            // Yeni satır için GameObject oluştur
            GameObject rowParent = new GameObject($"Row {pyramidHeight - i}");
            rowParent.transform.parent = parentTransform;
            rowParents.Add(rowParent.transform);

            // rowcountainer scriptini ekle
            rowParent.AddComponent<rowcountainer>();

            for (int j = 0; j < cubesInRow; j++)
            {
                Vector3 position = new Vector3(startX + j * cubeSizeX, startY, 0);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.transform.parent = rowParent.transform; // Küpü ilgili satırın parent'ına ata
                pyramidCubes.Add(cube);

                // rowcountainer scriptindeki listeye ekle
                rowParent.GetComponent<rowcountainer>().pyramidBalls.Add(cube);
            }
        }

        parentTransform.transform.position = new Vector3(0f, -pyramidHeight * 1.25f, 0f);
        kap.transform.position = new Vector3(0f, (-pyramidHeight * 1.25f) - 2f, 0f);

        kap.gameObject.SetActive(false);
        float pyramidHeightvalue = pyramidHeight;
        kaplar.Clear();

        List<GameObject> ballliste = rowParents[0].GetComponent<rowcountainer>().pyramidBalls;

        for (int i = 1; i < ballliste.Count; i++)
        {
            GameObject kapclone1 = Instantiate(kappre, kapparent);
            kapclone1.transform.position = new Vector3(ballliste[i].transform.position.x - 1f, (-pyramidHeight * 1.25f), 0f);
            kaplar.Add(kapclone1);
        }

        if (kaplar.Count % 2 == 0)
        {
            int kackere = kaplar.Count - (kaplar.Count / 2);

            for (int i = 0; i < (kackere); i++)
            {
                kaplar[kackere + i].GetComponent<kapsc>().value = i;
            }

            for (int i = 0; i < (kackere); i++)
            {
                kaplar[(kackere - 1) - i].GetComponent<kapsc>().value = i;
            }
        }
        if (kaplar.Count % 2 != 0)
        {
            float kackere = kaplar.Count - Mathf.Round((kaplar.Count + 1) / 2);
            print(kackere);

            for (int i = 0; i < kackere + 1; i++)
            {
                kaplar[(int)(kackere + i)].GetComponent<kapsc>().value = i;
            }

            for (int i = 1; i < kackere + 1; i++)
            {
                kaplar[(int)(kackere - i)].GetComponent<kapsc>().value = i;
            }
        }

        /*
        if (pyramidHeightvalue % 2 == 0)
        {
            kap.gameObject.SetActive(false);
            kapclone1 = Instantiate(kappre, kapparent);
            kapclone2 = Instantiate(kappre, kapparent);

            kapclone1.GetComponent<kapsc>().value = 0;
            kapclone2.GetComponent<kapsc>().value = 0;
            kapclone1.GetComponent<kapsc>().maxvalue = (int)(pyramidHeightvalue - 2) / 2;
            kapclone2.GetComponent<kapsc>().maxvalue = (int)(pyramidHeightvalue - 2) / 2;

            kapclone1.transform.position = new Vector3(-1.15f, (-pyramidHeight * 1.15f) - 2f, 0f);
            kapclone2.transform.position = new Vector3(1.15f, (-pyramidHeight * 1.15f) - 2f, 0f);

            pyramidHeightvalue = pyramidHeight - 2;
        }
        else
        {
            kap.gameObject.SetActive(false);
            kapclone1 = Instantiate(kappre, kapparent);
            kapclone2 = Instantiate(kappre, kapparent);

            kapclone1.GetComponent<kapsc>().value = 0;
            kapclone2.GetComponent<kapsc>().value = 0;
            kapclone1.GetComponent<kapsc>().maxvalue = (int)(pyramidHeightvalue - 1) / 2;
            kapclone2.GetComponent<kapsc>().maxvalue = (int)(pyramidHeightvalue - 1) / 2;

            kapclone1.transform.position = new Vector3(-1.15f, (-pyramidHeight * 1.15f) - 2f, 0f);
            kapclone2.transform.position = new Vector3(1.15f, (-pyramidHeight * 1.15f) - 2f, 0f);

            pyramidHeightvalue = pyramidHeight - 1;
        }

        for (int i = 1; i < pyramidHeightvalue / 2; i++)
        {
            if(kapclone2 != null)
            {
                GameObject kapclone = Instantiate(kappre, kapparent);
                kapclone.transform.position = new Vector3(kapclone2.transform.position.x + (2.3f * i), (-pyramidHeight * 1.15f) - 2f, 0f);
                kapclone.GetComponent<kapsc>().value = i;
                kapclone1.GetComponent<kapsc>().maxvalue = (int)(pyramidHeightvalue) / 2;
            }
            else
            {
                GameObject kapclone = Instantiate(kappre, kapparent);
                kapclone.transform.position = new Vector3(kap.position.x + (2.3f * i), (-pyramidHeight * 1.15f) - 2f, 0f);
                kapclone.GetComponent<kapsc>().value = i;
                kapclone1.GetComponent<kapsc>().maxvalue = (int)(pyramidHeightvalue) / 2;
            }
        }

        for (int i = 1; i < pyramidHeightvalue / 2; i++)
        {
            if (kapclone1 != null)
            {
                GameObject kapclone = Instantiate(kappre, kapparent);
                kapclone.transform.position = new Vector3(kapclone1.transform.position.x - (2.3f * i), (-pyramidHeight * 1.15f) - 2f, 0f);
                kapclone.GetComponent<kapsc>().value = i;
                kapclone1.GetComponent<kapsc>().maxvalue = (int)(pyramidHeightvalue) / 2;
            }
            else
            {
                GameObject kapclone = Instantiate(kappre, kapparent);
                kapclone.transform.position = new Vector3(kap.position.x - (2.3f * i), (-pyramidHeight * 1.15f) - 2f, 0f);
                kapclone.GetComponent<kapsc>().value = i;
                kapclone1.GetComponent<kapsc>().maxvalue = (int)(pyramidHeightvalue) / 2;
            }
        }
        */
        #endregion

        //colliderolustur sol
        Vector3 endPoint = rowParents[0].GetComponent<rowcountainer>().pyramidBalls[0].transform.position + new Vector3(-2f, 0f, 0f);
        Vector3 startPoint = rowParents[rowParents.Count - 1].GetComponent<rowcountainer>().pyramidBalls[0].transform.position + new Vector3(-2f, 0f, 0f);
        Vector3 middlePoint = (startPoint + endPoint) / 2;
        Vector3 direction = (endPoint - startPoint).normalized;

        GameObject kup = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        Destroy(kup.transform.GetComponent<CapsuleCollider>());
        kup.AddComponent<BoxCollider>();
        kup.transform.position = middlePoint;
        kup.transform.rotation = Quaternion.LookRotation(direction);
        float distance = Vector3.Distance(startPoint, endPoint);
        kup.transform.localScale = new Vector3(kup.transform.localScale.x + 4f, kup.transform.localScale.y - 0.5f, distance + 2f);
        kup.transform.SetParent(colliderparent);
        kup.GetComponent<MeshRenderer>().material = kupmat;
        kup.tag = "death";
        kup.gameObject.SetActive(true);

        //colliderolustur sağ
        Vector3 endPoint2 = rowParents[0].GetComponent<rowcountainer>().pyramidBalls
            [rowParents[0].GetComponent<rowcountainer>().pyramidBalls.Count - 1].transform.position + new Vector3(2f, 0f, 0f);
        Vector3 startPoint2 = rowParents[rowParents.Count - 1].GetComponent<rowcountainer>().pyramidBalls
            [rowParents[rowParents.Count - 1].GetComponent<rowcountainer>().pyramidBalls.Count - 1].transform.position + new Vector3(2f, 0f, 0f);

        Vector3 middlePoint2 = (startPoint2 + endPoint2) / 2;
        Vector3 direction2 = (endPoint2 - startPoint2).normalized;

        GameObject kup2 = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        Destroy(kup2.transform.GetComponent<CapsuleCollider>());
        kup2.AddComponent<BoxCollider>();
        kup2.transform.position = middlePoint2;
        kup2.transform.rotation = Quaternion.LookRotation(direction2);
        float distance2 = Vector3.Distance(startPoint2, endPoint2);
        kup2.transform.localScale = new Vector3(kup2.transform.localScale.x + 4f, kup2.transform.localScale.y - 0.5f, distance2 + 2f);
        kup2.transform.SetParent(colliderparent);
        kup2.GetComponent<MeshRenderer>().material = kupmat;
        kup2.tag = "death";
        kup2.gameObject.SetActive(true);

        kamsizersc.targetObject1 = kup.transform;
        kamsizersc.targetObject2 = kup2.transform;

        //DistributeObjects(kup);
        //DistributeObjects(kup2);
        testerecoinspawn();
    }

    void DistributeObjects(GameObject item)
    {
        float objectCount = item.transform.localScale.z * spacingFactor2;
        float spikeWidth = spike.transform.localScale.x;

        Vector3 startPosition = item.transform.position - item.transform.forward * item.transform.localScale.z / 2.15f;
        Vector3 endPosition = item.transform.position + item.transform.forward * item.transform.localScale.z / 2.15f;

        for (float i = 0; i < objectCount; i++)
        {
            float t = i / (objectCount - 1);
            Vector3 position = Vector3.Lerp(startPosition, endPosition, t);
            position += item.transform.forward * spikeWidth * t * spacingFactor2;
            GameObject newObj = Instantiate(spike, position, Quaternion.identity);
            Vector3 lookAtPosition = item.transform.position - item.transform.forward * t * item.transform.localScale.z;
            newObj.transform.LookAt(lookAtPosition, -Vector3.up);
            newObj.transform.Rotate(Vector3.up, 90f, Space.Self);
            newObj.transform.parent = item.transform;
        }
    }

    public void testerecoinspawn()
    {
        if(coins.Count > 0)
        {
            foreach (var item in coins)
                Destroy(item);
        }

        if (saws.Count > 0)
        {
            foreach (var item in saws)
                Destroy(item);
        }

        saws.Clear();
        coins.Clear();

        List<Transform> rowobjeler = rowParents;

        //testere olusturucu
        for (int i = 0; i < testerecount; i++)
        {
            #region // çift objeli spawn işlemi
            //int randomIndex = Random.Range(0, rowParents.Count);
            //Transform randomRowParent = rowParents[randomIndex];

            //List<rowcountainer> objeler = new List<rowcountainer>();
            //objeler.Add(rowParents[randomIndex].GetComponent<rowcountainer>());

            //if (randomIndex < rowParents.Count - 1 && rowParents[randomIndex + 1] != null)
            //    objeler.Add(rowParents[randomIndex + 1].GetComponent<rowcountainer>());

            //if (rowParents[randomIndex - 1] != null)
            //    objeler.Add(rowParents[randomIndex - 1].GetComponent<rowcountainer>());

            //int randomrowindex = Random.Range(0, objeler.Count);
            //rowcountainer randomrow = objeler[randomrowindex];

            //GameObject obje1 = null;
            //GameObject obje2 = null;
            //int obje1index = 0;

            //obje1 = randomrow.RandomBalls();
            //obje1index = randomrow.getindexrandomballs();

            //int randomrowindex2 = Random.Range(0, objeler.Count);
            //rowcountainer randomrow2 = objeler[randomrowindex2];

            //if (obje1index < randomrow2.pyramidBalls.Count - 1 && randomrow2.pyramidBalls[obje1index] != null)
            //    obje2 = randomrow2.pyramidBalls[obje1index];

            //else if (obje1index + 1 < randomrow2.pyramidBalls.Count - 1 && randomrow2.pyramidBalls[obje1index + 1] != null)
            //    obje2 = randomrow2.pyramidBalls[obje1index + 1];

            //else if (randomrow2.pyramidBalls[obje1index - 1] != null)
            //    obje2 = randomrow2.pyramidBalls[obje1index - 1];

            //GameObject sawclone = Instantiate(sawpre, sawcoinparent);
            //sawclone.transform.position = obje1.transform.position - obje2.transform.position;
            #endregion

            int randomIndex = Random.Range(1, rowobjeler.Count - 1);
            rowcountainer randomRowParent = rowobjeler[randomIndex].GetComponent<rowcountainer>();

            List<GameObject> pyramidballclone = randomRowParent.pyramidBalls;

            int randomRowObject;
            if (randomRowParent.pyramidBalls.Count > 3)
                randomRowObject = Random.Range(1, pyramidballclone.Count - 2);
            else
                randomRowObject = 1;
            GameObject randomRowObjectBall = pyramidballclone[randomRowObject];

            GameObject ballclone = Instantiate(sawpre, sawcoinparent);
            saws.Add(ballclone);
            float[] playerxrandom = new float[] { 0.8f, 0f, -0.8f };
            float[] playeryrandom = new float[] { 1.25f, -1.25f };

            float xpos = playerxrandom[Random.Range(0, playerxrandom.Length)];
            float ypos = playeryrandom[Random.Range(0, playeryrandom.Length)];
            ballclone.transform.position = randomRowObjectBall.transform.position + new Vector3(xpos, ypos, 0f);

            float[] plxrdm2 = new float[] { 1.25f, -1.25f };
            float xp2 = plxrdm2[Random.Range(0, plxrdm2.Length)];
            GameObject ballclonecoin = Instantiate(coinpre, ballclone.transform.position + new Vector3(xp2, 0f, 0f), Quaternion.identity);
            ballclonecoin.transform.SetParent(ballclone.transform);
            coins.Add(ballclonecoin);
        }

        /*
        //coin olusturucu
        for (int i = 0; i < coincount; i++)
        {
            int randomIndex = Random.Range(1, rowobjeler.Count - 1);
            rowcountainer randomRowParent = rowobjeler[randomIndex].GetComponent<rowcountainer>();

            List<GameObject> pyramidballclone = randomRowParent.pyramidBalls;

            int randomRowObject;
            if (randomRowParent.pyramidBalls.Count > 3)
                randomRowObject = Random.Range(1, randomRowParent.pyramidBalls.Count - 2);
            else
                randomRowObject = 1;

            GameObject randomRowObjectBall = randomRowParent.pyramidBalls[randomRowObject];

            GameObject ballclone = Instantiate(coinpre, sawcoinparent);
            coins.Add(ballclone);
            float[] playerxrandom = new float[] { 0.8f, 0f, -0.8f };
            float[] playeryrandom = new float[] { 1.25f, -1.25f };

            float xpos = playerxrandom[Random.Range(0, playerxrandom.Length)];
            float ypos = playeryrandom[Random.Range(0, playeryrandom.Length)];
            ballclone.transform.position = randomRowObjectBall.transform.position + new Vector3(xpos, ypos, 0f);
        }
        */ // coin olusturucu
    }

    public void testereteklispawn()
    {
        int randomIndex = Random.Range(1, rowParents.Count - 1);
        rowcountainer randomRowParent = rowParents[randomIndex].GetComponent<rowcountainer>();

        List<GameObject> pyramidballclone = randomRowParent.pyramidBalls;

        int randomRowObject;
        if (randomRowParent.pyramidBalls.Count > 3)
            randomRowObject = Random.Range(1, pyramidballclone.Count - 2);
        else
            randomRowObject = 1;
        GameObject randomRowObjectBall = pyramidballclone[randomRowObject];

        GameObject ballclone = Instantiate(sawpre, sawcoinparent);
        GameObject ballclonecoin = Instantiate(coinpre, ballclone.transform);
        saws.Add(ballclone);
        coins.Add(ballclonecoin);
        float[] playerxrandom = new float[] { 0.8f, 0f, -0.8f };
        float[] playerxrandom2 = new float[] { 1.25f, -1.25f };
        float[] playeryrandom = new float[] { 1.25f, -1.25f };

        float xpos = playerxrandom[Random.Range(0, playerxrandom.Length)];
        float xpos2 = playerxrandom2[Random.Range(0, playerxrandom2.Length)];
        float ypos = playeryrandom[Random.Range(0, playeryrandom.Length)];
        ballclone.transform.position = randomRowObjectBall.transform.position + new Vector3(xpos, ypos, 0f);
        ballclonecoin.transform.position = ballclone.transform.position + new Vector3(xpos2, 0f, 0f);
    }

    public void ClearPyramid()
    {
        foreach (Transform cube in rowParents)
        {
            Destroy(cube.gameObject);
        }
        if (coins.Count > 0)
        {
            foreach (var item in coins)
                Destroy(item);
        }

        if (saws.Count > 0)
        {
            foreach (var item in saws)
                Destroy(item);
        }
        for (int i = 0; i < kapparent.childCount; i++)
        {
            if (kapparent.GetChild(i).transform.name != "kap")
                Destroy(kapparent.GetChild(i).gameObject);
        }

        if(colliderparent.childCount > 0)
        {
            Destroy(colliderparent.transform.GetChild(0).gameObject);
            Destroy(colliderparent.transform.GetChild(1).gameObject);
        }

        parentTransform.position = new Vector3(0f, -4.5f, 0f);

        saws.Clear();
        coins.Clear();
        pyramidCubes.Clear();
        rowParents.Clear();
    }

    // Yeni bir piramit oluşturmak için bu fonksiyonu çağırabilirsiniz
    public void RebuildPyramid(int value)
    {
        pyramidHeight += value;
        pyramidHeight = Mathf.Clamp(pyramidHeight, 10, 24);
        BuildPyramid();
    }
}
