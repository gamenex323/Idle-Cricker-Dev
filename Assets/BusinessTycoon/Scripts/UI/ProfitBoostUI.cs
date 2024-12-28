﻿using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProfitBoostUI : MonoBehaviour
{
    public static ProfitBoostUI instance;

    [SerializeField]
    private Text body;
    [SerializeField]
    private Text boostTimer;
    [SerializeField]
    private Button adButton;

    private RectTransform rectTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.Hide();
    }

    public void Show()
    {
        if (instance != null)
        {
            adButton.gameObject.SetActive(true);
            instance.body.text = "Watch an ad and get x2 profit boost for 4 hours.";

            if (DateTime.FromOADate(GameManager.instance.LevelData.VideoStarted) > DateTime.Now)
            {
                instance.body.text = "Watch another ad to increase your boost timer by 4 hours.";
            }

            if (DateTime.FromOADate(GameManager.instance.LevelData.VideoStarted) >= DateTime.Now.AddHours(24))
            {
                instance.body.text = "No more ads available, check back later.";
                adButton.gameObject.SetActive(false);
            }

            rectTransform.Show();
        }
    }

    private void Update()
    {
        var videoAdBoostTimer = Convert.ToInt32((DateTime.FromOADate(GameManager.instance.LevelData.VideoStarted) - DateTime.Now).TotalSeconds - Time.deltaTime);
        if (videoAdBoostTimer >= 1)
        {
            var hours = videoAdBoostTimer / 3600;
            var minutes = videoAdBoostTimer / 60 % 60;
            var seconds = videoAdBoostTimer % 60;
            boostTimer.text = "Boost active for " + hours.ToString().PadLeft(2, '0') + ":" + minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString().PadLeft(2, '0');
        }
        else
        {
            boostTimer.text = "";
        }
    }

    public void ExtendBoostTime()
    {
        var hoursToAdd = 4f;
        if (DateTime.FromOADate(GameManager.instance.LevelData.VideoStarted) <= DateTime.Now.AddHours(24))
        {
            if (GameManager.instance.LevelData.VideoStarted >= DateTime.Now.ToOADate())
            {
                GameManager.instance.LevelData.VideoStarted = DateTime.FromOADate(GameManager.instance.LevelData.VideoStarted).AddHours(hoursToAdd).ToOADate();
            }
            else
            {
                GameManager.instance.LevelData.VideoStarted = DateTime.Now.AddHours(hoursToAdd).ToOADate();
            }
        }
        Show();
    }

    public void OnCloseClick()
    {
        rectTransform.Hide();
    }
}