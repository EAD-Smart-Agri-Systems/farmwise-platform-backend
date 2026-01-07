using MediatR;
using FarmManagement.Modules.Crop.Application.DTOs;

namespace FarmManagement.Modules.Crop.Application.Queries.GetCropCycleDetails
{
    public sealed record GetCropCycleDetailsQuery(Guid CropCycleId) 
        : IRequest<CropCycleDto>;
}