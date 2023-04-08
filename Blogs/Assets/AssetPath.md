# AssetPath

## 一、Application.dataPath
- 在unity 编辑器里面运行，配置文件放在在Assets文件夹下，使用CurrentDirectory来获取Assets目录路径，进而获取配置文件
- 将游戏打包成exe后，Application.dataPath指向exe同级目录，所以需要在exe同级目录再放一个配置文件供读写（Windows）
```csharp
 string configFile = Application.dataPath + "/config.txt";
#if !UNITY_EDITOR
    configFile = System.Environment.CurrentDirectory + "/config.txt";
#endif
```

## 二、Application.streamingAssetsPath

```csharp
var uri = new System.Uri(Path.Combine(Application.streamingAssetsPath, "ConfigMap.txt"));
var request = UnityWebRequest.Get(uri);
var www = request.SendWebRequest();
yield return www;
```

## 三、Application.persistentDataPath

全平台通用可读可写的文件夹，热更重要途径