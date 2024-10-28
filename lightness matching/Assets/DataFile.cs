using System;
using System.IO;

public class DataFile
{
    static string filename;

    public static void Open()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        filename = $"data_{timestamp}.txt";
        using (StreamWriter writer = new StreamWriter(filename, append: true))
            writer.WriteLine("#trial,referenceReflectance,referenceOrientation,matchReflectance,responseTime");
    }

    public static void Write(int trial, double referenceReflectance, double referenceOrientation,
                             double matchReflectance, double responseTime)
    {
        string dataline = $"{trial},{referenceReflectance:F3},{referenceOrientation:F3},{matchReflectance:F3},{responseTime:F3}";
        using (StreamWriter writer = new StreamWriter(filename, append: true))
            writer.WriteLine(dataline);
    }

}
