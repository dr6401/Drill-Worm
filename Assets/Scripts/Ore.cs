using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ore : Unit
{
    [SerializeField] private Transform crackFoldersRoot;
    [SerializeField] private Transform[] crackFolders;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        crackFolders = new Transform[crackFoldersRoot.childCount];
        for (int i = 0; i < crackFoldersRoot.childCount; i++)
        {
            crackFolders[i] = crackFoldersRoot.GetChild(i);
            crackFolders[i].gameObject.SetActive(false);
        }

        if (crackFolders.Length > 0)
        {
            crackFolders[0].gameObject.SetActive(true);
        }
    }

    protected override void TakeDamage(int damage)
    {
        hp -= damage;

        float healthRatio = (float)hp / maxHp;
        int totalFolders = crackFolders.Length;

        for (int i = 0; i < totalFolders; i++)
        {
            //Debug.Log($"healthRatio: {healthRatio}");
            float threshold = 1f - ((float)i / totalFolders);
            if (healthRatio <= threshold)
            {
                crackFolders[i].gameObject.SetActive(true);
                //Debug.Log($"HealthRatio was under threshold ({threshold}), enabling cracks at {i}");
            }
        }
        UpdateHealthBar();
        if (hp <= 0)
        {
            base.GetDestroyed();
        }
        else
        {
            Movement.Instance.GetKnockedBack(transform.position, knockbackForce);
        }
    }
}
