using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepository
{
    public  interface IMajorRepository
    {
        public Task<Major> GetMajorDetail(int majorId);
    }
}
