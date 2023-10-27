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

    [SerializeField] bool ParseData = true;

    string inFilePath = @"D:\Github Clones\VSIM-Folder\VSIMFolder\Assets\Height Data\merged.txt";
    string outFilepath = @"D:\Github Clones\VSIM-Folder\VSIMFolder\Assets\Height Data\terrain.txt";
   
    // Start is called before the first frame update
    void Start()
    {
        // This if sentence isn't necessary, but useful if the terrain data has already
        // been generated. 
        if (ParseData)
        {
            // Getting total amount of vertices in the file
            var lineCount = File.ReadLines(inFilePath).Count();
            Debug.Log(lineCount);

            // And putting it in the output file
            File.WriteAllText(outFilepath, lineCount.ToString() + "\n");

            // Now we convert the vertices to smaller x- and z-values.
            ConvertData(lineCount);
        }
    }

    void ConvertData(int fileLength)
    {
        string line;

        StreamReader read = new StreamReader(inFilePath);

        for (int i = 0; i < fileLength; i++)
        {
            line = read.ReadLine();
            List<String> pointValues = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            
            // To change the x- and z-values we need to convert to float
            List<float> fPointValues = new List<float>();
            fPointValues.Add(float.Parse(pointValues[0], CultureInfo.InvariantCulture.NumberFormat) - 611000.0f);
            fPointValues.Add(float.Parse(pointValues[2], CultureInfo.InvariantCulture.NumberFormat));
            fPointValues.Add(float.Parse(pointValues[1], CultureInfo.InvariantCulture.NumberFormat) - 6641000.0f);

            // And then convert it back into strings
            pointValues[0] = fPointValues[0].ToString();
            pointValues[1] = fPointValues[1].ToString();
            pointValues[2] = fPointValues[2].ToString();
            
            // Clearing the list to save memory
            fPointValues.Clear();
            
            // Creating the output string which will be one new line on the file
            string outputString = pointValues[0] + " " + pointValues[2] + " " + pointValues[1];

            using (StreamWriter w = File.AppendText(outFilepath))
            {
                w.WriteLine(outputString);
            }
            
        }
    }
}
