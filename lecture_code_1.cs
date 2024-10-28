// lecture_code_1.cs

// declaring a variable

int trial_num;
int trial_num = 1;

trial_num = 100;

// dotnetfiddle.net; use compiler version 8

using System;
					
public class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello World");
    }
}

// add this code to .NET Fiddle
int trial_num = 1;
Console.WriteLine(trial_num);

// data types
float reaction_time1 = 2.34f;  // floating point, with 4 bytes of precision
double reaction_time2 = 3.45;  // floating point, with 8 bytes of precision
bool correct_response = true;  // Boolean; can be true or false
char keypress = 'x';           // character; note the single quotes
string subject = "jfk";        // string; note the double quotes
byte grey = 128;               // unsigned integer, with 8 bits of precision (0-255)

// type casting

double x = 1;
int y = x;  // error

double x = 1;
int y = (int)x;

// arrays

int[] trialNums = new int[5];
int[] trialNums = [10, 20, 30, 40, 50];

trialNums[2] = 100;

// arithmetic

double x = 1 + ((2-3)/(4*5));

// note: ^ is not exponentiation; it is bitwise xor

x += 5;
x = x + 5;

x++;
x = x + 1;

// if statements

if (a > 0)
    b = 1;

if (a > 0)
{
    b = 1;
    c = 2;
}

if (a > 0)
{
    b = 1;
    c = 2;
}
else if(a == 1)
    b = 10;
else if(a == 2)
    b = 20;
else
    b = -1;

// boolean values

a > 0
a < 0
a >= 0
a <= 0
a == 0  # test for equality; note two equals signs
a != 0  # test for inequality

// logical operations: && (and), || (or), ! (not)

if ( a >= 15 && !( a >= 27 ) )
    b = -1;

// while loops

while (a < 10)
    a = a + 1;

while (a < 10)
{
    a = a + 1;
    b = b - 1;
}

int a = 0;
while (true)
{
    a++;
    if (a <= 10)
        continue;
    Console.WriteLine(a);
    if (a >= 15)
        break;	
}

// for loops

k = 0;
for (i = 0; i <= 10; i++)
    k = k + i;

k = 0;
for (int i = 0; i <= 10; i++)
    k = k + i;

// functions

double calc(int a, int b)
{
    double c = 0.5*(3*a + 2*b);
    c = c + 10;
    return c;
}

using System;

public class Program
{
    public static void Main()
    {
        double x = calc(1,2);
        Console.WriteLine(x);
    }
	
    static double calc(int a, int b)
    {
        double c = 0.5*(3*a + 2*b);
        c = c + 10;
        return c;
    }
}

// classes and objects

using System;
Random rng = new Random();
int x = rng.Next(0, 100);

int[] trialNums = new int[5]; // note the similarity

float elapsed_time = UnityEngine.Time.time;  // a static method

// namespaces

// using System;
public class Program
{
    public static void Main()
    {
        System.Console.WriteLine("Hello World");
    }
}

using UnityEngine.Rendering.HighDefinition;

using System;
double x = Math.Pow(2, 8);
double u = Math.Sin(Math.PI/2);

// bubblesort

using System;

// create an array of random integers from 0 to 99
int n = 16;                    // choose the number of integers
int[] x = new int[n];          // create an array of integers
Random rng = new Random();     // create a random number generator
for (int i = 0; i < n; i++)    // let i range from 0 to n-1
    x[i] = rng.Next(0, 100);   // get a random integer from 0 to 99

// sort the array of integers
while (true)                        // loop indefinitely
{
    int switches = 0;               // initialize a counter
    for (int i = 0; i < n - 1; i++) // let i range from 0 to n-2
        if (x[i] > x[i + 1])        // wrong order?
        {
            int tmp = x[i];         // if so, switch the integers
            x[i] = x[i + 1];
            x[i + 1] = tmp;
            switches++;             // increment the switch counter
        }

    // if no switches, then we're done
    if (switches==0)
        break;
}

// print the sorted integers
for (int i = 0; i < n; i++)
    Console.WriteLine(x[i]);
