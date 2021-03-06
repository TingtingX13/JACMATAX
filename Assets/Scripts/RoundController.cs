﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{

    [SerializeField] protected GameObject RightPlayerPrefab; // player that spawns on right, score, health, etc. also on right
    [SerializeField] protected GameObject LeftPlayerPrefab; // player that spawns on left, score, health, etc. also on left
    protected GameObject RightPlayer;
    protected HealthSystem RightPlayerHealth;

    protected GameObject LeftPlayer;
    protected HealthSystem LeftPlayerHealth;

    private Slider RightPlayerHealthSlider;
    private Slider LeftPlayerHealthSlider;
    private Text RightPlayerHealthText;
    private Text LeftPlayerHealthText;

    private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        GetSceneLoader();

        SpawnSprites();
        GetHealthObject();
        UnFreezePlayers();
    }


    // Update is called once per frame
    void Update()
    {
            UpdateHealthSlider();
            CheckHealth();
    }

    private void GetSceneLoader() {
        sceneLoader = this.gameObject.GetComponent<SceneLoader>();

    }

    private void FreezePlayers() {
        // freeze players 
        RightPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        LeftPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        // disable control scripts
        LeftPlayer.GetComponent<GenericPlayer>().enabled = false;
        RightPlayer.GetComponent<GenericPlayer>().enabled = false;

    }

    private void UnFreezePlayers() {
        // unfreeze players ( keep rotation frozen)
        RightPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        LeftPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        // enable controll scripts
        LeftPlayer.GetComponent<GenericPlayer>().enabled = true;
        RightPlayer.GetComponent<GenericPlayer>().enabled = true;

    }


    private void EndRound(GameObject loser) {

        FreezePlayers();
        Destroy(loser);
        // show win screen

        // go onto next round

        // end scene
        sceneLoader.LoadNextScene();

    }
    

    private void CheckHealth() {

        if (RightPlayerHealth.GetHealth() <= 0) {
            EndRound(RightPlayer);
            }

        else if (LeftPlayerHealth.GetHealth() <= 0) EndRound(LeftPlayer);

    }

    private void UpdateHealthSlider() {
        RightPlayerHealthSlider.value = RightPlayerHealth.GetHealth();
        LeftPlayerHealthSlider.value = LeftPlayerHealth.GetHealth();

        RightPlayerHealthText.text= RightPlayerHealthSlider.value.ToString();
        LeftPlayerHealthText.text = LeftPlayerHealthSlider.value.ToString();
    }


    private void GetHealthObject() {
        RightPlayerHealth = RightPlayer.GetComponent<GenericPlayer>().health;
        LeftPlayerHealth  = LeftPlayer.GetComponent<GenericPlayer>().health;

        RightPlayerHealthSlider = GameObject.Find("RightPlayerHealthSlider").GetComponent<Slider>();
        LeftPlayerHealthSlider  = GameObject.Find("LeftPlayerHealthSlider").GetComponent<Slider>();

        RightPlayerHealthText = GameObject.Find("RightPlayerHealthText").GetComponent<Text>();
        LeftPlayerHealthText  = GameObject.Find("LeftPlayerHealthText").GetComponent<Text>();

    }

    private void SpawnSprites() {

        Vector2 rightPos = new Vector2(20, 10);
        Vector2 leftPos = new Vector2(10, 10);


        // create right player
        RightPlayer = Instantiate(
            RightPlayerPrefab,
            rightPos,
            Quaternion.identity);


        // create right player
        LeftPlayer = Instantiate(
            LeftPlayerPrefab,
            leftPos,
            Quaternion.identity);

        FreezePlayers();
    }


}
