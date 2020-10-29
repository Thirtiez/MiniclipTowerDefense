namespace Thirties.Miniclip.TowerDefense
{
    public interface IAggrable
    {
        float FirePower { get; set; }
        float FireRate { get; set; }
        float FireRange { get; set; }

        void Shoot(ITargetable target);
    }
}
