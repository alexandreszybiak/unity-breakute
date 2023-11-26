using System;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class Ball : MonoBehaviour
{
    private const float ppu = 16.0f;

    private Vector2 velocity;
    private float xRemainder, yRemainder;

    private Sprite sprite;

    private Tilemap wallTilemap;

    [SerializeField] private Vector2 initialVelocity;
    [SerializeField] private TileBase wallTile;

    void Start()
    {
        wallTilemap = GameObject.Find("WallTilemap").GetComponent<Tilemap>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
        xRemainder = 0.0f;
        yRemainder = 0.0f;
        velocity = initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        MoveX(velocity.x);
        MoveY(velocity.y);
    }

    private bool OverlapTile(TileBase tile, Vector3 position)
    {
        if (wallTilemap == null) return false;

        var topleft = position - new Vector3(sprite.rect.width / 2, sprite.rect.height / 2, 0) / ppu;
        var bottomright = position + new Vector3(sprite.rect.width / 2, sprite.rect.height / 2, 0) / ppu;

        Vector3Int coord1 = wallTilemap.WorldToCell(topleft);
        Vector3Int coord2 = wallTilemap.WorldToCell(bottomright);

        BoundsInt area = new BoundsInt(coord1, coord2 - coord1 + Vector3Int.one);
        TileBase[] tiles = new TileBase[area.size.x * area.size.y];
        wallTilemap.GetTilesBlockNonAlloc(area, tiles);
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == tile) return true;
        }
        return false;
    }

    private void MoveX(float amount)
    {
        xRemainder += amount;
        int move = Mathf.RoundToInt(xRemainder);
        if (move != 0)
        {
            xRemainder -= move;
            int sign = Math.Sign(move);
            while (move != 0)
            {
                if (!OverlapTile(wallTile, transform.position + new Vector3(sign, 0, 0) / ppu))
                {
                    transform.Translate(new Vector3(sign, 0, 0) / ppu);
                    move -= sign;
                }
                else
                {
                    velocity.x *= -1.0f;
                    break;
                }

            }
        }

    }

    private void MoveY(float amount)
    {
        yRemainder += amount;
        int move = Mathf.RoundToInt(yRemainder);
        if (move != 0)
        {
            yRemainder -= move;
            int sign = Math.Sign(move);
            while (move != 0)
            {
                if (OverlapTile(wallTile, transform.position + new Vector3(0, sign, 0) / ppu))
                {
                    velocity.y *= -1.0f;
                    break;
                }
                else
                {
                    transform.Translate(new Vector3(0, sign, 0) / ppu);
                    move -= sign;
                }

            }
        }

    }

}
