# GameManager

> **一般以单例模式提供服务**

## 一、功能
* 控制游戏进程，其中包括控制关卡的开始延迟、每一回合间的延迟等
* 控制是否允许玩家操作等
* 判断游戏是否结束等；
* 初始化游戏信息，其中包括了生成地图等
* 记录游戏当中的一些数值，包括玩家的生命值、当前进行到的关卡级别等
* 持有敌人等部分对象的引用
* 控制游戏的显示状态、比如是应该显示地图还是显示正在加载等辅助信息
* 控制游戏的UI
* 界面之间联系的纽带，减少耦合

## 二、功能代码

### 1. 游戏暂停
#### 1.1 boolean开关
简单粗暴不容易出错

设置游戏是否暂停的布尔值，直接修改这个全局布尔值即可

#### 1.2 利用Time
控制细腻花样繁多易出错混淆

http://www.xuanyusong.com/archives/2956
注意Time暂停不对update起作用，而对fixupdate起作用

### 2. 出生点
SpawnPoint：相当于出生点(Transform)

### 3. 游戏关卡
* [场景切换的实现](https://blog.csdn.net/float_freedom/article/details/126221260)

#### 3.1 加载+切换关卡
```csharp
// 加载关卡 - 老式写法
Application.loadLevel(string sceneName)
// 加载关卡
using UnityEngine.SceneManagement;
SceneManager.LoadScene(1); //1是场景的索引
public static void LoadScene(string sceneName)
public static void LoadScene(int sceneBuildIndex)
public static AsyncOperation LoadSceneAsync(string sceneName)
public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
 // 加载关卡？
Application.LoadScene(...)
```
https://blog.csdn.net/float_freedom/article/details/126221260
https://blog.csdn.net/h522532768/article/details/53383603

切换场景会默认销毁当前场景中的所有游戏对象，若不想销毁某对象，可以调用 MonoBehaviour 的 DontDestroyOnLoad 方法，如下：

#### 3.2 获取关卡信息
```csharp
// 获取当前关卡的名称
Application.LoadLevelName()
```