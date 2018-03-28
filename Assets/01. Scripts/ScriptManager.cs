using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Threading;

public class GunAttribute {
	public float shotSpeed { get; set; }
	public int wayCount { get; set; }
}

public class ScriptManager {

	private static ScriptManager _instance = null;
    private static readonly object padlock = new object();

	private ScriptManager() {
	}

	// Singleton Pattern
	// Thread safe
	public static ScriptManager instance {
		get {
			lock (padlock) {
				if (_instance == null)
					_instance = new ScriptManager ();

				return _instance;
			}
		}
	}

	public GunAttribute FindGunItemAttr(string itemId) {
		string strFile = "Assets/attribute.csv";	// csv file name and location
		GunAttribute attr = new GunAttribute ();	// create GunAttribute class instance

		// read file
		using (FileStream fs = new FileStream (strFile, FileMode.Open)) {
			using (StreamReader sr = new StreamReader (fs, Encoding.UTF8, false)) {
				string strLineValue = null;
				string[] keys = null;	// string array to save key
				string[] values = null;	// string array to save value

				while ((strLineValue = sr.ReadLine ()) != null) {
					if (string.IsNullOrEmpty (strLineValue))
						continue;

					// add '#' to key value in csv file
					if (strLineValue.Substring (0, 1).Equals ("#")) {
						keys = strLineValue.Split (',');

						keys [0] = keys [0].Replace ("#", "");

						// Console.Write("Key : ");
						// Output
						for (int nIndex = 0; nIndex < keys.Length; nIndex++) {
							//Console.Write(keys[nIndex]);
							if (nIndex != keys.Length - 1)
								Debug.Log(keys[nIndex]);
							//Console.Write(", ");
						}
						//Console.WriteLine();

						continue;
					}
					// save Value code
					values = strLineValue.Split(',');

					attr.shotSpeed = 0.05f;
					attr.wayCount = 3;
					/*
					 * Console.Write("Value : ");
					 */
					// Output
					//for (int nIndex = 0; nIndex < values.Length; nIndex++) {
					//Console.Write(values[nIndex]);
					//if (nIndex != values.Length -1)
					//Console.Write(", ");
					//}
					//Console.WriteLine();
				}
			}
		}
		return attr;
	}
}
