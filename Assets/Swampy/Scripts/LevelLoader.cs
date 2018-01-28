using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    
    public List<GameObject> Levels;
    // 1 2 3 4 camera plat
    public void SetLevel(int SceneLevel)
    {
        if(SceneLevel<Levels.Count)
        {
            GameObject SelectLevel = Levels[SceneLevel];
            Camera.main.transform.position = SelectLevel.transform.GetChild(4).transform.position;
            Camera.main.transform.rotation = SelectLevel.transform.GetChild(4).transform.rotation;
            Camera.main.orthographicSize = SelectLevel.transform.GetChild(4).transform.localScale.z;

            GameObject[] TrashLevel = GameObject.FindGameObjectsWithTag("Level");
            foreach(GameObject Level in TrashLevel)
            {
                Destroy(Level);
            }
            Instantiate(SelectLevel);
        }
    }
}
