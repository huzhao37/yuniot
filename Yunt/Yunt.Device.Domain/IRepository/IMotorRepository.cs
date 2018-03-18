using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IMotorRepository :IDeviceRepositoryBase<Motor>
    {
        //Task AddAsync(Motor t);
    }
}
