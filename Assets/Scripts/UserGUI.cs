using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;
    string gameMessage;
    int points;

    public void SetMessage(string gameMessage)
    {
        this.gameMessage = gameMessage;
    }

    public void SetPoints(int points)
    {
        this.points = points;
    }

    void Start()
    {
        points = 0;
        gameMessage = "";
        userAction = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        if (userAction == null)
        {
            Debug.LogError("Failed to get IUserAction from CurrentScenceController.");
        }
    }

    void OnGUI()
    {
        // 小字体初始化
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        // 大字体初始化
        GUIStyle bigStyle = new GUIStyle();
        bigStyle.normal.textColor = Color.white;
        bigStyle.fontSize = 50;

        // 获取屏幕宽度和高度
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 计算元素的偏移，使其对齐到右侧的四分之一处
        float rightQuarterX = screenWidth * 0.75f; // 屏幕右侧四分之一位置
        float elementWidth = 200f; // UI 元素的宽度
        float elementHeight = 50f; // UI 元素的高度

        // 显示 "Hit UFO" 标签，放在屏幕右侧四分之一处
        GUI.Label(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 4 - 100, elementWidth, elementHeight), "Hit UFO", bigStyle);

        // 显示得分，放在屏幕右侧四分之一处
        GUI.Label(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 4, elementWidth, elementHeight), "Points: " + points, style);

        // 显示游戏消息，放在屏幕右侧四分之一处
        GUI.Label(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 4 + 50, elementWidth, elementHeight), gameMessage, style);

        // 显示按钮，放在屏幕右侧四分之一处
        if (GUI.Button(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 2 - 100, elementWidth, 40), "Restart"))
        {
            userAction.Restart();
        }
        if (GUI.Button(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 2 - 50, elementWidth, 40), "Normal Mode"))
        {
            userAction.SetMode(false);
        }
        if (GUI.Button(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 2, elementWidth, 40), "Infinite Mode"))
        {
            userAction.SetMode(true);
        }
        if (GUI.Button(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 2 + 50, elementWidth, 40), "Kinematics"))
        {
            userAction.SetFlyMode(false);
        }
        if (GUI.Button(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 2 + 100, elementWidth, 40), "Physis"))
        {
            userAction.SetFlyMode(true);
        }

        // 检测鼠标点击事件
        if (Input.GetButtonDown("Fire1"))
        {
            userAction.Hit(Input.mousePosition);
        }
    }







}
