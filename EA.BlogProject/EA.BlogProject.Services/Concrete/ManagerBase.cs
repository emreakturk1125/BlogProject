using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EA.BlogProject.Data.Abstract;
using EA.BlogProject.Data.UnitOfWork;

namespace EA.BlogProject.Services.Concrete
{
    public class ManagerBase
    {
        public ManagerBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }

    }
}
