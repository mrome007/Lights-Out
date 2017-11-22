using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutController : MonoBehaviour 
{
    public event EventHandler LightsOut;

    [SerializeField]
    private DarkBlock DarkObject;

    [SerializeField]
    private int LightPercentage = 3;

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
        numberOfColumns = 51;
        numberOfRows = 32;
        Debug.Log(numberOfColumns + " " + numberOfRows);
        Debug.Log(Screen.width.ToString() + "x" + Screen.height.ToString());
        Debug.Log(Camera.main.pixelWidth.ToString() + "x" + Camera.main.pixelHeight.ToString());
        darkGrid = new DarkBlock[numberOfRows, numberOfColumns];

        var worldXIncrementRate = DarkObject.DarkBlockSpriteRenderer.bounds.size.x;
        var worldYIncrementRate = DarkObject.DarkBlockSpriteRenderer.bounds.size.y;

        var startPosition = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 10f));
        var startPositionX = startPosition.x;

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
        yield return StartCoroutine(Darkness());

        var handler = LightsOut;
        if(handler != null)
        {
            handler(this, null);
        }

        InvokeRepeating("ContinueDarkness", 10f, 10f);
    }

    private void ContinueDarkness()
    {
        var decreaseLight = UnityEngine.Random.Range(0, 100);
        if(decreaseLight >= 60)
        {
            LightPercentage--;
            if(LightPercentage < 3)
            {
                LightPercentage = 3;
            }
        }
        StartCoroutine(Darkness());
    }

    private void ShowRow(int row)
    {
        for(int index = 0; index < numberOfColumns; index++)
        {
            var show = UnityEngine.Random.Range(0, 100);
            darkGrid[row, index].SetAlpha(show > LightPercentage ? 1f : 0f);
        }
    }

    private void ShowColumn(int column)
    {
        for(int index = 0; index < numberOfRows; index++)
        {
            var show = UnityEngine.Random.Range(0, 100);
            darkGrid[index, column].SetAlpha(show > LightPercentage ? 1f : 0f);
        }
    }

    private IEnumerator Darkness()
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
    }
}
