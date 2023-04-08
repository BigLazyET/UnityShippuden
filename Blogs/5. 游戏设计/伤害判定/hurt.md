# 伤害判定

## 一、Animation + Event - 动画帧绑事件处理

在animation中，在某一帧上添加collider碰撞器，然后用event检测

### 1. 编辑器实现
unity的Animation动画界面有Event事件，可插入到固定帧
在GameObject上挂的script脚本中，将伤害相关方法写为public，即可由Event事件选取

### 2. 代码实现
如果觉得直接在Animation界面插入Event不好维护，可以使用AnimationClip类的AddEvent方法来实现纯代码插入Event事件

## 二、Physics2D.OverlapXXX

返回某个范围内的碰撞体

- Physics2D.OverlapCircleAll
- Physics2D.OverlapCircleNonAlloc

## 三、碰撞器切换

动态变化碰撞框，不过这个可以用其他方式实现，效果相似。对该对象增加多个子对象，在子对象上挂不同的碰撞框，然后去勾禁用这些子对象或碰撞框。用Event事件来启用和禁用对应的碰撞框，从而实现碰撞框的变化。这个方法的缺陷是无法支持补间变化，以及碰撞框种类太多会影响性能。

## X、万能的raycast射线检测 - 适用于高速精密的动作

- Physics2D.Raycast

