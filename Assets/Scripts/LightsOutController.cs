using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutController : MonoBehaviour 
{
    [SerializeField]
    public SpriteRenderer DarkObject;

    private GameObject darkObjectParent = null;
    private SpriteRenderer[,] darkGrid;

    private int numberOfColumns;
    private int numberOfRows;

    private void Start()
    {
        CoverScreen();
        StartCoroutine(StartDarkness());
    }

    private void CoverScreen()
    {
        numberOfColumns = (int)((float)Screen.width / DarkObject.sprite.rect.width) - 3;
        numberOfRows = (int)((float)Screen.height / DarkObject.sprite.rect.height) - 1;

        darkGrid = new SpriteRenderer[numberOfRows, numberOfColumns];

        var worldXIncrementRate = DarkObject.bounds.size.x;
        var worldYIncrementRate = DarkObject.bounds.size.y;

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
                darkObj.gameObject.SetActive(false);
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
                ShowRow(fromBottom, true);
                ShowRow(fromTop, true);
                fromBottom++;
                fromTop--;
            }

            ShowColumn(fromLeft, true);
            ShowColumn(fromRight, true);
            fromLeft++;
            fromRight--;

            yield return new WaitForSeconds(1f);

            showRow = !showRow;
        }

        Debug.Log("Darkness");
    }

    private void ShowRow(int row, bool show)
    {
        for(int index = 0; index < numberOfColumns; index++)
        {
            darkGrid[row, index].gameObject.SetActive(show);
        }
    }

    private void ShowColumn(int column, bool show)
    {
        for(int index = 0; index < numberOfRows; index++)
        {
            darkGrid[index, column].gameObject.SetActive(show);
        }
    }
}
