using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminateNearBy : MonoBehaviour 
{
    [SerializeField]
    private bool stationary = false;

    [SerializeField]
    private float alpha = 1f;
    
    private DarkBlock previousDarkBlock = null;
    private bool TurnedDark = false;

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
