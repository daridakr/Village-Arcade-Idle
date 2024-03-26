namespace Vampire
{
    public class IceSkatesAbility : FloatUpgradeAbility<UpgradeableMovementSpeed>
    {
        public override void Select()
        {
            base.Select();
            //_playerHealth.UpdateMoveSpeed(); - PlayerMovement
        }

        public override bool RequirementsMet()
        {
            bool baseRequirementsMet = base.RequirementsMet();
            bool movementSpeedInUse = abilityManager.MovementSpeedUpgradeablesCount > 0;
            return baseRequirementsMet && movementSpeedInUse;
        }
    }
}