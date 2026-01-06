using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Advisory.Domain.Entities;

public class AdvisoryRule : Entity
{
    public string Condition { get; private set; }
    public string Action { get; private set; }
    private AdvisoryRule() { }
    public AdvisoryRule(string condition, string action)
    {
        Condition = condition;
        Action = action;
    }
}
