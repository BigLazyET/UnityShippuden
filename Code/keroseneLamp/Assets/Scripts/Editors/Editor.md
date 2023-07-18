# Unity Editor 扩展和开发

## 启动unity即运行 InitializeOnLoad
在unity loads或者scripts recompiled的时候会运行脚本

## 菜单自定义 MenuItem Attribute

## 自定义PropertyDrawers

### 内置的PropertyDrawers
```
// 可以推断出来，内置一定有 RangeAttribute这个类；同理，如下有HeaderAttribute和SerializeFieldAttribute类
[Range(0, 20)]
public int intValue = 10;
 
[Header("名称")]
public string nameStr;
 
[SerializeField]
private float floatValue = 10f;
```

### 可用于扩展的Editor基类
* PropertyDrawer (需要搭配自定义的PropertyAttribute来使用)- SceneNameDrawer 示例
* EditorWindow - BTTreeWindow 示例
