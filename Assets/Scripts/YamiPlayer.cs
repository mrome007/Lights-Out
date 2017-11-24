using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YamiPlayer : MonoBehaviour 
{
    [SerializeField]
    private LightsOutController lightsOutController;

    [SerializeField]
    private GameObject waveMessageContainer;

    [SerializeField]
    private Text waveNumberText;

    [SerializeField]
    private Text waveLabel;

    private Collider2D[] playerCollider;
    
    public enum Mode
    {
        PLAY,
        NOPLAY
    }

    public Mode YamiPlayerMode { get; private set; } 

    private void Awake()
    {
        lightsOutController.LightsOut += YamiLightsOutHandler;
        YamiPlayerMode = Mode.NOPLAY;
        playerCollider = GetComponents<Collider2D>();
    }

    private void OnDestroy()
    {
        lightsOutController.LightsOut -= YamiLightsOutHandler;
    }

    private void YamiLightsOutHandler(object sender, EventArgs e)
    {
        YamiPlayerMode = Mode.PLAY;
    }

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

    private void ShowEndMessage()
    {
        waveMessageContainer.SetActive(true);
        waveNumberText.text = EnemyWaveController.WaveNumber.ToString();
        waveLabel.text = EnemyWaveController.WaveNumber > 1 ? "WAVES" : "WAVE"; 
    }

    private IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(7.5f);
        SceneManager.LoadScene(0);
    }
}
