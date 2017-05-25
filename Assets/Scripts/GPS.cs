using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LocationState
{
    Disabled,
    TimedOut,
    Failed,
    Enabled
}

public class GPS : MonoBehaviour
{
    public static GPS Instance { set; get; }

    //Approximate radius of the earth (in kilometers)
    const float earthRadius = 6371;

    public LocationState state;

    // Position on earth (in degrees)
    public float latitude;
    public float longitude;

    //Distance walked (in meters)
    public float distance;

    private List<GameObject> models;
    private int Idle = 0;
    private int Walking = 1;

    private float speed;

    void Start()
    {
        state = LocationState.Disabled;
        latitude = 0f;
        longitude = 0f;
        distance = 0f;

        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine("startLocationService");

        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        models[Idle].SetActive(true);

        if (models[0].name == "Rookie Salamander") speed = 12f;
        if (models[0].name == "Ultimate Salamander") speed = 12f;
        if (models[0].name == "Champion Salamander") speed = 12f;
        if (models[0].name == "Rookie Leafdino") speed = 25f;
        if (models[0].name == "Ultimate Leafdino") speed = 25f;
        if (models[0].name == "Champion Leafdino") speed = 25f;
        if (models[0].name == "Rookie FlyingFish") speed = 8f;
        if (models[0].name == "Ultimate FlyingFish") speed = 8f;
        if (models[0].name == "Champion FlyingFish") speed = 8f;

        Debug.Log("Speed: " + speed);
    }

    private IEnumerator startLocationService()
    {

        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enabled GPS");
            yield break;
        }

        Input.location.Start();
        int waitTime = 15;
        while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
        {
            yield return new WaitForSeconds(1);
            waitTime--;
        }
        if (waitTime <= 0)
        {
            Debug.Log("Timed out");
            state = LocationState.TimedOut;
            yield break;
        }
        else if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            state = LocationState.Failed;
            yield break;
        }
        else
        {
            state = LocationState.Enabled;
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }
    }

    //Update is called once per frame
    void Update()
    {
        if (state == LocationState.Enabled)
        {
            float deltaDistance = Haversine(ref latitude, ref longitude) * 1000f;
            if (deltaDistance > 0f)
            {
                distance += deltaDistance;
            }
            scrollBackground();
        }
        else
        {
            models[Walking].SetActive(false);
            models[Idle].SetActive(true);
        }
    }

    void scrollBackground()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.x += Time.deltaTime / speed;
        //Debug.Log("off set: " + offset.x);
        mat.mainTextureOffset = offset;
        models[Idle].SetActive(false);
        models[Walking].SetActive(true);
    }


    //The haversine formula determines the great-circle distance between two points on a sphere given their longitudes and latitudes.
    float Haversine(ref float lastLatitude, ref float lastLongitude)
    {
        float newLatitude = Input.location.lastData.latitude;
        float newLongitude = Input.location.lastData.longitude;
        float deltaLatitude = (newLatitude - lastLatitude) * Mathf.Deg2Rad;
        float deltaLongitude = (newLongitude - lastLongitude) * Mathf.Deg2Rad;
        float a = Mathf.Pow(Mathf.Sin(deltaLatitude / 2), 2) +
            Mathf.Cos(lastLatitude * Mathf.Deg2Rad) * Mathf.Cos(newLatitude * Mathf.Deg2Rad) *
            Mathf.Pow(Mathf.Sin(deltaLongitude / 2), 2);
        lastLatitude = newLatitude;
        lastLongitude = newLongitude;
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return earthRadius * c;
    }

    IEnumerator OnApplicationPause(bool pauseState)
    {
        if (pauseState)
        {
            Input.location.Stop();
            state = LocationState.Disabled;
        }
        else
        {
            Input.location.Start();
            int waitTime = 15;
            while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
            {
                yield return new WaitForSeconds(1);
                waitTime--;
            }
            if (waitTime <= 0)
            {
                Debug.Log("Timed out");
                state = LocationState.TimedOut;
                yield break;
            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
                state = LocationState.Failed;
                yield break;
            }
            else
            {
                state = LocationState.Enabled;
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;
            }
        }
    }
}
