using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lights out controller.
/// </summary>
public class LightsOutController : MonoBehaviour 
{
    #region event

    /// <summary>
    /// Occurs when lights out.
    /// </summary>
    public event EventHandler LightsOut;

    #endregion

    #region Inspector elements

    /// <summary>
    /// The dark object.
    /// </summary>
    [SerializeField]
    private DarkBlock DarkObject;

    /// <summary>
    /// The light percentage.
    /// </summary>
    [SerializeField]
    private int LightPercentage = 3;

    #endregion

    #region Private fields

    /// <summary>
    /// The dark object parent.
    /// </summary>
    private GameObject darkObjectParent = null;

    /// <summary>
    /// The dark grid.
    /// </summary>
    private DarkBlock[,] darkGrid;

    /// <summary>
    /// The number of columns.
    /// </summary>
    private int numberOfColumns;

    /// <summary>
    /// The number of rows.
    /// </summary>
    private int numberOfRows;

    /// <summary>
    /// The time to lights out.
    /// </summary>
    private float timeToLightsOut = 0.15f;

    #endregion

    /// <summary>
    /// Begins the lights out.
    /// </summary>
    public void BeginLightsOut()
    {
        CoverScreen();
        StartCoroutine(StartDarkness());
    }

    /// <summary>
    /// Covers the screen.
    /// </summary>
    private void CoverScreen()
    {
        numberOfColumns = 51;
        numberOfRows = 32;

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

    /// <summary>
    /// Starts the darkness.
    /// </summary>
    /// <returns>The darkness.</returns>
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

    /// <summary>
    /// Continues the darkness.
    /// </summary>
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

    /// <summary>
    /// Shows the row.
    /// </summary>
    /// <param name="row">Row.</param>
    private void ShowRow(int row)
    {
        for(int index = 0; index < numberOfColumns; index++)
        {
            var show = UnityEngine.Random.Range(0, 100);
            darkGrid[row, index].SetAlpha(show > LightPercentage ? 1f : 0f);
        }
    }

    /// <summary>
    /// Shows the column.
    /// </summary>
    /// <param name="column">Column.</param>
    private void ShowColumn(int column)
    {
        for(int index = 0; index < numberOfRows; index++)
        {
            var show = UnityEngine.Random.Range(0, 100);
            darkGrid[index, column].SetAlpha(show > LightPercentage ? 1f : 0f);
        }
    }

    /// <summary>
    /// Darkness this instance.
    /// </summary>
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
