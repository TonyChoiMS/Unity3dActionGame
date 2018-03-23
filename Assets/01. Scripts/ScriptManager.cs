using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Threading;

// 총에 대한 밸런싱 정보를 저장할 데이터 
public class GunAttribute
{
    public float shotSpeed { get; set; }
    public int wayCount { get; set; }
}

public class ScriptManager {

    private static ScriptManager _instance = null;
    private static readonly object padlock = new object();

    private ScriptManager()
    {
    }
	// Singleton Pattern 적용
	// Thread safe한 방식
	public static ScriptManager instance
    {
        get
        {
            lock (padlock)
            {
                if (_instance == null)
                {
                    _instance = new ScriptManager();
                }
                return _instance;
            }
        }
    }

    // Attribute.csv 파일을 읽어와서 파일 내 속성을 적용하는 함수
    public GunAttribute FindGunItemAttr(string itemID)
    {
        string strFile = "Assets/attribute.csv";    // csv 파일을 저장한 위치와 이름
        GunAttribute attr = new GunAttribute();		// GunAtttribute 객체 생성
	// 파일 읽기 시작 
        using (FileStream fs = new FileStream(strFile, FileMode.Open))
        {
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
            {
                string strLineValue = null;
                string[] keys = null;	// 키값을 저장할 배열
                string[] values = null;	// 키값에 해당하는 value를 저장할 배열

                while ((strLineValue = sr.ReadLine()) != null)
                {
		    // 
                    if (string.IsNullOrEmpty(strLineValue)) continue;

                    // key값을 정했을 때 사용할 코드 부분
                    // key값을 저장할 row 맨 앞에 '#' 를 붙인다.
                    if (strLineValue.Substring(0, 1).Equals("#"))
                    {
                        keys = strLineValue.Split(',');

                        keys[0] = keys[0].Replace("#", "");

                        //Console.Write("Key : ");
                        // Output
                        for (int nIndex = 0; nIndex < keys.Length; nIndex++)
                        {
                            //Console.Write(keys[nIndex]);
                            if (nIndex != keys.Length - 1)
                                Debug.Log(keys[nIndex]);
                                //Console.Write(", ");
                        }

                        //Console.WriteLine();

                        continue;
                    }
		    // value를 저장하는 부분
                    values = strLineValue.Split(',');
                    
                    attr.shotSpeed = 0.05f;
                    attr.wayCount = 3;
                    /*
                    Console.Write("Value : ");
                    // Output
                    for (int nIndex = 0; nIndex < values.Length; nIndex++)
                    {
                        Console.Write(values[nIndex]);
                        if (nIndex != values.Length - 1)
                            Console.Write(", ");
                    }
		    Console.WriteLine();
                    */
                }
            }
        }
            
        
        return attr;
    }
}
