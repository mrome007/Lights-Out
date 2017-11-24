using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Yami player.
/// </summary>
public class YamiPlayer : MonoBehaviour 
{
    /// <summary>
    /// The lights out controller.
    /// </summary>
    [SerializeField]
    private LightsOutController lightsOutController;

    /// <summary>
    /// The wave message container.
    /// </summary>
    [SerializeField]
    private GameObject waveMessageContainer;

    /// <summary>
    /// The wave number text.
    /// </summary>
    [SerializeField]
    private Text waveNumberText;

    /// <summary>
    /// The wave label.
    /// </summary>
    [SerializeField]
    private Text waveLabel;

    /// <summary>
    /// The player collider.
    /// </summary>
    private Collider2D[] playerCollider;

    /// <summary>
    /// Mode.
    /// </summary>
    public enum Mode
    {
        PLAY,
        NOPLAY
    }

    /// <summary>
    /// Gets the yami player mode.
    /// </summary>
    /// <value>The yami player mode.</value>
    public Mode YamiPlayerMode { get; private set; } 

    /// <summary>
    /// Unity Awake method.
    /// </summary>
    private void Awake()
    {
        lightsOutController.LightsOut += YamiLightsOutHandler;
        YamiPlayerMode = Mode.NOPLAY;
        playerCollider = GetComponents<Collider2D>();
    }

    /// <summary>
    /// Unity OnDestroy method.
    /// </summary>
    private void OnDestroy()
    {
        lightsOutController.LightsOut -= YamiLightsOutHandler;
    }

    /// <summary>
    /// Yamis the lights out handler.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void YamiLightsOutHandler(object sender, EventArgs e)
    {
        YamiPlayerMode = Mode.PLAY;
    }

    /// <summary>
    /// Raises the trigger enter2 d event.
    /// </summary>
    /// <param name="other">Other.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        YamiPlayerMode = Mode.NOPLAY;
        foreach(var collider in playerCollider)
        {
            collider.enabled = false;
        }
        ShowEndMessage();
        StartCoroutine(DelayEnd());
    }

    /// <summary>
    /// Shows the end message.
    /// </summary>
    private void ShowEndMessage()
    {
        waveMessageContainer.SetActive(true);
        waveNumberText.text = EnemyWaveController.WaveNumber.ToString();
        waveLabel.text = EnemyWaveController.WaveNumber > 1 ? "WAVES" : "WAVE"; 
    }

    /// <summary>
    /// Delays the end.
    /// </summary>
    /// <returns>The end.</returns>
    private IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(7.5f);
        SceneManager.LoadScene(0);
    }
}
