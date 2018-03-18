using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;
using Yunt.Device.Domain.Model.IdModel;

namespace Yunt.Device.Domain.IRepository
{
    public interface IMotorIdFactoriesRepository : IDeviceRepositoryBase<MotorIdFactories>
    {
        //Task AddAsync(Motor t);
    }
}
