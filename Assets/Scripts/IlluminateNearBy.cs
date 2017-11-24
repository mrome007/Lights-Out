using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Illuminate near by.
/// </summary>
public class IlluminateNearBy : MonoBehaviour 
{
    #region Inspector elements

    /// <summary>
    /// flag whether object is stationary.
    /// </summary>
    [SerializeField]
    private bool stationary = false;

    /// <summary>
    /// The alpha.
    /// </summary>
    [SerializeField]
    private float alpha = 1f;

    #endregion

    /// <summary>
    /// The previous dark block.
    /// </summary>
    private DarkBlock previousDarkBlock = null;

    /// <summary>
    /// TurnedDark flag.
    /// </summary>
    private bool TurnedDark = false;

    /// <summary>
    /// Unity FixedUpdate method.
    /// </summary>
    private void FixedUpdate()
    {
        RaycastHit hit;
        var fwd = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, fwd, out hit, 20f))
        {
            var darkBlock = hit.collider.gameObject.GetComponent<DarkBlock>();
            if(!TurnedDark && darkBlock.IsDark)
            {
                TurnedDark = darkBlock.IsDark;
            }

            if(previousDarkBlock == null || previousDarkBlock.name != hit.collider.name || stationary)
            {
                TurnedDark = darkBlock.IsDark;

                if(TurnedDark)
                {
                    if(previousDarkBlock != null)
                    {
                        previousDarkBlock.SetAlpha(1f);
                    }
                    previousDarkBlock = darkBlock;
                    previousDarkBlock.SetAlpha(Random.Range(0f, alpha));
                }
            }
        }
    }
}
