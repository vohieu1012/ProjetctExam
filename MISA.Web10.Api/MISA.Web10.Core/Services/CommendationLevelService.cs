using MISA.Web10.Core.Interfaces.Repository;
using MISA.Web10.Core.Interfaces.Services;
using MISA.Web10.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web10.Core.Services
{
    public class CommendationLevelService : BaseService<CommendationLevel>, ICommendationLevelService
    {
        #region field
        private readonly ICommendationLevelRepository _repository;
        #endregion
        #region contructor
        public CommendationLevelService(ICommendationLevelRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion
    }
}
