// lecture_code_2.cs

// callbacks

void Start()
{
}

void Update()
{
}

// writing a value to the Unity console

float response_time = 3.42f;
UnityEngine.Debug.Log(response_time);

// keyboard input

bool keyDown = Input.GetKeyDown(KeyCode.A);
bool buttonDown = Input.GetKeyDown(KeyCode.JoystickButton1);

bool keyPressed = Input.GetKey(KeyCode.A);

// mouse input

Vector3 mousePos = Input.mousePosition;
bool mouseClick = Input.GetMouseButtonDown(0);

// The UnityEngine.Input namespace is deprecated, but it works fine,
// and it's easy to use, so that's what we'll use here. You can read
// about its replacement, UnityEngine.InputSystem, to learn about a newer
// namespace that has some more advanced features.

// linking variables to objects

GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 

public GameObject cube;

// transforming objects

cube.transform.localPosition = new Vector3(4, 0, 0);
cube.transform.localRotation = Quaternion.Euler(45, 30, 0);
cube.transform.localScale = new Vector3(2, 2, 2);

// random numbers

using UnityEngine;
int k = Random.Range(0, 100);      // lower limit is inclusive, upper limit is exclusive
float x = Random.Range(0f, 100f);  // lower and upper limits are inclusive

int rngseed = (int)System.DateTime.Now.Ticks;
Random.InitState(rngseed);

// UnityEngine.Random vs. System.Random

// strings

int trial_num = 1;
int stimulus_code = 2;
double response_time = 3.45;
string dataline = $"{trial_num},{stimulus_code},{response_time}";

// writing a string to a file

using System.IO;

filename = "data.txt";

using (StreamWriter writer = new StreamWriter(filename, true))
    writer.WriteLine(dataline);

// getting the current time

float t = UnityEngine.Time.time;
string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

// capturing a screenshot

UnityEngine.ScreenCapture.CaptureScreenshot("screenshot.png");

// stopping the program

UnityEditor.EditorApplication.isPlaying = false;     // in the Unity editor
UnityEngine.Application.Quit();                      // in a compiled program
