using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject treasure;

    public Text objEnemy;
    public Text objTreasure;
    public Text objEscape;

    // Update is called once per frame
    void Update()
    {
        //Update Enemy Objective
        if (KillCount() != enemies.Count) {
            objEnemy.text = "Kill Enemies (" + KillCount() + "/" + enemies.Count + ")";
        }
        else
        {
            objEnemy.text = " ̶K̶i̶l̶l̶ ̶E̶n̶e̶m̶i̶e̶s̶ ( ̶" + KillCount() + " ̶/" + enemies.Count + " ̶)";
        }

        //Update Treasure Objective
        if (treasure.activeSelf)
        {
            objTreasure.text = "Get Treasure (0/1)";
        }
        else
        {
            objTreasure.text = " ̶̶̶G̶e̶t̶ ̶T̶r̶e̶a̶s̶u̶r̶e̶ ̶(̶1̶/̶1̶)̶";
        }

        //Update Escape Objective
        if ((!treasure.activeSelf) && (KillCount() == enemies.Count))
        {
            objEscape.text = "ESCAPE!";
        }
    }

    //Get Player kill count by counting deactivated enemies
    int KillCount()
    {
        int enemyCount = 0;
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeSelf)
            {
                enemyCount += 1;
            }
        }
        return enemyCount;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("DebugRoom");
    }
}
