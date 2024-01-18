using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PaddleController : MonoBehaviour
{
    private const float ppu = 16.0f;

    private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    private float speed;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
        speed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0.0f, 0.0f);
    }

    void OnMoveHorizontally(InputValue i)
    {
        speed = i.Get<float>() * 0.1f;

        
    }

    public Rect GetRect()
    {
        return new Rect(new Vector2(transform.position.x, transform.position.y), new Vector2(spriteRenderer.size.x, 1.0f ));
    }

    private void OnDrawGizmosSelected()
    {
        // Hit box
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(GetRect().x, GetRect().y, 0), Vector3.one * 10);
    }
}
