using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dark block.
/// </summary>
public class DarkBlock : MonoBehaviour 
{
    /// <summary>
    /// The dark block sprite renderer.
    /// </summary>
    public SpriteRenderer DarkBlockSpriteRenderer;

    /// <summary>
    /// IsDark flag.
    /// </summary>
    public bool IsDark = false;

    /// <summary>
    /// Unity Awake method.
    /// </summary>
    private void Awake()
    {
        IsDark = false;
        DarkBlockSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    /// <summary>
    /// Sets the alpha.
    /// </summary>
    /// <param name="alpha">Alpha.</param>
    public void SetAlpha(float alpha)
    {
        DarkBlockSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);
        IsDark = DarkBlockSpriteRenderer.color.a > 0f;
    }
}
