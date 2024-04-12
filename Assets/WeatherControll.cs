using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Rendering.UI;
using UnityEngine.Rendering.Universal;

public class WeatherControll : MonoBehaviour
{
    [SerializeField]
    private GameObject cloud, fog, rain, sun,test;
    private void Start()
    {
        /*string json = "{\"city_name\":\"Beijing\",\"clouds\":7,\"country\":\"CN\",\"id\":1115,\"is_rain\":false,\"rain_fall\":\"none\",\"temp\":281,\"visibility\":10000,\"wind_deg\":\"West\",\"wind_speed\":34}";
        ReceiveWeather(json);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [DllImport("__Internal")]
    public static extern void RequestWeather();
    public void ReceiveWeather(string jSon)
    {
        test.GetComponent<TextMeshProUGUI>().SetText(jSon);
        Dictionary<string, object> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jSon);
        if (jsonObject != null)
        {
            sun.GetComponent<Light2D>().intensity = float.Parse(jsonObject["visibility"].ToString()) / 10000.0f;
            //Default Sunny
            cloud.GetComponent<CloudSpawner>().spawnInterval = 100.0f - float.Parse(jsonObject["clouds"].ToString());
            cloud.GetComponent<CloudSpawner>().speed = float.Parse(jsonObject["wind_speed"].ToString()) / 10.0f;
            if (Boolean.Parse(jsonObject["is_rain"].ToString()))
            {
                //Start Rain
                rain.SetActive(true);
                float flowWind = 1.0f;
                float windSpeed = float.Parse(jsonObject["wind_speed"].ToString());
                if (jsonObject["wind_deg"].ToString().CompareTo("West") == 0)
                {
                    flowWind = -1.0f;
                }
                rain.GetComponent<ParticleSystem>().startRotation =  flowWind * (Mathf.Atan2(windSpeed, 10.0f) * (180 / Mathf.PI));
                rain.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1.0f * flowWind * (Mathf.Atan2(windSpeed, 10.0f) * (180 / Mathf.PI)));
                GameObject ControllMob = GameObject.FindGameObjectWithTag("MOBDATA");
                ControllMob.GetComponent<ManageMobData>().MobInRain();
            }
            else if (float.Parse(jsonObject["visibility"].ToString()) / 1000.0f < 2.0f)
            {
                //Start Fog
                fog.SetActive(true);
                fog.GetComponent<SpriteRenderer>().material.SetVector("FogSpeed", new Vector2(float.Parse(jsonObject["wind_speed"].ToString()) / 100.0f, 0.0f));
            }
        }
    }
}

