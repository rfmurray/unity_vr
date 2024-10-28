using System;
using System.IO;
using UnityEngine;

public class LightnessExpt : MonoBehaviour
{
    public GameObject preApparatus, experimentApparatus, postApparatus, calibrationApparatus;
    public GameObject referencePlane;
    public Material referenceMaterial, matchMaterial, calibrationMaterial;

    enum Phase { Instructions, ShowStimulus, GetResponse, Finished, Calibration };
    Phase phase;

    int trial = 1, ntrial = 40;
    float startTime, responseTime;

    float[] referenceReflectances = { 0.30f, 0.40f, 0.50f };
    float[] referenceOrientations = { -45f, -30f, -15f, 0f, 15f, 30f, 45f };
    float referenceReflectance, referenceOrientation;
    float matchReflectance;

    float[] calibrationReflectances = { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };
    int calibrationk = 0;

    string filename;

    void Start()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        filename = $"data_{timestamp}.txt";
        using (StreamWriter writer = new StreamWriter(filename, append: true))
            writer.WriteLine("#trial,referenceReflectance,referenceOrientation,matchReflectance,responseTime");
        //DataFile.Open();

        SetPhase(Phase.Instructions);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetPhase(Phase.Instructions);
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SetPhase(Phase.Calibration);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            Quit();

        if (phase == Phase.Instructions)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                SetPhase(Phase.ShowStimulus);
        }

        else if (phase == Phase.ShowStimulus)
        {
            int k = UnityEngine.Random.Range(0, referenceReflectances.Length);
            referenceReflectance = referenceReflectances[k];
            float r = sRGBfn.sRGBinv(referenceReflectance);
            Color c = new Color(r, r, r);
            referenceMaterial.SetColor("_BASE_COLOR", c);
            //SetPatchReflectance(referenceMaterial, referenceReflectance);

            k = UnityEngine.Random.Range(0, referenceOrientations.Length);
            referenceOrientation = referenceOrientations[k];
            referencePlane.transform.rotation = Quaternion.Euler(-90f, -referenceOrientation, 0f);
            // check how Unity's Euler rotations work

            SetPhase(Phase.GetResponse);
            startTime = Time.time;
        }

        else if (phase == Phase.GetResponse)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                responseTime = Time.time - startTime;

                string dataline = $"{trial},{referenceReflectance:F3},{referenceOrientation:F3},{matchReflectance:F3},{responseTime:F3}";
                using (StreamWriter writer = new StreamWriter(filename, append: true))
                    writer.WriteLine(dataline);
                //DataFile.Write(trial, referenceReflectance, referenceOrientation, matchReflectance, responseTime);

                if (++trial > ntrial)
                {
                    SetPhase(Phase.Finished);
                    return;
                }

                SetPhase(Phase.ShowStimulus);
                return;

            }

            Vector3 v = Input.mousePosition;
            matchReflectance = v.y / Screen.height;
            matchReflectance = Math.Clamp(matchReflectance, 0f, 1f);
            float r = sRGBfn.sRGBinv(matchReflectance);
            Color c = new Color(r, r, r);
            matchMaterial.SetColor("_BASE_COLOR", c);
            //SetPatchReflectance(matchMaterial, matchReflectance);
        }

        else if (phase == Phase.Finished)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                Quit();
        }

        else if (phase == Phase.Calibration)
        {
            if (Input.GetKeyDown(KeyCode.C))
                SetPhase(Phase.Instructions);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                calibrationk = (calibrationk + 1) % calibrationReflectances.Length;
                float r = sRGBfn.sRGBinv(calibrationReflectances[calibrationk]);
                calibrationMaterial.color = new Color(r, r, r);
                //SetCalibrationColour();
            }
        }

    }

    void SetPhase(Phase p)
    {
        phase = p;

        preApparatus.SetActive(p == Phase.Instructions);
        experimentApparatus.SetActive(p == Phase.ShowStimulus || p == Phase.GetResponse);
        postApparatus.SetActive(p == Phase.Finished);
        calibrationApparatus.SetActive(p == Phase.Calibration);

        if (p == Phase.Instructions)
            trial = 1;

        if(p==Phase.Calibration)
        {
            calibrationk = calibrationReflectances.Length - 1;
            float r = sRGBfn.sRGBinv(calibrationReflectances[calibrationk]);
            calibrationMaterial.color = new Color(r, r, r);
            //SetCalibrationColour();
        }
    }

    //void SetPatchReflectance(Material m, float r)
    //{
    //    float g = sRGBfn.sRGBinv(r);
    //    Color c = new Color(g, g, g);
    //    m.SetColor("_BASE_COLOR", c);
    //}

    //void SetCalibrationColour()
    //{
    //    float r = sRGBfn.sRGBinv(calibrationReflectances[calibrationk]);
    //    calibrationMaterial.color = new Color(r, r, r);
    //}

    void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
