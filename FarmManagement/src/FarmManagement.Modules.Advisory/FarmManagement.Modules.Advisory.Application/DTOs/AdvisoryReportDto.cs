namespace FarmManagement.Modules.Advisory.Application.DTOs;
public class AdvisoryReportDto
{
    public Guid Id { get; set; }
    public Guid FarmId { get; set; }
    public string CropType { get; set; } = default!;
    public string PestRiskLevel { get; set; } = default!;
    public string Recommendation { get; set; } = default!;
    public DateTime GeneratedOn { get; set; }
}
