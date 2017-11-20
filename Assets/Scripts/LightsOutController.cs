using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutController : MonoBehaviour 
{
    public DarkBlock DarkObject;

    private GameObject darkObjectParent = null;
    private DarkBlock[,] darkGrid;

    private int numberOfColumns;
    private int numberOfRows;

    private float timeToLightsOut = 0.15f;

    private void Start()
    {
        CoverScreen();
        StartCoroutine(StartDarkness());
    }

    private void CoverScreen()
    {
        numberOfColumns = (int)((float)Screen.width / DarkObject.DarkBlockSpriteRenderer.sprite.rect.width) - 3;
        numberOfRows = (int)((float)Screen.height / DarkObject.DarkBlockSpriteRenderer.sprite.rect.height) - 1;

        darkGrid = new DarkBlock[numberOfRows, numberOfColumns];

        var worldXIncrementRate = DarkObject.DarkBlockSpriteRenderer.bounds.size.x;
        var worldYIncrementRate = DarkObject.DarkBlockSpriteRenderer.bounds.size.y;

        var startPosition = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 10f));
        var startPositionX = startPosition.x;
        var startPositionY = startPosition.y;

        darkObjectParent = new GameObject("DarkObjectsParent");
        darkObjectParent.transform.position = Vector3.zero;

        for(int yIndex = 0; yIndex < numberOfRows; yIndex++)
        {
            //make a row
            for(int xIndex = 0; xIndex < numberOfColumns; xIndex++)
            {
                var darkObj = Instantiate(DarkObject, startPosition, Quaternion.identity);
                darkObj.name = darkObj.name + yIndex.ToString() + "_" + xIndex.ToString();
                darkObj.gameObject.transform.parent = darkObjectParent.transform;
                darkGrid[yIndex, xIndex] = darkObj;

                startPosition = new Vector3(startPosition.x + worldXIncrementRate, startPosition.y, startPosition.z);
            }
            startPosition = new Vector3(startPositionX, startPosition.y + worldYIncrementRate, startPosition.z);
        }
    }

    private IEnumerator StartDarkness()
    {
        var fromBottom = 0;
        var fromTop = numberOfRows - 1;
        var fromLeft = 0;
        var fromRight = numberOfColumns - 1;

        var showRow = true;

        while(fromBottom <= fromTop && fromLeft <= fromRight)
        {
            if(showRow)
            {
                ShowRow(fromBottom);
                ShowRow(fromTop);
                fromBottom++;
                fromTop--;
            }

            ShowColumn(fromLeft);
            ShowColumn(fromRight);
            fromLeft++;
            fromRight--;

            yield return new WaitForSeconds(timeToLightsOut);

            showRow = !showRow;
        }

        Debug.Log("Darkness");
    }

    private void ShowRow(int row)
    {
        for(int index = 0; index < numberOfColumns; index++)
        {
            var show = Random.Range(0, 9);
            darkGrid[row, index].SetAlpha(show > 0 ? 1f : 0f);
        }
    }

    private void ShowColumn(int column)
    {
        for(int index = 0; index < numberOfRows; index++)
        {
            var show = Random.Range(0, 9);
            darkGrid[index, column].SetAlpha(show > 0 ? 1f : 0f);
        }
    }
}
