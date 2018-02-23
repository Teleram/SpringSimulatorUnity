using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Access : MonoBehaviour {

    public String[] stringArray;

	// Use this for initialization
	void Start () {
        stringArray = new String[5];

        WriteToFile();

        ReadFromFile();

        fillArray();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void WriteToFile()
    {
        using (StreamWriter sw = new StreamWriter("TestFile.txt"))
        {
            // Add some text to the file.
            sw.Write("This is the ");
            sw.WriteLine("header for the file.");
            sw.WriteLine("-------------------");
            // Arbitrary objects can also be written to the file.
            sw.Write("The date is: ");
            sw.WriteLine(DateTime.Now);
        }
    }

    public void ReadFromFile()
    {
        StreamReader reader = new StreamReader("TestFile.txt");
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    public void fillArray()
    {
        StreamReader reader = new StreamReader("TestFile.txt");
        for (int i = 0; i < 3; i++)
        {
            stringArray[i] = reader.ReadLine();
        }
    }
}
