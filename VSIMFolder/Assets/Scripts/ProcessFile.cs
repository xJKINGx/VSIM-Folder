using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProcessFile : MonoBehaviour
{
    DirectoryInfo dir = new DirectoryInfo("D:\\Github Clones\\VSIM-Folder\\VSIMFolder\\Assets\\Height Data");

    FileInfo inputFile = new FileInfo("merged.txt");

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetLinesCount(inputFile));
    }

    long GetLinesCount(FileInfo file)
    {
        long x = File.ReadLines(file.FullName).Count();
        return x;
    }
}
