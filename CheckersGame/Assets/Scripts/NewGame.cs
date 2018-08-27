using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public Sprite sprite1; // Drag your first sprite here
    public Sprite sprite2; // Drag your second sprite here

    private SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null)// if the sprite on spriteRenderer is null then
        {
            spriteRenderer.sprite = sprite1; // set the sprite to sprite1
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If clicked
        {
            spriteRenderer.sprite = sprite2; // call method to change sprite
        }
        if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.sprite = sprite1; // call method to change sprite
        }
    }
}
