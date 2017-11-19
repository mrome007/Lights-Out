using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutController : MonoBehaviour 
{
    [SerializeField]
    public SpriteRenderer DarkObject;

    private void Start()
    {
        CoverScreen();
    }

    private void CoverScreen()
    {
        var numberOfColumns = (int)((float)Screen.width / DarkObject.sprite.rect.width);
        var numberOfRows = (int)((float)Screen.height / DarkObject.sprite.rect.height);

        var worldXIncrementRate = DarkObject.bounds.size.x;
        var worldYIncrementRate = DarkObject.bounds.size.y;

        var startPosition = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 10f));
        var startPositionX = startPosition.x;
        var startPositionY = startPosition.y;

        var increment = startPositionX;

        for(int yIndex = 0; yIndex < numberOfRows; yIndex++)
        {
            //make a row
            for(int xIndex = 0; xIndex < numberOfColumns; xIndex++)
            {
                Instantiate(DarkObject, startPosition, Quaternion.identity);
                startPosition = new Vector3(startPosition.x + worldXIncrementRate, startPosition.y, startPosition.z);
            }
            startPosition = new Vector3(startPositionX, startPosition.y + worldYIncrementRate, startPosition.z);
        }
    }
}
