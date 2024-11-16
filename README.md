# UFO Game - Unity Project

## 项目概述

这是一个简单的 Unity UFO 游戏项目，旨在展示如何使用 Unity GUI 系统显示文本和按钮，控制 UFO 动作，并进行简单的用户交互。该游戏涉及得分、游戏模式选择和飞行模式的切换。

## 主要功能

- **显示文本**：通过 GUI 在屏幕上显示得分、消息和其他信息。
- **按钮功能**：提供多种按钮选项，包括重启、切换游戏模式、切换飞行模式等。
- **UFO 控制**：使用物理引擎和自定义动作控制 UFO 的运动轨迹。
- **界面布局**：界面布局支持响应式设计，可以动态调整位置，使 UI 元素根据屏幕大小进行适配。

## 关键代码

### `OnGUI` 方法

在 `OnGUI` 方法中，我们使用 Unity 的 `GUI.Label` 和 `GUI.Button` 来显示游戏信息和提供交互按钮。

```csharp
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
```

### UFO 控制

通过自定义的 `CCFlyAction` 脚本，我们控制 UFO 的飞行轨迹，使用了重力和运动速度控制 UFO 在 X 和 Y 轴上的运动。

```csharp
void Update()
{
    time += Time.deltaTime;
    // y 方向的重力
    transform.Translate(Vector3.down * gravity * time * Time.deltaTime);
    // x 方向的速度
    transform.Translate(direction * speed * Time.deltaTime);

    if (this.transform.position.y < -3)
    {
        this.destroy = true;
        this.enable = false;
        this.callback.SSActionEvent(this);
    }
}
```

### SSActionManager

`SSActionManager` 脚本负责管理和更新所有的动作，并处理动作完成后的事件。

```csharp
void Update()
{
    // 保存就绪的动作到哈希表
    foreach (SSAction action in ready)
    {
        actions[action.GetInstanceID()] = action;
    }
    ready.Clear();

    // 启动并更新哈希表中的动作
    foreach(KeyValuePair<int, SSAction> k in actions)
    {
        SSAction ac = k.Value;
        if (ac.destroy)
        {
            delete.Add(ac.GetInstanceID());
        }
        else if(ac.enable)
        {
            ac.Update();
        }
    }

    // 删除已完成的动作
    foreach(int k in delete)
    {
        if (actions.ContainsKey(k))
        {
            SSAction ac = actions[k];
            actions.Remove(k);
            Destroy(ac.gameObject);
        }
    }

    delete.Clear();
}
```

## 游戏模式

- **普通模式**：玩家击中 UFO 进行得分。
- **无限模式**：随着时间推移，UFO 会不断出现并挑战玩家。
- **运动学模式和物理模式**：控制 UFO 的飞行方式，使用不同的物理规则。

## 游戏控制

- **重启游戏**：点击重启按钮重新开始游戏。
- **切换模式**：切换普通模式和无限模式。
- **切换飞行模式**：选择 UFO 飞行的运动学或物理模式。

## 运行与测试

1. 打开 Unity 项目。
2. 确保所有脚本和资源已正确导入。
3. 点击播放按钮运行游戏并进行测试。

## 也感谢师兄的帮助！
