using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Rendering.Universal;

public class WeatherControll : MonoBehaviour
{
    [SerializeField]
    private GameObject cloud, fog, rain, sun, test, globalCloud;
    private void Start()
    {
        /*RequestWeather();*/
        /*string json = "{\"city_name\":\"hue\",\"clouds\":96,\"country\":\"VN\",\"id\":1112,\"is_rain\":false,\"rain_fall\":\"none\",\"temp\":292,\"visibility\":1000,\"wind_deg\":\"East\",\"wind_speed\":16,\"txDigest\":\"6bYfzTRY3kzVG8VA7bU6ijwf47wSxLAhqa9LPCxUswCf\"}";
        ReceiveWeather(json);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*[DllImport("__Internal")]
    public static extern void RequestWeather();*/
    public void ReceiveWeather(string jSon)
    {
        test.GetComponent<TextMeshProUGUI>().SetText(jSon);
        Dictionary<string, object> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jSon);
        if (jsonObject != null)
        {
            sun.GetComponent<Light2D>().intensity = float.Parse(jsonObject["visibility"].ToString()) / 10000.0f;
            //Default Sunny
            cloud.GetComponent<CloudSpawner>().spawnInterval = 100.0f - float.Parse(jsonObject["clouds"].ToString());
            float flowWind = 1.0f;
            if (jsonObject["wind_deg"].ToString().CompareTo("West") == 0)
            {
                flowWind = -1.0f;
            }
            globalCloud.transform.localScale = new Vector3(flowWind, 0, 0);
            cloud.GetComponent<CloudSpawner>().speed = flowWind*float.Parse(jsonObject["wind_speed"].ToString()) / 10.0f;
            if (Boolean.Parse(jsonObject["is_rain"].ToString()))
            {
                //Start Rain      
                float windSpeed = float.Parse(jsonObject["wind_speed"].ToString());
                rain.GetComponent<ParticleSystem>().startRotation *= flowWind;
                rain.transform.rotation = Quaternion.Euler(0.0f, 0.0f, flowWind * 20.0f);
                GameObject ControllMob = GameObject.FindGameObjectWithTag("MOBDATA");
                ControllMob.GetComponent<ManageMobData>().MobInRain();
                rain.SetActive(true);
            }
            else if (float.Parse(jsonObject["visibility"].ToString()) / 1000.0f < 2.0f)
            {
                //Start Fog
                fog.SetActive(true);
                float speedFog = float.Parse(jsonObject["wind_speed"].ToString()) / 100.0f;
                fog.GetComponent<SpriteRenderer>().material.SetVector("_FogSpeed", new Vector2(speedFog, 0.0f));
                fog.gameObject.transform.localScale = new Vector3(-1.0f*flowWind*300,100,0);
            }
        }
    }
}

