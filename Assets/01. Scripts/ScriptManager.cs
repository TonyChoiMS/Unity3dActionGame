using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

struct GunAttribute
{
    public float shotSpeed { get; set; }
    public int wayCount { get; set; }
}

public class ScriptManager {
  

    private ScriptManager _instance;

    private ScriptManager()
    {

    }

	public ScriptManager getInstance()
    {
        if (_instance == null)
        {
            _instance = new ScriptManager();
        }
        return _instance;
    }

    private GunAttribute FindGunItemAttr(string itemID)
    {
        string strFile = "attribute.csv";
        using (FileStream fs = new FileStream(strFile, FileMode.Open))
        {
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
            {
                string strLineValue = null;
                string[] keys = null;
                string[] values = null;

                while ((strLineValue = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(strLineValue)) return;

                    if (strLineValue.Substring(0, 1).Equals("#"))
                    {
                        keys = strLineValue.Split(',');

                        keys[0] = keys[0].Replace("#", "");

                        Console.Write("Key : ");
                        // Output
                        for (int nIndex = 0; nIndex < keys.Length; nIndex++)
                        {
                            Console.Write(keys[nIndex]);
                            if (nIndex != keys.Length - 1)
                                Console.Write(", ");
                        }

                        Console.WriteLine();

                        continue;
                    }

                    values = strLineValue.Split(',');

                    Console.Write("Value : ");
                    // Output
                    for (int nIndex = 0; nIndex < values.Length; nIndex++)
                    {
                        Console.Write(values[nIndex]);
                        if (nIndex != values.Length - 1)
                            Console.Write(", ");
                    }

                    Console.WriteLine();
                }
            }

        }
            
        GunAttribute attr = new GunAttribute();
        attr.shotSpeed = 0.05f;
        attr.wayCount = 3;
        return attr;
    }
}
