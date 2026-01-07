using MediatR;
using Crop.Application.DTOs;

namespace Crop.Application.Queries.GetCropCycleDetails
{
    public sealed record GetCropCycleDetailsQuery(Guid CropCycleId) 
        : IRequest<CropCycleDto>;
}