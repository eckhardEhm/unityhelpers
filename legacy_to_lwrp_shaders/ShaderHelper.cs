using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShaderHelper : EditorWindow {


    string GameObjectsAmount = "Hello World";
    List<Material> all_materials;
    List<Renderer> all_renderers;
    bool did_run = false;
/*
    bool myBool = true;
    float myFloat = 1.23f;
*/

    [MenuItem("Window/ShaderHelper")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(ShaderHelper));
    }

    void OnGUI()
    {
        GUILayout.Label ("Shaders in your Project", EditorStyles.boldLabel);
    	if (GUI.Button(new Rect(5, 20, 150, 50), "Start analyzing"))
        {
            findAllGameobjects();
        }
    	if (GUI.Button(new Rect(155, 20, 50, 50), "Reset"))
        {
            Debug.Log("reset");
            did_run = false;
            all_materials = null;
            all_renderers = null;
            all_materials = new List<Material>();
            all_renderers = new List<Renderer>();
        }
    	if (GUI.Button(new Rect(5, 80, 100, 50), "Find Shader"))
        {
            Debug.Log("find shader LightweightPipeline/Standard (Physically Based) (UnityEngine.Shader)");
			Shader lwrp;
			lwrp = Shader.Find("LightweightPipeline/Standard (Physically Based)");
			Debug.Log(lwrp);
        }
        if (GUI.Button(new Rect(105, 80, 100, 50), "Assign LWRP"))
        {
            Debug.Log("assigning LWRP Shaders to all Materials...");
			Shader lwrp;
			lwrp = Shader.Find("LightweightPipeline/Standard (Physically Based)");
			foreach(Material m in all_materials){
				m.shader = lwrp;
			}
        }

    }

    void findAllGameobjects(){

        Debug.Log("findAllGameObjects - did run? : "+did_run);

    	if(did_run){
    		Debug.Log("findAllGameObjects() - did already run");
    		return;
    	}

    	Debug.Log("findAllGameobjects running");
    	
    	GameObject[] foundGameObjects = FindObjectsOfType<GameObject>();
    	Debug.Log("Amount of GameObjects found: "+foundGameObjects.Length);
    	parseGOs(foundGameObjects);
    	GUILayout.Label ("Gameobjects total "+GameObjectsAmount, EditorStyles.boldLabel);

    	did_run = true;
    }

    void parseGOs(GameObject[] gos){
    	
    	Debug.Log("parseGOs()");

		foreach(GameObject go in gos){
    	//	Debug.Log(go.name);
			Renderer r = go.GetComponent<Renderer>();
			//Debug.Log(r);
			if(r != null){
				all_renderers.Add(r);
//				Debug.Log("RendererName: "+r.name);
			}
			//Debug.Log(all_renderers.Length);
    	}
    	Debug.Log("renderers in total: "+all_renderers.Count);
    	Debug.Log("parseGOs() done.");
    	parseRenderers(all_renderers);
    }

    void parseRenderers(List<Renderer> rndrs){
    	Debug.Log("parseRenderers");
    	foreach(Renderer r in rndrs){
    		Material[] mats = r.materials;
    		if(mats != null){
    			foreach(Material m in mats){
    				all_materials.Add(m);
    				Debug.Log("Shader? : "+m.shader.name);
    				//LightweightPipeline/Standard (Physically Based) (UnityEngine.Shader)
    			}
    		Debug.Log("added "+r.name+" with "+mats.Length+" materials.");
    		}
    	}
    	Debug.Log("Total of "+all_materials.Count+" Materials found.");
    }

}
