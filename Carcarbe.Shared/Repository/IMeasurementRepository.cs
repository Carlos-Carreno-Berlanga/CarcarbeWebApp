using Carcarbe.Shared.Domain;
using Carcarbe.Shared.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carcarbe.Shared.Repository
{
    public interface IMeasurementRepository :  IRepository<Measurement>
    {
        IEnumerable<Measurement> FindAllByType(MeasurementType type);
        Task<IEnumerable<Measurement>> FindAllByTypeAsync(MeasurementType type);
    }
}
