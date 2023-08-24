# 说明

参考链接：
* [BT-Framework2](https://github.com/f15gdsy/BT-Framework-2)
* [BT-Framework](https://github.com/f15gdsy/BT-Framework)
* [BT-Test](https://github.com/f15gdsy/BT-Test)
* [行为树(Behaviour Tree)](https://blog.csdn.net/qq_36382054/article/details/117226107)

`其中BT-Test分为master和improved分支，但是都是基于BT-Framework版本，参考意义不大`
`BT-Framework2在BT-Framework的基础上单独把Condition分支拿出来作为条件(Conditional)节点和装饰(Decorator)节点，实现趋于标准化；因为行为树主要由以下四种节点抽象而成组合节点、装饰节点、条件节点、行为节点`

## 一、场景
案例：一个怪物在指定路径巡逻，当玩家怪物与玩家距离小于等于5米时会追逐玩家，当大于5米时会继续巡逻。

条件：大于5五米、小于等于5米

行为：巡逻、追逐玩家

### 1. 状态机实现

### 2. 行为树实现



