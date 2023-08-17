namespace Assets.Scripts.ProjectileSystem
{
    /*
    * 弹头组件的数据并不总设置在预制件上，也有可能来自生成弹头组件的武器的组件中来
    * 举个例子，一些弓可能使用相同的弓箭，但是这些弓箭具有不同的伤害值，所以我们需要一种方式让武器在生成弓箭的时候可以设置对应的弓箭伤害值
    *
    * 此类是所有弹头组件数据的基类，在弹头组件基类中使用此类，
    * 这样武器组件就能够通过SendDataPackage来设置对应的弹头组件数据
    */
    public abstract class ProjectileDataPackage
    {
    }
}
