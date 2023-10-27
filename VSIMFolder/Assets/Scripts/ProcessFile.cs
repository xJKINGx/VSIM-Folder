using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProcessFile : MonoBehaviour
{

    int xMin;
    int xMax;
    int zMin;
    int zMax;

    string filePath = @"D:\Github Clones\VSIM-Folder\VSIMFolder\Assets\Height Data\merged.txt";

    // Start is called before the first frame update
    void Start()
    {
        var lineCount = File.ReadLines(filePath).Count();
        Debug.Log(lineCount);
        FindMinMax(lineCount);
    }

    void FindMinMax(int fileLength)
    {
        string line;
        Vector3 temp = new Vector3();
        StreamReader read = new StreamReader(filePath);

        for (int i = 0; i < fileLength; i++)
        {
            line = read.ReadLine();
            List<String> pointValues = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            temp = new Vector3(float.Parse(pointValues[0], CultureInfo.InvariantCulture.NumberFormat),
                               float.Parse(pointValues[2], CultureInfo.InvariantCulture.NumberFormat),
                               float.Parse(pointValues[1], CultureInfo.InvariantCulture.NumberFormat));
            Debug.Log(temp);
        }
    }
}
