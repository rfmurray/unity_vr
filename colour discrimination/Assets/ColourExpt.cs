using System;
using System.IO;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public GameObject testObject;  // test object whose colour will be varied
    public Material testMaterial;  // material of test object

    int phase = 1;                      // trial phase
    int trial = 1, ntrials = 40;        // current trial number and total number of trials
    int blueRange = 20, blueLevel = 0;  // range of blue level and current blue level
    float startTime = 0;

    string filename;  // data file name

    void Start()
    {
        // make filename
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        filename = $"data_{timestamp}.txt";

        // write column names to data file
        using (StreamWriter writer = new StreamWriter(filename, append: true))
            writer.WriteLine("#trial,blueLevel,responseBlue,responseTime");
    }

    void Update()
    {

        if (phase == 1) // set stimulus properties for a new trial
        {
            // assign random blue level to test object
            blueLevel = UnityEngine.Random.Range(-blueRange, blueRange + 1);
            Color32 c = new Color32(128, 128, (byte)(128 + blueLevel), 255);
            testMaterial.color = c;

            // get subject's response
            phase = 2;
            startTime = Time.time;
        }

        else if (phase == 2) // get subject's response
        {

            // see which keys have been pressed
            bool responseBlue = Input.GetKeyDown(KeyCode.Alpha1);
            bool responseYellow = Input.GetKeyDown(KeyCode.Alpha2);
            bool pressQuitKey = Input.GetKeyDown(KeyCode.Q);
            bool pressScreenCapture = Input.GetKeyDown(KeyCode.S);

            // screen capture
            if(pressScreenCapture)
                ScreenCapture.CaptureScreenshot(Directory.GetCurrentDirectory() + "/screenshot.png");

            // quit the experiment
            if (pressQuitKey)
                Quit();

            // remaining code is for a keypress response to the stimulus
            if (!responseBlue && !responseYellow)
                return;

            // get response time
            float responseTime = Time.time - startTime;

            // save data for this trial
            using (StreamWriter writer = new StreamWriter(filename, append: true))
            {
                string datastr = $"{trial},{blueLevel},{Convert.ToByte(responseBlue)},{responseTime:F6}";
                writer.WriteLine(datastr);
            }

            // all trials done?
            if (++trial > ntrials)
                Quit();

            // start next trial
            phase = 1;

        }

    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }
}
