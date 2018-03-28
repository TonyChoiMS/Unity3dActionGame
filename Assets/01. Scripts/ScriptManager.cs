using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Threading;

public class GunAttribute {
	private string shotSpeed { get; set; }
    private string wayCount { get; set; }

    public void setShotSpeed(string shotSpeed) {
        this.shotSpeed = shotSpeed;
    }

    public string getShotSpeed() {
        return shotSpeed;
    }

    public void setWayCount(string wayCount) {
        this.wayCount = wayCount;
    }

    public string getWayCount() {
        return wayCount;
    }
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
                    Debug.Log("key : " + strLineValue.Substring(0, 1));
					if (strLineValue.Substring (1, 1).Equals("#")) {
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
                    } else {
                        // save Value code
                        // index 0 = shotSpeed
                        // index 1 = wayCount
                        values = strLineValue.Split(',');
                        Debug.Log("length : " + values.Length);
                        Debug.Log("0 : " + values[0] + ", 1 : " + values[1]);
                        attr.setShotSpeed(values[0]);
                        attr.setWayCount(values[1]);
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
		}
		return attr;
	}
}
