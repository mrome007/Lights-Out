using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBlock : MonoBehaviour 
{
    public SpriteRenderer DarkBlockSpriteRenderer;
    public bool IsDark = false;

    private void Awake()
    {
        IsDark = false;
        DarkBlockSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    public void SetAlpha(float alpha)
    {
        DarkBlockSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);
        IsDark = DarkBlockSpriteRenderer.color.a > 0f;
    }
}
