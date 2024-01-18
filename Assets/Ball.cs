using System;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class Ball : MonoBehaviour
{
    private const float ppu = 16.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();

        if (tilemap == null) return;

        List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
        
        int count = collision.GetContacts(contactPoints);
        
        foreach (ContactPoint2D cp in contactPoints)
        {
            tilemap.SetTile(tilemap.WorldToCell(new Vector3(cp.point.x, cp.point.y, 0)), null);
        }
        
    }
}
