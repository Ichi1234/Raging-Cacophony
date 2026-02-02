using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Vfx : Entity_Vfx
{
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject bigSmokePrefab;
    [SerializeField] private GameObject bossPoop;
    [SerializeField] private Transform assLocation;

    public List<GameObject> poopLists;

    protected override void Awake()
    {
        base.Awake();

        poopLists = new List<GameObject>();

        CreateBossPoop();
    }

    private void CreateBossPoop()
    {
        for (int i = 0; i <= 10; i++)
        {
            GameObject poop = Instantiate(bossPoop);

            Boss_Poop poopScript = poop.GetComponent<Boss_Poop>();

            poopScript.Initialize(this, GetComponent<Boss_Combat>());

            poop.SetActive(false);

            poopLists.Add(poop);
        }
    }

    public GameObject CreateSmokeVfx()
    {
        Vector3 smokeOffset = new Vector3(0.5f * GetComponent<Boss>().facingDir, 0, 0);
        return Instantiate(smokePrefab, transform.position + smokeOffset, smokePrefab.transform.rotation, transform);
    }

    public GameObject CreateBigSmoke()
    {
        return Instantiate(bigSmokePrefab, transform);
    }

    public void ShootBossPoop()
    {
        int lastIndex = poopLists.Count - 1;

        try
        {
            GameObject curPoop = poopLists[lastIndex];

            curPoop.transform.SetPositionAndRotation(assLocation.position, Quaternion.Euler(0, 0, 90));

            curPoop.SetActive(true);

            poopLists.RemoveAt(lastIndex);
        }

        catch (ArgumentOutOfRangeException)
        {
            Debug.Log("Boss out of poop");
        }

    }

    public void ReturnPoop(GameObject newPoop) => poopLists.Add(newPoop);
}
