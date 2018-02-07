using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public List<GameObject> Levels;

    public void SetLevel(int SceneLevel)
    {
        if (SceneLevel < Levels.Count)
        {
            Camera MainCamera = Camera.main;

            GameObject SelectLevel = Levels[SceneLevel];
            MainCamera.transform.position = SelectLevel.transform.GetChild(4).transform.position;
            MainCamera.transform.rotation = SelectLevel.transform.GetChild(4).transform.rotation;
            MainCamera.orthographicSize = SelectLevel.transform.GetChild(4).transform.localScale.z;

            GameObject[] TrashLevel = GameObject.FindGameObjectsWithTag("Level");
            foreach (GameObject Level in TrashLevel)
            {
                Destroy(Level);
            }

            Instantiate(SelectLevel);
        }
    }
}
